using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKEEMAIL", value = 3)]
    public class FakeBreachEmailNotifier : IBreachEmailNotifier
    {
        public virtual void BreachNotifier(string recepient)
        {
            Console.WriteLine("Fake email notifier");
        }
    }
}
