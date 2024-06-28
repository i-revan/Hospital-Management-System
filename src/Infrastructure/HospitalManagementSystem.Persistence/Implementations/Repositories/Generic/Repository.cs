using HospitalManagementSystem.Application.Abstraction.Repositories.Generic;
using HospitalManagementSystem.Domain.Entities.Common;
using HospitalManagementSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Generic
{
    public class Repository<T>:IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public IQueryable<T> GetAll(bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return query;
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (expression != null) query = query.Where(expression);
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            query = _addIncludes(query, includes);
            return !isTracking ? query:query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            var query = _table.AsQueryable().Where(x=>x.Id==id);

            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            var query = _table.AsQueryable().Where(expression);

            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _table.AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return await query.AnyAsync(expression);
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
        }

        public void ReverseDelete(T entity)
        {
            entity.IsDeleted = false;
        }

        private IQueryable<T> _addIncludes(IQueryable<T> query, params string[] includes)
        {
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}
