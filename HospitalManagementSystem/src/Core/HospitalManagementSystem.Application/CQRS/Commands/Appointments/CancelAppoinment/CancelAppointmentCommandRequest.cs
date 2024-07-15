namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.CancelAppoinment;

public record CancelAppointmentCommandRequest(string Id) : IRequest<CancelAppointmentCommandResponse>;
