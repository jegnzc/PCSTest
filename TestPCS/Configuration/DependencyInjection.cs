using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.DataAccess.MongoDB;

namespace TestPCS.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDBSettings = configuration.GetSection("MongoDB");
        services.AddSingleton(sp => new MongoDbContext(mongoDBSettings["ConnectionString"], mongoDBSettings["DatabaseName"]));

        services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
        return services;
    }
}