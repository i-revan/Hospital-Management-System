using HospitalManagementSystem.Application.Abstraction.Repositories;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Persistence.Contexts;
using HospitalManagementSystem.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories
{
    internal class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {

        }
    }
}
