using Corazon.Time.TimeProviders;
using NodaTime;
using System;

namespace Corazon.Time
{
    /// <summary>
    /// Defines a time abstraction as an Ambient Context, allowing mocking and reducing the need to inject in all classes
    /// </summary>
    /// <see cref="https://relentlessdevelopment.wordpress.com/2013/09/30/testing-datetimes-and-other-things-with-an-ambient-context/" />
    public abstract class InstantProvider
    {
        public static InstantProvider DefaultInstanceProvider { get; set; } = DefaultInstantProvider.Instance;

        [ThreadStatic]
        private static InstantProvider current;

        public static InstantProvider Current
        {
            get
            {
                return InstantProvider.current ?? DefaultInstanceProvider;
            }

            set
            {
                InstantProvider.current = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public static void ResetToDefault()
        {
            InstantProvider.current = DefaultInstantProvider.Instance;
        }

        public abstract Instant Now { get; }
    }
}