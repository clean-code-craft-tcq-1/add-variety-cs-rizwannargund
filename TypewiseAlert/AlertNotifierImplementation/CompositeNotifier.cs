using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    public class CompositeNotifier : IAlertNotifier
    {
        List<IAlertNotifier> _alertNotifiers = new List<IAlertNotifier>();

        public void Add(IAlertNotifier alertNotifier)
        {
            _alertNotifiers.Add(alertNotifier);
        }

        public void SendNotification(BreachType breachType)
        {
            foreach (IAlertNotifier alertNotifier in _alertNotifiers)
            {
                alertNotifier.SendNotification(breachType);
            }
        }
    }
}
