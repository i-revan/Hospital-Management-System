namespace HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
public class GetDepartmentByIdQueryRequest : IRequest<GetDepartmentByIdQueryResponse>
{
    public string Id { get; set; } = null!;
}
