using Cards.Services;
using Cards.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.ServiceCollectionExtensions;

public static class ServiceInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<ApiBehaviorOptions>(options
            => options.SuppressModelStateInvalidFilter = true);
        
        //Locally injected services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IJwtHelper, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}