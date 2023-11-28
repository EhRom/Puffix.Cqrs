namespace Puffix.Cqrs.Executors;

/// <summary>
/// Parameters checker.
/// </summary>
public class ParametersChecker : Checker
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="result">Result execution.</param>
    public ParametersChecker(IWrittableResult result)
        : base(result)
    { }

    /// <summary>
    /// Set whether the element is valid or not.
    /// </summary>
    /// <param name="result">Result.</param>
    /// <param name="isValid">Indicates whether the element is valid or not.</param>
    protected override void SetValid(IWrittableResult result, bool isValid)
    {
        result.SetValidParameters(isValid);
    }
}
