namespace HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;
public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommandRequest, UpdateDepartmentCommandResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;

    public UpdateDepartmentCommandHandler(IDepartmentService departmentService, IMapper mapper)
    {
        _departmentService = departmentService;
        _mapper = mapper;
    }
    public async Task<UpdateDepartmentCommandResponse> Handle(UpdateDepartmentCommandRequest request, CancellationToken cancellationToken)
    {
        var departmentDto = _mapper.Map<DepartmentUpdateDto>(request);
        bool result = await _departmentService.UpdateDepartmentAsync(request.Id, departmentDto);
        return new UpdateDepartmentCommandResponse
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Department is successfully updated!" : "Error occured"
        };
    }
}
