using Market.Auth.Application.Auth;
using Market.Auth.Application.Services.AuthenticationService;
using Market.Auth.Application.Services.PermissionGroupService;
using Market.Auth.Application.Services.PermissionServices;
using Market.Auth.Application.Services.RoleService;
using Market.Auth.Application.Services.UserDeviceServices;
using Market.Auth.Application.Services.UserServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Market.Auth.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddServices(configuration);
        return services;
    }
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IPermissionGroupService, PermissionGroupService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserDeviceService, UserDeviceService>();

        //Bind Jwt settings from configuration
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddSingleton<JwtSecurityTokenHandler>();
        services.AddSingleton<JwtHelper>();
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthenticationService, AuthenticationService>();


    }
}
