using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    public interface ITemperatureBreach
    {
        BreachType BreachLimit(double value);
    }
    public enum BreachType
    {
        NORMAL,
        TOO_LOW,
        TOO_HIGH
    };
}
