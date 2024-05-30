using VanThiel.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VanThiel.Application.Repositories;
using VanThiel.Domain.Authentication;

namespace VanThiel.Infrastructure.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add BE services
        builder.Services.AddSqlServerProviders(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddAuthenticationPolicies(builder.Configuration);
        builder.Services.AddExceptionHandlerDefinition();
        builder.Services.AddCors();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerDefinition();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(builder => builder
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseExceptionHandler();

        app.Run();
    }
}
