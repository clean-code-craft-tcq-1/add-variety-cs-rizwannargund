using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TO_CONSOLE", value = 2)]
    public class ConsoleNotifier : IAlertNotifier
    {
        public void SendNotification(BreachType breachType)
        {
            Console.WriteLine("Temperature is {0}", breachType.ToString().ToLower());
        }
    }
}
