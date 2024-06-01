using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Repositories.Base;
using VanThiel.Domain.Entities;

namespace VanThiel.Core.Repositories;

public interface ICartRepository : IBaseVanThielRepository<Cart>
{
    #region [ Public Method - Get Many ]
    ValueTask<IEnumerable<Cart>> GetMany_ByUserAsync(string userId, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Get Single ]
    ValueTask<Cart> GetSingle_ByProductAndUserAsync(string userId, string productId, CancellationToken cancellationToken = default);
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
