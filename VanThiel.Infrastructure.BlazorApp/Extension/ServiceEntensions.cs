using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using VanThiel.Infrastructure.Blazor.Authentication;
using VanThiel.Infrastructure.Blazor.Service.Classes;
using VanThiel.Infrastructure.Blazor.Service.Interfaces;

namespace VanThiel.Infrastructure.Blazor.Extension;

public static class ServiceEntensions
{
    #region [ Methods -  ]
    public static void AddHttpClientProviders(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("BaseClient", clients => {
            clients.BaseAddress = new Uri(configuration["BaseUrl"]);
        });

        var pagingSettings = new PagingSettings();
        configuration.GetSection("PagingSettings").Bind(pagingSettings);
        services.AddSingleton(pagingSettings);

        services.AddTransient<AuthenticationStateProvider, AuthenticationProvider>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<JwtSecurityTokenHandler>(); 
        services.AddScoped<HttpContextAccessor>();

        //services.AddTransient<HttpClientContext>();
    }
    #endregion
}
