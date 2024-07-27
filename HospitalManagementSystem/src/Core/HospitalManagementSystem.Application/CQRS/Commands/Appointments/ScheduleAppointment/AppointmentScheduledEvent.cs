namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.ScheduleAppointment;

public record AppointmentScheduledEvent
{
    public Guid DoctorId { get; init; }
    public DateTime ScheduledTime { get; init; }
}
