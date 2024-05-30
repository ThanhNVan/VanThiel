using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Core.Repositories;
using VanThiel.Domain.Entities;
using VanThiel.Domain.Settings;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Application.Repositories;

public class UserRepository : BaseVanThielRepository<User>, IUserRepository
{
    #region [ Ctor ]
    public UserRepository(ILogger<UserRepository> logger, IDbContextFactory<VanThielDbContext> dbContextFactory, PagingSettings pagingSettings)
        : base(logger, dbContextFactory, pagingSettings)
    {
    }
    #endregion

    #region [ Public Method - Get Many ]
    public async ValueTask<PagingResult<User>> GetManyAllUsersAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = new PagingResult<User>();

        var dbContext = await this.GetDbContextAsync(cancellationToken);

        var data = await dbContext.Users.ToListAsync(cancellationToken);

        result.CurrentPage = 1;
        result.TotalPages = data.Count / this._pagingSettings.PageSize + 1;
        result.PageSize = this._pagingSettings.PageSize;
        result.DataCount = data.Count;
        result.Data = data;

        return result;
    }
    #endregion

    #region [ Public Method - Get Single ]
    #endregion

    #region [ Public Method - Create ]
    #endregion

    #region [ Public Method - Update ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}