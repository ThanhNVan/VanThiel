using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
