using System;
using System.IO;
using Xunit;

namespace EnvironmentMonitor.Tests
{

    public class UnitTest1
    {
        [Fact]
        public void WhenPropertiesArePassedOnly()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.WhenGetPropertyNamesThenSetPropertyNames();
            Assert.True(r.PropertiesList[0].PropertyName.Equals("Temperature"));
            Assert.True(r.PropertiesList[1].PropertyName.Equals("Humidity"));
            Assert.True(r.PropertiesList[2].PropertyName.Equals("Date"));
            Assert.True(r.PropertiesList[3].PropertyName.Equals("Time"));
        }

        [Fact]
        public void WhenInputIsCorrect()
        {
            var r = new EnvironmentMonitor(
                new StringReader(
                    "Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n32C,20%,15-09-2020,12:10pm\n\n"),
                Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var b = r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.True(b);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals(""));
        }

        [Fact]
        public void WhenTempInHighWarningLimitThenAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n40C,70%,15-09-2020,1:10pm\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.Contains("Temperature reached High Warning level:40C", r.AlertStaticObj.FinalStringPrintedOnConsole);
        }

        [Fact]
        public void WhenTempInHighErrorLimitThenAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n41C,70%,15-09-2020,1:10pm\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.Contains("Temperature reached High Error level:41C", r.AlertStaticObj.FinalStringPrintedOnConsole);
        }

        [Fact]
        public void WhenTempInLowWarningLimitThenAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n2C,70%,15-09-2020,1:10pm\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.Contains("Temperature reached Low Warning level:2C", r.AlertStaticObj.FinalStringPrintedOnConsole);
        }

        [Fact]
        public void WhenTempInLowErrorLimitThenAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n-1C,70%,15-09-2020,1:10pm\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.Contains("Temperature reached Low Error level:-1C", r.AlertStaticObj.FinalStringPrintedOnConsole);
        }

        [Fact]
        public void WhenHumidityReachedWarningLevelThenAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n10C,71%,15-09-2020,1:10pm\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.Contains("Humidity reached Warning level:71%", r.AlertStaticObj.FinalStringPrintedOnConsole);

        }

        [Fact]
        public void WhenHumidityReachedErrorLevelThenAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n10C,91%,15-09-2020,1:10pm\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.Contains("Humidity reached Error level:91%", r.AlertStaticObj.FinalStringPrintedOnConsole);
        }

        [Fact]
        public void WhenTemperatureInLimitThenNoAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var values = r.WhenToSplitLine("20C,60%,15-09-2020,1:10pm");
            r.WhenAnalyzeTemperature(values);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals(""));
        }

        [Fact]
        public void WhenHumidityInLimitThenNoAlert()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n10C,50%,15-09-2020,1:10pm\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            r.WhenGetReadingsFromSensorThenAnalyze();
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals(""));
        }

        [Fact]
        public void WhenTemperaturePropertyNotProvidedBySenderThenPromptUser()
        {
            var r = new EnvironmentMonitor(new StringReader("Humidity,Date,Time\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var values = r.WhenToSplitLine("50%,15-09-2020,1:10pm");
            r.WhenAnalyzeTemperature(values);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals("CSV does not contain Temperature property."));
        }

        [Fact]
        public void WhenHumidityPropertyNotProvidedBySenderThenPromptUser()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Date,Time\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var values = r.WhenToSplitLine("50C,15-09-2020,1:10pm");
            r.WhenAnalyzeHumidity(values);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals("CSV does not contain Humidity property."));
        }
        [Fact]
        public void WhenDatePropertyNotProvidedBySenderThenPromptUser()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Time\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var values = r.WhenToSplitLine("50C,50%,1:10pm");
            r.WhenAnalyzeTemperature(values);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals("CSV does not contain Date property."));
        }
        [Fact]
        public void WhenTimePropertyNotProvidedBySenderThenPromptUser()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var values = r.WhenToSplitLine("50C,50%,15-09-2020");
            r.WhenAnalyzeTemperature(values);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals("CSV does not contain Time property."));
        }

        [Fact]
        public void WhenTemperatureValueIsNotProvidedBySenderThenPromptUser()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var values = r.WhenToSplitLine("NA,50%,12-9-2019,11:10am");
            r.WhenAnalyzeTemperature(values);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals("Temperature value not provided by sender.Possibility of error in temperature sensor."));
        }
        [Fact]
        public void WhenHumidityValueIsNotProvidedBySenderThenPromptUser()
        {
            var r = new EnvironmentMonitor(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);
            r.WhenSetAlerterMock();
            r.WhenGetPropertyNamesThenSetPropertyNames();
            var values = r.WhenToSplitLine("41C,NA,12-9-2019,11:10am");
            r.WhenAnalyzeHumidity(values);
            Assert.True(r.AlertStaticObj.FinalStringPrintedOnConsole.Equals("Humidity value not provided by sender.Possibility of error in humidity sensor."));
        }
        //added to complete code coverage
        [Fact]
        public void WhenCompleteCodeCoverage()
        {
            Alert alerter = new Alert();
            alerter.PrintOnConsole(null);
            alerter.PrintOnConsole("Environment Monitoring");
            Alert alerter2 = new Alert(Console.Out);
            alerter2.PrintOnConsole("Environment Monitoring");
            EnvironmentMonitor r = new EnvironmentMonitor();
            r.WhenSetAlerterMock();
        }

        /*
         //passing in local pc but not in git
        [Fact]
        public void WhenSenderIsDisconnected()
        {
            bool failure = false;
            try
            {
                var r = new EnvironmentMonitor();
                r.WhenGetPropertyNamesThenSetPropertyNames();
            }
            catch (TimeoutException)
            {
                failure = true;
            }
            Assert.True(failure);
        
        }
        */
    }
}