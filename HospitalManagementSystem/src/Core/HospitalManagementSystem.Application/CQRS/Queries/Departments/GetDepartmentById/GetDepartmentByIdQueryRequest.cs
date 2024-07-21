using HospitalManagementSystem.Application.Common.Results;

namespace HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
public class GetDepartmentByIdQueryRequest : IRequest<Result<GetDepartmentByIdQueryResponse>>
{
    public string Id { get; set; } = null!;
}
