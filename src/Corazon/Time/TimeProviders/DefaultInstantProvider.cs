using NodaTime;

namespace Corazon.Time.TimeProviders
{
    public class DefaultInstantProvider : InstantProvider
    {
        public static readonly DefaultInstantProvider Instance = new DefaultInstantProvider();

        public override Instant Now => SystemClock.Instance.GetCurrentInstant();
    }
}