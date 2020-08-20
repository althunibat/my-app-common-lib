// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Godwit.Common.Data.Core.Model;

namespace Godwit.Common.Data.Core.Repository {
    public interface IRepository<TEntity, TId>
        where TId : IEquatable<TId>
        where TEntity : IEntity<TId> {
        Task Add(TEntity item, CancellationToken cancellationToken = default);
        Task<bool> Edit(TEntity item, CancellationToken cancellationToken = default);
        Task<bool> Delete(TEntity item, CancellationToken cancellationToken = default);

        Task<bool> DeleteRange(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default);

        Task AddRange(IEnumerable<TEntity> items, CancellationToken cancellationToken = default);
    }
}