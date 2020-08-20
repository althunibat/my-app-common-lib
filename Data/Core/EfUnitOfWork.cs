// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Godwit.Common.Data.Core {
    public class EfUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext {
        private readonly TContext _context;
        private readonly ILogger _logger;

        public EfUnitOfWork(ILoggerFactory loggerFactory, TContext context) {
            _context = context;
            _logger = loggerFactory.CreateLogger<EfUnitOfWork<TContext>>();
        }

        public async Task<EntityResult> SaveAsync(CancellationToken cancellationToken = default) {
            try {
                _logger.LogInformation("Attempt to Save Data", Array.Empty<object>());
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation("Data has been saved!", Array.Empty<object>());
                return EntityResult.Success;
            }
            catch (DbUpdateConcurrencyException ex) {
                _logger.LogError(ex,
                    "Unable to save data changes due to concurrency check violation");
                return EntityResult.Failed();
            }
            catch (DbUpdateException ex) {
                _logger.LogError(ex, "Unable to save data changes");
                return EntityResult.Failed();
            }
        }

        public async Task<EntityResult> SaveAsync(DbTransaction transaction,
            CancellationToken cancellationToken = default) {
            if (transaction == null) {
                _logger.LogError("A transaction is required!");
                return EntityResult.Failed();
            }

            try {
                _logger.LogInformation("Attempt to Save Data", Array.Empty<object>());
                await _context.Database.UseTransactionAsync(transaction, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation("Data has been saved!", Array.Empty<object>());
                return EntityResult.Success;
            }
            catch (DbUpdateConcurrencyException ex) {
                _logger.LogError(ex,
                    "Unable to save data changes due to concurrency check violation",
                    Array.Empty<object>());
                return EntityResult.Failed(new Error.Error(1, "ConcurrencyError",
                    new Dictionary<string, string> {
                        {"msg", "Unable to save data changes due to concurrency check violation"},
                        {"exception", ex.Message}
                    }));
            }
            catch (DbUpdateException ex) {
                _logger.LogError(ex, "Unable to save data changes", Array.Empty<object>());
                return EntityResult.Failed(new Error.Error(2, "DbUpdateError",
                    new Dictionary<string, string> {
                        {"msg", "Unable to save data changes"},
                        {"exception", ex.Message}
                    }));
            }
        }
    }
}