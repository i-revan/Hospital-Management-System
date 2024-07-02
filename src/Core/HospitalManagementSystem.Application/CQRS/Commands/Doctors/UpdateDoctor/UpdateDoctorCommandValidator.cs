namespace HospitalManagementSystem.Application.CQRS.Commands.Doctors.UpdateDoctor;

public class UpdateDoctorCommandValidator:AbstractValidator<UpdateDoctorCommandRequest>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(b => b.Id)
            .NotNull().WithMessage("Doctor id is required")
            .NotEmpty().WithMessage("Doctor id cannot be empty");
        RuleFor(c => c.Name)
            .NotNull().WithMessage("Enter doctor's name!")
            .NotEmpty().WithMessage("Doctor's name cannot be empty!")
            .MinimumLength(2).WithMessage("Minimum length is 2 characters")
            .MaximumLength(50).WithMessage("Doctor's name must not exceed 50 characters")
            .Matches(@"^[a-zA-Z\s]*$").WithMessage("Enter name correctly!");
        RuleFor(c => c.Surname)
            .NotNull().WithMessage("Enter doctor's surname!")
            .NotEmpty().WithMessage("Doctor's surname cannot be empty!")
            .MinimumLength(2).WithMessage("Minimum length is 2 characters")
            .MaximumLength(50).WithMessage("Doctor's surname must not exceed 50 characters")
            .Matches(@"^[a-zA-Z\s]*$").WithMessage("Enter surname correctly!");
        RuleFor(c => c.Address)
            .NotNull().WithMessage("Enter doctor's address!")
            .NotEmpty().WithMessage("Doctor's address cannot be empty!")
            .MinimumLength(2).WithMessage("Minimum length is 2 characters")
            .MaximumLength(100).WithMessage("Address must not exceed 100 characters");
        RuleFor(c => c.Phone)
            .NotNull().WithMessage("Enter doctor's surname!")
            .NotEmpty().WithMessage("Doctor's surname cannot be empty!")
            .Matches(@"^[(\+994|0)(50|51|55|70|77)(\d{7})]*$").WithMessage("Enter phone number correctly!");
    }
}
