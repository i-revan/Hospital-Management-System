namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.UpdateAppointment;

public record UpdateAppointmentCommandRequest(
    string Id,
    string DoctorId,
    DateTime StartTime,
    DateTime EndTime,
    string? Remarks
    ) : IRequest<UpdateAppointmentCommandResponse>; 
