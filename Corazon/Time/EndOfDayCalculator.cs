using System;
using System.Collections.Generic;
using NodaTime;

namespace Corazon.Time
{
    /// <summary>
    /// The goal of this service is to determine when the end-of-day occurs in each of the time zones supported.
    /// </summary>
    public class EndOfDayCalculator
    {
        private readonly IDateTimeZoneProvider _timeZoneProvider;
        
        public EndOfDayCalculator(IDateTimeZoneProvider timeZoneProvider)
        {
            this._timeZoneProvider = timeZoneProvider;
        }

        public IEnumerable<string> GetTimeZonesWhereDayEndedDuringInterval(Interval interval)
        {
            if (interval.Duration > Duration.FromHours(24))
            {
                throw new Exception("Cannot handle more than one day of end-of-day interval since it represents several end-of-days at once...");
            }

            var timeZoneIds = this._timeZoneProvider.Ids;

            foreach (var timeZoneId in timeZoneIds)
            {
                var timeZone = this._timeZoneProvider.GetZoneOrNull(timeZoneId);

                var referenceInterval = GetDayIntervalForZone(interval.Start, timeZone);

                var inZoneTime = interval.End.InZone(timeZone);

                if (!referenceInterval.Contains(inZoneTime.ToInstant()))
                {
                    yield return timeZoneId;
                }
            }
        }

        private static Interval GetDayIntervalForZone(Instant time, DateTimeZone timeZone)
        {
            var day = time.InZone(timeZone).Date;
            var dayStart = timeZone.AtStartOfDay(day);
            var dayEnd = timeZone.AtStartOfDay(day.PlusDays(1));
            return new Interval(dayStart.ToInstant(), dayEnd.ToInstant());
        }
    }
}
