using Asp.Versioning;
using HospitalManagementSystem.Application.DTOs.Users;

namespace HospitalManagementSystem.API.Controllers.v1;
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAuthService _service;

    public UsersController(IAuthService service)
    {
        _service = service;
    }
    [HttpPost("[Action]")]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
        await _service.Register(dto);
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpPost("[Action]")]
    public async Task<IActionResult> Login([FromForm] LoginDto dto)
    {
        return StatusCode(StatusCodes.Status200OK, await _service.Login(dto));
    }
    [HttpPost("[Action]")]
    public async Task<IActionResult> LoginByRefresh(string refreshToken)
    {
        return Ok(await _service.LoginByRefreshToken(refreshToken));
    }
}