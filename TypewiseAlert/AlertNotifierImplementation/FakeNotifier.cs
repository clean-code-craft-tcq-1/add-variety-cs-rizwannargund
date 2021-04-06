using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKE", value = 3)]
    public class FakeNotifier : IAlertRaiser
    {
        public static bool isSendNotificationMethodCalledAtleastOnce = false;
        public virtual void SendNotification(BreachType breachType)
        {
            isSendNotificationMethodCalledAtleastOnce = true;
            object instanceObject = MetaDataUtility.CreateInstanceWithInterfaceAndAttribute("TypewiseAlert", "TypewiseAlert", typeof(IBreachEmailNotifier),
                "TO_FAKEEMAIL");
            if (instanceObject != null)
            {
                ((IBreachEmailNotifier)instanceObject).BreachNotifier("");
            }
        }
    }
}
