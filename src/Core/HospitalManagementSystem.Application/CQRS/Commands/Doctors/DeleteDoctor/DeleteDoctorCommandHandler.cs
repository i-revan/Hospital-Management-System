namespace HospitalManagementSystem.Application.CQRS.Commands.Doctors.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommandRequest, DeleteDoctorCommandResponse>
{
    private readonly IDoctorService _doctorService;
    public DeleteDoctorCommandHandler(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }
    public async Task<DeleteDoctorCommandResponse> Handle(DeleteDoctorCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _doctorService.SoftDeleteDoctorAsync(request.Id);
        return new DeleteDoctorCommandResponse
        {
            StatusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
            Message = result ? "Doctor is successfully deleted" : "Error occured"
        };
    }
}
