namespace HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAppointmentById;
public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQueryRequest, GetAppointmentByIdQueryResponse>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public GetAppointmentByIdQueryHandler(IAppointmentService appointmentService, IMapper mapper)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
    }
    public async Task<GetAppointmentByIdQueryResponse> Handle(GetAppointmentByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentService.GetByIdAsync(request.Id);
        return _mapper.Map<GetAppointmentByIdQueryResponse>(appointment);
    }
}
