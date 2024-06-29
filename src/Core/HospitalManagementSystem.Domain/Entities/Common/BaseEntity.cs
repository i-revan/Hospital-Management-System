namespace HospitalManagementSystem.Domain.Entities.Common;
public abstract class BaseEntity
{
    public int Id { get; set; }

    //Soft-delete
    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }
}
