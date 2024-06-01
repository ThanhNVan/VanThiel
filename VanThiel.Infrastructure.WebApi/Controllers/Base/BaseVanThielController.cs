using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
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

    #region [ Protected Methods ]
    protected IActionResult GetOkResult<T>(T data)
        where T : class
    {
        var result = new ApiResult<T>();

        result.Data = data;
        result.StatusCode = nameof(StatusCodes.Status200OK);
        result.Message = "Ok";

        return Ok(result);
    }
    #endregion
}
