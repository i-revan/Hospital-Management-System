namespace HospitalManagementSystem.Application.DTOs.Appointments;
public class AppointmentItemDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string DoctorName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Remarks { get; set; }
}
