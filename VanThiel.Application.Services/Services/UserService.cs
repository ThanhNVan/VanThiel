using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Services.Base;
using VanThiel.Core.ExceptionClasses;
using VanThiel.Core.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.DTOs;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Application.Services;

public class UserService : BaseVanThielService<User, IUserRepository>, IUserService
{
    #region [ Ctor ]
    public UserService(ILogger<UserService> logger, IUserRepository repository) : base(logger, repository)
    {
    }
    #endregion

    #region [ Public Method - Get ]
    public ValueTask<PagingResult<User>> GetManyAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return this._repository.GetManyAllUsersAsync(cancellationToken);
    }

    public ValueTask<UserMyProfile> GetSingle_MyProfileAsync(ClaimsIdentity identity, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidIdentity(identity);

        var userId = identity.FindFirst("UserId").Value;
        GuardParametter.IsValidJwtClaim(userId);

        return this._repository.GetSingle_MyProfileAsync(userId, cancellationToken);
    }
    public ValueTask<bool> GetSingle_IsExistEmailAsync(string email, string currentEmail, CancellationToken cancellationToken = default)
    {
        GuardParametter.StringIsNullOrEmpty(email);
        GuardParametter.StringIsNullOrEmpty(currentEmail);

        if (email.Equals(currentEmail))
        {
            return ValueTask.FromResult(false);
        }

        return this._repository.IsExistedEmailAsync(email, cancellationToken);
    }
    public ValueTask<bool> GetSingle_IsExistPhoneNumberAsync(string phone, string currentPhone, CancellationToken cancellationToken = default)
    {
        GuardParametter.StringIsNullOrEmpty(phone);
        GuardParametter.StringIsNullOrEmpty(currentPhone);

        if (phone.Equals(currentPhone))
        {
            return ValueTask.FromResult(false);
        }

        return this._repository.IsExistedPhoneAsync(phone, cancellationToken);
    }
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
