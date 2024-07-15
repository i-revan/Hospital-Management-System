namespace HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQueryRequest, GetDepartmentByIdQueryResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;

    public GetDepartmentByIdQueryHandler(IDepartmentService departmentService, IMapper mapper)
    {
        _departmentService = departmentService;
        _mapper = mapper;
    }
    public async Task<GetDepartmentByIdQueryResponse> Handle(GetDepartmentByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var department = await _departmentService.GetByIdAsync(request.Id);
        return _mapper.Map<GetDepartmentByIdQueryResponse>(department);
    }
}
