using HospitalManagementSystem.Application.Abstraction.Repositories.Billings;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Billings;

public class BillingWriteRepository : WriteRepository<Billing>, IBillingWriteRepository
{
    public BillingWriteRepository(AppDbContext context) : base(context)
    {
    }
}
