using Blazored.SessionStorage;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

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
    public async ValueTask<OrderInfo> GetSingle_InfoByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var url = this._entityUrl + $"/single/{id}";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<OrderInfo>(response);

        return this.EnsureCustomSuccessStatusCode(apiResult);
    }

    public async ValueTask<IEnumerable<OrderInfo>> GetMany_ActiveAsync(CancellationToken cancellationToken = default)
    {
        var url = this._entityUrl + $"/many-active";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<IEnumerable<OrderInfo>>(response);

        return this.EnsureCustomSuccessStatusCode(apiResult);
    }

    public async ValueTask<IEnumerable<MyOrderInfo>> GetMany_ByUserAsync(CancellationToken cancellationToken = default)
    {
        var result = default(bool);
        var url = this._entityUrl + $"/many-active";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url, cancellationToken);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<IEnumerable<OrderInfo>>(response);

        return this.EnsureCustomSuccessStatusCode(apiResult);
    }
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

        return this.EnsureCustomSuccessStatusCode(apiResult, true);
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
