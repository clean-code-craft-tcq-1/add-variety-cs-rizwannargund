using System;
using System.Collections.Generic;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        public static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
        {
            List<object> instanceObjects = MetaDataUtility.CreateInstanceFromInterface("TypewiseAlert", "TypewiseAlert",
                typeof(ITemperatureBreach), new object[,] { { lowerLimit }, { upperLimit } });
            List<BreachType> breachTypes = new List<BreachType>();
            BreachType result = BreachType.NORMAL;
            if (instanceObjects?.Count > 0)
            {
                foreach (object obj in instanceObjects)
                {
                    breachTypes.Add(new InferTemperatureBreach((ITemperatureBreach)obj).BreachLimit(value));
                }
                result = breachTypes.Find(x => x != BreachType.NORMAL);
            }
            return result;
        }

        public static BreachType ClassifyTemperatureBreach(
            CoolingType coolingType, double temperatureInC)
        {
            int lowerLimit = 0;
            int upperLimit = 0;
            object instanceObject = MetaDataUtility.CreateInstanceWithInterfaceAndAttribute("TypewiseAlert", "TypewiseAlert", 
                typeof(ICoolingLimits), Enum.GetName(typeof(CoolingType), coolingType));
            if (instanceObject != null)
            {
                int[] limits = new TemperatureCooling((ICoolingLimits)instanceObject).GetLimits();
                lowerLimit = limits[0];
                upperLimit = limits[1];
            }
            return InferBreach(temperatureInC, lowerLimit, upperLimit);
        }

        public struct BatteryCharacter
        {
            public CoolingType coolingType;
            public string brand;
        }
        public static bool CheckAndAlert(
            AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
        {
            BreachType breachType = ClassifyTemperatureBreach(
              batteryChar.coolingType, temperatureInC
            );
            object instanceObject = MetaDataUtility.CreateInstanceWithInterfaceAndAttribute("TypewiseAlert", "TypewiseAlert", 
                typeof(IAlertRaiser), Enum.GetName(typeof(AlertTarget), alertTarget));
            if (instanceObject != null)
            {
                new AlertNotifier((IAlertRaiser)instanceObject).SendNotification(breachType);
                return true;
            }
            return false;
        }
    }
}
