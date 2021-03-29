using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("MED_ACTIVE_COOLING", value = 2)]
    public class MediumActiveCooling : CoolingLimits, ICoolingLimits
    {
        public int[] GetLimits()
        {
            return new int[] { Lower = 0, Higher = 40 };
        }
    }
}
