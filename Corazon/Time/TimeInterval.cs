using System;

using NodaTime;

namespace Corazon.Time
{
    /// <summary>
    ///     A period of 2 times, including the dates.
    ///     The interval includes the start, and excludes the end, which means that if you have abutting intervals any instant
    ///     will be in exactly one of those intervals.
    /// </summary>
    public class TimeInterval : ValueObject<TimeInterval>
    {
        public readonly LocalDateTime From;

        public readonly LocalDateTime To;

        public TimeInterval(LocalDateTime from, LocalDateTime to)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException("from", from + " needs to be lower than 'To':" + to);
            }

            this.From = from;
            this.To = to;
        }

        public TimeInterval(LocalDateTime from, Period period)
            : this(from, from.Plus(period))
        {
        }

        public bool Includes(LocalDateTime date)
        {
            return date >= this.From && date < this.To;
        }

        public bool IsAfter(LocalDateTime date)
        {
            return date >= this.To;
        }

        public bool IsBefore(LocalDateTime date)
        {
            return date < this.From;
        }

        public Period GetPeriod(PeriodUnits units)
        {
            return Period.Between(this.From, this.To, units);
        }

        public TimeInterval Previous()
        {
            var period = Period.Between(this.From, this.To);
            return new TimeInterval(this.From.Minus(period), period);
        }

        public TimeInterval Next()
        {
            return new TimeInterval(this.To, Period.Between(this.From, this.To));
        }

        public override string ToString()
        {
            return string.Format("From: {0}, To: {1}", this.From, this.To);
        }
    }
}