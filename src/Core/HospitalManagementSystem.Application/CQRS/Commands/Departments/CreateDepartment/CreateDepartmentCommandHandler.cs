namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.CreateDepartment;
public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommandRequest, CreateDepartmentCommandResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandler(IDepartmentService departmentService, IMapper mapper)
    {
        _departmentService = departmentService;
        _mapper = mapper;
    }
    public async Task<CreateDepartmentCommandResponse> Handle(CreateDepartmentCommandRequest request, CancellationToken cancellationToken)
    {
        var departmentDto = _mapper.Map<DepartmentCreateDto>(request);
        bool result = await _departmentService.CreateDepartmentAsync(departmentDto);
        return new CreateDepartmentCommandResponse
        {
            StatusCode = result ? System.Net.HttpStatusCode.Created : System.Net.HttpStatusCode.BadRequest,
            Message = result ? "Department is successfully created!" : "Error occured"
        };
    }
}

