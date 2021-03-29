using System;

namespace TypewiseAlert
{
    public class AlertNotifier
    {
        IAlertRaiser _alert;
        public AlertNotifier(IAlertRaiser alert)
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
