using HospitalManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
