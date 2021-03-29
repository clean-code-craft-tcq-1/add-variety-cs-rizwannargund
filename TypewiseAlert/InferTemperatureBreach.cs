using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    public class InferTemperatureBreach
    {
        ITemperatureBreach _temperatureBreach;
        public InferTemperatureBreach(ITemperatureBreach temperatureBreach)
        {
            if (temperatureBreach == null)
                throw new ArgumentNullException("ITemperatureBreach cannot be null");
            this._temperatureBreach = temperatureBreach;
        }
        public BreachType BreachLimit(double value)
        {
           return _temperatureBreach.BreachLimit(value);
        }
    }
}
