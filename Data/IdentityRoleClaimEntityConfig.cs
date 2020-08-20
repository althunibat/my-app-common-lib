using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class IdentityRoleClaimEntityConfig : IEntityTypeConfiguration<IdentityRoleClaim<string>> {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder) {
            builder.HasKey(rc => rc.Id);
            builder.Property(x => x.ClaimType).IsRequired();
            builder.Property(x => x.ClaimValue);
        }
    }
}