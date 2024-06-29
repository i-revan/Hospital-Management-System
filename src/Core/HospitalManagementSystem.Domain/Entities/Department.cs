namespace HospitalManagementSystem.Domain.Entities;
public class Department : BaseNameEntity
{
    public ICollection<Doctor>? Doctors { get; set; }
}
