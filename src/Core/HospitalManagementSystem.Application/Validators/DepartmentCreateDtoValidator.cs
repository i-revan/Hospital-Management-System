using FluentValidation;
using HospitalManagementSystem.Application.DTOs.Departments;

namespace HospitalManagementSystem.Application.Validators
{
    public class DepartmentCreateDtoValidator:AbstractValidator<DepartmentCreateDto>
    {
        public DepartmentCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Enter a department name")
                .MaximumLength(50).WithMessage("Maximum length is 50 characters")
                .MinimumLength(2).WithMessage("Minimum length is 2 characters")
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("The format is not correct");
        }
    }
}
