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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Godwit.Common.Data.Core.Repository {
    public class EfRepository<TEntity, TContext, TId> : IRepository<TEntity, TId>,
        IReadOnlyRepository<TEntity, TId>
        where TEntity : class, IEntity<TId> where TContext : DbContext where TId : IEquatable<TId> {
        protected readonly DbSet<TEntity> Entities;

        public EfRepository(TContext dbContext) {
            Entities = dbContext.Set<TEntity>();
        }


        public async Task<bool> Any(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            return await Entities.AsNoTracking().AnyAsync(filter, cancellationToken);
        }

        public Task<PaginatedList<TResult>> GetPagedItems<TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector, int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) where TResult : class {
            cancellationToken.ThrowIfCancellationRequested();
            IQueryable<TEntity> queryable = Entities;
            queryable = queryable.Where(filter);
            if (disableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (orderBy != null) queryable = orderBy(queryable);
            return queryable.CreatePaginatedListAsync<TEntity, TId, TResult>(selector, pageNumber, pageSize,
                cancellationToken);
        }

        public async Task<IEnumerable<TResult>> GetBy<TResult>(
            Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) {
            cancellationToken.ThrowIfCancellationRequested();
            IQueryable<TEntity> queryable = Entities;
            queryable = queryable.Where(filter);
            if (disableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.Where(filter).Select(selector).ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<TResult> GetFirstOrDefaultBy<TResult>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) {
            cancellationToken.ThrowIfCancellationRequested();
            IQueryable<TEntity> queryable = Entities;
            queryable = queryable.Where(filter);
            if (disableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return queryable.Where(filter).Select(selector).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            CancellationToken cancellationToken = default,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) {
            cancellationToken.ThrowIfCancellationRequested();
            IQueryable<TEntity> queryable = Entities;
            if (disableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.Select(selector).ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public Task<bool> All(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            return Entities.AsNoTracking().AllAsync(filter, cancellationToken);
        }

        public Task Add(TEntity item, CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Entities.Add(item);
            return Task.CompletedTask;
        }

        public Task<bool> Edit(TEntity item, CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Entities.Attach(item);
            Entities.Update(item);
            return Task.FromResult(true);
        }

        public Task<bool> Delete(TEntity item, CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Entities.Remove(item);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteRange(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            var items = Entities.AsNoTracking().Where(filter);
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            Entities.RemoveRange(items);
            return Task.FromResult(true);
        }

        public Task AddRange(IEnumerable<TEntity> items,
            CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            return Entities.AddRangeAsync(items, cancellationToken);
        }
    }
}