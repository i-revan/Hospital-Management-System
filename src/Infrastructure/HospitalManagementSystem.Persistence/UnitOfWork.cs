using HospitalManagementSystem.Application.Abstraction.Repositories.Appointments;
using HospitalManagementSystem.Persistence.Implementations.Repositories.Appointments;
using HospitalManagementSystem.Persistence.Implementations.Repositories.Departments;
using HospitalManagementSystem.Persistence.Implementations.Repositories.Doctors;

namespace HospitalManagementSystem.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IDepartmentReadRepository? _departmentReadRepository;
    private IDepartmentWriteRepository? _departmentWriteRepository;

    private IDoctorReadRepository? _doctorReadRepository;
    private IDoctorWriteRepository? _doctorWriteRepository;

    private IAppointmentReadRepository? _appointmentReadRepository;
    private IAppointmentWriteRepository? _appointmentWriteRepository;   

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IDepartmentReadRepository DepartmentReadRepository => _departmentReadRepository = _departmentReadRepository ?? new DepartmentReadRepository(_context);
    public IDepartmentWriteRepository DepartmentWriteRepository => _departmentWriteRepository = _departmentWriteRepository ?? new DepartmentWriteRepository(_context);

    public IDoctorReadRepository DoctorReadRepository => _doctorReadRepository = _doctorReadRepository ?? new DoctorReadRepository(_context);
    public IDoctorWriteRepository DoctorWriteRepository => _doctorWriteRepository = _doctorWriteRepository ?? new DoctorWriteRepository(_context);

    public IAppointmentReadRepository AppointmentReadRepository => _appointmentReadRepository = _appointmentReadRepository ?? new AppointmentReadRepository(_context);
    public IAppointmentWriteRepository AppointmentWriteRepository => _appointmentWriteRepository = _appointmentWriteRepository ?? new AppointmentWriteRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
