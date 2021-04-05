using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKE", value = 3)]
    public class FakeNotifier : IAlertRaiser
    {
        public virtual void SendNotification(BreachType breachType)
        {
            object instanceObject = MetaDataUtility.CreateInstanceWithInterfaceAndAttribute("TypewiseAlert", "TypewiseAlert", typeof(IBreachEmailNotifier),
                "TO_FAKEEMAIL");
            if (instanceObject != null)
            {
                ((IBreachEmailNotifier)instanceObject).BreachNotifier("");
            }
        }
    }
}
