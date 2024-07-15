namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;
public class UpdateDepartmentCommandValidator:AbstractValidator<UpdateDepartmentCommandRequest>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Add department name!")
                .NotNull().WithMessage("Add department name!")
                .MaximumLength(50).WithMessage("Maximum length is 50 characters")
                .MinimumLength(2).WithMessage("Minimum length is 2 characters")
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("The format is not correct");
    }
}
