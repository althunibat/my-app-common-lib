using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class IdentityUserRoleEntityConfig : IEntityTypeConfiguration<IdentityUserRole<string>> {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) {
            builder.HasKey(l => new {l.UserId, l.RoleId});
            builder.Property(x => x.RoleId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}