using System;
using System.ComponentModel;

namespace Corazon.Common
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var info = value.GetType().GetField(value.ToString());
            var attribs = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attribs.Length > 0)
            {
                return attribs[0].Description;
            }

            return value.ToString();
        }
    }
}
