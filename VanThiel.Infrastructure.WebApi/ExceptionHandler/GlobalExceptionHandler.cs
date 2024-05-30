using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Application.ExceptionClasses;
using VanThiel.Domain.Entities;
using VanThiel.SharedLibrary.Entity;

namespace VanThiel.Infrastructure.WebApi.ExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var result = new ApiResult<User>();

        switch (exception)
        {
            case ArgumentNullException:
                result.StatusCode = nameof(StatusCodes.Status400BadRequest);
                result.Message = exception.Message;
                break;

            case ArgumentException:
                result.StatusCode = nameof(StatusCodes.Status400BadRequest);
                result.Message = exception.Message;
                break;

            case UnauthorizedException:
                result.StatusCode = nameof(StatusCodes.Status401Unauthorized);
                result.Message = "You are allowed to process this Api, please sign in to continue.";
                break;

            case NotFoundException:
                result.StatusCode = nameof(StatusCodes.Status404NotFound);
                result.Message = "Not found";
                break;

            default:
                result.StatusCode = nameof(StatusCodes.Status500InternalServerError);
                result.Message = exception.Message;
                break;
        }
        result.Data = null;

        httpContext.Response.StatusCode = 200;

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}
