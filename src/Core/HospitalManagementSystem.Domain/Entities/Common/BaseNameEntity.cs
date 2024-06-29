namespace HospitalManagementSystem.Domain.Entities.Common;
public abstract class BaseNameEntity : BaseEntity
{
    public string Name { get; set; } = null!;
}
