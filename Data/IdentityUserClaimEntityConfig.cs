using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class IdentityUserClaimEntityConfig : IEntityTypeConfiguration<IdentityUserClaim<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> b) {
            b.HasKey(uc => uc.Id);
            b.Property(x => x.Id).UseHiLo();
            b.Property(x => x.ClaimType).IsRequired();
            b.Property(x => x.ClaimValue);
        }
    }
}