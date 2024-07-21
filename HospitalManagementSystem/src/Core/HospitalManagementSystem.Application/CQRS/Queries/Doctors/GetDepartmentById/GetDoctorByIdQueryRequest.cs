namespace HospitalManagementSystem.Application.CQRS.Queries.Doctors.GetDepartmentById;
public record GetDoctorByIdQueryRequest(string Id):IRequest<Result<GetDoctorByIdQueryResponse>>;
