using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Domain.Entities.Identity;
public class AppUser : IdentityUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiredAt { get; set; }

    public ICollection<Appointment>? Appointments { get; set; }
}
