using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.UpdateAppointment;

public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommandRequest, UpdateAppointmentCommandResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public UpdateAppointmentCommandHandler(IAppointmentService appointmentService, IMapper mapper)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
    }
    public async Task<UpdateAppointmentCommandResponse> Handle(UpdateAppointmentCommandRequest request, CancellationToken cancellationToken)
    {
        var appointmentDto = _mapper.Map<AppointmentUpdateDto>(request);
        bool result = await _appointmentService.UpdateAppointmentAsync(request.Id, appointmentDto);
        return new UpdateAppointmentCommandResponse
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Appointment is successfully updated!" : "Error occured"
        };
        throw new NotImplementedException();
    }
}
