using HospitalManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Abstraction.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity,new()
    {
        IQueryable<T> GetAll(bool isTracking=false,bool ignoreQuery=false, params string[] includes);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, bool isTracking = false, bool ignoreQuery=false, params string[] includes);
        Task<T> GetByIdAsync(int id, bool isTracking=false,bool ignoreQuery=false, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking=false,bool ignoreQuery=false, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseDelete(T entity);
        Task SaveChangeAsync();
    }
}
