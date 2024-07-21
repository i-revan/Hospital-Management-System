using HospitalManagementSystem.Application.CQRS.Commands.Doctors.CreateDoctor;
using HospitalManagementSystem.Application.CQRS.Commands.Doctors.DeleteDoctor;
using HospitalManagementSystem.Application.CQRS.Commands.Doctors.UpdateDoctor;
using HospitalManagementSystem.Application.CQRS.Queries.Doctors.GetAllDoctors;
using HospitalManagementSystem.Application.CQRS.Queries.Doctors.GetDepartmentById;

namespace HospitalManagementSystem.API.Controllers.v1;
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] GetAllDoctorsQueryRequest request) =>
        Ok(await _mediator.Send(request));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _mediator.Send(new GetDoctorByIdQueryRequest(Id: id));
        if(response.IsFailure) return NotFound(new { response.Error.Code, response.Error.Description });
        return Ok(response.Value.Doctor);
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromForm] CreateDoctorCommandRequest doctor)
    {
        var response = await _mediator.Send(doctor);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Put([FromForm] UpdateDoctorCommandRequest doctor)
    {
        var response = await _mediator.Send(doctor);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromForm] DeleteDoctorCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }
}
