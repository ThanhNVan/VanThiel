using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs;

namespace VanThiel.Infrastructure.Blazor.Service.Interfaces;

public interface IUserService
{
    #region [ Public Method - Get ]
    ValueTask<UserMyProfile> GetSingle_MyProfileAsync(CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
