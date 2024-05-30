using Microsoft.Extensions.DependencyInjection;
using VanThiel.Core.Services;

namespace VanThiel.Application.Services;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IOrderDetailService, OrderDetailService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
    }
}
