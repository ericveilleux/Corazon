using System;

namespace Corazon.Time.TimeProviders
{
    public class DefaultUtcDateTimeProvider : DateTimeProvider
    {
        public static readonly DefaultUtcDateTimeProvider Instance = new DefaultUtcDateTimeProvider();

        public override DateTime Now => DateTime.UtcNow;
    }
}