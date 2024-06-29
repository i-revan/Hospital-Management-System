namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;
public record UpdateDepartmentCommandRequest(int Id, string Name) : IRequest<UpdateDepartmentCommandResponse>;

