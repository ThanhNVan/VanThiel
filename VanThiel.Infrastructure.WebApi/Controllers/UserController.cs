using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.WebApi.Base;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.WebApi;

[ApiController]
[Route("api/v1/user")]
public class UserController : BaseVanThielController<User, IUserService>
{
    #region [ Ctor ]
    public UserController(IUserService service, ILogger<UserController> logger) : base(service, logger)
    {
    }
    #endregion

    #region [ Public Method - Get ]
    [HttpGet()]
    public async ValueTask<IActionResult> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = new ApiResult<PagingResult<User>>();

       
        result.Data = await this._service.GetManyAllUsersAsync(cancellationToken);
        result.StatusCode = nameof(StatusCodes.Status200OK);
        result.Message = "Ok";

        throw new NotImplementedException("hehe");

        return Ok(result);
        
    }
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
