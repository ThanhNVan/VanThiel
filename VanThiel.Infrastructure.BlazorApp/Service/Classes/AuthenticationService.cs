using Blazored.SessionStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.Blazor.Service.Classes;

public class AuthenticationService : BaseService, IAuthenticationService
{
    #region [ CTor ]
    public AuthenticationService(IHttpClientFactory httpClientFactory,
                                ILogger<AuthenticationService> logger,
                                ISessionStorageService sessionStorageService)
        : base(httpClientFactory, logger, sessionStorageService)
    {
        this._entityUrl = "api/v1/authentication";

    }
    #endregion

    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    public async ValueTask<string> UserSignInAsync(SignInModel model, CancellationToken cancellationToken = default)
    {
        var result = default(string);
        var url = this._entityUrl + "/user/sign-in";

        var httpClient = this.CreateClient();

        var response = await httpClient.PostAsJsonAsync(url, model);
        EnsureSuccessfullStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<string>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = apiResult.Data;
            return result;
        }

        throw new Exception($"{apiResult.Message}");
    }
    public async ValueTask<string> UserSignUpAsync(SignUpModel model, CancellationToken cancellationToken = default)
    {
        var result = default(string);
        var url = this._entityUrl + "/user/sign-up";

        var httpClient = this.CreateClient();

        var response = await httpClient.PostAsJsonAsync(url, model);
        EnsureSuccessfullStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<string>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = apiResult.Data;
            return result;
        }

        throw new Exception($"{apiResult.Message}");
    }
    #endregion

    #region [ Public Method - Put ]
    public async ValueTask<string> Update_UserProfileAsync(UserMyProfile model, CancellationToken cancellationToken = default)
    {
        var result = default(string);
        var url = this._entityUrl + "/user/update-info";

        var httpClient = this.CreateClient();

        var response = await httpClient.PutAsJsonAsync(url, model);
        EnsureSuccessfullStatusCode(response);
        var apiResult = await this.DeserializeObjectAsync<string>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = apiResult.Data;
            return result;
        }

        throw new Exception($"{apiResult.Message}");
    }
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
