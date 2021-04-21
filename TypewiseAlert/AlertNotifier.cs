using System;

namespace TypewiseAlert
{
    public class AlertNotifier
    {
        IAlertNotifier _alert;
        public AlertNotifier(IAlertNotifier alert)
        {
            if (alert == null)
                throw new ArgumentNullException("IAlertRaiser cannot be null.");
            this._alert = alert;
        }

        public void SendNotification(BreachType breachType)
        {
            _alert.SendNotification(breachType);
        }
    }
}
