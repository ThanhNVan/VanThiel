using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Services.Base;
using VanThiel.Core.ExceptionClasses;
using VanThiel.Core.Repositories;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Application.Services;

public class ProductService : BaseVanThielService<Product, IProductRepository>, IProductService
{
    #region [ Ctor ]
    public ProductService(ILogger<ProductService> logger, IProductRepository repository) : base(logger, repository)
    {
    }
    #endregion

    #region [ Method - Get ]
    public ValueTask<PagingResult<Product>> GetMany_PagingAsync(int page, CancellationToken cancellationToken = default)
    {
        GuardParametter.IsValidPage(page);
        return this._repository.GetMany_PagingAsync(page, cancellationToken);
    }
    #endregion

    #region [ Method - Post ]
    public ValueTask<bool> AddManyAsync(IEnumerable<Product> list)
    {
        return this._repository.AddManyAsync(list);
    }
    #endregion
}
