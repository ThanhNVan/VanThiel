using Blazored.SessionStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System;

namespace VanThiel.Infrastructure.Blazor.Service.Classes;

public class OrderService : BaseService, IOrderService
{
    #region [ Ctor ]
    public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger, ISessionStorageService sessionStorageService) 
        : base(httpClientFactory, logger, sessionStorageService)
    {
        this._entityUrl = "api/v1/order";
    }
    #endregion

    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    public async ValueTask<bool> Post_CheckOutAsync(IEnumerable<string> cartIdList, CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/check-out";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.PostAsJsonAsync(url, cartIdList, cancellationToken);

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
    #endregion
}
