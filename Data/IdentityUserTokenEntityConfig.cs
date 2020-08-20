using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class IdentityUserTokenEntityConfig : IEntityTypeConfiguration<IdentityUserToken<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> b) {
            b.HasKey(t => new {t.UserId, t.LoginProvider, t.Name});
            b.Property(t => t.LoginProvider);
            b.Property(t => t.Name);
            b.Property(t => t.Value);
        }
    }
}