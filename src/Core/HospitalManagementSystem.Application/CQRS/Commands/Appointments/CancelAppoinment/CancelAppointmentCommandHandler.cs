namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.CancelAppoinment;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommandRequest, CancelAppointmentCommandResponse>
{
    private readonly IAppointmentService _appointmentService;

    public CancelAppointmentCommandHandler(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    public async Task<CancelAppointmentCommandResponse> Handle(CancelAppointmentCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _appointmentService.SoftDeleteAppointmentAsync(request.Id);
        return new CancelAppointmentCommandResponse
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Appointment is successfully cancelled!" : "Error occured"
        };
        throw new NotImplementedException();
    }
}
