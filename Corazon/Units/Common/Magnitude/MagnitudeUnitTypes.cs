using System.ComponentModel;

namespace Corazon.Units.Common.Magnitude
{
    public enum MagnitudeUnitTypes
    {
        [Description("unit")]
        Unit = 0,

        [Description("decade")]
        Decade = 1,

        [Description("hundred")]
        Hundred = 2,

        [Description("thousand")]
        Thousand = 3,

        [Description("ten thousand")]
        TenThousand = 4,

        [Description("hundred thousand")]
        HundredThousand = 5,

        [Description("million")]
        Million = 6
    }
}
