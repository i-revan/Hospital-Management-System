using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Domain.Entities.Common
{
    public abstract class BaseNameEntity:BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
