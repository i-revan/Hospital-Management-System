using HospitalManagementSystem.Application.Abstraction.Repositories.Appointments;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Appointments;
public class AppointmentReadRepository:ReadRepository<Appointment>, IAppointmentReadRepository
{
    private readonly AppDbContext _context;

    public AppointmentReadRepository(AppDbContext context):base(context)
    {
        _context = context;
    }

    public async Task<bool> IsDoctorAvailableAsync(Guid doctorId, DateTime startTime, DateTime endTime)
    {
        return !await _context.Appointments.AnyAsync(a =>
            a.DoctorId == doctorId &&
            ((a.StartTime <= startTime && a.EndTime > startTime) ||
            (a.StartTime < endTime && a.EndTime >= endTime)));
    }
}
