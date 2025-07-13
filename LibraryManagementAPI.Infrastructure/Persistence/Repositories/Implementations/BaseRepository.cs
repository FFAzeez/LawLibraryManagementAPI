using System.Linq.Expressions;
using LibraryManagementAPI.Domain.DTOs.Responses;
using LibraryManagementAPI.Infrastructure.Persistence.Context;
using LibraryManagementAPI.Infrastructure.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Infrastructure.Persistence.Repositories.Implementations
{

    public class BaseRepository<TEntity>(AppDbContext dbContext) : IAsyncRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext _dbContext = dbContext;
        private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public async Task<PagedResult<TEntity>> GetPagedFilteredAsync(Expression<Func<TEntity, bool>> filter, int page, int pageSize, string sortColumn, string sortOrder,
            bool includeDeleted = false)
        {
            var query = _dbSet.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = ApplySorting(query, sortColumn, sortOrder);

            int totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // return new PagedResult<TEntity> { Items = items, TotalCount = totalCount };

            return new PagedResult<TEntity>
            {
                Items = items,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize
            };
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false)
        {
            var query = _dbSet.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        private IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string sortColumn, string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, sortColumn);
            var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);

            if (sortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(lambda);
            }
            else
            {
                query = query.OrderBy(lambda);
            }

            return query;
        }
    }
}
