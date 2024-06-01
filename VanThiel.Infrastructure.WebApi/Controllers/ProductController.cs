using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.WebApi.Base;

namespace VanThiel.Infrastructure.WebApi;

[ApiController]
[Route("api/v1/product")]
public class ProductController : BaseVanThielController<Product, IProductService>
{
    #region [ Ctor ]
    public ProductController(IProductService service, ILogger<ProductController> logger) : base(service, logger)
    {
    }
    #endregion

    #region [ Public Method - Get ]
    [HttpGet("paging/{page}")]
    public async ValueTask<IActionResult> GetMany_PagingAsync(int page, CancellationToken cancellationToken = default)
    {
        var result = await this._service.GetMany_PagingAsync(page, cancellationToken);
        return this.ReturnOkResult(result);
    }
    #endregion

    #region [ Public Method - Post ]
    [Authorize]
    [Obsolete]
    [HttpPost]
    public async ValueTask<IActionResult> AddDemoProductDataAsync(int value, CancellationToken cancellation = default)
    {

        var productList = DataGenerator.Current.CreateData(value);
        var result = await this._service.AddManyAsync(productList);
        //var result = true;
        return this.ReturnOkResult(result.ToString());
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
