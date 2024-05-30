using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Domain.Entities;
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
    public async ValueTask<UserAccessInfo> GetSingleUserAccessInfoAsync(SignInModel model, CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = new UserAccessInfo();
        var dbContext = await this.GetDbContextAsync(cancellationToken);

        var dbUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email
                                                    && x.PasswordHash == model.Password
                                                    && x.IsActive, cancellationToken);
        if (dbUser is null)
        {
            throw new ArgumentException("Wrong Password");
        }

        result.Id = dbUser.Id;
        result.PhoneNumber = dbUser.PhoneNumber;
        result.Fullname = dbUser.Fullname;
        result.Email = dbUser.Email;

        return result;
    }
    #endregion

    #region [ Public Method - Create ]
    #endregion

    #region [ Public Method - Update ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion

    #region [ Public Method - Validate ]
    public async Task IsValidUserAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
    {
        var dbContext = await this.GetDbContextAsync(cancellationToken);

        var dbUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (dbUser is null)
        {
            throw new ArgumentException("Not found any user with this email address");
        }

        if (dbUser.IsActive is false)
        {
            throw new ArgumentException("This user has been prohibited.");
        }

        return;
    }
    #endregion
}