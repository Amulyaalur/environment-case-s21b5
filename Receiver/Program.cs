using System;
using System.Collections.Generic;
using System.IO;
namespace Receiver
{
    public struct Properties
    {
        public string PropertyName;

    }
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string message) : base(message)
        {
        }
    }
    public class Receiver
    {
        public readonly List<Properties> PropertiesList = new List<Properties>();
        private static string[] _propertiesNames;
        private readonly Reader _reader;
        public Alert AlertStaticObj;
        public Receiver()
        {
            _reader = new Reader();
            AlertStaticObj = new Alert();
        }
        public Receiver(TextReader input, TextWriter output)
        {
            _reader = new Reader(input);
            AlertStaticObj = new Alert(output);
        }

        public void WhenSetAlerterMock()
        {
            AlertStaticObj = new AlertChild();
        }
        public string[] WhenToSplitLine(string line)
        {
            string[] split = line.Split(',');
            return split;
        }
        public void WhenGetPropertyNamesThenSetPropertyNames()
        {
            string line = _reader.WhenReadLine();
            _propertiesNames = WhenToSplitLine(line);
        }
        public void WhenWantToAssignIndexToProperties()
        {
            foreach (var t in _propertiesNames)
            {
                Properties temp;
                temp.PropertyName = t;
                PropertiesList.Add(temp);
            }
        }
        public bool WhenGetReadingsFromSensorThenAnalyze()
        {
            string line = _reader.WhenReadLine();
            while (line != null && !line.Equals(""))
            {
                var values = WhenToSplitLine(line);
                WhenAnalyzeTemperature(values);
                WhenAnalyzeHumidity(values);
                line = _reader.WhenReadLine();
            }
            return true;
        }
        int WhenGetIndex(string propertyName)
        {
            int id = -1;
            int index = 0;
            foreach (var t in PropertiesList)
            {
                if (t.PropertyName.Equals(propertyName))
                {
                    id = index;
                    break;
                }
                index++;
            }
            return id;
        }

        private string WhenWantValueOfProperty(string property, string[] values)
        {
            int index = WhenGetIndex(property);
            if (index == -1)
                throw (new PropertyNotFoundException("CSV does not contain " + property + " property."));
            return values[index];
        }
        public void WhenAnalyzeTemperature(string[] values)
        {
            try
            {
                string valueInString = WhenWantValueOfProperty("Temperature", values);
                WhenTemperatureIsOutOfLimitsThenAlert(valueInString, values);
            }
            catch (PropertyNotFoundException e)
            {
                AlertStaticObj.PrintOnConsole(e.Message);
            }
        }
        private void WhenTemperatureIsOutOfLimitsThenAlert(string valueInString, string[] values)
        {
            int valueInInt = int.Parse((valueInString.Split('C'))[0]);
            if (valueInInt > 37)
            {
                WhenWantToAlertForHighLimitsForTemperature(valueInInt, values);
            }
            else if (valueInInt < 4)
            {
                WhenWantToAlertForLowerLimitsForTemperature(valueInInt, values);
            }
        }
        void WhenWantToAlertForHighLimitsForTemperature(int valueInInt, string[] values)
        {
            if (valueInInt > 40)
            {
                AlertStaticObj.PrintOnConsole("Temperature reached High Error level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
            else
            {
                AlertStaticObj.PrintOnConsole("Temperature reached High Warning level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
        }
        void WhenWantToAlertForLowerLimitsForTemperature(int valueInInt, string[] values)
        {
            if (valueInInt < 0)
            {
                AlertStaticObj.PrintOnConsole("Temperature reached Low Error level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
            else
            {
                AlertStaticObj.PrintOnConsole("Temperature reached Low Warning level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
        }
        public void WhenAnalyzeHumidity(string[] values)
        {
            try
            {
                string valueInString = WhenWantValueOfProperty("Humidity", values);
                WhenHumidityIsOutOfLimitsThenAlert(valueInString, values);
            }
            catch (PropertyNotFoundException e)
            {
                AlertStaticObj.PrintOnConsole(e.Message);
            }
        }
        void WhenHumidityIsOutOfLimitsThenAlert(string valueInString, string[] values)
        {
            int valueInInt = int.Parse((valueInString.Split('%'))[0]);
            if (valueInInt > 90)
            {
                AlertStaticObj.PrintOnConsole("Humidity reached Error level:" + valueInString + " at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
            else if (valueInInt > 70)
            {
                AlertStaticObj.PrintOnConsole("Humidity reached Warning level:" + valueInString + " at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }

        }

        static void Main()
        {
            Receiver r = new Receiver(); try
            { 
                r.WhenGetPropertyNamesThenSetPropertyNames();
                r.WhenWantToAssignIndexToProperties();
                r.WhenGetReadingsFromSensorThenAnalyze();
            }
            catch (TimeoutException) { r.AlertStaticObj.PrintOnConsole("Sender is disconnected"); }


        }
    }
}
