using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace VanThiel.Domain.Authentication;

public static class AuthenticationPolicies
{
    public static void AddAuthenticationPolicies(this IServiceCollection services,
             IConfiguration configuration)
    {

        var secretKey = configuration["JwtSettings:Secret"];
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        services.AddAuthentication(
            //    options => {
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            //}
            JwtBearerDefaults.AuthenticationScheme
            ).AddJwtBearer(options =>
            {

                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = configuration["JwtSettingModels:Audience"],
                    ValidIssuer = configuration["JwtSettingModels:Issuer"],
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}
