// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Godwit.Common.Data.Core.Model {
    public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>> where TId : IEquatable<TId> {
        protected Entity() { }

        protected Entity(TId id) {
            Id = id;
        }

        public TId Id { get; protected set; }

        public bool Equals(Entity<TId> other) {
            return other != null && EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Entity<TId>) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right) {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right) {
            return !Equals(left, right);
        }
    }
}