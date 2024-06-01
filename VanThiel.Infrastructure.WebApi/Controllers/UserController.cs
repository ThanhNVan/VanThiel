using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Principal;
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
    public async ValueTask<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var data = await this._service.GetManyAllUsersAsync(cancellationToken);

        return this.GetOkResult(data);
    }

    [Authorize(Roles = "User")]
    [HttpGet("my-profile")]
    public async ValueTask<IActionResult> GetSingle_MyProfileAsync(CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var data = await this._service.GetSingle_MyProfileAsync(identity, cancellationToken);

        return this.GetOkResult(data);
    }

    [HttpGet("existed-email/newEmail={email}&currentEmail={currentEmail}")]
    public async ValueTask<IActionResult> GetSingle_IsExistEmailAsync(string email, string currentEmail, CancellationToken cancellationToken = default)
    {
        var data = await this._service.GetSingle_IsExistEmailAsync(email, currentEmail, cancellationToken);

        return this.GetOkResult(data.ToString());
    }
    
    [HttpGet("existed-phone/newPhone={phone}&&currentPhone={currentPhone}")]
    public async ValueTask<IActionResult> GetSingle_IsExistPhoneNumberAsync(string phone, string currentPhone, CancellationToken cancellationToken = default)
    {
        var data = await this._service.GetSingle_IsExistPhoneNumberAsync(phone, currentPhone,cancellationToken);

        return this.GetOkResult(data.ToString());
    }
    #endregion

    #region [ Public Method - Post ]
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
