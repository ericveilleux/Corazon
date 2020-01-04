using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    internal class HourOfDayExecutionTimeSpecification : IExecutionTimeSpecification
    {
        private int _hourOfDay;

        private int _minuteOfHour;

        public HourOfDayExecutionTimeSpecification(int hourOfDay, int minuteOfHour)
        {
            this._hourOfDay = hourOfDay;
            this._minuteOfHour = minuteOfHour;
        }

        public LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime)
        {
            var currentDayExecutionTime = new LocalDateTime(lastExecutionTime.Year, lastExecutionTime.Month, lastExecutionTime.Day, this._hourOfDay, this._minuteOfHour);
            if (lastExecutionTime < currentDayExecutionTime)
            {
                return currentDayExecutionTime;
            }

            return currentDayExecutionTime.PlusDays(1);
        }
    }
}