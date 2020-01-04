using NodaTime;

namespace Corazon.Time
{
    /// <summary>
    /// Compute prorata ratio based on date and time intervals
    /// </summary>
    public class Proration : ValueObject<Proration>
    {
        private readonly int _referenceIntervalLength;

        private readonly int _targetIntervalLength;

        private readonly bool _applyProrata;

        /// <summary>
        /// Proration setting based on a date in reference to an interval.
        /// Proratio will be from this reference date to the end of the reference interval.
        /// </summary>
        public Proration(DateInterval referenceInterval, LocalDate targetDate, bool applyProrata)
            : this(referenceInterval, targetDate, referenceInterval.End, applyProrata)
        {
        }

        /// <summary>
        /// Proration setting based on an interval in reference to another interval.
        /// Proratio will be computed based on the number of days within each interval
        /// </summary>
        public Proration(DateInterval referenceInterval, DateInterval targetInterval, bool applyProrata)
            : this(referenceInterval, targetInterval.Start, targetInterval.End, applyProrata)
        {
        }

        private Proration(DateInterval referenceInterval, LocalDate targetDateFrom, LocalDate targetDateTo, bool applyProrata)
        {
            this._referenceIntervalLength = referenceInterval.Length;
            this._applyProrata = applyProrata;

            if (targetDateFrom < referenceInterval.Start)
            {
                targetDateFrom = referenceInterval.Start;
            }

            if (targetDateFrom > referenceInterval.End)
            {
                targetDateFrom = referenceInterval.End;
            }

            if (targetDateTo < referenceInterval.Start)
            {
                targetDateTo = referenceInterval.Start;
            }

            if (targetDateTo > referenceInterval.End)
            {
                targetDateTo = referenceInterval.End;
            }

            this._targetIntervalLength = new DateInterval(targetDateFrom, targetDateTo).Length;
        }

        public Ratio GetProrataRatio()
        {
            if (!this._applyProrata)
            {
                return Ratio.OneHundred;
            }

            if (this._referenceIntervalLength == 0 && this._targetIntervalLength == 0)
            {
                return Ratio.OneHundred;
            }

            if (this._referenceIntervalLength == 0)
            {
                return Ratio.Zero;
            }

            return new Ratio(this._targetIntervalLength / this._referenceIntervalLength * 100);
        }
    }
}