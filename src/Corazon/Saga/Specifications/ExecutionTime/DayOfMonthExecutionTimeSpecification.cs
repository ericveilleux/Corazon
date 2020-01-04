using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    internal class DayOfMonthExecutionTimeSpecification : IExecutionTimeSpecification
    {
        private readonly int _dayOfMonth;

        private readonly int _hourOfDay;

        private readonly int _minuteOfHour;

        public DayOfMonthExecutionTimeSpecification(int dayOfMonth, int hourOfDay, int minuteOfHour)
        {
            this._dayOfMonth = dayOfMonth;
            this._hourOfDay = hourOfDay;
            this._minuteOfHour = minuteOfHour;
        }

        public LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime)
        {
            var currentMonthExecutionTime = new LocalDateTime(lastExecutionTime.Year, lastExecutionTime.Month, this._dayOfMonth, this._hourOfDay, this._minuteOfHour);
            if (lastExecutionTime < currentMonthExecutionTime)
            {
                return currentMonthExecutionTime;
            }

            return currentMonthExecutionTime.PlusMonths(1);
        }
    }
}