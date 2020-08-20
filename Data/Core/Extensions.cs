// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Godwit.Common.Data.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Godwit.Common.Data.Core {
    internal static class Extensions {
        internal static async Task<PaginatedList<TResult>>
            CreatePaginatedListAsync<TEntity, TId, TResult>(this IQueryable<TEntity> source,
                Expression<Func<TEntity, TResult>>
                    selector, int pageIndex,
                int pageSize,
                CancellationToken cancellationToken =
                    default)
            where TEntity : IEntity<TId> where TId : IEquatable<TId> where TResult : class {
            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            return new
                PaginatedList<TResult>(
                    await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(selector)
                        .ToListAsync(cancellationToken).ConfigureAwait(false),
                    count, pageIndex, pageSize);
        }

        internal static PaginatedList<TResult> CreatePaginatedList<TEntity, TId, TResult>(
            this IQueryable<TEntity> source, Expression<Func<TEntity, TResult>> selector,
            int pageIndex, int pageSize) where TEntity : IEntity<TId>
            where TId : IEquatable<TId>
            where TResult : class {
            var totalCount = source.Count();
            return new
                PaginatedList<TResult>(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(selector).ToList(),
                    totalCount, pageIndex, pageSize);
        }
    }
}