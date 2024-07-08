namespace HospitalManagementSystem.Application.DTOs.Appointments;
public record AppointmentUpdateDto(
    Guid DoctorId,
    DateTime StartTime,
    DateTime EndTime,
    string? Remarks
    );
