using System;
using NodaTime;

namespace Corazon.Time
{
    public static class NodaDateTimeExtensions
    {
        public static LocalDate ToLocalDate(this DateTime dtm)
        {
            return LocalDate.FromDateTime(dtm);
        }

        public static LocalDateTime ToLocalDateTime(this DateTime dtm)
        {
            return LocalDateTime.FromDateTime(dtm);
        }
    }
}