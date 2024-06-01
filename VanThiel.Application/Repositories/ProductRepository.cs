using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Repositories;

public class ProductRepository : BaseVanThielRepository<Product>, IProductRepository
{
    #region [ Ctor ]
    public ProductRepository(ILogger<ProductRepository> logger, IDbContextFactory<VanThielDbContext> dbContextFactory, PagingSettings pagingSettings)
        : base(logger, dbContextFactory, pagingSettings)
    {
    }
    #endregion

    #region [ Public Method - Get Many ]
    #endregion

    #region [ Public Method - Get Single ]
    public async ValueTask<int> GetSingle_InstockAsync(string productId, CancellationToken cancellationToken = default)
    {
        using var dbContext = await GetDbContextAsync(cancellationToken);
        var result = await dbContext.Products.Select(x => new { Id = x.Id, Instock = x.Instock}).FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

        return result.Instock;
    }
    #endregion

    #region [ Public Method - Create ]
    #endregion

    #region [ Public Method - Update ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion

    #region [ Public Method - Validate ]
    #endregion
}