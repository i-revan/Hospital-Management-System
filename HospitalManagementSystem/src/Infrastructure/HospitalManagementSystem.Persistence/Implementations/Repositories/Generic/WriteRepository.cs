using HospitalManagementSystem.Application.Abstraction.Repositories.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Generic;
public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public bool Update(T entity)
    {
        EntityEntry<T> entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public bool Delete(T entity)
    {
        EntityEntry<T> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool SoftDelete(T entity)
    {
        entity.IsDeleted = true;
        return true;
    }

    public void ReverseDelete(T entity)
    {
        entity.IsDeleted = false;
    }
}
