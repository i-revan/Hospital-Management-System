using HospitalManagementSystem.Application.Abstraction.Repositories.Billings;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Billings;

public class BillingReadRepository : ReadRepository<Billing>, IBillingReadRepository
{
    public BillingReadRepository(AppDbContext context):base(context)
    {
        
    }
}
