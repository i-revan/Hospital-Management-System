using HospitalManagementSystem.Application;
using HospitalManagementSystem.Application.Abstraction.Repositories;
using HospitalManagementSystem.Persistence.Contexts;
using HospitalManagementSystem.Persistence.Implementations.Repositories;

namespace HospitalManagementSystem.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDepartmentRepository? _departmentRepository;
        private IDoctorRepository? _doctorRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IDepartmentRepository DepartmentRepository => _departmentRepository = _departmentRepository ?? new DepartmentRepository(_context);
        public IDoctorRepository DoctorRepository => _doctorRepository = _doctorRepository ?? new DoctorRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}
