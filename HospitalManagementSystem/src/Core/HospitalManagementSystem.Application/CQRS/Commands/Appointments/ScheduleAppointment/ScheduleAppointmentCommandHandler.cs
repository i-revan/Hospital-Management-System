using HospitalManagementSystem.Application.Abstraction.EventBus;
using HospitalManagementSystem.Application.DTOs.Appointments;

namespace HospitalManagementSystem.Application.CQRS.Commands.Appointments.ScheduleAppointment;

public class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommandRequest, ScheduleAppointmentCommandResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public ScheduleAppointmentCommandHandler(IAppointmentService appointmentService, IMapper mapper, IEventBus eventBus)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<ScheduleAppointmentCommandResponse> Handle(ScheduleAppointmentCommandRequest request, CancellationToken cancellationToken)
    {
        var appointmentDto = _mapper.Map<ScheduleAppointmentDto>(request);
        var result = await _appointmentService.ScheduleAppointmentAsync(appointmentDto);

        await _eventBus.PublishAsync(new AppointmentScheduledEvent
        {
            DoctorId = request.DoctorId,
            ScheduledTime = request.StartTime
        }, cancellationToken);

        return new ScheduleAppointmentCommandResponse
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? "Appointment is successfully added!" : result.Error.Description
        };
    }
}
