using Godwit.Common.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class NotificationEntityConfig : IEntityTypeConfiguration<Notification> {
        public void Configure(EntityTypeBuilder<Notification> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseHiLo();
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.ReadOn);
            builder.HasOne(x => x.User).WithMany(y => y.Notifications).HasForeignKey(x => x.UserId).IsRequired();
        }
    }
}