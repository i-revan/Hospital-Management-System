using HospitalManagementSystem.Application.Abstraction.Repositories.Appointments;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Appointments;

public class AppointmentWriteRepository:WriteRepository<Appointment>, IAppointmentWriteRepository
{
    private readonly AppDbContext _context;

    public AppointmentWriteRepository(AppDbContext context):base(context)
    {
        _context = context;
    }
}
