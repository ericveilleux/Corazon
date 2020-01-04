using NodaTime;
using System;

namespace Corazon.Time
{
    public static class NodaDateExtensions
    {
        public static bool IsBusinessDay(this LocalDate date)
        {
            return date.DayOfWeek == IsoDayOfWeek.Monday ||
                   date.DayOfWeek == IsoDayOfWeek.Tuesday ||
                   date.DayOfWeek == IsoDayOfWeek.Wednesday ||
                   date.DayOfWeek == IsoDayOfWeek.Thursday ||
                   date.DayOfWeek == IsoDayOfWeek.Friday;
        }

        public static DateTime ToDateTime(this LocalDate date)
        {
            return date.ToDateTimeUnspecified();
        }
    }
}