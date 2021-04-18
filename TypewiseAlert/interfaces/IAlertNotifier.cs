namespace TypewiseAlert
{
    public interface IAlertNotifier
    {
        void SendNotification(BreachType breachType);
    }
    public enum AlertTarget
    {
        TO_CONTROLLER,
        TO_EMAIL,
        TO_CONSOLE
    };
}
