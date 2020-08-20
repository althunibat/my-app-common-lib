// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Godwit.Common.Data.Core.Model;
using Microsoft.EntityFrameworkCore.Query;

namespace Godwit.Common.Data.Core.Repository {
    public interface IReadOnlyRepository<TEntity, TId>
        where TEntity : class, IEntity<TId> where TId : IEquatable<TId> {
        Task<bool> Any(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default);

        Task<PaginatedList<TResult>> GetPagedItems<TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector, int pageIndex = 1, int pageSize = 10,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) where TResult : class;

        Task<IEnumerable<TResult>> GetBy<TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        Task<TResult> GetFirstOrDefaultBy<TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        Task<bool> All(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default);
    }
}