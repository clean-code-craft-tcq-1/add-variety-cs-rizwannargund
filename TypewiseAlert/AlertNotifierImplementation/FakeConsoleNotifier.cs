using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKE_CONSOLE", value = 3)]
    public class FakeConsoleNotifier : IAlertRaiser
    {
        public bool isSendNotificationMethodCalledAtleastOnce = false;
        public void SendNotification(BreachType breachType)
        {
            isSendNotificationMethodCalledAtleastOnce = true;
            Console.WriteLine("Temperature is {0}", breachType.ToString().ToLower());
        }
    }
}
