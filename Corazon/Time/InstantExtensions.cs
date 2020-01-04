using NodaTime;

namespace Corazon.Time
{
    public static class InstantExtensions
    {
        public static LocalDate ToLocalDateUtc(this Instant instant)
        {
            return instant.InUtc().Date; 
        }

        public static LocalDateTime ToLocalDateTimeUtc(this Instant instant)
        {
            return instant.InUtc().LocalDateTime;
        }
    }
}
