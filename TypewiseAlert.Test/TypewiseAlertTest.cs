using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace TypewiseAlert.Test
{
    public class TypewiseAlertTest
    {
        public static IEnumerable<object[]> FakeCheckAndAlert =>
        new List<object[]>
        {
            new object[] { new TypewiseAlert.BatteryCharacter() { coolingType = CoolingType.PASSIVE_COOLING,brand="BOSCH"}, -1 },
            new object[] { new TypewiseAlert.BatteryCharacter() { coolingType = CoolingType.MED_ACTIVE_COOLING,brand="BOSCH"}, 12 },
            new object[] { new TypewiseAlert.BatteryCharacter() { coolingType = CoolingType.HI_ACTIVE_COOLING,brand="BOSCH"}, 46 },
        };

        private TypewiseAlert typewiseAlert;

        [Fact(DisplayName = "Metadata utility test")]
        public void metadataUtilityTest()
        {
            //Arrange
            var metaDataUtility = new MetaDataUtility("TypewiseAlert");

            //Act
            object instance = metaDataUtility.CreateInstanceFromInterfaceAndAttribute(typeof(ICoolingLimits), 
                CoolingType.HI_ACTIVE_COOLING.ToString());

            //Assert
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
            typewiseAlert = new TypewiseAlert(new FakeConsoleNotifier(), new MetaDataUtility("TypewiseAlert"));
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
        [MemberData(nameof(FakeCheckAndAlert))]
        public void fakeConsoleCheckAndAlertTest(TypewiseAlert.BatteryCharacter batteryCharacter,
        double tempValue)
        {
            //Arrange
            var fakeConsoleNotifier = new FakeConsoleNotifier();
            typewiseAlert = new TypewiseAlert(fakeConsoleNotifier, new MetaDataUtility("TypewiseAlert"));

            //Act
            TypewiseAlert.CheckAndAlert(batteryCharacter, tempValue);

            //Assert
            Assert.True(fakeConsoleNotifier.isSendNotificationMethodCalledAtleastOnce);
        }

        [Theory(DisplayName = "When breach infered is TOO_LOW and TOO_HIGH and FAKE CONTROLLER alert triggered")]
        [MemberData(nameof(FakeCheckAndAlert))]
        public void fakeContollerCheckAndAlertTest(TypewiseAlert.BatteryCharacter batteryCharacter,
        double tempValue)
        {
            //Arrange
            var fakeControllerNotifier = new FakeControllerNotifier();
            typewiseAlert = new TypewiseAlert(fakeControllerNotifier, new MetaDataUtility("TypewiseAlert"));

            //Act
            TypewiseAlert.CheckAndAlert(batteryCharacter, tempValue);

            //Assert
            Assert.True(fakeControllerNotifier.isSendNotificationMethodCalledAtleastOnce);
        }

        [Theory(DisplayName = "When breach infered is TOO_LOW,TOO_HIGH and FAKE EMAIL alert triggered")]
        [MemberData(nameof(FakeCheckAndAlert))]
        public void fakeEmailBreachCheckAndAlertTest(TypewiseAlert.BatteryCharacter batteryCharacter,
        double temperatureValue)
        {
            //Arrange
            var fakeEmailNotifier = new FakeEmailNotifier();
            typewiseAlert = new TypewiseAlert(fakeEmailNotifier, new MetaDataUtility("TypewiseAlert"));

            //Act
            TypewiseAlert.CheckAndAlert(batteryCharacter, temperatureValue);

            //Assert
            Assert.True(fakeEmailNotifier.isSendNotificationMethodCalledAtleastOnce ||
                 fakeEmailNotifier.isBreachNotifierMethodCalledAtleastOnce);
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
        [MemberData(nameof(FakeCheckAndAlert))]
        public void checkAndAlertForNormalBreach(TypewiseAlert.BatteryCharacter batteryCharacter,
        double tempValue)
        {
            //Arrange
            var emailNotifier = new EmailNotifier();
            typewiseAlert = new TypewiseAlert(emailNotifier, new MetaDataUtility("TypewiseAlert"));

            //Act
            var anyException = Record.Exception(() => TypewiseAlert.CheckAndAlert(batteryCharacter, tempValue));
            
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

        [Theory(DisplayName = "Composite notifier for fake")]
        [MemberData(nameof(FakeCheckAndAlert))]
        public void fakeCompositeNotifier(TypewiseAlert.BatteryCharacter batteryCharacter,
        double temperatureValue)
        {
            //Arrange
            CompositeNotifier compositeNotifier = new CompositeNotifier();
            var fakeConsoleNotifier = new FakeConsoleNotifier();
            var fakeControllerNotifier = new FakeControllerNotifier();
            var fakeEmailNotifier = new FakeEmailNotifier();
            compositeNotifier.Add(fakeConsoleNotifier);
            compositeNotifier.Add(fakeControllerNotifier);
            compositeNotifier.Add(fakeEmailNotifier);
            typewiseAlert = new TypewiseAlert(compositeNotifier, new MetaDataUtility("TypewiseAlert"));

            //Act
            TypewiseAlert.CheckAndAlert(batteryCharacter, temperatureValue);

            //Assert
            Assert.True(fakeConsoleNotifier.isSendNotificationMethodCalledAtleastOnce &&
                        fakeControllerNotifier.isSendNotificationMethodCalledAtleastOnce &&
                        fakeEmailNotifier.isSendNotificationMethodCalledAtleastOnce);
        }

        #region Null Handling
        [Fact(DisplayName = "Null check for Type Wise Alert")]
        public void nullCheckForTypeWiseAlertTest()
        {
            IAlertNotifier alertNotifier = null;
            IMetaDataUtilisation metaDataUtilisation = null;

            Assert.Throws<System.ArgumentNullException>(() => new TypewiseAlert(alertNotifier, metaDataUtilisation));
        }

        [Fact(DisplayName = "Null check for alert notifier")]
        public void nullCheckForAlertNotifierTest()
        {
            IAlertNotifier alertNotifier = null;

            Assert.Throws<System.ArgumentNullException>(() => new AlertNotifier(alertNotifier));
        }

        [Fact(DisplayName = "Null check for infer temperature breach")]
        public void nullCheckForInferBreachTest()
        {
            ITemperatureBreach temperatureBreach = null;


            Assert.Throws<System.ArgumentNullException>(() => new InferTemperatureBreach(temperatureBreach));
        }

        [Fact(DisplayName = "Null check for infer temperature breach")]
        public void nullCheckForTemperatureCoolingTest()
        {
            ICoolingLimits coolingLimits = null;

            Assert.Throws<System.ArgumentNullException>(() => new TemperatureCooling(coolingLimits));
        }
        #endregion
    }
}
