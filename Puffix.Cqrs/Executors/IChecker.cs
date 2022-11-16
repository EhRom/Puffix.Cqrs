using System;

namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Checker contract.
    /// </summary>
    public interface IChecker
    {
        /// <summary>
        /// Cehck element.
        /// </summary>
        /// <param name="expression">Check expression.</param>
        /// <param name="failCheckMessage">Error message when the check fails.</param>
        void Check(bool expression, string failCheckMessage);

        /// <summary>
        /// Cehck element.
        /// </summary>
        /// <param name="expression">Check expression.</param>
        /// <param name="failCheckErrorFunction">Function to build exception to throw  when the check fails.</param>
        void Check(bool expression, Func<Exception> failCheckErrorFunction);
    }
}
