namespace HospitalManagementSystem.Domain.Entities.Common;
public abstract class FileEntity : BaseEntity
{
    public string FileName { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string Storage { get; set; } = null!;
}
