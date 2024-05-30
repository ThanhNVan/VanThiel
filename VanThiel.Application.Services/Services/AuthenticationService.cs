using VanThiel.Core.Repositories.Context;
using VanThiel.Core.Services;

namespace VanThiel.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    #region [ Fields ]
    private readonly RepositoryContext repositoryContext;
    #endregion

    #region [ Ctor ]
    public AuthenticationService(RepositoryContext repositoryContext)
    {
        this.repositoryContext = repositoryContext;
    }
    #endregion

    #region [ Public Methods - Get Many ]
    #endregion

    #region [ Public Methods - Get Single ]
    #endregion

    #region [ Public Methods - Create ]
    #endregion

    #region [ Public Methods - Update ]
    #endregion

    #region [ Public Methods - Delete ]
    #endregion
}
