namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
public class LoginByRefreshCommandValidator : AbstractValidator<LoginByRefreshCommandRequest>
{
    public LoginByRefreshCommandValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotNull().WithMessage("Refresh token cannot be null")
            .NotEmpty().WithMessage("Refresh token cannot be empty");
    }
}