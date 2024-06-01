using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories;
using VanThiel.Core.ExceptionClasses;
using VanThiel.Domain.DTOs;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Domain.Entities;
using VanThiel.Domain.Enums;
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
    public async ValueTask<PagingResult<User>> GetManyAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var result = new PagingResult<User>();

        using var dbContext = await this.GetDbContextAsync(cancellationToken);

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
    public async ValueTask<UserAccessInfo> GetSingleUserAccessInfoAsync(SignInModel model, CancellationToken cancellationToken = default)
    {
        var result = default(UserAccessInfo);
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

        var dbUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email
                                                    && x.PasswordHash == model.Password
                                                    && x.IsActive, cancellationToken);
        if (dbUser is null)
        {
            throw new ArgumentException("Wrong Password");
        }
        result = new UserAccessInfo();
        result.Id = dbUser.Id;
        result.PhoneNumber = dbUser.PhoneNumber;
        result.Fullname = dbUser.Fullname;
        result.Email = dbUser.Email;

        return result;
    }

    public async ValueTask<UserMyProfile> GetSingle_MyProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = default(UserMyProfile);
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

        var dbUser = await dbContext.Users.FindAsync(userId, cancellationToken);

        if (dbUser is null)
        {
            throw new NotFoundException($"Not found any user associated with this Id: {userId}");
        }

        result = new UserMyProfile();
        result.Id = dbUser.Id;
        result.PhoneNumber = dbUser.PhoneNumber;
        result.Fullname = dbUser.Fullname;
        result.Email = dbUser.Email;
        result.Address = dbUser.Address;

        return result;
    }

    public async ValueTask<bool> IsExistedEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

        result = await dbContext.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);

        return result;
    }
    
    public async ValueTask<bool> IsExistedPhoneAsync(string phone, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

        result = await dbContext.Users.AnyAsync(x => x.PhoneNumber.ToLower() == phone.ToLower(), cancellationToken);

        return result;
    }
    #endregion

    #region [ Public Method - Create ]
    public async ValueTask<UserAccessInfo> CreateUserAsync(SignUpModel model, CancellationToken cancellationToken = default)
    {
        var result = default(UserAccessInfo);
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

        var user = new User { 
            Id = Guid.NewGuid().ToString(),
            IsActive = true,
            CreatedAt = DateTime.Now,
            LastUpdatedAt = DateTime.Now,

            Email = model.Email,
            Fullname = model.Fullname,
            PasswordHash = model.Password,
            Role = RoleEnum.User,
            PhoneNumber = model.PhoneNumber,
        };

        await dbContext.AddAsync(user, cancellationToken);   
        await dbContext.SaveChangesAsync(cancellationToken);

        result = new UserAccessInfo 
        { 
            Id = user.Id,
            Email = model.Email,    
            Fullname = model.Fullname,
            PhoneNumber = model.PhoneNumber, 
        };

        return result;
    }
    #endregion

    #region [ Public Method - Update ]
    public async ValueTask<UserAccessInfo> UpdateAsync(UserMyProfile model, CancellationToken cancellationToken = default)
    {
        var result = default(UserAccessInfo);

        using var dbContext = await this.GetDbContextAsync(cancellationToken);
        var dbEntity = await dbContext.Users.FindAsync(model.Id);
    
        dbEntity.PhoneNumber = model.PhoneNumber;
        dbEntity.Email = model.Email;
        dbEntity.Address = model.Address;
        dbEntity.Fullname = model.Fullname;

        dbContext.Users.Update(dbEntity);
        await dbContext.SaveChangesAsync(cancellationToken);

        result = new UserAccessInfo { 
            Id = model.Id,
            Email = model.Email,
            Fullname = model.Fullname,
            PhoneNumber = model.PhoneNumber,
        };
        return result;
    }
    #endregion

    #region [ Public Method - Delete ]
    #endregion

    #region [ Public Method - Validate ]
    public async Task IsValidUserAsync(string email, CancellationToken cancellationToken = default)
    {
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

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

    public async Task IsValidUserByPhoneAndEmailAsync(string email, string phone, CancellationToken cancellationToken = default)
    {
        var isExistedPhone = await this.IsExistedPhoneAsync(phone, cancellationToken);
        if (isExistedPhone) {
            throw new ArgumentException("Phone number cannot be duplicate");
        }
        var isExistEmail = await this.IsExistedEmailAsync(email, cancellationToken);
        if (isExistEmail)
        {
            throw new ArgumentException("Email cannot be duplcate");
        }
        return;
    }
    #endregion
}