using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_CONTROLLER", value = 0)]
    public class ControllerNotifier : IAlertNotifier
    {
        public void SendNotification(BreachType breachType)
        {
            const ushort header = 0xfeed;
            Console.WriteLine("{0} : {1}\n", header, breachType);
        }
    }
}
