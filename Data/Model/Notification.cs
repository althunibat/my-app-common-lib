using Godwit.Common.Data.Core.Model;
using NodaTime;

namespace Godwit.Common.Data.Model {
    public class Notification : Entity<long> {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string Message { get; set; }
        public Instant CreatedOn { get; set; }
        public Instant? ReadOn { get; set; }
    }
}