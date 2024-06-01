using Blazored.SessionStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Service.Classes;

public class CartService : BaseService, ICartService
{
    #region [ Ctor ]
    public CartService(IHttpClientFactory httpClientFactory, ILogger<CartService> logger, ISessionStorageService sessionStorageService)
        : base(httpClientFactory, logger, sessionStorageService)
    {
        this._entityUrl = "api/v1/cart";
    }
    #endregion

    #region [ Public Method - Get ]
    public async ValueTask<IEnumerable<CartInfo>> GetMany_ByUserAsync(CancellationToken cancellationToken = default)
    {
        var result = default(IEnumerable<CartInfo>);
        var url = this._entityUrl + $"/my-cart";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<List<CartInfo>>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = apiResult.Data;
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }
    #endregion

    #region [ Public Method - Post ]
    public async ValueTask<bool> Update_AddSingleToCartAsync(string productId, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/add-single-to-cart";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.PostAsJsonAsync(url, productId, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<string>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = bool.Parse(apiResult.Data);
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }

    public async ValueTask<bool> Update_SubtractFromCartAsync(string productId, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/subtract-from-cart";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.PostAsJsonAsync(url, productId, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<string>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = bool.Parse(apiResult.Data);
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }
    
    public async ValueTask<bool> Update_AddManyToCartAsync(UpdateCart model, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/add-many-to-cart";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.PostAsJsonAsync(url, model, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<string>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = bool.Parse(apiResult.Data);
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    public async ValueTask<bool> Update_RemoveFromCartAsync(string productId, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/{productId}";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.DeleteAsync(url, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<string>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = bool.Parse(apiResult.Data);
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }
    #endregion
}
