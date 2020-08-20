// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Godwit.Common.Data.Core.Model {
    public class PaginatedList<TEntity> : IEnumerable<TEntity>
        where TEntity : class {
        private readonly List<TEntity> _list;

        public PaginatedList(IEnumerable<TEntity> items, long totalCount, int pageNumber,
            int pageSize) {
            Ensure.That(items, x => x != null, nameof(items), "List of items can't be null");
            if (pageNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber),
                    string.Format(CultureInfo.InvariantCulture,
                        "{0} value is out of its valid range.",
                        pageNumber));

            if (totalCount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalCount),
                    string.Format(CultureInfo.InvariantCulture,
                        "{0} value is out of its valid range.",
                        totalCount));

            if (pageSize < 10 || pageSize > 100)
                throw new ArgumentOutOfRangeException(nameof(pageSize),
                    string.Format(CultureInfo.InvariantCulture,
                        "{0} value is out of its valid range.",
                        pageSize));

            _list = new List<TEntity>(items);
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            TotalCount = totalCount;
        }

        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public long TotalCount { get; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
        public IReadOnlyCollection<TEntity> Items => _list;

        /// <inheritdoc />
        public IEnumerator<TEntity>
            GetEnumerator() {
            return _list.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}