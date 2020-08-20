using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class IdentityUserLoginEntityConfig : IEntityTypeConfiguration<IdentityUserLogin<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> b) {
            b.HasKey(l => new {l.LoginProvider, l.ProviderKey});
            b.Property(t => t.LoginProvider);
            b.Property(t => t.LoginProvider);
            b.Property(t => t.ProviderDisplayName);
        }
    }
}