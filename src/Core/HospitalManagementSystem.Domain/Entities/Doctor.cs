using HospitalManagementSystem.Domain.Entities.Common;

namespace HospitalManagementSystem.Domain.Entities
{
    public class Doctor:BaseNameEntity
    {
        public string Surname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

    }
}
