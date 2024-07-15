namespace HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAppointmentById;

public record GetAppointmentByIdQueryRequest(string Id) : IRequest<GetAppointmentByIdQueryResponse>;
