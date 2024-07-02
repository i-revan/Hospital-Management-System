namespace HospitalManagementSystem.Application.CQRS.Queries.Doctors.GetAllDoctors;
public record GetAllDoctorsQueryResponse(List<DoctorItemDto> Doctors);
