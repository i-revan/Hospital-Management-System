namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Departments;
public class DepartmentReadRepository : ReadRepository<Department>, IDepartmentReadRepository
{
    public DepartmentReadRepository(AppDbContext context) : base(context)
    {

    }
}
