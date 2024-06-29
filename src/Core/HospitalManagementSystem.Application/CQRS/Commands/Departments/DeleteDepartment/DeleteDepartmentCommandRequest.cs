namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.DeleteDepartment;
public class DeleteDepartmentCommandRequest:IRequest<DeleteDepartmentCommandResponse>
{
    public int Id { get; set; }
}

