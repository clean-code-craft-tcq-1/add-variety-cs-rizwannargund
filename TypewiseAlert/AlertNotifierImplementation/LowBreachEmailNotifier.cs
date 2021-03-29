using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TOO_LOW", value = 1)]
    public class LowBreachEmailNotifier : IBreachEmailNotifier
    {
        public void BreachNotifier(string recepient)
        {
            Console.WriteLine("To: {0}\n", recepient);
            Console.WriteLine("Hi, the temperature is too low\n");
        }
    }
}
