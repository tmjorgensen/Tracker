using Domain.Identity;
using Infrastructure.Store.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.Options;

namespace Infrastructure.Store;
public class AppDbContext : DbContext
{
    private readonly StoreConfiguration _configuration;

    public AppDbContext(
        IOptions<StoreConfiguration> configuration,
        DbContextOptions options) : base(options)
    {
        _configuration = configuration.Value;
    }

    public DbSet<AppRole> Roles { get; set; }
    public DbSet<AppRoleClaim> RoleClaims { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<AppUserClaim> UserClaims { get; set; }
    public DbSet<AppUserLogin> UserLogins { get; set; }
    public DbSet<AppUserRole> UserRoles { get; set; }
    public DbSet<AppUserToken> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        if (_configuration.Provider.ToLowerInvariant() == "sqlserver")
            builder.HasDefaultSchema(Constants.Schema);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
