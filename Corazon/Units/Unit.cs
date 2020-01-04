using Corazon.Common;
using System;

namespace Corazon.Units
{
    /// <summary>
    /// Represents a "unit" base type.  It is an attribute that is associated with a value that can take different values on a linear scale.
    /// </summary>
    public class Unit : StandardType
    {
        /// <summary>
        /// This is the base multiplier that represents the value of the unit.  1 means the base unit.
        /// </summary>
        public double BaseMultiplier { get; protected set; }

        /// <summary>
        /// This is the multiplier that allows setting up a non unitary unit
        /// </summary>
        public double UnitMultiplier { get; set; }

        public double ValueMultiplier { get => this.BaseMultiplier * this.UnitMultiplier; }

        public string UnitName { get => this.UnitMultiplier != 1.0d ? $"{this.UnitMultiplier} {this.DisplayName}" : this.DisplayName; }

        protected Unit() 
        { 
        }
        
        protected Unit(int value, string displayName) 
            : base(value, displayName) 
        {
        }

        protected internal Unit(int value, string displayName, double baseMultiplier, double unitMultiplier)
            : base(value, displayName)
        {
            if (unitMultiplier < double.Epsilon || unitMultiplier < 0d)
            {
                throw new ArgumentException("Cannot have a unit multiplier of 0");
            }
            this.BaseMultiplier = baseMultiplier;
            this.UnitMultiplier = unitMultiplier;
        }
    }
}
