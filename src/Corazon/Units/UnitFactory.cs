namespace Corazon.Units
{
    internal static class UnitFactory
    {
        public static Unit Create(int value, string displayName, double baseMultiplier, double unitMultiplier)
        {
            return new Unit(value, displayName, baseMultiplier, unitMultiplier);
        }
    }
}
