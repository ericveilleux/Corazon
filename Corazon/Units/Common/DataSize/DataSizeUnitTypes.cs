using System.ComponentModel;

namespace Corazon.Units.Common.DataSize
{
    internal enum DataSizeUnitTypes
    {
        [Description("B")]
        Byte = 0,

        [Description("kB")]
        Kilobyte = 1,

        [Description("MB")]
        Megabyte = 2,

        [Description("GB")]
        Gigabyte = 3,

        [Description("TB")]
        Terabyte = 4,

        [Description("PB")]
        Petabyte = 5,

        [Description("EB")]
        Exabyte = 6,

        [Description("ZB")]
        Zettabyte = 7,

        [Description("YB")]
        Yottabyte = 8
    }
}
