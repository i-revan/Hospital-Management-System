using HospitalManagementSystem.Application.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IDoctorRepository DoctorRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    }
}
