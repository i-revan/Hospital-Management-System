namespace HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
public class GetDepartmentByIdRequest : IRequest<GetDepartmentByIdResponse>
{
    public int Id { get; set; }
}
