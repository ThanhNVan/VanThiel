﻿using System.Threading.Tasks;
using System.Threading;
using VanThiel.SharedLibrary;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Core.Repositories.Base;

public interface IBaseVanThielRepository<TEntity> : IBaseEntityRepository<TEntity>
    where TEntity : BaseEntity
{
    #region [ Get Many ]
    ValueTask<PagingResult<TEntity>> GetMany_PagingAsync(int page, CancellationToken cancellationToken = default);
    #endregion
}
