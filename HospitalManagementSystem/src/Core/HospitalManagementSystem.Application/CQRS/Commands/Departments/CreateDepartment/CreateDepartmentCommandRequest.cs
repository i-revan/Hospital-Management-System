namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.CreateDepartment;

public class CreateDepartmentCommandRequest : IRequest<CreateDepartmentCommandResponse>
{
    public string Name { get; set; } = null!;
}

