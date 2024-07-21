namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginUser;
public record LoginUserCommandRequest(
    string UserNameOrEmail,
    string Password
    ) : IRequest<Result<LoginUserCommandResponse>>;