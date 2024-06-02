using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Core.Services.Base;
using VanThiel.Domain.Entities;
using VanThiel.Domain.DTOs.ReturnModel;

namespace VanThiel.Core.Services;

public interface IOrderService : IBaseVanThielService<Order>
{
    #region [ Public Method - Get ]
    ValueTask<OrderInfo> GetSingle_InfoByIdAsync(string id, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<OrderInfo>> GetMany_ActiveAsync(CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<MyOrderInfo>> GetMany_ByUserAsync(ClaimsIdentity identity, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Post ]
    ValueTask<bool> Post_CheckOutAsync(ClaimsIdentity identity, IEnumerable<string> cartIdList, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
