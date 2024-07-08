using HospitalManagementSystem.Application.Abstraction.Repositories.Appointments;
using HospitalManagementSystem.Application.Abstraction.Repositories.Departments;
using HospitalManagementSystem.Application.Abstraction.Repositories.Doctors;

namespace HospitalManagementSystem.Application;

public interface IUnitOfWork
{
    IDepartmentReadRepository DepartmentReadRepository { get; }
    IDepartmentWriteRepository DepartmentWriteRepository { get; }

    IDoctorReadRepository DoctorReadRepository { get; }
    IDoctorWriteRepository DoctorWriteRepository { get; }

    IAppointmentReadRepository AppointmentReadRepository { get; }
    IAppointmentWriteRepository AppointmentWriteRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
}
