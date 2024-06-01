using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.DTOs.ReturnModel;

namespace VanThiel.Infrastructure.Blazor.Service.Interfaces;

public interface ICartService
{
    #region [ Public Method - Get ]
    ValueTask<IEnumerable<CartInfo>> GetMany_ByUserAsync(CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Post ]
    ValueTask<bool> Update_AddSingleToCartAsync(string productId, CancellationToken cancellationToken = default);

    ValueTask<bool> Update_SubtractFromCartAsync(string productId, CancellationToken cancellationToken = default);

    ValueTask<bool> Update_AddManyToCartAsync(UpdateCart model, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    ValueTask<bool> Update_RemoveFromCartAsync(string productId, CancellationToken cancellationToken = default);
    #endregion
}
