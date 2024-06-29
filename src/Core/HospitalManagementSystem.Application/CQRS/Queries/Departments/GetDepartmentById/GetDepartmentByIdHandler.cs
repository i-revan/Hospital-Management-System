namespace HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdRequest, GetDepartmentByIdResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;

    public GetDepartmentByIdHandler(IDepartmentService departmentService, IMapper mapper)
    {
        _departmentService = departmentService;
        _mapper = mapper;
    }
    public async Task<GetDepartmentByIdResponse> Handle(GetDepartmentByIdRequest request, CancellationToken cancellationToken)
    {
        var department = await _departmentService.GetByIdAsync(request.Id);
        return _mapper.Map<GetDepartmentByIdResponse>(department);
    }
}
