using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.SharedLibrary;

public interface IBaseEntityRepository<TEntity>
    where TEntity : BaseEntity
{
    #region [ Public Methods - CRUD ]
    ValueTask<bool> AddSingleAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<bool> AddManyAsync(IEnumerable<TEntity> entityList, CancellationToken cancellationToken = default(CancellationToken));


    ValueTask<bool> IsExistAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<TEntity> GetSingleByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<bool> SoftDeleteAsync(string entityId, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<bool> RecoverAsync(string entityId, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<bool> DestroyAsync(string entityId, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<IEnumerable<TEntity>> GetManyAsync(int take, int skip, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<IEnumerable<TEntity>> GetManyActiveAsync(int take, int skip, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<IEnumerable<TEntity>> GetManyInactiveAsync(int take, int skip, CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<int> CountAllAsync(CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<int> CountActiveAsync(CancellationToken cancellationToken = default(CancellationToken));

    ValueTask<int> CountInactiveAsync(CancellationToken cancellationToken = default(CancellationToken));
    #endregion
}

