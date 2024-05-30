using Microsoft.Extensions.DependencyInjection;
using VanThiel.Infrastructure.WebApi.ExceptionHandler;

namespace VanThiel.Infrastructure.WebApi;

public static class ExceptionHandlerDefinition
{
    public static void AddExceptionHandlerDefinition(this IServiceCollection services) {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }
}
