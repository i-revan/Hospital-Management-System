using HospitalManagementSystem.Domain.Entities.Identity;

namespace HospitalManagementSystem.Domain.Entities;

public class Billing : BaseEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;

    public decimal Amount { get; set; }
    public string Currency { get; set; } = null!;
    public string PaymentIntentId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}
