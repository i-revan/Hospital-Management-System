namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.DeleteDepartment;
public class DeleteDepartmentCommandRequest:IRequest<DeleteDepartmentCommandResponse>
{
    public string Id { get; set; } = null!;
}

