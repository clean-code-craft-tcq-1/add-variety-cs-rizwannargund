using System;

namespace TypewiseAlert
{
    public class TemperatureCooling
    {
        ICoolingLimits _coolingLimits;
        public TemperatureCooling(ICoolingLimits coolingLimits)
        {
            if (coolingLimits == null)
                throw new ArgumentNullException("ICoolingLimits cannot be null");
            this._coolingLimits = coolingLimits;
        }

        public int[] GetLimits()
        {
            return _coolingLimits.GetLimits();
        }
    }
}
