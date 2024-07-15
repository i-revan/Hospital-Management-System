namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
public record LoginByRefreshCommandResponse(
    string Token,
    string UserName,
    DateTime ExpiredAt,
    string RefreshToken,
    DateTime RefreshTokenExpiredAt
    );
