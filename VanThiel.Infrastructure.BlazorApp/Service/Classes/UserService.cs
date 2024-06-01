using Blazored.SessionStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.Blazor.Service.Classes;

public class UserService : BaseService, IUserService
{
    #region [ CTor ]
    public UserService(IHttpClientFactory httpClientFactory,
                                ILogger<AuthenticationService> logger,
                                ISessionStorageService sessionStorageService)
        : base(httpClientFactory, logger, sessionStorageService)
    {
        this._entityUrl = "api/v1/user";

    }
    #endregion

    #region [ Public Method - Get ]
    public async ValueTask<UserMyProfile> GetSingle_MyProfileAsync(CancellationToken cancellationToken = default)
    {
        var result = new UserMyProfile();
        var url = this._entityUrl + "/my-profile";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url);
        EnsureSuccessfullStatusCode(response);

        var apiResult = JsonConvert.DeserializeObject<ApiResult<UserMyProfile>>(await response.Content.ReadAsStringAsync());

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = apiResult.Data;
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }

    public async ValueTask<bool> GetSingle_IsExistEmailAsync(string email, string currentEmail, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/existed-email/newEmail={email}&currentEmail={currentEmail}";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url);
        EnsureSuccessfullStatusCode(response);

        var apiResult = JsonConvert.DeserializeObject<ApiResult<string>>(await response.Content.ReadAsStringAsync());

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = bool.Parse(apiResult.Data);
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }
    
    public async ValueTask<bool> GetSingle_IsExistPhoneNumberAsync(string phone, string currentPhone, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/existed-phone/newPhone={phone}&&currentPhone={currentPhone}";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url);
        EnsureSuccessfullStatusCode(response);

        var apiResult = JsonConvert.DeserializeObject<ApiResult<string>>(await response.Content.ReadAsStringAsync());

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = bool.Parse(apiResult.Data);
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
