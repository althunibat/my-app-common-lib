using Godwit.Common.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class UserEntityConfig : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedEmail);
            builder.HasIndex(x => x.NormalizedUserName).IsUnique();
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.NormalizedEmail).IsRequired().HasMaxLength(256);
            builder.Property(x => x.NormalizedUserName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Gender);
            builder.Property(x => x.BirthDate);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.EmailConfirmed);
            builder.Property(x => x.LockoutEnabled);
            builder.Property(x => x.LockoutEnd);
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(x => x.SecurityStamp).IsRequired();
            builder.Property(x => x.AccessFailedCount);
            builder.Property(x => x.PhoneNumberConfirmed);
            builder.Property(x => x.TwoFactorEnabled);
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);

            builder.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            builder.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            builder.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            builder.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}