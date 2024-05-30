using Microsoft.AspNetCore.Mvc;
using VanThiel.Core.Services;

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
}
