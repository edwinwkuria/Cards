using Cards.Infrastructure.Context;
using Cards.Infrastructure.Repository;
using Cards.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.ServiceCollectionExtensions;

public static class DatabaseExtension
{
    public static IServiceCollection ConfigureApplicationDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var db = builder.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DatabaseContext>
            (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IUnitOfWork), u =>
        {
            var context = u.GetService<DatabaseContext>();
            return new UnitOfWork(context);
        });
        return services;
    }
}