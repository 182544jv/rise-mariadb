using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rise.Persistence.Configurations.Identity;

/// <summary>
/// Configuration for the Identity tables.
/// </summary>
internal class IdentityConfiguration :
    IEntityTypeConfiguration<IdentityUser>,
    IEntityTypeConfiguration<IdentityRole>,
    IEntityTypeConfiguration<IdentityUserRole<string>>,
    IEntityTypeConfiguration<IdentityUserClaim<string>>,
    IEntityTypeConfiguration<IdentityUserLogin<string>>,
    IEntityTypeConfiguration<IdentityRoleClaim<string>>,
    IEntityTypeConfiguration<IdentityUserToken<string>>
{
    // Configures the IdentityUser tables.
    // NOTE:
    // If you want to use a separate schema for your Identity tables,
    // you can specify the schema as "auth" like this:
    //     builder.ToTable("Users", "auth");
    // However, be aware that SQLite does NOT support schemas, so this only works with SQL Server, MarioDB, PostgreSQL,...
    // The default below will work on any provider.

    // MariaDB/MySQL can index at most 3072 bytes (768 characters in utf8mb4).
    // The convention in ApplicationDbContext gives all strings a max. length of 4000,
    // which is too long for the key columns of the Identity tables.
    private const int KeyLength = 255;
    private const int ProviderKeyLength = 128;
    
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.ToTable("Users");
        builder.Property(x => x.Id).HasMaxLength(KeyLength);
    }

    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.ToTable("Roles");
        builder.Property(x => x.Id).HasMaxLength(KeyLength);
    }

    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.ToTable("UserRoles");
        builder.Property(x => x.UserId).HasMaxLength(KeyLength);
        builder.Property(x => x.RoleId).HasMaxLength(KeyLength);
    }

    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.ToTable("UserClaims");
        builder.Property(x => x.UserId).HasMaxLength(KeyLength);
    }

    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder.ToTable("UserLogins");
        builder.Property(x => x.LoginProvider).HasMaxLength(ProviderKeyLength);
        builder.Property(x => x.ProviderKey).HasMaxLength(ProviderKeyLength);
        builder.Property(x => x.UserId).HasMaxLength(KeyLength);
    }

    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.ToTable("RoleClaims");
        builder.Property(x => x.RoleId).HasMaxLength(KeyLength);
    }

    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.ToTable("UserTokens");
        builder.Property(x => x.UserId).HasMaxLength(KeyLength);
        builder.Property(x => x.LoginProvider).HasMaxLength(ProviderKeyLength);
        builder.Property(x => x.Name).HasMaxLength(ProviderKeyLength);
    }
}