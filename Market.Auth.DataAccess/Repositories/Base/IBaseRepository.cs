using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.Base;

public interface IBaseRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    Task<TId> CreateAsync(TEntity entity);
    Task DeleteAsync(int id);
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<TId> UpdateAsync(TEntity entity);
}
