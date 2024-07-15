using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginUser;
using HospitalManagementSystem.Application.CQRS.Commands.AppUsers.RegisterUser;

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
        return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
    }
    [HttpPost("[Action]")]
    public async Task<IActionResult> LoginByRefresh([FromForm] LoginByRefreshCommandRequest request)
    {
        return Ok(await _mediator.Send(request));
    }
}