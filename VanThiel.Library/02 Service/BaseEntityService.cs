using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.SharedLibrary;

public abstract class BaseEntityService<TEntity, TRepository> : IBaseEntityService<TEntity>
    where TEntity : BaseEntity
    where TRepository : IBaseEntityRepository<TEntity>
{
    #region [ Fields ]
    protected readonly ILogger<BaseEntityService<TEntity,TRepository>> _logger;
    protected readonly TRepository _repository;
    #endregion

    #region [ CTor ]
    public BaseEntityService(ILogger<BaseEntityService<TEntity, TRepository>> logger, TRepository repository)
    {
        this._logger = logger;
        this._repository = repository;
    }
    #endregion
}
