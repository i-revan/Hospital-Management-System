using HospitalManagementSystem.Domain.Entities.Identity;

namespace HospitalManagementSystem.Application.Abstraction.Services.Stripe;

public interface IStripeService
{
    Task<string> CreatePaymentIntentAsync(decimal amount, string currency, Guid appointmentId);
}
