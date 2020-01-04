using NodaTime;

namespace Corazon.Time.TimeProviders
{
    public class ClockBasedInstantProvider : InstantProvider
    {
        private readonly IClock _clock;

        public ClockBasedInstantProvider(IClock clock)
        {
            this._clock = clock;
        }

        public override Instant Now 
        { 
            get 
            { 
                return this._clock.GetCurrentInstant(); 
            } 
        }
    }
}