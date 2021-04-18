using System;
using System.Collections.Generic;

namespace TypewiseAlert
{
    public class TypewiseAlert
    {
        public static IAlertNotifier AlertNotifier { get; private set; }
        public static IMetaDataUtilisation MetaDataUtilisation { get; private set; }
        public TypewiseAlert(IAlertNotifier alertNotifier, IMetaDataUtilisation metaDataUtilisation)
        {
            if (alertNotifier == null || metaDataUtilisation == null)
                throw new ArgumentNullException("AlertNotifier and MetaDataUtilisation cannot be null");

            AlertNotifier = alertNotifier;
            MetaDataUtilisation = metaDataUtilisation;
        }
        public static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
        {
            List<object> instanceObjects = MetaDataUtilisation.CreateInstanceFromInterface(typeof(ITemperatureBreach), new object[,] { { lowerLimit }, { upperLimit } });

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
            object instanceObject = MetaDataUtilisation.CreateInstanceFromInterfaceAndAttribute(typeof(ICoolingLimits), coolingType.ToString());
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
        public static void CheckAndAlert(BatteryCharacter batteryChar, double temperatureInC)
        {
            BreachType breachType = ClassifyTemperatureBreach(
              batteryChar.coolingType, temperatureInC
            );

            new AlertNotifier((IAlertNotifier)AlertNotifier).SendNotification(breachType);
        }

    }
}
