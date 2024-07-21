using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginUser;
using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.RegisterUser;
using System.Net;

namespace HospitalManagementSystem.API.Controllers.v1;
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("[Action]")]
    public async Task<IActionResult> Register([FromForm] RegisterUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return StatusCode((int) response.StatusCode, response);
    }
    [HttpPost("[Action]")]
    public async Task<IActionResult> Login([FromForm] LoginUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.IsSuccess) return StatusCode(StatusCodes.Status200OK, response.Value);
        return StatusCode((int) HttpStatusCode.BadRequest, response.Error);
    }
    [HttpPost("[Action]")]
    public async Task<IActionResult> LoginByRefresh([FromForm] LoginByRefreshCommandRequest request)
    {
        var response = await _mediator.Send(request);
        if (response.IsSuccess) return StatusCode(StatusCodes.Status200OK, response?.Value);
        return StatusCode((int)HttpStatusCode.BadRequest, response.Error);
    }
}