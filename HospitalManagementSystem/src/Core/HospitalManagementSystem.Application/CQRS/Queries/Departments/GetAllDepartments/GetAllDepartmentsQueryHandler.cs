namespace HospitalManagementSystem.Application.CQRS.Queries.Departments.GetAllDepartments;
public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQueryRequest, GetAllDepartmentsQueryResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;

    public GetAllDepartmentsQueryHandler(IDepartmentService departmentService, IMapper mapper)
    {
        _departmentService = departmentService;
        _mapper = mapper;
    }

    public async Task<GetAllDepartmentsQueryResponse> Handle(GetAllDepartmentsQueryRequest request, CancellationToken cancellationToken)
    {
        var departmentDtos = await _departmentService.GetAllAsync();
        var departments = _mapper.Map<GetAllDepartmentsQueryResponse>(departmentDtos);
        return departments;
    }
}
