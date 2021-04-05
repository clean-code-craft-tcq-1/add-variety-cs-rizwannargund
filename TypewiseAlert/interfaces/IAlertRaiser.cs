namespace TypewiseAlert
{
    public interface IAlertRaiser
    {
        void SendNotification(BreachType breachType);
    }
    public enum AlertTarget
    {
        TO_CONTROLLER,
        TO_EMAIL,
        TO_CONSOLE,
        TO_FAKE
    };
}
