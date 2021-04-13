using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {

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

            //Assert
            Assert.True(fakeEmailNotifier.isSendNotificationMethodCalledAtleastOnce &&
                !fakeEmailNotifier.isBreachNotifierMethodCalledAtleastOnce);
        }

        
    }
}
