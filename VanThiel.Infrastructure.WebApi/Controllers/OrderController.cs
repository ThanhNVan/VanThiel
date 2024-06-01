using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Services;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.WebApi.Base;

namespace VanThiel.Infrastructure.WebApi;

[ApiController]
[Route("api/v1/order")]
public class OrderController : BaseVanThielController<Order, IOrderService>
{
    #region [ Ctor ]
    public OrderController(IOrderService service, ILogger<OrderController> logger) : base(service, logger)
    {
    }
    #endregion

    #region [ Public Method - Get ]
    [Authorize(Roles = "User")]
    [HttpPost("check-out")]
    public async ValueTask<IActionResult> Post_CheckOutAsync([FromBody] IEnumerable<string> cartIdList, CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var result = await this._service.Post_CheckOutAsync(identity, cartIdList, cancellationToken);

        return this.GetOkResult(result.ToString());
    }
    #endregion

    #region [ Public Method - Post ]
    [Authorize(Roles = "User")]
    [HttpGet("many-active")]
    public async ValueTask<IActionResult> GetMany_ActiveAsync(CancellationToken cancellationToken = default)
    {
        var result = await this._service.GetMany_ActiveAsync(cancellationToken);

        return this.GetOkResult(result);
    }

    [Authorize(Roles = "User")]
    [HttpGet("many-by-user")]
    public async ValueTask<IActionResult> GetMany_ByUserAsync(CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var result = await this._service.GetMany_ByUserAsync(identity, cancellationToken);

        return this.GetOkResult(result);
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    #endregion
}
