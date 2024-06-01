using Blazored.SessionStorage;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.Blazor.Service.Classes;

public class ProductService : BaseService, IProductService
{
    #region [ Ctor ]
    public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger, ISessionStorageService sessionStorageService)
        : base(httpClientFactory, logger, sessionStorageService)
    {
        this._entityUrl = "api/v1/product";
    }
    #endregion

    #region [ Method - Get ]
    public async ValueTask<PagingResult<Product>> GetMany_PagingAsync(int page, CancellationToken cancellationToken = default)
    {
        var result = new PagingResult<Product>();
        var url = this._entityUrl + $"/paging/{page}";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<PagingResult<Product>>(response);

        return this.EnsureCustomSuccessStatusCode(apiResult);
    }

    public async ValueTask<IEnumerable<Product>> GetMany_ActiveAsync(CancellationToken cancellationToken = default)
    {
        var result = default(IEnumerable<Product>);
        var url = this._entityUrl + $"/active";

        var httpClient = await this.CreateClientAsync();

        var response = await httpClient.GetAsync(url);

        this.EnsureSuccessfulStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<IEnumerable<Product>>(response);

        return this.EnsureCustomSuccessStatusCode(apiResult);
    }
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
