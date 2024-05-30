using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.WebApi.Base;

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
    [Authorize(Roles = "User")]
    [HttpGet()]
    public async ValueTask<IActionResult> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var data = await this._service.GetManyAllUsersAsync(cancellationToken);

        return this.ReturnOkResult(data);
    }
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
