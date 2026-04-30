using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using mss.ims.application.Common.Interfaces;
using mss.ims.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;


namespace mss.ims.infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}

/// <summary>
/// NOT BEING USED
/// </summary>
//public static class DependencyInjectionInMemory
//{
//    public static IServiceCollection AddInfrastructure(
//        this IServiceCollection services,
//        IConfiguration configuration)
//    {
//        services.AddDbContext<ApplicationDbContext>(options =>
//            options.UseInMemoryDatabase("IMSDb"));

//        services.AddScoped<IApplicationDbContext>(provider =>
//            provider.GetRequiredService<ApplicationDbContext>());

//        return services;
//    }
//}
