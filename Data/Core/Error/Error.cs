// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Godwit.Common.Data.Core.Error {
    public class Error {
        public Error(int code, string name, IDictionary<string, string> errorMessages) {
            Code = code;
            Name = name;
            ErrorMessages = errorMessages;
        }

        public int Code { get; }
        public string Name { get; }
        public IDictionary<string, string> ErrorMessages { get; }
    }
}