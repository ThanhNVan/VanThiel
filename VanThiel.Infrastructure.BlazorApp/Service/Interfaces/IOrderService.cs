using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VanThiel.Infrastructure.Blazor.Service.Interfaces;

public interface IOrderService
{
    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    ValueTask<bool> Post_CheckOutAsync(IEnumerable<string> cartIdList, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
