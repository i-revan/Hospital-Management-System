namespace HospitalManagementSystem.Application.DTOs.Doctors;
public record DoctorUpdateDto(
    string Name,
    string Surname,
    string Address,
    string Phone,
    Guid DepartmentId
    );

