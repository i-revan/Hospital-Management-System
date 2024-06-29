namespace HospitalManagementSystem.Application.Abstraction.Repositories.Generic;
public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    Task<bool> AddAsync(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    bool SoftDelete(T entity);
    void ReverseDelete(T entity);
}
