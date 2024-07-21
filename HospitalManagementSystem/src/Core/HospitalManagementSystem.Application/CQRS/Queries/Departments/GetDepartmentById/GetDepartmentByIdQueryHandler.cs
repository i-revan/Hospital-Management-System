using HospitalManagementSystem.Application.Common.Results;

namespace HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQueryRequest, Result<GetDepartmentByIdQueryResponse>>
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;

    public GetDepartmentByIdQueryHandler(IDepartmentService departmentService, IMapper mapper)
    {
        _departmentService = departmentService;
        _mapper = mapper;
    }
    public async Task<Result<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var departmentResult = await _departmentService.GetByIdAsync(request.Id);
        if (departmentResult.IsFailure)
            return Result<GetDepartmentByIdQueryResponse>.Failure(departmentResult.Error);
        var response = _mapper.Map<GetDepartmentByIdQueryResponse>(departmentResult.Value);
        return Result<GetDepartmentByIdQueryResponse>.Success(response);
    }
}
