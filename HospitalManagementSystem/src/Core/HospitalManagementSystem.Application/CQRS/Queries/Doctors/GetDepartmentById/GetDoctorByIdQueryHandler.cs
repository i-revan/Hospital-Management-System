namespace HospitalManagementSystem.Application.CQRS.Queries.Doctors.GetDepartmentById;
public class GetDoctorByIdQueryHandler:IRequestHandler<GetDoctorByIdQueryRequest, GetDoctorByIdQueryResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    public async Task<GetDoctorByIdQueryResponse> Handle(GetDoctorByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorService.GetByIdAsync(request.Id);
        return _mapper.Map<GetDoctorByIdQueryResponse>(doctor);
    }
}
