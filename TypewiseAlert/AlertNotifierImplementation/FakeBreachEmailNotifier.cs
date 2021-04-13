using System;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKEEMAIL", value = 3)]
    public class FakeBreachEmailNotifier : IBreachEmailNotifier
    {
        public static bool isBreachNotifierMethodCalledAtleastOnce = false;
        public void BreachNotifier(string recepient)
        {
            isBreachNotifierMethodCalledAtleastOnce = true;
            Console.WriteLine("Fake email notifier");
        }
    }
}
