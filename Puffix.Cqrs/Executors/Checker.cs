using System;

namespace Puffix.Cqrs.Executors;

/// <summary>
/// Base checker.
/// </summary>
public abstract class Checker : IChecker
{
    private readonly IWrittableResult result;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="result">Result execution.</param>
    public Checker(IWrittableResult result)
    {
        this.result = result;
    }

    /// <summary>
    /// Cehck element.
    /// </summary>
    /// <param name="expression">Check expression.</param>
    /// <param name="failCheckMessage">Error message when the check fails.</param>
    public void Check(bool expression, string failCheckMessage)
    {
        Check(expression, () => new ArgumentException(failCheckMessage));
    }

    /// <summary>
    /// Cehck element.
    /// </summary>
    /// <param name="expression">Check expression.</param>
    /// <param name="failCheckErrorFunction">Function to build exception to throw  when the check fails.</param>
    public void Check(bool expression, Func<Exception> failCheckErrorFunction)
    {
        if (!expression)
        {
            SetValid(result, false);
            result.AddError(failCheckErrorFunction());
        }
    }

    /// <summary>
    /// Set whether the element is valid or not.
    /// </summary>
    /// <param name="result">Result.</param>
    /// <param name="isValid">Indicates whether the element is valid or not.</param>
    protected abstract void SetValid(IWrittableResult result, bool isValid);
}
