namespace HospitalManagementSystem.Application.DTOs.Appointments;
public record ScheduleAppointmentDto
(
    Guid DoctorId,
    DateTime StartTime,
    DateTime EndTime,
    string? Remarks
);
