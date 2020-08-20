using Godwit.Common.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class UserIntakeTargetEntityConfig : IEntityTypeConfiguration<UserIntakeTarget> {
        public void Configure(EntityTypeBuilder<UserIntakeTarget> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseHiLo();
            builder.Property(x => x.Goal);
            builder.Property(x => x.Height);
            builder.Property(x => x.ActivityLevel);
            builder.Property(x => x.DeficitPercentage);
            builder.Property(x => x.FatPercentage);
            builder.Property(x => x.ProteinTaken);
            builder.Property(x => x.NetCarbsPercentage);
            builder.Property(x => x.TotalCarbsIntake);
            builder.Property(x => x.TotalProteinIntake);
            builder.Property(x => x.TotalCaloriesIntake);
            builder.Property(x => x.TotalFatIntake);
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);
            builder.HasOne(x => x.User).WithMany(y => y.Targets).HasForeignKey(x => x.UserId).IsRequired();
        }
    }
}