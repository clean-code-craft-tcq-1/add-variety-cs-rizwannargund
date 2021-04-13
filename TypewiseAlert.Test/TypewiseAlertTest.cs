using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        public static IEnumerable<object[]> CheckAndAlertForNormalBreach =>
        new List<object[]>
        {
            new object[] { AlertTarget.TO_CONSOLE,
                new TypewiseAlert.BatteryCharacter() { coolingType = CoolingType.PASSIVE_COOLING,brand="BOSCH"}, 12 },
            new object[] { AlertTarget.TO_CONTROLLER,
                new TypewiseAlert.BatteryCharacter() { coolingType = CoolingType.MED_ACTIVE_COOLING,brand="BOSCH"}, 12 },
            new object[] { AlertTarget.TO_EMAIL,
                new TypewiseAlert.BatteryCharacter() { coolingType = CoolingType.HI_ACTIVE_COOLING,brand="BOSCH"}, 12 },
        };


        [Fact(DisplayName = "Metadata utility test")]
        public void metadataUtilityTest()
        {
            var instance = MetaDataUtility.CreateInstanceWithInterfaceAndAttribute("TypewiseAlert", "TypewiseAlert", typeof(IAlertRaiser),
                "TO_FAKE_EMAIL");
            Assert.NotNull(instance);
        }

        [Fact(DisplayName = "Infer breach for passive cooling - low")]
        public void classifyTemperatureBreachForPassiveCoolingTest()
        {
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.PASSIVE_COOLING, -1) ==
                BreachType.TOO_LOW);
        }

        [Fact(DisplayName = "Infer breach for medium active cooling - normal")]
        public void classifyTemperatureBreachForMediumCoolingTest()
        {
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.MED_ACTIVE_COOLING, 12) ==
                BreachType.NORMAL);
        }

        [Fact(DisplayName = "Infer breach for High active cooling - high")]
        public void classifyTemperatureBreachForHighCoolingTest()
        {
            Assert.True(TypewiseAlert.ClassifyTemperatureBreach(CoolingType.HI_ACTIVE_COOLING, 46) ==
                BreachType.TOO_HIGH);
        }

        [Theory(DisplayName = "When breach infered is TOO_LOW and TOO_HIGH and FAKE CONSOLE alert triggered")]
        [InlineData(BreachType.TOO_LOW)]
        [InlineData(BreachType.TOO_HIGH)]
        public void fakeConsoleCheckAndAlertTest(BreachType breachType)
        {
            //Arrange
            var fakeConsoleNotifier = new FakeConsoleNotifier();
            var alertNotifier = new AlertNotifier(fakeConsoleNotifier);

            //Act
            alertNotifier.SendNotification(breachType);

            //Assert
            Assert.True(fakeConsoleNotifier.isSendNotificationMethodCalledAtleastOnce);
        }

        [Theory(DisplayName = "When breach infered is TOO_LOW and TOO_HIGH and FAKE CONTROLLER alert triggered")]
        [InlineData(BreachType.TOO_LOW)]
        [InlineData(BreachType.TOO_HIGH)]
        public void fakeContollerCheckAndAlertTest(BreachType breachType)
        {
            //Arrange
            var fakeControllerNotifier = new FakeControllerNotifier();
            var alertNotifier = new AlertNotifier(fakeControllerNotifier);

            //Act
            alertNotifier.SendNotification(breachType);

            //Assert
            Assert.True(fakeControllerNotifier.isSendNotificationMethodCalledAtleastOnce);
        }

        [Theory(DisplayName = "When breach infered is TOO_LOW,TOO_HIGH and FAKE EMAIL alert triggered")]
        [InlineData(BreachType.TOO_LOW)]
        [InlineData(BreachType.TOO_HIGH)]
        public void fakeEmailBreachCheckAndAlertTest(BreachType breachType)
        {
            //Arrange
            var fakeEmailNotifier = new FakeEmailNotifier();
            var alertNotifier = new AlertNotifier(fakeEmailNotifier);

            //Act
            alertNotifier.SendNotification(breachType);

            //Assert
            Assert.True(fakeEmailNotifier.isSendNotificationMethodCalledAtleastOnce &&
                fakeEmailNotifier.isBreachNotifierMethodCalledAtleastOnce);
        }

        [Theory(DisplayName = "When breach infered is NORMAL and no FAKE EMAIL alert triggered")]
        [InlineData(BreachType.NORMAL)]
        public void fakeEmailNormalBreachCheckAndAlertTest(BreachType breachType)
        {
            //Arrange
            var fakeEmailNotifier = new FakeEmailNotifier();
            var alertNotifier = new AlertNotifier(fakeEmailNotifier);

            //Act
            alertNotifier.SendNotification(breachType);
            var anyException = Record.Exception(() => new EmailNotifier().SendNotification(breachType));

            //Assert
            Assert.True(fakeEmailNotifier.isSendNotificationMethodCalledAtleastOnce &&
                !fakeEmailNotifier.isBreachNotifierMethodCalledAtleastOnce && (anyException == null));
        }

        [Fact(DisplayName = "When breach infered is HIGH and dummy EMAIL alert triggered")]
        public void emailHighBreachCheckAndAlertTest()
        {
            //Arrange
            var highBreachEmailNotifier = new HighBreachEmailNotifier();

            //Act
            var anyException = Record.Exception(() => highBreachEmailNotifier.BreachNotifier("dummy@a.com"));

            //Assert
            Assert.Null(anyException);
        }

        [Fact(DisplayName = "When breach infered is LOW and dummy EMAIL alert triggered")]
        public void emailLowBreachCheckAndAlertTest()
        {
            //Arrange
            var lowBreachEmailNotifier = new LowBreachEmailNotifier();

            //Act
            var anyException = Record.Exception(() => lowBreachEmailNotifier.BreachNotifier("dummy@a.com"));

            //Assert
            Assert.Null(anyException);
        }

        [Theory(DisplayName = "Check and alert when normal breach")]
        [MemberData(nameof(CheckAndAlertForNormalBreach))]
        public void checkAndAlertForNormalBreach(AlertTarget alertTarget, TypewiseAlert.BatteryCharacter batteryCharacter,
            double tempValue)
        {
            var anyException = Record.Exception(() => TypewiseAlert.CheckAndAlert(alertTarget, batteryCharacter, tempValue));
            //Assert
            Assert.Null(anyException);
        }

        [Fact(DisplayName = "Test cooling limits")]
        public void coolingLimitsTest()
        {
            var highActiveCooling = new HighActiveCooling();
            highActiveCooling.Lower = 0;
            highActiveCooling.Higher = 45;

            Assert.True(highActiveCooling.Lower == 0 && highActiveCooling.Higher == 45);
        }
    }
}
