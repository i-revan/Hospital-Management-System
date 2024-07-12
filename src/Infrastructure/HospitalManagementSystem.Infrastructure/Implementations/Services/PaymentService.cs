using HospitalManagementSystem.Application.Abstraction.Services.Stripe;

namespace HospitalManagementSystem.Infrastructure.Implementations.Services;

public class PaymentService : IPaymentService
{
    private readonly IStripeService _stripeService;

    public PaymentService(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }
    public async Task<string> CreatePaymentAsync(decimal amount, Guid appointmentId)
    {
        return await _stripeService.CreatePaymentIntentAsync(amount, "usd", appointmentId);
    }
}
