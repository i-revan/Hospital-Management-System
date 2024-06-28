using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Domain.Entities.Common
{
    public abstract class FileEntity:BaseEntity
    {
        public string FileName { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Storage { get; set; } = null!;
    }
}
