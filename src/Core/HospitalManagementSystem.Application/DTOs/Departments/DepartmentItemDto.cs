using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.DTOs.Departments
{
    public record DepartmentItemDto(int Id,string Name, ICollection<Doctor>? Doctors);
}
