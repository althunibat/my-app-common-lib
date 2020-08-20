// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

namespace Godwit.Common.Data.Core.Model {
    public interface IEntity<TId> {
        TId Id { get; }
    }
}