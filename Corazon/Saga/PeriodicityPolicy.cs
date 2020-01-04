using NodaTime;

namespace Corazon.Saga
{
    /// <summary>
    /// This policy defines the periodicity of the saga, i.e. how often should the saga run.  
    /// </summary>
    public enum PeriodicityPolicyType
    {
        // The saga does not repeat
        Once,
        // The saga needs to run every hour
        Hourly,
        // The saga needs to run every day
        Daily,
        // The saga needs to run every week
        Weekly,
        // The saga needs to run every month
        Monthly,
        // The saga needs to run every year
        Annually
    }

    public class PeriodicityPolicy
    {
        internal int? MinuteOfHour { get; private set; }

        internal int? HourOfDay { get; private set; }

        internal IsoDayOfWeek? DayOfWeek { get; private set; }

        internal int? DayOfMonth { get; private set; }

        internal int? MonthOfYear { get; private set; }

        public PeriodicityPolicyType PolicyType { get; private set; }

        private PeriodicityPolicy(
            PeriodicityPolicyType type, 
            int? minuteOfHour = null, 
            int? hourOfDay = null, 
            IsoDayOfWeek? dayOfWeek = null, 
            int? dayOfMonth = null, 
            int? monthOfYear = null)
        {
            this.PolicyType = type;
            this.MinuteOfHour = minuteOfHour;
            this.HourOfDay = hourOfDay;
            this.DayOfWeek = dayOfWeek;
            this.DayOfMonth = dayOfMonth;
            this.MonthOfYear = monthOfYear;
        }

        public static PeriodicityPolicy None()
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Once);
        }

        public static PeriodicityPolicy Hourly()
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Hourly);
        }

        public static PeriodicityPolicy HourlyAt(int minuteOfHour)
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Hourly, minuteOfHour: minuteOfHour);
        }

        public static PeriodicityPolicy Daily()
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Daily);
        }

        public static PeriodicityPolicy DailyAt(int hourOfDay, int minuteOfHour)
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Daily, minuteOfHour: minuteOfHour, hourOfDay: hourOfDay);
        }

        public static PeriodicityPolicy Weekly()
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Weekly);
        }

        public static PeriodicityPolicy WeeklyOn(IsoDayOfWeek dayOfWeek, int hourOfDay, int minuteOfHour)
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Weekly, minuteOfHour: minuteOfHour, hourOfDay: hourOfDay, dayOfWeek: dayOfWeek);
        }

        public static PeriodicityPolicy Monthly()
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Monthly);
        }

        public static PeriodicityPolicy MonthlyOn(int dayOfMonth, int hourOfDay, int minuteOfHour)
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Monthly, minuteOfHour: minuteOfHour, hourOfDay: hourOfDay, dayOfMonth: dayOfMonth);
        }

        public static PeriodicityPolicy Annually()
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Annually);
        }

        public static PeriodicityPolicy AnnuallyOn(int monthOfYear, int dayOfMonth, int hourOfDay, int minuteOfHour)
        {
            return new PeriodicityPolicy(PeriodicityPolicyType.Annually, minuteOfHour: minuteOfHour, hourOfDay: hourOfDay, dayOfMonth: dayOfMonth, monthOfYear: monthOfYear);
        }
    }
}