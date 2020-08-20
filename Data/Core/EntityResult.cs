// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Godwit.Common.Data.Core {
    public class EntityResult {
        private EntityResult(params Error.Error[] errors) {
            Errors = errors;
        }

        private EntityResult() {
            Succeeded = true;
        }

        /// <summary>
        ///     Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>True if the operation succeeded, otherwise false.</value>
        public bool Succeeded { get; }

        public IEnumerable<Error.Error> Errors { get; }

        public static EntityResult Success { get; } = new EntityResult();

        public static EntityResult Failed(params Error.Error[] errors) {
            return new EntityResult(errors);
        }
    }
}