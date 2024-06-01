﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Core.Repositories.Base;
using VanThiel.Domain.DTOs.ReturnModel;
using VanThiel.Domain.Entities;

namespace VanThiel.Core.Repositories;

public interface IOrderRepository: IBaseVanThielRepository<Order>
{
    #region [ Public Method - Get Many ]
    #endregion

    #region [ Public Method - Get Single ]
    #endregion

    #region [ Public Method - Create ]
    ValueTask<bool> Create_TransactionAsync(IEnumerable<CartInfo> cartInfos, string userId, CancellationToken cancellationToken = default);
    #endregion

    #region [ Public Method - Update ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion

    #region [ Public Method - Validate ]
    #endregion
}
