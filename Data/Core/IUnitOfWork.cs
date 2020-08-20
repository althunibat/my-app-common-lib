// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Godwit.Common.Data.Core {
    public interface IUnitOfWork {
        Task<EntityResult> SaveAsync(CancellationToken cancellationToken = default);

        Task<EntityResult> SaveAsync(DbTransaction transaction,
            CancellationToken cancellationToken = default);
    }
}