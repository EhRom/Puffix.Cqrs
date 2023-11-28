using System;

namespace Puffix.Cqrs.Executors;

/// <summary>
/// Writtable execution result contract.
/// </summary>
public interface IWrittableResult : IResult
{
    /// <summary>
    /// Set whether the command succeeds.
    /// </summary>
    /// <param name="success">Indicates whether the command succeeds.</param>
    void SetSucces(bool success);

    /// <summary>
    /// Set whether the context is valid.
    /// </summary>
    /// <param name="vadidContext">Indicates whether the context is valid.</param>
    void SetVadidContext(bool vadidContext);

    /// <summary>
    /// Set whether the parameters are valid.
    /// </summary>
    /// <param name="validParameters">Indicates whether the parameters are valid.</param>
    void SetValidParameters(bool validParameters);

    /// <summary>
    /// Add error in the command execution context.
    /// </summary>
    /// <param name="error">Error.</param>
    void AddError(Exception error);
}

/// <summary>
/// Writtable execution result contract.
/// </summary>
/// <typeparam name="ResultT">Result type.</typeparam>
public interface IWrittableResult<ResultT> : IWrittableResult, IResult<ResultT>
{
    /// <summary>
    /// Set the execution result.
    /// </summary>
    /// <param name="Result">Command result.</param>
    void SetResult(ResultT result);
}
