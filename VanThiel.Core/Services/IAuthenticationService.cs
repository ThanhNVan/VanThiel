﻿using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.RequestModel;

namespace VanThiel.Core.Services;

public interface IAuthenticationService
{
    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    ValueTask<string> UserSignInAsync(SignInModel model, CancellationToken cancellationToken = default);

    ValueTask<string> UserSignUpAsync(SignUpModel model, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
