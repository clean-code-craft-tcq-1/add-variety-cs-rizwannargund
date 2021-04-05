using Moq;
using System;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        [Fact]
        public void InfersBreachAsPerLimits_Test()
        {
            Assert.True(TypewiseAlert.InferBreach(12, 20, 30) ==
              BreachType.TOO_LOW);
            Assert.True(TypewiseAlert.InferBreach(31, 20, 30) ==
              BreachType.TOO_HIGH);
            Assert.True(TypewiseAlert.InferBreach(25, 20, 30) ==
              BreachType.NORMAL);
        }

        [Fact]
        public void ClassifyTemperatureBreach_Test()
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
        public void CheckAndAlert_Test()
        {
            var batteryCharacter = new TypewiseAlert.BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            batteryCharacter.brand = "BOSCH";
            string[] alertTargets = Enum.GetNames(typeof(AlertTarget));
            string[] coolingTypes = Enum.GetNames(typeof(CoolingType));
            for (int i = 0; i < alertTargets.Length; i++)
            {
                for (int j = 0; j < coolingTypes.Length; j++)
                {
                    Enum.TryParse(alertTargets[i], out AlertTarget alertTarget);
                    Enum.TryParse(coolingTypes[j], out CoolingType coolingType);
                    batteryCharacter.coolingType = coolingType;
                    var exception = Record.Exception(() => TypewiseAlert.CheckAndAlert(alertTarget, batteryCharacter, -1));
                    Assert.Null(exception);
                    exception = Record.Exception(() => TypewiseAlert.CheckAndAlert(alertTarget, batteryCharacter, 46));
                    Assert.Null(exception);
                    exception = Record.Exception(() => TypewiseAlert.CheckAndAlert(alertTarget, batteryCharacter, 12));
                    Assert.Null(exception);
                }
            }
        }

        [Fact]
        public void Fake_CheckAndAlert_Test()
        {
            var batteryCharacter = new TypewiseAlert.BatteryCharacter();
            batteryCharacter.coolingType = CoolingType.PASSIVE_COOLING;
            batteryCharacter.brand = "BOSCH";
            BreachType breachType = BreachType.NORMAL;

            var notifier = new Mock<FakeNotifier>();
            notifier.Setup(x => x.SendNotification(breachType)).Verifiable();

            var emailNotifier = new Mock<FakeBreachEmailNotifier>();
            emailNotifier.Setup(x => x.BreachNotifier("")).Verifiable();

            var exception = Record.Exception(() => TypewiseAlert.CheckAndAlert(AlertTarget.TO_FAKE, batteryCharacter, 12));
            Assert.Null(exception);

            Assert.True(TypewiseAlert.isCheckAndAlertMethodCalledAtLeastOnce);
        }
    }
}
