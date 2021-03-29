using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("HI_ACTIVE_COOLING", value = 1)]
    public class HighActiveCooling : CoolingLimits, ICoolingLimits
    {
        public int[] GetLimits()
        {
            return new int[] { Lower = 0, Higher = 45 };
        }
    }
}
