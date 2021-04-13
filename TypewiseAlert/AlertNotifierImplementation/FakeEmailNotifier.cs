using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKE_EMAIL", value = 3)]
    public class FakeEmailNotifier : IAlertRaiser
    {
        public bool isSendNotificationMethodCalledAtleastOnce = false;
        public bool isBreachNotifierMethodCalledAtleastOnce = false;
        public void SendNotification(BreachType breachType)
        {
            isSendNotificationMethodCalledAtleastOnce = true;
            switch (breachType)
            {
                case BreachType.TOO_LOW:
                    var fakeLowBreachEmailNotifier = new FakeLowBreachEmailNotifier();
                    fakeLowBreachEmailNotifier.BreachNotifier("");
                    this.isBreachNotifierMethodCalledAtleastOnce = fakeLowBreachEmailNotifier.isBreachNotifierMethodCalledAtleastOnce;
                    break;
                case BreachType.TOO_HIGH:
                    var fakeHighBreachEmailNotifier = new FakeHighBreachEmailNotifier();
                    fakeHighBreachEmailNotifier.BreachNotifier("");
                    this.isBreachNotifierMethodCalledAtleastOnce = fakeHighBreachEmailNotifier.isBreachNotifierMethodCalledAtleastOnce;
                    break;
                default:
                    Console.WriteLine("No email triggered for {0}", breachType.ToString().ToLower());
                    break;
            }
        }
    }
}
