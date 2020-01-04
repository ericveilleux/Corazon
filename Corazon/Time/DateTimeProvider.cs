using Corazon.Time.TimeProviders;
using System;

namespace Corazon.Time
{
    /// <summary>
    /// Defines a time abstraction as an Ambient Context, allowing mocking and reducing the need to inject in all classes
    /// </summary>
    /// <see cref="https://relentlessdevelopment.wordpress.com/2013/09/30/testing-datetimes-and-other-things-with-an-ambient-context/" />
    public abstract class DateTimeProvider
    {
        public static DateTimeProvider DefaultInstanceProvider { get; set; } = DefaultUtcDateTimeProvider.Instance;

        [ThreadStatic]
        private static DateTimeProvider current;

        public static DateTimeProvider Current
        {
            get
            {
                return DateTimeProvider.current ?? DefaultInstanceProvider;
            }

            set
            {
                DateTimeProvider.current = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public static void ResetToDefault()
        {
            DateTimeProvider.current = null;
        }

        public abstract DateTime Now { get; }
    }
}
