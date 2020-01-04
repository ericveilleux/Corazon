using System.Collections.Generic;
using NodaTime;

namespace Corazon.Time
{
    public static class IntervalExtensions
    {
        public static IEnumerable<Interval> Split(this Interval interval, Duration duration)
        {
            var currentIntervalStart = interval.Start;
            var currentIntervalEnd = interval.Start.Plus(duration);
            while (currentIntervalEnd < interval.End)
            {
                yield return new Interval(currentIntervalStart, currentIntervalEnd);
                currentIntervalStart = currentIntervalEnd;
                currentIntervalEnd = currentIntervalStart.Plus(duration);
            }

            yield return new Interval(currentIntervalStart, interval.End);
        }
    }
}
