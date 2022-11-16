namespace Puffix.Cqrs.Context
{
    /// <summary>
    /// Application context contract.
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// Current user.
        /// </summary>
        IApplicationUser CurrentUser { get; }
    }
}
