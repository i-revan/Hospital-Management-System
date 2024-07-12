using HospitalManagementSystem.Application.CQRS.Commands.Payments;

namespace HospitalManagementSystem.API.Controllers.v1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromForm] CreatePaymentCommandRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
