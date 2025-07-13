using System.Linq.Expressions;
using LibraryManagementAPI.Domain.DTOs.Responses;

namespace LibraryManagementAPI.Infrastructure.Persistence.Repositories.Interface
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<PagedResult<TEntity>> GetPagedFilteredAsync(Expression<Func<TEntity, bool>> filter, int page, int pageSize, string sortColumn, string sortOrder,
            bool includeDeleted = false);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}