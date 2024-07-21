using HospitalManagementSystem.Application.Abstraction.Services;
using HospitalManagementSystem.Application.CQRS.Queries.Departments.GetDepartmentById;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.CQRS.Queries.Appointments.GetAppointmentById;
public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQueryRequest, 
    Result<GetAppointmentByIdQueryResponse>>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public GetAppointmentByIdQueryHandler(IAppointmentService appointmentService, IMapper mapper)
    {
        _appointmentService = appointmentService;
        _mapper = mapper;
    }
    public async Task<Result<GetAppointmentByIdQueryResponse>> Handle(GetAppointmentByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var appointmentResult = await _appointmentService.GetByIdAsync(request.Id);
        if (appointmentResult.IsFailure) 
            return Result<GetAppointmentByIdQueryResponse>.Failure(appointmentResult.Error);
        var response = _mapper.Map<GetAppointmentByIdQueryResponse>(appointmentResult.Value);
        return Result<GetAppointmentByIdQueryResponse>.Success(response);
    }
}
