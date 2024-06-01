using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories.Base;
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

    #region [ Get Many ]
    public async ValueTask<PagingResult<TEntity>> GetMany_PagingAsync(int page, CancellationToken cancellationToken = default)
    {
        var result = new PagingResult<TEntity>();

        var dbContext = await this.GetDbContextAsync(cancellationToken);

        result.PageSize = this._pagingSettings.PageSize;
        result.CurrentPage = page;

        var data = await dbContext.Set<TEntity>().AsNoTracking()
                                            .Where(x => x.IsActive)
                                            .Skip(this._pagingSettings.PageSize * (page - 1))
                                            .Take(this._pagingSettings.PageSize)
                                            .ToListAsync(cancellationToken);
        result.Data = data;

        var dataCount = await dbContext.Set<TEntity>().AsNoTracking().CountAsync(x => x.IsActive, cancellationToken);
        result.DataCount = dataCount;
        var totalPage = 0;

        if (dataCount % this._pagingSettings.PageSize == 0)
        {
            totalPage = dataCount / this._pagingSettings.PageSize; 
        } else
        {
            totalPage = dataCount / this._pagingSettings.PageSize + 1; 
        }
        result.TotalPages = totalPage;  

        return result;
    }
    #endregion
}
