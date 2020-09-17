using System;
using System.IO;
using Xunit;

namespace Receiver.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WhenPropertiesArePassedOnly()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            Assert.True(r.PropertiesList[0].PropertyName.Equals("Temperature"));
            Assert.True(r.PropertiesList[1].PropertyName.Equals("Humidity"));
            Assert.True(r.PropertiesList[2].PropertyName.Equals("Date"));
            Assert.True(r.PropertiesList[3].PropertyName.Equals("Time"));
        }

        [Fact]
        public void WhenInputIsCorrect()
        {
            var r = new Receiver(
                new StringReader(
                    "Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n32C,20%,15-09-2020,12:10pm\n\n"),
                Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var b = r.WhenGetReadingsFromSensorAndAnalyze();
            Assert.True(b);
        }

        [Fact]
        public void WhenTempInHighWarningLimitThenAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("40C,70%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeTemperature(values).Equals("Temperature reached High Warning level:40C"));
        }

        [Fact]
        public void WhenTempInHighErrorLimitThenAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("41C,70%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeTemperature(values).Equals("Temperature reached High Error level:41C"));
        }

        [Fact]
        public void WhenTempInLowWarningLimitThenAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("2C,70%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeTemperature(values).Equals("Temperature reached Low Warning level:2C"));
        }

        [Fact]
        public void WhenTempInLowErrorLimitThenAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("-1C,70%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeTemperature(values).Equals("Temperature reached Low Error level:-1C"));
        }

        [Fact]
        public void WhenHumidityReachedWarningLevelThenAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("2C,71%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeHumidity(values).Equals("Humidity reached Warning level:71%"));
        }

        [Fact]
        public void WhenHumidityReachedErrorLevelThenAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("-1C,91%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeHumidity(values).Equals("Humidity reached Error level:91%"));
        }

        [Fact]
        public void WhenTemperatureInLimitThenNoAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("20C,91%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeTemperature(values) == null);
        }

        [Fact]
        public void WhenHumidityInLimitThenNoAlert()
        {
            var r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("-1C,50%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeHumidity(values) == null);
        }

        [Fact]
        public void WhenTemperaturePropertyNotProvidedBySenderThenPromptUser()
        {
            var r = new Receiver(new StringReader("Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("50%,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeTemperature(values).Equals("CSV does not contain Temperature property."));
        }

        [Fact]
        public void WhenHumidityPropertyNotProvidedBySenderThenPromptUser()
        {
            var r = new Receiver(new StringReader("Temperature,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            var values = r.WhenSplitLine("50C,15-09-2020,1:10pm");
            Assert.True(r.WhenAnalyzeHumidity(values).Equals("CSV does not contain Humidity property."));
        }

        //added to complete code coverage
        [Fact]
        public void WhenCompleteCodeCoverage()
        {
            var r = new Receiver(new StringReader("Temperature,Date,Time\n"), Console.Out);
            r.PrintOnConsole(null);
            r.PrintOnConsole("Environment Monitoring");
            var r2 = new Receiver();
            r2.PrintOnConsole("Environment Monitoring");
        }
        /*
        [Fact]
        public void WhenSenderIsDisconnected()
        {
            bool failure = false;
            try
            {
                var r = new Receiver();
                r.WhenGetPropertyNames();
            }
            catch (TimeoutException)
            {
                failure = true;
            }
            catch(System.NullReferenceException)
            {}
            Assert.True(failure);
        }
        */
    }
}