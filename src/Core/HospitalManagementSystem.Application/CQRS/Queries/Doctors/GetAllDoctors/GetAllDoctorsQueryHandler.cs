namespace HospitalManagementSystem.Application.CQRS.Queries.Doctors.GetAllDoctors;
public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQueryRequest, GetAllDoctorsQueryResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public GetAllDoctorsQueryHandler(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    public async Task<GetAllDoctorsQueryResponse> Handle(GetAllDoctorsQueryRequest request, CancellationToken cancellationToken)
    {
        var doctorDtos = await _doctorService.GetAllAsync();
        var doctors = _mapper.Map<GetAllDoctorsQueryResponse>(doctorDtos);
        return doctors;
    }

}
