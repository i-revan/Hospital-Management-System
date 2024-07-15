namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Doctors;
public class DoctorWriteRepository : WriteRepository<Doctor>, IDoctorWriteRepository
{
    public DoctorWriteRepository(AppDbContext context) : base(context)
    {
    }
}
