namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.ScheduleAppointment;

public record ScheduleAppointmentCommandRequest(
    Guid DoctorId,
    DateTime StartTime,
    DateTime EndTime,
    string? Remarks
    ) : IRequest<ScheduleAppointmentCommandResponse>;
