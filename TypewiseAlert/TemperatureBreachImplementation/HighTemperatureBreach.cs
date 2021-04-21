using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TOO_HIGH", value = 2)]
    public class HighTemperatureBreach : ITemperatureBreach
    {
        public double Limit { get; private set; }
        public HighTemperatureBreach(double maxLimit)
        {
            this.Limit = maxLimit;
        }
        public BreachType BreachLimit(double value)
        => (value > this.Limit) ? BreachType.TOO_HIGH : BreachType.NORMAL;
    }
}
