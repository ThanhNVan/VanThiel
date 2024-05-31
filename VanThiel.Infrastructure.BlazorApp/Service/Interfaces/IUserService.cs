using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs;

namespace VanThiel.Infrastructure.Blazor.Service.Interfaces;

public interface IUserService
{
    #region [ Public Method - Get ]
    ValueTask<UserMyProfile> GetSingle_MyProfileAsync(CancellationToken cancellationToken = default);

    ValueTask<bool> GetSingle_IsExistEmailAsync(string email, string currentEmail, CancellationToken cancellationToken = default);

    ValueTask<bool> GetSingle_IsExistPhoneNumberAsync(string phone, string currentPhone, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
