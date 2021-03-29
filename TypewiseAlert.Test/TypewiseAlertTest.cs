using System;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        [Fact]
        public void InfersBreachAsPerLimits()
        {
            Assert.True(TypewiseAlert.InferBreach(12, 20, 30) ==
              BreachType.TOO_LOW);
            Assert.True(TypewiseAlert.InferBreach(31, 20, 30) ==
              BreachType.TOO_HIGH);
            Assert.True(TypewiseAlert.InferBreach(25, 20, 30) ==
              BreachType.NORMAL);
        }

        [Fact]
        public void ClassifyTemperatureBreachTest()
        {
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -1) ==
                BreachType.TOO_LOW);
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 38) ==
                BreachType.TOO_HIGH);
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, 12) ==
                BreachType.NORMAL);

            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, -1) ==
                BreachType.TOO_LOW);
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, 41) ==
                BreachType.TOO_HIGH);
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, 12) ==
                BreachType.NORMAL);

            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, -1) ==
                BreachType.TOO_LOW);
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 46) ==
                BreachType.TOO_HIGH);
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 12) ==
                BreachType.NORMAL);
        }

        [Fact]
        public void CheckAndAlertTest()
        {
            var batteryCharacter = new TypewiseAlert.BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            batteryCharacter.brand = "BOSCH";
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, -1));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 38));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 12));

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            batteryCharacter.brand = "PHILIP";
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, -1));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 46));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 12));

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            batteryCharacter.brand = "HITACHI";
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, -1));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 41));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_CONTROLLER, batteryCharacter, 12));

            //Alert to email
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            batteryCharacter.brand = "BOSCH";
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, -1));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 38));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 12));

            batteryCharacter.coolingType = CoolingType.HI_ACTIVE_COOLING;
            batteryCharacter.brand = "BOSCH";
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, -1));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 46));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 12));

            batteryCharacter.coolingType = CoolingType.MED_ACTIVE_COOLING;
            batteryCharacter.brand = "BOSCH";
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, -1));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 41));
            Assert.True(TypewiseAlert.CheckAndAlert(AlertTarget.TO_EMAIL, batteryCharacter, 12));
        }
    }
}
