namespace HospitalManagementSystem.Application.DTOs.Departments;
public class DepartmentItemDto 
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ICollection<Doctor>? Doctors { get; set; } = null!;
}
