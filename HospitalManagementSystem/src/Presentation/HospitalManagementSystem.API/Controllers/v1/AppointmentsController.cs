using HospitalManagementSystem.Application.Common.Results;
using HospitalManagementSystem.Application.CQRS.Commands.Appointments.CancelAppoinment;
using HospitalManagementSystem.Application.CQRS.Commands.Appointments.ScheduleAppointment;
using HospitalManagementSystem.Application.CQRS.Commands.Appointments.UpdateAppointment;
using HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAllAppointments;
using HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAppointmentById;

namespace HospitalManagementSystem.API.Controllers.v1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] GetAllAppointmentsQueryRequest request) =>
    Ok(await _mediator.Send(request));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _mediator.Send(new GetAppointmentByIdQueryRequest(id));
        if (response.IsFailure) return NotFound(new { response.Error.Code, response.Error.Description });
        return Ok(response.Value.Appointment);
    }

    [HttpPost]
    public async Task<IActionResult> MakeAppointment([FromForm] ScheduleAppointmentCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAppointment([FromForm] UpdateAppointmentCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> CancelAppointment([FromForm] CancelAppointmentCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int)response.StatusCode, response);
    }
}
