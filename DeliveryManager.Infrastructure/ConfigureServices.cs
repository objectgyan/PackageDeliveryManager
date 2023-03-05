
using DeliveryManager.Core.Interfaces;
using DeliveryManager.Core.Model;
using DeliveryManager.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("deliverymanagerdb"));
        services.AddScoped<IGenericRepository<Package>, GenericRepository<Package>>();
        services.AddScoped<IGenericRepository<Recipient>, GenericRepository<Recipient>>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}


