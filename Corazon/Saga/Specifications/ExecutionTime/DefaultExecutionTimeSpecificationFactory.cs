using System;
using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    public class DefaultExecutionTimeSpecificationFactory : IExecutionTimeSpecificationFactory
    {
        private const int DefaultMinuteOfHour = 0;
        private const int DefaultHourOfDay = 0;
        private const IsoDayOfWeek DefaultDayOfWeek = IsoDayOfWeek.Saturday;
        private const int DefaultDayOfMonth = 1;
        private const int DefaultMonthOfYear = 1;

        public IExecutionTimeSpecification CreateForPolicy(PeriodicityPolicy policy)
        {
            switch (policy.PolicyType)
            {
                case PeriodicityPolicyType.Once:
                    return new SingleShotExecutionTimeSpecification();
                case PeriodicityPolicyType.Hourly:
                    return new MinuteOfHourExecutionTimeSpecification(policy.MinuteOfHour ?? DefaultMinuteOfHour);
                case PeriodicityPolicyType.Daily:
                    return new HourOfDayExecutionTimeSpecification(policy.HourOfDay ?? DefaultHourOfDay, policy.MinuteOfHour ?? DefaultMinuteOfHour);
                case PeriodicityPolicyType.Weekly:
                    return new DayOfWeekExecutionTimeSpecification(policy.DayOfWeek ?? DefaultDayOfWeek, policy.HourOfDay ?? DefaultHourOfDay, policy.MinuteOfHour ?? DefaultMinuteOfHour);
                case PeriodicityPolicyType.Monthly:
                    return new DayOfMonthExecutionTimeSpecification(policy.DayOfMonth ?? DefaultDayOfMonth, policy.HourOfDay ?? DefaultHourOfDay, policy.MinuteOfHour ?? DefaultMinuteOfHour);
                case PeriodicityPolicyType.Annually:
                    return new DayOfYearExecutionTimeSpecification(policy.MonthOfYear ?? DefaultMonthOfYear, policy.DayOfMonth ?? DefaultDayOfMonth, policy.HourOfDay ?? DefaultHourOfDay, policy.MinuteOfHour ?? DefaultMinuteOfHour);
                default:
                    throw new NotImplementedException("Unhandled enum value");
            }
        }
    }
}