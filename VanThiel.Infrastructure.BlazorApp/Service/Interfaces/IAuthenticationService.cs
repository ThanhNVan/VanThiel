using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs;
using VanThiel.Domain.DTOs.RequestModel;

namespace VanThiel.Infrastructure.Blazor.Service.Interfaces;

public interface IAuthenticationService
{
    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    ValueTask<string> UserSignInAsync(SignInModel model, CancellationToken cancellationToken = default);
    
    ValueTask<string> UserSignUpAsync(SignUpModel model, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    ValueTask<string> Update_UserProfileAsync(UserMyProfile model, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
