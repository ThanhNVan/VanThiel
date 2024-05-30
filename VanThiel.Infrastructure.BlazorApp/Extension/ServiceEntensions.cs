using Microsoft.AspNetCore.Components.Authorization;
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


        services.AddTransient<AuthenticationStateProvider, AuthenticationProvider>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<JwtSecurityTokenHandler>();

        //services.AddTransient<HttpClientContext>();
    }
    #endregion
}
