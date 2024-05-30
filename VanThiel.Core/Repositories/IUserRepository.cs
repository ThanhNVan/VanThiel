using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Repositories.Base;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Repositories;

public interface IUserRepository : IBaseVanThielRepository<User>
{
    #region [ Public Method - Get Many ]
    ValueTask<PagingResult<User>> GetManyAllUsersAsync(CancellationToken cancellationToken = default(CancellationToken));
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
