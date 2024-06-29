namespace HospitalManagementSystem.Application.Validators;
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(l => l.UserNameOrEmail)
            .NotNull()
            .NotEmpty().WithMessage("Enter your email or username")
            .MaximumLength(256).WithMessage("Not valid entry!")
            .MinimumLength(4).WithMessage("Not valid entry!");
        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Enter your password")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters");
    }
}
