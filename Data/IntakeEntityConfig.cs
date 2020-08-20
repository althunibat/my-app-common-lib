using Godwit.Common.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class IntakeEntityConfig : IEntityTypeConfiguration<Intake> {
        public void Configure(EntityTypeBuilder<Intake> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseHiLo();
            builder.Property(x => x.SizeTaken);
            builder.Property(x => x.Calories);
            builder.Property(x => x.Carbs);
            builder.Property(x => x.Fat);
            builder.Property(x => x.Protein);
            builder.Property(x => x.Time);
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);

            builder.HasOne(x => x.UserIntakeTarget).WithMany(y => y.Intakes).HasForeignKey(x => x.TargetId)
                .IsRequired();
            builder.HasOne(x => x.Ingredient).WithMany(y => y.Intakes).HasForeignKey(x => x.IngredientId).IsRequired();
        }
    }
}