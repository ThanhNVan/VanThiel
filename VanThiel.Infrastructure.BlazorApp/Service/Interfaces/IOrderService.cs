using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.DTOs.ReturnModel;

namespace VanThiel.Infrastructure.Blazor.Service.Interfaces;

public interface IOrderService
{
    #region [ Public Method - Get ]
    ValueTask<OrderInfo> GetSingle_InfoByIdAsync(string id, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<OrderInfo>> GetMany_ActiveAsync(CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<MyOrderInfo>> GetMany_ByUserAsync(CancellationToken cancellationToken = default);

    #endregion

    #region [ Public Method - Post ]
    ValueTask<bool> Post_CheckOutAsync(IEnumerable<string> cartIdList, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
