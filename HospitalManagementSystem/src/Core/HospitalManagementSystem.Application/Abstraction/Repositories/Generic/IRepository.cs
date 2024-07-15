using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Application.Abstraction.Repositories.Generic;
public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }
}
