using Blazored.SessionStorage;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VanThiel.Infrastructure.Blazor.Data;

namespace VanThiel.Infrastructure.Blazor.Service.Classes;

public abstract class BaseService
{
    #region [ Fields ]
    protected readonly IHttpClientFactory _httpClientFactory;
    protected string _entityUrl;
    protected readonly ILogger<BaseService> _logger;
    protected readonly ISessionStorageService _sessionStorageService;
    #endregion

    #region [ CTor ]
    public BaseService(IHttpClientFactory httpClientFactory,
                        ILogger<BaseService> logger,
                        ISessionStorageService sessionStorageService)
    {
        this._httpClientFactory = httpClientFactory;
        this._logger = logger;
        this._sessionStorageService = sessionStorageService;
    }
    #endregion

    #region [ Protected Methods -  ]
    protected HttpClient CreateClient(string clientName = "BaseClient", string accessToken = "")
    {
        // RoutingUrl.BaseClientName = "BaseClientName"
        var result = this._httpClientFactory.CreateClient(clientName);

        if (!string.IsNullOrEmpty(accessToken))
        {
            result.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return result;
    }

    protected async ValueTask<HttpClient> CreateClientAsync(bool isAuthenticated = true) {
        var clientName = "BaseClient";
        var result = this._httpClientFactory.CreateClient(clientName);

        if (isAuthenticated)
        {
            var accessToken = (await this._sessionStorageService.GetItemAsync<UserSession>(nameof(UserSession))).AccessToken;
            result.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return result;
    }

    protected void EnsureSuccessfullStatusCode(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        throw new Exception("Something is wrong please try later.");
    }
    #endregion
}
