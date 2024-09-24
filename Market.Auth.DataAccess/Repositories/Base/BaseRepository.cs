using Market.Auth.DataAccess.Repositories.Base;
using Market.Auth.Domain.Enums;
using Market.Auth.Domain.Models;
using Market.Auth.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Market.Auth.DataAccess.Repositories;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext dbContext)
    {
        Context = dbContext;
    }
    public IQueryable<TEntity> GetAll()
    {
        return Context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new Exception("Entity not found with this id");

        return entity;
    }

    public async Task<TId> CreateAsync(TEntity entity)
    {
        Context.Add(entity);
        if (entity != null && entity is Auditable<TId>)
        {
            var auditableEntity = entity as Auditable<TId>;
            auditableEntity.CreatedAt = DateTime.Now;
        }
        await Context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<TId> UpdateAsync(TEntity entity)
    {
        Context.Update(entity);
        if(entity != null && entity is Auditable<TId>)
        {
            var auditable = entity as Auditable<TId>;
            auditable.ModifiedAt = DateTime.Now;
        }
        await Context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if(entity is IHaveState)
        {
            var iHaveState = entity as IHaveState;
            iHaveState.State = State.Inactive;
        }
        else
        {
            Context.Set<TEntity>().Remove(entity);
        }

        await Context.SaveChangesAsync();
    }
}
