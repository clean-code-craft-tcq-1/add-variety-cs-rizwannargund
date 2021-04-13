using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKE_LOWBREACH", value = 3)]
    public class FakeLowBreachEmailNotifier : IBreachEmailNotifier
    {
        public bool isBreachNotifierMethodCalledAtleastOnce = false;
        public void BreachNotifier(string recepient)
        {
            isBreachNotifierMethodCalledAtleastOnce = true;
            Console.WriteLine("Hi, fake email is sent for too low\n");
        }
    }
}
