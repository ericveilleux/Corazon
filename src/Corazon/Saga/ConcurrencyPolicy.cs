namespace Corazon.Saga
{
    /// <summary>
    /// This policy defines the concurrency level of a saga.  It defines how many commands are run in parallel for any given state.
    /// </summary>
    public enum ConcurrencyPolicy
    {
        // A single command can run at a time
        Single, 
        // A few commands can be published at a time
        Low, 
        // Many commands can be published at once
        High
    }
}