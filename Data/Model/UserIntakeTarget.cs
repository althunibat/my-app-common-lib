using System.Collections.Generic;
using Godwit.Common.Data.Core.Model;
using NodaTime;

namespace Godwit.Common.Data.Model {
    public class UserIntakeTarget : Entity<long> {
        public UserIntakeTarget() {
            Intakes = new HashSet<Intake>();
            DailyLogs = new HashSet<DailyLog>();
        }

        public string UserId { get; set; }
        public virtual User User { get; set; }
        public float Height { get; set; }
        public float FatPercentage { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public DeficitGoal Goal { get; set; }
        public float DeficitPercentage { get; set; }
        public float NetCarbsPercentage { get; set; }
        public float ProteinTaken { get; set; } // value >0.6 && value <=1.2 per gram weight.
        public float? TotalCaloriesIntake { get; set; }
        public float? TotalFatIntake { get; set; }
        public float? TotalCarbsIntake { get; set; }
        public float? TotalProteinIntake { get; set; }
        public Instant CreatedOn { get; set; }
        public Instant? UpdatedOn { get; set; }
        public virtual ICollection<Intake> Intakes { get; set; }
        public virtual ICollection<DailyLog> DailyLogs { get; set; }
    }
}