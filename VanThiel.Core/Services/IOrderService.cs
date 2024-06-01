using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Core.Services.Base;
using VanThiel.Domain.Entities;

namespace VanThiel.Core.Services;

public interface IOrderService : IBaseVanThielService<Order>
{
    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    ValueTask<bool> Post_CheckOutAsync(ClaimsIdentity identity, IEnumerable<string> cartIdList, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
