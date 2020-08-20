using System;
using System.Collections.Generic;
using Godwit.Common.Data.Core.Model;
using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace Godwit.Common.Data.Model {
    public class User : IdentityUser, IEntity<string>, IEquatable<User> {
        public User() {
            Targets = new HashSet<UserIntakeTarget>();
            Notifications = new HashSet<Notification>();
        }

        public User(string username) : base(username) {
            Targets = new HashSet<UserIntakeTarget>();
            Notifications = new HashSet<Notification>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LocalDate BirthDate { get; set; }
        public Gender Gender { get; set; }
        public ICollection<UserIntakeTarget> Targets { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public Instant CreatedOn { get; set; }
        public Instant? UpdatedOn { get; set; }

        public bool Equals(User other) {
            return other != null && EqualityComparer<string>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((User) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, nameof(User));
        }

        public static bool operator ==(User left, User right) {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right) {
            return !Equals(left, right);
        }
    }
}