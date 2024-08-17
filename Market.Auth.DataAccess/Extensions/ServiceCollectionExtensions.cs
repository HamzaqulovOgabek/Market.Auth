using Market.Auth.DataAccess.Repositories.PermissionGroupRepo;
using Market.Auth.DataAccess.Repositories.PermissionRepo;
using Market.Auth.DataAccess.Repositories.RoleRepo;
using Market.Auth.DataAccess.Repositories.UserDeviceRepo;
using Market.Auth.DataAccess.Repositories.UserRepo;
using Market.Auth.DataAccess.Repositories.UserTokenRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Auth.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Market.Auth"));
        });
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserDeviceRepository, UserDeviceRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();



        return services;
    }

}
