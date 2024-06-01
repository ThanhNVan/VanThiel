using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.Repositories.Base;
using VanThiel.Application.Repositories.DatabaseContext;
using VanThiel.Application.Settings;
using VanThiel.Core.Repositories;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Repositories;

public class CartRepository : BaseVanThielRepository<Cart>, ICartRepository
{
    #region [ Ctor ]
    public CartRepository(ILogger<CartRepository> logger, IDbContextFactory<VanThielDbContext> dbContextFactory, PagingSettings pagingSettings)
        : base(logger, dbContextFactory, pagingSettings)
    {
    }
    #endregion

    #region [ Public Method - Get Many ]
    public async ValueTask<IEnumerable<Cart>> GetMany_ByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var result = new List<Cart>();
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

        result = await dbContext.Carts.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

        return result;
    }
    #endregion

    #region [ Public Method - Get Single ]
    public async ValueTask<Cart> GetSingle_ByProductAndUserAsync(string userId, string productId, CancellationToken cancellationToken = default)
    {
        var result = default(Cart);
        using var dbContext = await this.GetDbContextAsync(cancellationToken);

        result = await dbContext.Carts.FirstOrDefaultAsync(x => x.IsActive
                                                                && x.ProductId == productId
                                                                && x.UserId == userId
                                                                ,cancellationToken);

        return result;
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