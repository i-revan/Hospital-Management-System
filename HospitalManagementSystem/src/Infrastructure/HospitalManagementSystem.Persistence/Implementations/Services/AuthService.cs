﻿using HospitalManagementSystem.Application.Common.Errors;
using HospitalManagementSystem.Application.DTOs.Users;
using HospitalManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace HospitalManagementSystem.Persistence.Implementations.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ITokenHandler _handler;

    public AuthService(UserManager<AppUser> userManager, IMapper mapper, ITokenHandler handler)
    {
        _userManager = userManager;
        _mapper = mapper;
        _handler = handler;
    }

    public async Task<Result<RegisterUserResponseDto>> Register(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(u => u.UserName == registerDto.UserName || u.Email == registerDto.Email))
        {
            return UserErrors.UserAlreadyExist;
        }
        AppUser user = _mapper.Map<AppUser>(registerDto);
        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IdentityError error in result.Errors)
            {
                sb.AppendLine(error.Description);
            }
            return Result<RegisterUserResponseDto>.Failure(new Error("UserRegistrationFailed",sb.ToString()));
        }
        await _userManager.AddToRoleAsync(user, Role.Member.ToString());
        return Result<RegisterUserResponseDto>.Success(new(true, "User is successfully created!"));
    }


    public async Task<Result<TokenResponseDto>> Login(LoginDto loginDto)
    {
        AppUser user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            if (user is null) return UserErrors.UserNotFound;
        }
        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) return UserErrors.UserLoginFailed;
        await _userManager.GetRolesAsync(user);
        TokenResponseDto tokenDto = await _createTokenDto(user, await _createClaims(user));
        return Result<TokenResponseDto>.Success(tokenDto);
    }


    public async Task<Result<TokenResponseDto>> LoginByRefreshToken(string refresh)
    {
        AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refresh);
        if (user is null) return UserErrors.UserNotFound;
        if (user.RefreshTokenExpiredAt < DateTime.UtcNow) return UserErrors.UserRefreshTokenExpiration;
        TokenResponseDto tokenDto = await _createTokenDto(user, await _createClaims(user));
        return Result<TokenResponseDto>.Success(tokenDto);
    }

    private async Task<TokenResponseDto> _createTokenDto(AppUser user, ICollection<Claim> userClaims)
    {
        TokenResponseDto tokenDto = _handler.CreateJwt(user, userClaims, 60);
        user.RefreshToken = tokenDto.RefreshToken;
        user.RefreshTokenExpiredAt = tokenDto.RefreshTokenExpiredAt;
        await _userManager.UpdateAsync(user);
        return tokenDto;
    }

    private async Task<ICollection<Claim>> _createClaims(AppUser user)
    {
        ICollection<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname)
            };
        foreach (var role in await _userManager.GetRolesAsync(user))
        {
            userClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        return userClaims;
    }
}
