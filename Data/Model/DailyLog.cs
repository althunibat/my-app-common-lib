using Godwit.Common.Data.Core.Model;
using NodaTime;

namespace Godwit.Common.Data.Model {
    public class DailyLog : Entity<long> {
        public long TargetId { get; set; }
        public virtual UserIntakeTarget UserIntakeTarget { get; set; }
        public short TotalCalories { get; set; }
        public short TotalCarbs { get; set; }
        public short TotalFat { get; set; }
        public short TotalProtein { get; set; }
        public Instant CreatedOn { get; set; }
        public Instant? UpdatedOn { get; set; }
    }
}