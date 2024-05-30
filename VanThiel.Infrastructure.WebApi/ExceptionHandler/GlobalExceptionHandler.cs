using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.WebApi.ExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var result = new ApiResult<User>();

        result.StatusCode = nameof(StatusCodes.Status500InternalServerError);
        result.Message = exception.Message;
        result.Data = null;

        await httpContext
        .Response
        .WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}
