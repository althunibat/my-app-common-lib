using Godwit.Common.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Godwit.Common.Data {
    public class IngredientEntityConfig : IEntityTypeConfiguration<Ingredient> {
        public void Configure(EntityTypeBuilder<Ingredient> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseHiLo();

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired().HasColumnType("citext");
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.MeasurementUnit);
            builder.Property(x => x.ServingValue);
            builder.Property(x => x.CaloriesPerServing);
            builder.Property(x => x.CarbsPerServing);
            builder.Property(x => x.FatPerServing);
            builder.Property(x => x.ProteinPerServing);
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.UpdatedOn);
        }
    }
}