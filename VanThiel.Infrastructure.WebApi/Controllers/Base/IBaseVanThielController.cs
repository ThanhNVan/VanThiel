using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VanThiel.Core.Services.Base;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.WebApi.Base;

public abstract class BaseVanThielController<TEntity, TService> : ControllerBase
    where TEntity : BaseEntity
    where TService : IBaseVanThielService<TEntity>
{
    #region [ Fields ]
    protected readonly TService _service;
    protected readonly ILogger<BaseVanThielController<TEntity, TService>> _logger;
    #endregion

    #region [ Ctor ]
    protected BaseVanThielController(TService service, ILogger<BaseVanThielController<TEntity, TService>> logger)
    {
        this._service = service;
        this._logger = logger;
    }
    #endregion
}
