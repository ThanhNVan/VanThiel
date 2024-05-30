using Microsoft.Extensions.Logging;
using VanThiel.Application.Services.Base;
using VanThiel.Core.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Services;

public class ProductService : BaseVanThielService<Product, IProductRepository>, IProductService
{
    #region [ Ctor ]
    public ProductService(ILogger<ProductService> logger, IProductRepository repository) : base(logger, repository)
    {
    }
    #endregion
}
