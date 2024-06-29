namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Departments;
public class DepartmentWriteRepository : WriteRepository<Department>, IDepartmentWriteRepository
{
    public DepartmentWriteRepository(AppDbContext context) : base(context)
    {
    }
}