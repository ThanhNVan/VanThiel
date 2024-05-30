using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Core.Repositories.Base;
using VanThiel.Domain.Settings;
using VanThiel.SharedLibrary;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Application.Repositories.Base;

public abstract class BaseVanThielRepository<TEntity> : BaseEntityRepository<TEntity, VanThielDbContext>, IBaseVanThielRepository<TEntity>
    where TEntity : BaseEntity
{
    #region [ Fields ]
    protected readonly PagingSettings _pagingSettings;
    #endregion

    #region [ Ctor ]
    protected BaseVanThielRepository(ILogger<BaseEntityRepository<TEntity, VanThielDbContext>> logger, IDbContextFactory<VanThielDbContext> dbContextFactory, PagingSettings pagingSettings) : base(logger, dbContextFactory)
    {
        this._pagingSettings = pagingSettings;
    }
    #endregion
}
