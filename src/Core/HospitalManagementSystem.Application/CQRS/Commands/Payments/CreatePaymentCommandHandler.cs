
using HospitalManagementSystem.Application.Abstraction.Services.Stripe;

namespace HospitalManagementSystem.Application.CQRS.Commands.Payments;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommandRequest, CreatePaymentCommandResponse>
{
    private readonly IPaymentService _paymentService;

    public CreatePaymentCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommandRequest request, CancellationToken cancellationToken)
    {
        var clientSecret = await _paymentService.CreatePaymentAsync(request.Amount, request.AppointmentId);
        return new CreatePaymentCommandResponse(clientSecret);
    }
}
