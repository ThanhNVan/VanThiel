using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using VanThiel.Core.Services;
using VanThiel.Domain.DTOs.RequestModel;
using Microsoft.AspNetCore.Http;
using VanThiel.SharedLibrary.Entity;
using VanThiel.Domain.DTOs;

namespace VanThiel.Infrastructure.WebApi;

[ApiController]
[Route("api/v1/authentication")]
public class AuthenticationController : ControllerBase
{
    #region [ Fields ]
    private readonly IAuthenticationService _service;
    #endregion

    #region [ Ctor ]
    public AuthenticationController(IAuthenticationService service)
    {
        this._service = service;
    }
    #endregion

    #region [ Public Method - Get ]
    #endregion

    #region [ Public Method - Post ]
    [HttpPost("user/sign-in")]
    public async ValueTask<IActionResult> UserSignInAsync([FromBody] SignInModel model, CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = await _service.UserSignInAsync(model, cancellationToken);
        return this.ReturnOkResult(result);
    }
    
    [HttpPost("user/sign-up")]
    public async ValueTask<IActionResult> UserSignUpAsync([FromBody] SignUpModel model, CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = await _service.UserSignUpAsync(model, cancellationToken);
        return this.ReturnOkResult(result);
    }
    #endregion

    #region [ Public Method - Put ]
    [HttpPut("user/update-info")]
    public async ValueTask<IActionResult> Update_UserProfileAsync([FromBody] UserMyProfile model, CancellationToken cancellationToken = default)
    {
        var result = await this._service.Update_UserProfileAsync(model, cancellationToken);
        return this.ReturnOkResult(result);
    }
    #endregion

    #region [ Public Method - Delete ]
    #endregion

    #region [ Private Methods ]
    private IActionResult ReturnOkResult<T>(T data)
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
