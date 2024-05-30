using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Repositories.Base;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Repositories;

public interface IUserRepository : IBaseVanThielRepository<User>
{
    #region [ Public Method - Get Many ]
    ValueTask<PagingResult<User>> GetManyAllUsersAsync(CancellationToken cancellationToken = default(CancellationToken));
    #endregion

    #region [ Public Method - Get Single ]
    ValueTask<UserAccessInfo> GetSingleUserAccessInfoAsync(SignInModel model,CancellationToken cancellationToken = default(CancellationToken));
    #endregion

    #region [ Public Method - Create ]
    #endregion

    #region [ Public Method - Update ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion

    #region [ Public Method - Validate ]
    Task IsValidUserAsync(string email, CancellationToken cancellationToken = default(CancellationToken));
    #endregion
}
