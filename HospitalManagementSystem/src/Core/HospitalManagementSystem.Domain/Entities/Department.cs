using System.Text.Json.Serialization;

namespace HospitalManagementSystem.Domain.Entities;
public class Department : BaseNameEntity
{
    [JsonIgnore]
    public ICollection<Doctor>? Doctors { get; set; }
}
