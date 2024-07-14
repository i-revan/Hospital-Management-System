namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.RegisterUser;

public record RegisterUserCommandRequest(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword,
    string Name,
    string Surname
    ) : IRequest<RegisterUserCommandResponse>;
