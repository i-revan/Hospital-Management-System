using HospitalManagementSystem.Application.DTOs.Users;
using HospitalManagementSystem.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Implementations.Services;
public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenResponseDto CreateJwt(AppUser user, IEnumerable<Claim> userClaims, int minutes)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(minutes),
            claims: userClaims,
            signingCredentials: credentials
            );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return new TokenResponseDto(handler.WriteToken(token),
            user.UserName,
            token.ValidTo,
            CreateRefreshToken(),
            token.ValidTo.AddMinutes(1));
    }

    public string CreateRefreshToken()
    {
        //byte[] bytes = new byte[32];
        //var random = RandomNumberGenerator.Create();
        //random.GetBytes(bytes);
        //return Convert.ToBase64String(bytes);

        return Guid.NewGuid().ToString();
    }
}