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
    ValueTask<PagingResult<User>> GetManyAllUsersAsync(CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Get Single ]
    ValueTask<UserAccessInfo> GetSingleUserAccessInfoAsync(SignInModel model, CancellationToken cancellationToken = default);

    ValueTask<bool> IsExistedEmailAsync(string email, CancellationToken cancellationToken = default);

    ValueTask<bool> IsExistedPhoneAsync(string phone, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Create ]
    ValueTask<UserAccessInfo> CreateUserAsync(SignUpModel model, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Update ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion

    #region [ Public Method - Validate ]
    Task IsValidUserAsync(string email, CancellationToken cancellationToken = default);

    Task IsValidSignUpAsync(string email, string phone, CancellationToken cancellationToken = default);
    #endregion
}
