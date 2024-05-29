using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.SharedLibrary;

public abstract class BaseEntityRepository<TEntity, TDbContext> : IBaseEntityRepository<TEntity>
    where TEntity : BaseEntity
    where TDbContext : DbContext
{
    #region [ Fields ]
    protected readonly ILogger<BaseEntityRepository<TEntity, TDbContext>> _logger;

    protected readonly IDbContextFactory<TDbContext> _dbContextFactory;
    #endregion

    #region [ CTor ]
    public BaseEntityRepository(ILogger<BaseEntityRepository<TEntity, TDbContext>> logger,
                            IDbContextFactory<TDbContext> dbContextFactory)
    {
        this._logger = logger;
        this._dbContextFactory = dbContextFactory;
    }
    #endregion

    #region [ Public Methods - CRUD ]
    public virtual async ValueTask<bool> AddSingleAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = await this.GetDbContextAsync(cancellationToken);
        if (await context.Set<TEntity>().FindAsync(entity.Id, cancellationToken) != null)
        {
            return false;
        }

        await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public virtual async ValueTask<bool> AddManyAsync(IEnumerable<TEntity> entityList, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var context = await this.GetDbContextAsync(cancellationToken);

        await context.AddRangeAsync(entityList, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public virtual async ValueTask<bool> IsExistAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var context = await this.GetDbContextAsync(cancellationToken);

        if (await context.Set<TEntity>().FindAsync(id, cancellationToken) == null)
        {
            return false;
        }

        return true;

    }

    public virtual async ValueTask<TEntity> GetSingleByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = default(TEntity);

        using var context = await this.GetDbContextAsync(cancellationToken);
        result = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return result;
    }

    public virtual async ValueTask<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var context = await this.GetDbContextAsync(cancellationToken);
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;

    }

    public virtual async ValueTask<bool> SoftDeleteAsync(string entityId, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var context = await this.GetDbContextAsync(cancellationToken);
        var dbResult = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
        if (dbResult == null)
        {
            
            return false;
        }
        dbResult.IsActive = false;
        context.Set<TEntity>().Update(dbResult);
        await context.SaveChangesAsync(cancellationToken);
        return true;

    }

    public virtual async ValueTask<bool> RecoverAsync(string entityId, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var context = await this.GetDbContextAsync(cancellationToken);
        var dbResult = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
        if (dbResult == null)
        {
            
            return false;
        }
        dbResult.IsActive = true;
        context.Set<TEntity>().Update(dbResult);
        await context.SaveChangesAsync(cancellationToken);
        return true;

    }

    public virtual async ValueTask<bool> DestroyAsync(string entityId, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();

        using var context = await this.GetDbContextAsync(cancellationToken);
        var dbResult = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
        if (dbResult == null)
        {
            
            return false;
        }
        context.Set<TEntity>().Remove(dbResult);
        await context.SaveChangesAsync(cancellationToken);
        return true;

    }

    public virtual async ValueTask<IEnumerable<TEntity>> GetManyAsync(int take, int skip, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = default(IEnumerable<TEntity>);

        using var context = await this.GetDbContextAsync(cancellationToken);
        result = await context.Set<TEntity>().AsNoTracking()
                        .OrderByDescending(x => x.IsActive)
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync(cancellationToken);
        return result;

    }

    public virtual async ValueTask<IEnumerable<TEntity>> GetManyActiveAsync(int take, int skip, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = default(IEnumerable<TEntity>);

        using var context = await this.GetDbContextAsync(cancellationToken);
        result = await context.Set<TEntity>().AsNoTracking().Where(x => x.IsActive)
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync(cancellationToken);
        return result;

    }

    public virtual async ValueTask<IEnumerable<TEntity>> GetManyInactiveAsync(int take, int skip, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = default(IEnumerable<TEntity>);

        using var context = await this.GetDbContextAsync(cancellationToken);
        result = await context.Set<TEntity>().AsNoTracking().Where(x => !x.IsActive)
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync(cancellationToken);
        return result;

    }

    public virtual async ValueTask<int> CountAllAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = default(int);

        using var context = await this.GetDbContextAsync(cancellationToken);
        result = await context.Set<TEntity>().AsNoTracking().CountAsync(cancellationToken);
        return result;

    }

    public virtual async ValueTask<int> CountActiveAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = default(int);

        using var context = await this.GetDbContextAsync(cancellationToken);
        result = await context.Set<TEntity>().AsNoTracking().CountAsync(x => x.IsActive, cancellationToken);
        return result;

    }

    public virtual async ValueTask<int> CountInactiveAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = default(int);

        using var context = await this.GetDbContextAsync(cancellationToken);
        result = await context.Set<TEntity>().AsNoTracking().CountAsync(x => !x.IsActive, cancellationToken);
        return result;

    }
    #endregion

    #region [ Private Methods - TContext ]
    protected async ValueTask<TDbContext> GetDbContextAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await this._dbContextFactory.CreateDbContextAsync(cancellationToken);
    }
    #endregion
}

