namespace HospitalManagementSystem.Application.Validators;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Valid email address required.");
        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters!");
        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(4).WithMessage("Username must contain at least 4 characters!")
            .MaximumLength(256).WithMessage("Username length cannot exceed 256!");
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Enter your name!")
            .MinimumLength(3).WithMessage("It is not valid!")
            .MaximumLength(50).WithMessage("It is not valid!");
        RuleFor(r => r.Surname)
            .NotEmpty().WithMessage("Enter your surname!")
            .MinimumLength(3).WithMessage("It is not valid!")
            .MaximumLength(50).WithMessage("It is not valid!");
        RuleFor(r => r)
            .Must(r => r.ConfirmPassword == r.Password).WithMessage("Please confirm password correctly!");
    }
}
