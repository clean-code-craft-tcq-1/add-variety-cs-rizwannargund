using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TOO_LOW", value = 1)]
    public class LowTemperatureBreach : ITemperatureBreach
    {
        public double Limit { get; private set; }
        public LowTemperatureBreach(double lowLimit)
        {
            this.Limit = lowLimit;
        }
        public BreachType BreachLimit(double value)
        => (value < this.Limit) ? BreachType.TOO_LOW : BreachType.NORMAL;
    }
}
