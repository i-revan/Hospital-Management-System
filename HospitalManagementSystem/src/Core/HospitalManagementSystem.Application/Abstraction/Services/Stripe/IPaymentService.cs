namespace HospitalManagementSystem.Application.Abstraction.Services.Stripe;

public interface IPaymentService
{
    Task<string> CreatePaymentAsync(decimal amount, Guid appointmentId);
}
