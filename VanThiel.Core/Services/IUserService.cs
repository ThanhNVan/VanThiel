using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Services.Base;
using VanThiel.Domain.DTOs;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Services;

public interface IUserService : IBaseVanThielService<User>
{
    #region [ Public Method - Get ]
    ValueTask<PagingResult<User>> GetManyAllUsersAsync(CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<UserMyProfile> GetSingle_MyProfileAsync(ClaimsIdentity identity, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
