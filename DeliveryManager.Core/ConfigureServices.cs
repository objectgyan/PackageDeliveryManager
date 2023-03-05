using DeliveryManager.Core.Interfaces;
using DeliveryManager.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryManager.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBarCodeGenerator), typeof(BarCodeGenerator));
            return services;
        }
    }
}