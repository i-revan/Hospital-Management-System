namespace HospitalManagementSystem.Application.DTOs.Doctors;
public class DoctorItemDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string DepartmentName { get; set; } = null!;
}
    

