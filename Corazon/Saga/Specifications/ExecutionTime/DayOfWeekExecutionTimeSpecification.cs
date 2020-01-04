using NodaTime;

namespace Corazon.Saga.Specifications.ExecutionTime
{
    internal class DayOfWeekExecutionTimeSpecification : IExecutionTimeSpecification
    {
        private IsoDayOfWeek _dayOfWeek;

        private int _hourOfDay;

        private int _minuteOfHour;

        public DayOfWeekExecutionTimeSpecification(IsoDayOfWeek dayOfWeek, int hourOfDay, int minuteOfHour)
        {
            this._dayOfWeek = dayOfWeek;
            this._hourOfDay = hourOfDay;
            this._minuteOfHour = minuteOfHour;
        }

        public LocalDateTime? ComputeNextExecutionTime(LocalDateTime lastExecutionTime)
        {
            var currentWeekExecutionTime = new LocalDateTime(lastExecutionTime.Year, lastExecutionTime.Month, lastExecutionTime.Day, this._hourOfDay, this._minuteOfHour);
            if (lastExecutionTime.DayOfWeek == this._dayOfWeek &&
                lastExecutionTime < currentWeekExecutionTime)
            {
                return currentWeekExecutionTime;
            }

            return currentWeekExecutionTime.Next(this._dayOfWeek);
        }
    }
}