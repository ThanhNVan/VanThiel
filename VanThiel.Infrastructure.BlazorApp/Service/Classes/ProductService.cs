using Blazored.SessionStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
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

        this.EnsureSuccessfullStatusCode(response);

        var apiResult = await this.DeserializeObjectAsync<PagingResult<Product>>(response);

        if (apiResult.StatusCode == nameof(StatusCodes.Status200OK))
        {
            result = apiResult.Data;
            return result;
        }
        throw new Exception($"{apiResult.Message}");
    }
    #endregion
}
