namespace HospitalManagementSystem.Application.DTOs.Doctors
{
    public record DoctorItemDto(
        string Name,
        string Surname,
        string Address,
        string Phone,
        string DepartmentName
        );
}
