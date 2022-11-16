using System;
using System.Collections.Generic;

namespace Puffix.Cqrs.Executors
{
    /// <summary>
    /// Execution result contract.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Indicates whether the command succeed.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Indicates whether the context is valid.
        /// </summary>
        bool ValidContext { get; }

        /// <summary>
        /// Indicates whether the parameters are valid.
        /// </summary>
        bool ValidParameters { get; }

        /// <summary>
        /// Command error collection.
        /// </summary>
        IEnumerable<Exception> Errors { get; }
    }

    /// <summary>
    /// Typed execution result contract.
    /// </summary>
    /// <typeparam name="ResultT">Result type.</typeparam>
    public interface IResult<ResultT> : IResult
    {
        /// <summary>
        /// Typed result.
        /// </summary>
        ResultT Result { get; }
    }
}
