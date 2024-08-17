using Market.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Auth.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }   
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
}
