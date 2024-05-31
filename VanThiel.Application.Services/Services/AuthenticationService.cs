using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Settings;
using VanThiel.Core.ExceptionClasses;
using VanThiel.Core.Extension;
using VanThiel.Core.Repositories.Context;
using VanThiel.Core.Services;
using VanThiel.Domain.DTOs.RequestModel;

namespace VanThiel.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    #region [ Fields ]
    private readonly RepositoryContext _repositoryContext;
    private readonly JwtSettings _jwtSettings;
    #endregion

    #region [ Ctor ]
    public AuthenticationService(RepositoryContext repositoryContext, JwtSettings jwtSettings)
    {
        this._repositoryContext = repositoryContext;
        this._jwtSettings = jwtSettings;
    }
    #endregion

    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    public async ValueTask<string> UserSignInAsync(SignInModel model, CancellationToken cancellationToken = default)
    {
        var result = string.Empty;
        GuardParametter.ParamIsNull(model);
        GuardParametter.StringIsNullOrEmpty(model.Password, "Password is empty.");
        GuardParametter.StringIsNullOrEmpty(model.Email, "Email is empty.");

        await this._repositoryContext.User.IsValidUserAsync(model.Email, cancellationToken);

        model.Password = CoreExtensions.HashPassword512(model.Password);

        var userAccessInfo = await this._repositoryContext.User.GetSingleUserAccessInfoAsync(model, cancellationToken);

        result = CoreExtensions.GenerateUserAccessToken(userAccessInfo, this._jwtSettings.Secret);

        return result;
    }

    public async ValueTask<string> UserSignUpAsync(SignUpModel model, CancellationToken cancellationToken = default)
    {
        var result = string.Empty;
        GuardParametter.ParamIsNull(model);
        GuardParametter.StringIsNullOrEmpty(model.Password, "Password is empty.");
        GuardParametter.StringIsNullOrEmpty(model.Email, "Email is empty.");
        GuardParametter.StringIsNullOrEmpty(model.PhoneNumber, "Phone Number is empty.");

        await this._repositoryContext.User.IsValidSignUpAsync(model.Email, model.PhoneNumber, cancellationToken);

        model.Password = CoreExtensions.HashPassword512(model.Password);

        var userAccessInfo = await this._repositoryContext.User.CreateUserAsync(model, cancellationToken);
        result = CoreExtensions.GenerateUserAccessToken(userAccessInfo, this._jwtSettings.Secret);
        return result;
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
