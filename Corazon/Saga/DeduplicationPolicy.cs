namespace Corazon.Saga
{
    /// <summary>
    /// This policy defines how much high frequency requests that want to start the saga are combined together.  This is useful when a long-running operation can be triggered 
    /// by multiple events.  Without a deduplication policy, each event would trigger the saga.  With it, they are combined together for a period of time, therefore delaying 
    /// the start and reducing the number of occurrences, depending on the frequency that the saga needs to be run.
    /// </summary>
    public enum DeduplicationPolicy
    {
        None,
        // The saga needs to run at a low frequency compared to the events that trigger it
        Low,
        // The saga needs to run at a medium frequency compared to the events that trigger it
        Medium,
        // The saga needs to run at a high frequency compared to the events that trigger it
        High
    }
}