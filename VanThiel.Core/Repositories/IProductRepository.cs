﻿using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Repositories.Base;
using VanThiel.Domain.Entities;

namespace VanThiel.Core.Repositories;

public interface IProductRepository : IBaseVanThielRepository<Product>
{
    #region [ Public Method - Get Many ]
    #endregion

    #region [ Public Method - Get Single ]
    ValueTask<int> GetSingle_InstockAsync(string productId, CancellationToken cancellationToken = default);
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
