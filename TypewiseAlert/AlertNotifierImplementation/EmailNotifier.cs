using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_EMAIL", value = 1)]
    public class EmailNotifier : IAlertRaiser
    {
        public void SendNotification(BreachType breachType)
        {
            string recepient = "a.b@c.com";
            
            object instanceObject = MetaDataUtility.CreateInstanceWithInterfaceAndAttribute("TypewiseAlert", "TypewiseAlert", typeof(IBreachEmailNotifier), Enum.GetName(typeof(BreachType), breachType));
            if (instanceObject != null)
            {
                ((IBreachEmailNotifier)instanceObject).BreachNotifier(recepient);
            }
        }
    }
}
