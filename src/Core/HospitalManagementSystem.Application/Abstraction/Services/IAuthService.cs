using HospitalManagementSystem.Application.DTOs.Users;

namespace HospitalManagementSystem.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDto registerDto);
        Task<TokenResponseDto> Login(LoginDto loginDto);
        Task<TokenResponseDto> LoginByRefreshToken(string refresh);
    }
}
