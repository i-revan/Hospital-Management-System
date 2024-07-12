namespace HospitalManagementSystem.Application.CQRS.Commands.Payments;

public record CreatePaymentCommandRequest(decimal Amount, Guid AppointmentId) : IRequest<CreatePaymentCommandResponse>;