namespace Corazon.Units
{
    public class UnitConverter<TUnit>
        where TUnit : Unit
    {
        private readonly TUnit _srcUnit;
        private readonly TUnit _dstUnit;

        public UnitConverter(TUnit srcUnit, TUnit dstUnit)
        {
            this._srcUnit = srcUnit;
            this._dstUnit = dstUnit;
        }

        public double GetUnitRatio()
        {
            return this._srcUnit.ValueMultiplier / this._dstUnit.ValueMultiplier;
        }

        public static double GetUnitRatio(TUnit srcUnit, TUnit dstUnit)
        {
            return dstUnit.ValueMultiplier / srcUnit.ValueMultiplier;
        }

        public static Quantity<TUnit> ConvertQuantity(Quantity<TUnit> quantity, TUnit dstUnit)
        {
            return new Quantity<TUnit>(quantity.Value * (quantity.Unit.ValueMultiplier / dstUnit.ValueMultiplier), dstUnit);
        }
    }
}
