namespace HospitalManagementSystem.Application.Abstraction.Services;
public interface IAuthService
{
    Task<Result<RegisterUserResponseDto>> Register(RegisterDto registerDto);
    Task<Result<TokenResponseDto>> Login(LoginDto loginDto);
    Task<Result<TokenResponseDto>> LoginByRefreshToken(string refresh);
}
