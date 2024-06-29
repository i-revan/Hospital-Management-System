using HospitalManagementSystem.Application.Abstraction.Repositories.Generic;
using HospitalManagementSystem.Domain.Entities.Common;
using HospitalManagementSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagementSystem.Persistence.Implementations.Repositories.Generic
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = Table;
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return query;
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = Table;
            if (expression != null) query = query.Where(expression);
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            query = _addIncludes(query, includes);
            return !isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            var query = Table.AsQueryable().Where(x => x.Id == id);

            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            var query = Table.AsQueryable().Where(expression);

            if (ignoreQuery) query = query.IgnoreQueryFilters();
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = Table.AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return await query.AnyAsync(expression);
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
