﻿using System.Threading.Tasks;
using System.Threading;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Infrastructure.Blazor.Data;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using VanThiel.SharedLibrary.Entity;
using Microsoft.AspNetCore.Http;
using System;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;
using Blazored.SessionStorage;

namespace VanThiel.Infrastructure.Blazor.Service.Classes;

public class AuthenticationService :BaseService, IAuthenticationService
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

        var apiResult = JsonConvert.DeserializeObject<ApiResult<string>>(await response.Content.ReadAsStringAsync());

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

        var apiResult = JsonConvert.DeserializeObject<ApiResult<string>>(await response.Content.ReadAsStringAsync());

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = apiResult.Data;
            return result;
        }

        throw new Exception($"{apiResult.Message}");
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}