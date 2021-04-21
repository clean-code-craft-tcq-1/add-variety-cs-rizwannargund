using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TOO_HIGH", value = 2)]
    public class HighBreachEmailNotifier : IBreachEmailNotifier
    {
        public void BreachNotifier(string recepient)
        {
            Console.WriteLine("To: {0}\n", recepient);
            Console.WriteLine("Hi, the temperature is too high\n");
        }
    }
}
