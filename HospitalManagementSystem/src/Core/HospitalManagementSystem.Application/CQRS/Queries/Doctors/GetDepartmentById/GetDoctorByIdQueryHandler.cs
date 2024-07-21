using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;

namespace HospitalManagementSystem.Application.CQRS.Queries.Doctors.GetDepartmentById;
public class GetDoctorByIdQueryHandler:IRequestHandler<GetDoctorByIdQueryRequest, Result<GetDoctorByIdQueryResponse>>
{
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    public async Task<Result<GetDoctorByIdQueryResponse>> Handle(GetDoctorByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var doctorResult = await _doctorService.GetByIdAsync(request.Id);
        if (doctorResult.IsFailure) return Result<GetDoctorByIdQueryResponse>.Failure(doctorResult.Error);
        var response = _mapper.Map<GetDoctorByIdQueryResponse>(doctorResult.Value);
        return Result<GetDoctorByIdQueryResponse>.Success(response);
    }
}
