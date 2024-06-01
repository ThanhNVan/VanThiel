using Blazored.SessionStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Infrastructure.Blazor.Data;
using VanThiel.SharedLibrary.Entity;

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

    protected async ValueTask<HttpClient> CreateClientAsync(bool isAuthenticated = true, CancellationToken cancellationToken = default) {
        var clientName = "BaseClient";
        var result = this._httpClientFactory.CreateClient(clientName);

        if (isAuthenticated)
        {
            var accessToken = (await this._sessionStorageService.GetItemAsync<UserSession>(nameof(UserSession), cancellationToken)).AccessToken;
            result.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return result;
    }

    protected void EnsureSuccessfulStatusCode(HttpResponseMessage? response)
    {
        if (response is null || !response.IsSuccessStatusCode)
        {
            throw new ArgumentException("Something is wrong please try later.");
        }

        return;
    }

    protected async ValueTask<ApiResult<TType>> DeserializeObjectAsync<TType>(HttpResponseMessage response, CancellationToken cancellationToken = default)
        where TType : class
    {
        var apiResult = JsonConvert.DeserializeObject<ApiResult<TType>>(await response.Content.ReadAsStringAsync(cancellationToken));
        return apiResult;
    }

    protected TType EnsureCustomSuccessStatusCode<TType>(ApiResult<TType> apiResult)
        where TType : class
    {
        if (apiResult.StatusCode != nameof(StatusCodes.Status200OK))
        {
            throw new Exception($"{apiResult.Message}");
        }

        return apiResult.Data;
    }
    
    protected bool EnsureCustomSuccessStatusCode(ApiResult<string> apiResult, bool isBool)
    {
        if (apiResult.StatusCode != nameof(StatusCodes.Status200OK))
        {
            throw new Exception($"{apiResult.Message}");
        }
      
        var result = bool.Parse(apiResult.Data);
        
        return result;
    }
    #endregion
}
