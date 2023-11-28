using System;
using System.Collections.Generic;

namespace Puffix.Cqrs.Executors;

/// <summary>
/// Execution result.
/// </summary>
public class ExecutionResult : IWrittableResult
{
    /// <summary>
    /// Indicates whether the command succeed.
    /// </summary>
    public bool Success { get; private set; } = true;

    /// <summary>
    /// Indicates whether the context is valid.
    /// </summary>
    public bool ValidContext { get; private set; } = true;

    /// <summary>
    /// Indicates whether the parameters are valid.
    /// </summary>
    public bool ValidParameters { get; private set; } = true;

    /// <summary>
    /// Command error collection.
    /// </summary>
    public IEnumerable<Exception> Errors { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ExecutionResult()
    {
        Errors = new List<Exception>();
    }

    /// <summary>
    /// Set whether the command succeeds.
    /// </summary>
    /// <param name="success">Indicates whether the command succeeds.</param>
    public void SetSucces(bool success)
    {
        Success = success;
    }

    /// <summary>
    /// Set whether the context is valid.
    /// </summary>
    /// <param name="vadidContext">Indicates whether the context is valid.</param>
    public void SetVadidContext(bool vadidContext)
    {
        if (!vadidContext)
            Success = false;

        ValidContext = vadidContext;
    }

    /// <summary>
    /// Set whether the parameters are valid.
    /// </summary>
    /// <param name="validParameters">Indicates whether the parameters are valid.</param>
    public void SetValidParameters(bool validParameters)
    {
        if (!validParameters)
            Success = false;

        ValidParameters = validParameters;
    }

    /// <summary>
    /// Add error in the command execution context.
    /// </summary>
    /// <param name="error">Error.</param>
    public void AddError(Exception error)
    {
        ((List<Exception>)Errors).Add(error);
    }
}

/// <summary>
/// Execution command result.
/// </summary>
/// <typeparam name="ResultT">Command result type.</typeparam>
public class ExecutionResult<ResultT> : ExecutionResult, IWrittableResult<ResultT>
{
    /// <summary>
    /// Command result.
    /// </summary>
    public ResultT Result { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ExecutionResult()
        : base()
    { }

    /// <summary>
    /// Specify the command result.
    /// </summary>
    /// <param name="Result">Command result.</param>
    public void SetResult(ResultT result)
    {
        Result = result;
    }
}
