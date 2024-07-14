namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IAuthService
{
    Task<RegisterUserResponseDto> Register(RegisterDto registerDto);
    Task<TokenResponseDto> Login(LoginDto loginDto);
    Task<TokenResponseDto> LoginByRefreshToken(string refresh);
}
