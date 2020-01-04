using System.ComponentModel;

namespace Corazon.Units.Common.Time
{
    internal enum TimeUnitTypes
    {
        [Description("Seconds")]
        Second = 1,

        [Description("Minutes")]
        Minute = 60,

        [Description("Hours")]
        Hour = 3600,

        [Description("Days")]
        Day = 3600 * 24,

        [Description("Weeks")]
        Week = 3600 * 24 * 7,

        [Description("Months")]
        Month = 3600 * 24 * 30
    }
}
