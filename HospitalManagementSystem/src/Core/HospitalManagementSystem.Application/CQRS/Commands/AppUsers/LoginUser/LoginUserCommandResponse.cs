namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginUser;

public record LoginUserCommandResponse
(
    string Token,
    string UserName,
    DateTime ExpiredAt,
    string RefreshToken,
    DateTime RefreshTokenExpiredAt
);
