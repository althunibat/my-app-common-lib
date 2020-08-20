// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace Godwit.Common.Data.Core {
    /// <summary>
    ///     Represents methods that can be used to ensure that parameter values meet expected conditions.
    /// </summary>
    [DebuggerStepThrough]
    public static class Ensure {
        /// <summary>Ensures that an assertion is true.</summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">The message to use with the exception that is thrown if the assertion is false.</param>
        public static void That(bool assertion, string message) {
            if (!assertion)
                throw new ArgumentException(message);
        }

        /// <summary>Ensures that an assertion is true.</summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">The message to use with the exception that is thrown if the assertion is false.</param>
        /// <param name="paramName">The parameter name.</param>
        public static void That(bool assertion, string message, string paramName) {
            if (!assertion)
                throw new ArgumentException(message, paramName);
        }

        /// <summary>
        ///     Ensures that the value of a parameter meets an assertion.
        /// </summary>
        /// <typeparam name="T">Type type of the value.</typeparam>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="assertion">The assertion.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to use with the exception that is thrown if the assertion is false.</param>
        /// <returns>The value of the parameter.</returns>
        public static T That<T>(T value, Func<T, bool> assertion, string paramName,
            string message) {
            if (!assertion(value))
                throw new ArgumentException(message, paramName);
            return value;
        }
    }
}