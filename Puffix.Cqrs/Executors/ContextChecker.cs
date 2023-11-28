namespace Puffix.Cqrs.Executors;

/// <summary>
/// Context checker.
/// </summary>
public class ContextChecker : Checker
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="result">Result execution.</param>
    public ContextChecker(IWrittableResult result)
        : base(result)
    { }

    /// <summary>
    /// Set whether the element is valid or not.
    /// </summary>
    /// <param name="result">Result.</param>
    /// <param name="isValid">Indicates whether the element is valid or not.</param>
    protected override void SetValid(IWrittableResult result, bool isValid)
    {
        result.SetVadidContext(isValid);
    }
}
