using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Store.Configurations;
internal class IdentityConfiguration : 
    IEntityTypeConfiguration<AppRole>,
    IEntityTypeConfiguration<AppRoleClaim>,
    IEntityTypeConfiguration<AppUser>,
    IEntityTypeConfiguration<AppUserClaim>,
    IEntityTypeConfiguration<AppUserLogin>,
    IEntityTypeConfiguration<AppUserRole>,
    IEntityTypeConfiguration<AppUserToken>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(Constants.Tables.Roles);

        builder.Property(x => x.Name).HasMaxLength(Constants.TextMedium);
        builder.Property(x => x.NormalizedName).HasMaxLength(Constants.TextMedium);

        builder.HasIndex(x => x.NormalizedName).IsUnique().HasFilter("[NormalizedName] IS NOT NULL");
    }

    public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
    {
        builder.ToTable(Constants.Tables.RoleClaims);

        builder.HasOne(x => x.Role).WithMany(x => x.Claims).OnDelete(DeleteBehavior.Cascade);

        //builder.HasIndex(x => x.RoleId);
    }

    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(Constants.Tables.Users);

        builder.Property(x => x.UserName).HasMaxLength(Constants.TextMedium);
        builder.Property(x => x.NormalizedUserName).HasMaxLength(Constants.TextMedium);
        builder.Property(x => x.Email).HasMaxLength(Constants.TextMedium);
        builder.Property(x => x.NormalizedEmail).HasMaxLength(Constants.TextMedium);

        builder.HasIndex(x => x.NormalizedEmail);
        builder.HasIndex(x => x.NormalizedUserName).IsUnique().HasFilter("[NormalizedUserName] IS NOT NULL");
    }

    public void Configure(EntityTypeBuilder<AppUserClaim> builder)
    {
        builder.ToTable(Constants.Tables.UserClaims);

        builder.HasOne(x => x.User).WithMany(x => x.Claims).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
    }

    public void Configure(EntityTypeBuilder<AppUserLogin> builder)
    {
        builder.ToTable(Constants.Tables.UserLogins);

        builder.Property(x => x.LoginProvider).HasMaxLength(Constants.TextShort);
        builder.Property(x => x.ProviderKey).HasMaxLength(Constants.TextShort);

        builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });

        builder.HasOne(x => x.User).WithMany(x => x.Logins).OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
    }

    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.ToTable(Constants.Tables.UserRoles);

        builder.HasKey(x => new {x.UserId, x.RoleId});

        builder.HasOne(x => x.User).WithMany(x => x.Roles).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Role).WithMany(x => x.Users).OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(x => x.RoleId);
    }

    public void Configure(EntityTypeBuilder<AppUserToken> builder)
    {
        builder.ToTable(Constants.Tables.UserTokens);

        builder.Property(x => x.LoginProvider).HasMaxLength(Constants.TextShort);
        builder.Property(x => x.Name).HasMaxLength(Constants.TextShort);

        builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

        builder.HasOne(x => x.User).WithMany(x => x.Tokens).OnDelete(DeleteBehavior.Cascade);
    }
}
