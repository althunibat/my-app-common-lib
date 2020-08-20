using Godwit.Common.Data.Core.Model;
using NodaTime;

namespace Godwit.Common.Data.Model {
    public class Intake : Entity<long> {
        public long TargetId { get; set; }
        public virtual UserIntakeTarget UserIntakeTarget { get; set; }
        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public float SizeTaken { get; set; }
        public float Calories { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }
        public float Protein { get; set; }
        public Instant Time { get; set; }
        public Instant CreatedOn { get; set; }
        public Instant? UpdatedOn { get; set; }
    }
}