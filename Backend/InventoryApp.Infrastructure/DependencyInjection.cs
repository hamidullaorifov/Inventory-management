using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
