using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_EMAIL", value = 1)]
    public class EmailNotifier : IAlertNotifier
    {
        public void SendNotification(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            object instanceObject = TypewiseAlert.MetaDataUtilisation.CreateInstanceFromInterfaceAndAttribute(typeof(IBreachEmailNotifier),
                breachType.ToString());
            if (instanceObject != null)
            {
                ((IBreachEmailNotifier)instanceObject).BreachNotifier(recepient);
            }
        }
    }
}
