using System;

namespace Corazon.Time.TimeProviders
{
    public class DefaultLocalDateTimeProvider : DateTimeProvider
    {
        public static readonly DefaultLocalDateTimeProvider Instance = new DefaultLocalDateTimeProvider();

        public override DateTime Now => DateTime.Now;
    }
}