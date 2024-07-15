using FluentValidation.TestHelper;
using HospitalManagementsSystem.Application.Tests.Validators.Base;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.CreateDepartment;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;

namespace HospitalManagementsSystem.Application.Tests.Validators;

public class DepartmentCommandValidatorTests:CoreData
{
    private readonly CreateDepartmentCommandValidator _createValidator;
    private readonly UpdateDepartmentCommandValidator _updateValidator;

    public DepartmentCommandValidatorTests()
    {
        _createValidator = new CreateDepartmentCommandValidator();
        _updateValidator = new UpdateDepartmentCommandValidator();
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void CreateDepartmentName_ShouldHaveError_WhenNullOrEmpty(string name)
    {
        //Arrange
        var model = new CreateDepartmentCommandRequest { Name = name };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void UpdateDepartmentName_ShouldReturnError_WhenNullOrEmpty(string name)
    {
        var model = new UpdateDepartmentCommandRequest(Guid.NewGuid().ToString(),name);

        var result = _updateValidator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(m => m.Name);
    }

}
