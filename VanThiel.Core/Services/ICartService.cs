using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Core.Services.Base;
using VanThiel.Domain.Entities;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.DTOs.ReturnModel;

namespace VanThiel.Core.Services;

public interface ICartService : IBaseVanThielService<Cart>
{
    #region [ Public Method - Get ]
    ValueTask<IEnumerable<CartInfo>> GetMany_ByUserAsync(ClaimsIdentity identity, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Post ]
    ValueTask<bool> Update_AddSingleToCartAsync(ClaimsIdentity identity, string productId, CancellationToken cancellationToken = default);

    ValueTask<bool> Update_SubtractFromCartAsync(ClaimsIdentity identity, string productId, CancellationToken cancellationToken = default);

    ValueTask<bool> Update_AddManyToCartAsync(ClaimsIdentity identity, UpdateCart model, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    ValueTask<bool> Update_RemoveFromCartAsync(ClaimsIdentity identity, string productId, CancellationToken cancellationToken = default);
    #endregion
}
