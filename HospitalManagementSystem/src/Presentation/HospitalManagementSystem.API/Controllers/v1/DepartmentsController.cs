using HospitalManagementSystem.Application.CQRS.Commands.Departments.CreateDepartment;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.DeleteDepartment;
using HospitalManagementSystem.Application.CQRS.Commands.Departments.UpdateDepartment;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetAllDepartments;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;

namespace HospitalManagementSystem.API.Controllers.v1;
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] GetAllDepartmentsQueryRequest request) =>
        Ok(await _mediator.Send(request));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _mediator.Send(new GetDepartmentByIdQueryRequest
        {
            Id = id
        });
        return Ok(response.Department);
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromForm] CreateDepartmentCommandRequest department)
    {
        var response = await _mediator.Send(department);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Put([FromForm] UpdateDepartmentCommandRequest department)
    {
        var response = await _mediator.Send(department);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromForm] DeleteDepartmentCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }
}
