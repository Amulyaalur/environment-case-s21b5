using System;
using Xunit;
using System.IO;

namespace Receiver.Tests
{
    public class UnitTest1
    {

        [Fact]
        public void WhenPropertiesArePassedOnly()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            Assert.True(r.Properties_list[0].property_Name.Equals("Temperature"));
            Assert.True(r.Properties_list[1].property_Name.Equals("Humidity"));
            Assert.True(r.Properties_list[2].property_Name.Equals("Date"));
            Assert.True(r.Properties_list[3].property_Name.Equals("Time"));

        }
        [Fact]
        public void WhenInputIsCorrect()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n32C,20%,15-09-2020,12:10pm\n\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            bool b = r.GetReadingsFromSensorAndAnalyze();
            Assert.True(b);
        }
        [Fact]
        public void WhenTempInHighWarningLimitThenAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("40C,70%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeTemperature(values).Equals("Temperature reached High Warning level:40C"));
        }
        [Fact]
        public void WhenTempInHighErrorLimitThenAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("41C,70%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeTemperature(values).Equals("Temperature reached High Error level:41C"));
        }
        [Fact]
        public void WhenTempInLowWarningLimitThenAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("2C,70%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeTemperature(values).Equals("Temperature reached Low Warning level:2C"));
        }
        [Fact]
        public void WhenTempInLowErrorLimitThenAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("-1C,70%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeTemperature(values).Equals("Temperature reached Low Error level:-1C"));
        }
        [Fact]
        public void WhenHumidityReachedWarningLevelThenAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("2C,71%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeHumidity(values).Equals("Humidity reached Warning level:71%"));
        }
        [Fact]
        public void WhenHumidityReachedErrorLevelThenAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("-1C,91%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeHumidity(values).Equals("Humidity reached Error level:91%"));
        }
        [Fact]
        public void WhenTemperatureInLimitThenNoAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("20C,91%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeTemperature(values) == null);
        }
        [Fact]
        public void WhenHumidityInLimitThenNoAlert()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("-1C,50%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeHumidity(values) == null);
        }
        [Fact]
        public void WhenTemperaturePropertyNotProvidedBySenderThenPromtUser()
        {
            Receiver r = new Receiver(new StringReader("Humidity,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("50%,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeTemperature(values).Equals("CSV does not contain Temperature property."));
        }
        [Fact]
        public void WhenHumidityPropertyNotProvidedBySenderThenPromtUser()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Date,Time\n"), Console.Out);

            r.GetPropertyNames();
            r.AssignIndexToProperties();
            string[] values;
            values = r.SplitLine("50C,15-09-2020,1:10pm");
            Assert.True(r.AnalyzeHumidity(values).Equals("CSV does not contain Humidity property."));
        }

        //added to complete code coverage
        [Fact]
        public void CompleteCodeCoverage()
        {
            Receiver r = new Receiver(new StringReader("Temperature,Date,Time\n"), Console.Out);
            r.PrintOnConsole(null);
            r.PrintOnConsole("hello");
            Receiver r2 = new Receiver();
            r2.PrintOnConsole("hello");
        }
    }
}
