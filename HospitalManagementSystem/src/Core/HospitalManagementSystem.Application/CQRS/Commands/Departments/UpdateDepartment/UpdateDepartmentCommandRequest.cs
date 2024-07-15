namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;
public record UpdateDepartmentCommandRequest(string Id, string Name) : IRequest<UpdateDepartmentCommandResponse>;

