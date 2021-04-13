using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKE_CONTROLLER", value = 3)]
    public class FakeControllerNotifier : IAlertRaiser
    {
        public bool isSendNotificationMethodCalledAtleastOnce = false;
        public void SendNotification(BreachType breachType)
        {
            isSendNotificationMethodCalledAtleastOnce = true;
            Console.WriteLine("Temperature is {0}", breachType.ToString().ToLower());
        }
    }
}
