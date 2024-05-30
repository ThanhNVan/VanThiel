using Microsoft.Extensions.Logging;
using VanThiel.Core.Repositories.Base;
using VanThiel.Core.Services.Base;
using VanThiel.SharedLibrary;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Application.Services.Base;

public abstract class BaseVanThielService<TEntity, TRepository> : BaseEntityService<TEntity, TRepository>, IBaseVanThielService<TEntity>
    where TEntity : BaseEntity
    where TRepository : IBaseVanThielRepository<TEntity>
{
    #region [ Ctor ]
    protected BaseVanThielService(ILogger<BaseEntityService<TEntity, TRepository>> logger, TRepository repository) : base(logger, repository)
    {
    }
    #endregion
}
