
namespace HospitalManagementSystem.Application.DTOs.Users
{
    public record TokenResponseDto(
        string Token, 
        string UserName, 
        DateTime ExpiredAt, 
        string RefreshToken, 
        DateTime RefreshTokenExpiredAt
        );
}
