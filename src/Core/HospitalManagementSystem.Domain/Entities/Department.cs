using HospitalManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Domain.Entities
{
    public class Department:BaseNameEntity
    {
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
