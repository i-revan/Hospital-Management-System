namespace HospitalManagementSystem.Application.DTOs.Doctors;
public record DoctorCreateDto(
    string Name,
    string Surname,
    string Address,
    string Phone,
    int DepartmentId
    );
