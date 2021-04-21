using System;
using System.Collections.Generic;
using System.Text;

namespace TypewiseAlert
{
    [CustomAttribute("TO_FAKE_HIGHBREACH", value = 3)]
    public class FakeHighBreachEmailNotifier : IBreachEmailNotifier
    {
        public bool isBreachNotifierMethodCalledAtleastOnce = false;
        public void BreachNotifier(string recepient)
        {
            isBreachNotifierMethodCalledAtleastOnce = true;
            Console.WriteLine("Hi, fake email is sent for too high\n");
        }
    }
}
