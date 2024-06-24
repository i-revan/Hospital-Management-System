using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        //Soft-delete
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set;}
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public BaseEntity()
        {
            CreatedBy = "ravan.iskandarov";
        }
    }
}
