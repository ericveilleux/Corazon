using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    internal class DayOfYearExecutionTimeSpecification : IExecutionTimeSpecification
    {
        private int _monthOfYear;

        private int _dayOfMonth;

        private int _hourOfDay;

        private int _minuteOfHour;

        public DayOfYearExecutionTimeSpecification(int monthOfYear, int dayOfMonth, int hourOfDay, int minuteOfHour)
        {
            this._monthOfYear = monthOfYear;
            this._dayOfMonth = dayOfMonth;
            this._hourOfDay = hourOfDay;
            this._minuteOfHour = minuteOfHour;
        }

        public LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime)
        {
            var currentYearExecutionTime = new LocalDateTime(lastExecutionTime.Year, this._monthOfYear, this._dayOfMonth, this._hourOfDay, this._minuteOfHour);
            if (lastExecutionTime < currentYearExecutionTime)
            {
                return currentYearExecutionTime;
            }

            return currentYearExecutionTime.PlusYears(1);
        }
    }
}