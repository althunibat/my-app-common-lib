using System.Collections.Generic;
using Godwit.Common.Data.Core.Model;
using NodaTime;

namespace Godwit.Common.Data.Model {
    public class Ingredient : Entity<long> {
        public Ingredient() {
            Intakes = new HashSet<Intake>();
        }

        public string Name { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public float ServingValue { get; set; }
        public short CaloriesPerServing { get; set; }
        public short CarbsPerServing { get; set; }
        public short FatPerServing { get; set; }
        public short ProteinPerServing { get; set; }
        public Instant CreatedOn { get; set; }
        public Instant? UpdatedOn { get; set; }
        public virtual ICollection<Intake> Intakes { get; set; }
    }
}