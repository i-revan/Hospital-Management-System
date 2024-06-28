using HospitalManagementSystem.Application.Abstraction.Repositories;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Persistence.Contexts;
using HospitalManagementSystem.Persistence.Implementations.Repositories.Generic;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories
{
    internal class DoctorRepository : Repository<Doctor>,IDoctorRepository
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
