using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    internal class MinuteOfHourExecutionTimeSpecification : IExecutionTimeSpecification
    {
        private int _minuteOfHour;

        public MinuteOfHourExecutionTimeSpecification(int minuteOfHour)
        {
            this._minuteOfHour = minuteOfHour;
        }

        public LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime)
        {
            var currentHourExecutionTime = new LocalDateTime(lastExecutionTime.Year, lastExecutionTime.Month, lastExecutionTime.Day, lastExecutionTime.Hour, this._minuteOfHour);
            if (lastExecutionTime < currentHourExecutionTime)
            {
                return currentHourExecutionTime;
            }

            return currentHourExecutionTime.PlusHours(1);
        }
    }
}