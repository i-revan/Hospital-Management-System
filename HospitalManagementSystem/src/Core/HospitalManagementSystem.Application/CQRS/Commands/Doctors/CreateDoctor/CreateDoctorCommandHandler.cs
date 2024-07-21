namespace HospitalManagementSystem.Application.CQRS.Commands.Doctors.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommandRequest, CreateDoctorCommandResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public CreateDoctorCommandHandler(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }
    public async Task<CreateDoctorCommandResponse> Handle(CreateDoctorCommandRequest request, CancellationToken cancellationToken)
    {
        var doctorDto = _mapper.Map<DoctorCreateDto>(request);
        var result = await _doctorService.CreateDoctorAsync(doctorDto);
        return new CreateDoctorCommandResponse
        {
            StatusCode = result.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
            Message = result.IsSuccess ? "Doctor is successfully created!" : result.Error.Description
        };
    }
}
