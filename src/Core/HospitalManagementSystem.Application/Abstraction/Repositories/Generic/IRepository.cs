using HospitalManagementSystem.Domain.Entities.Common;
using System.Linq.Expressions;

namespace HospitalManagementSystem.Application.Abstraction.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity,new()
    {
        IQueryable<T> GetAll(bool isTracking=false,bool ignoreQuery=false, params string[] includes);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, bool isTracking = false, bool ignoreQuery=false, params string[] includes);
        Task<T> GetByIdAsync(int id, bool isTracking=false,bool ignoreQuery=false, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking=false,bool ignoreQuery=false, params string[] includes);
        Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseDelete(T entity);
    }
}
