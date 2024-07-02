namespace HospitalManagementSystem.Application.CQRS.Commands.Doctors.UpdateDoctor;
public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommandRequest, UpdateDoctorCommandResponse>
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public UpdateDoctorCommandHandler(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }
    public async Task<UpdateDoctorCommandResponse> Handle(UpdateDoctorCommandRequest request, CancellationToken cancellationToken)
    {
        var doctorDto = _mapper.Map<DoctorUpdateDto>(request);
        bool result = await _doctorService.UpdateDoctorAsync(request.Id,doctorDto);
        return new UpdateDoctorCommandResponse
        {
            StatusCode = result ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest,
            Message = result ? "Doctor is successfully updated!" : "Error occured"
        };
    }
}
