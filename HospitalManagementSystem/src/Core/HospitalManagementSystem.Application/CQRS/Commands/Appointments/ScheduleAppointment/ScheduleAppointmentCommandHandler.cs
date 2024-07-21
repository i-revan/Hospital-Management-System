using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.ScheduleAppointment;

public class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommandRequest, ScheduleAppointmentCommandResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public ScheduleAppointmentCommandHandler(IAppointmentService appointmentService, IMapper mapper)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
    }

    public async Task<ScheduleAppointmentCommandResponse> Handle(ScheduleAppointmentCommandRequest request, CancellationToken cancellationToken)
    {
        var appointmentDto = _mapper.Map<ScheduleAppointmentDto>(request);
        var result = await _appointmentService.ScheduleAppointmentAsync(appointmentDto);
        return new ScheduleAppointmentCommandResponse
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? "Appointment is successfully added!" : result.Error.Description
        };
    }
}
