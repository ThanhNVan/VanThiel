using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Core.Services;
using VanThiel.Domain.DTOs.RequestModel;
using VanThiel.Domain.Entities;
using VanThiel.Infrastructure.WebApi.Base;

namespace VanThiel.Infrastructure.WebApi;

[ApiController]
[Route("api/v1/cart")]
public class CartController : BaseVanThielController<Cart, ICartService>
{
    #region [ Ctor ]
    public CartController(ICartService service, ILogger<CartController> logger) : base(service, logger)
    {
    }
    #endregion

    #region [ Public Method - Get ]
    [Authorize(Roles = "User")]
    [HttpGet("my-cart")]
    public async ValueTask<IActionResult> GetMany_ByUserAsync(CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var result = await this._service.GetMany_ByUserAsync(identity, cancellationToken);

        return this.ReturnOkResult(result);
    }
    #endregion

    #region [ Public Method - Post ]
    [Authorize(Roles = "User")]
    [HttpPost("add-single-to-cart")]
    public async ValueTask<IActionResult> Update_AddSingleToCartAsync([FromBody] string productId, CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var result = await this._service.Update_AddSingleToCartAsync(identity, productId, cancellationToken);

        return this.ReturnOkResult(result.ToString());  
    }
    
    [Authorize(Roles = "User")]
    [HttpPost("subtract-from-cart")]
    public async ValueTask<IActionResult> Update_SubtractFromCartAsync([FromBody] string productId, CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var result = await this._service.Update_SubtractFromCartAsync(identity, productId, cancellationToken);

        return this.ReturnOkResult(result.ToString());  
    }
    
    [Authorize(Roles = "User")]
    [HttpPost("add-many-to-cart")]
    public async ValueTask<IActionResult> Update_AddManyToCartAsync([FromBody] UpdateCart model, CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var result = await this._service.Update_AddManyToCartAsync(identity, model, cancellationToken);

        return this.ReturnOkResult(result.ToString());  
    }
    #endregion

    #region [ Public Method - Put ]
    #endregion

    #region [ Public Method - Delete ]
    [Authorize(Roles = "User")]
    [HttpDelete("{productId}")]
    public async ValueTask<IActionResult> Update_RemoveFromCartAsync(string productId, CancellationToken cancellationToken = default)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var result = await this._service.Update_RemoveFromCartAsync(identity, productId, cancellationToken);

        return this.ReturnOkResult(result.ToString());
    }
    #endregion
}
