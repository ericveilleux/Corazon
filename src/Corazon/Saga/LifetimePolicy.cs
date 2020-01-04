namespace Corazon.Saga
{
    /// <summary>
    /// This policy defines how long should the saga live, and if any retries are attempted before it is considered timed out
    /// </summary>
    public enum LifetimePolicy
    {
        // The saga has no retry policy
        Once,
        // The saga should only live for a short period of time, and retry often
        Short,
        // The saga should live for moderate amount of time, and retry often
        Medium,
        // The saga should live for a long period of time, and retry once in a while
        Long,
        // The saga could live forever, so it never times out and retries once in a while
        Eternal
    }
}