namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Doctors;
public class DoctorReadRepository : ReadRepository<Doctor>, IDoctorReadRepository
{
    public DoctorReadRepository(AppDbContext context) : base(context)
    {
    }
}
