namespace HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAllAppointments;

public record GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQueryRequest, GetAllAppointmentsQueryResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public GetAllAppointmentsQueryHandler(IAppointmentService service, IMapper mapper)
    {
        _appointmentService = service;
        _mapper = mapper;
    }
    public async Task<GetAllAppointmentsQueryResponse> Handle(GetAllAppointmentsQueryRequest request, CancellationToken cancellationToken)
    {
        var appointmentDtos = await _appointmentService.GetAllAsync();
        var appointments = _mapper.Map<GetAllAppointmentsQueryResponse>(appointmentDtos);
        return appointments;
    }
}
