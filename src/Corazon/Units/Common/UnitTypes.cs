using System.ComponentModel;

namespace Corazon.Units.Common
{
    internal enum UnitTypes
    {
        [Description("Data Size")]
        DataSize = 0,
        [Description("Time")]
        Time = 1,
        [Description("Data Rate")]
        DataRate = 2,
        [Description("Magnitude")]
        Magnitude = 3,
        [Description("Rate")]
        Rate = 4,
    }
}
