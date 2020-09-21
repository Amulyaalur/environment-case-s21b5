/*
 * Analyzer module is responsible for
 * 1.Storing properties
 * 2.Analyzing there values
 * 3.Alerting user when required
 *
 *
 * Condition all limits are inclusive i.e warning limit 37 then temperature t=37 is normal where 38 is in warning level
 */
using System;
using System.Collections.Generic;

namespace EnvironmentMonitor
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
    public class Analyzer
    {
        /*
         * Properties are stored in List so that
        * 1.we don't need to add index as 0 every where for getting property value
        * 2.If sensor sends data in different order we don't need to change the index value
         */
        //List of properties containing property in properties structure
        public readonly List<Properties> PropertiesList = new List<Properties>();

        public void SetProperties(string[] propertiesNames)
        {
            foreach (var t in propertiesNames)
            {
                Properties temp;
                temp.PropertyName = t;
                PropertiesList.Add(temp);
            }
        }
        //to get index of property
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
        //return value of property
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
                if (!valueInString.Equals("NA"))
                {
                    WhenTemperatureIsOutOfLimitsThenAlert(valueInString, values);
                }
                else
                {
                    //NA means either temperature is not provided by CSV or its value is not in AnalyzerObj practical limits
                    AlertWrapper.PrintOnConsole("Temperature value not provided by sender.Possibility of error in temperature sensor.");
                }
            }
            catch (PropertyNotFoundException e)
            {
                AlertWrapper.PrintOnConsole(e.Message);
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
                AlertWrapper.PrintOnConsole("Temperature reached High Error level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
            else
            {
                AlertWrapper.PrintOnConsole("Temperature reached High Warning level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
        }
        void WhenWantToAlertForLowerLimitsForTemperature(int valueInInt, string[] values)
        {
            if (valueInInt < 0)
            {
                AlertWrapper.PrintOnConsole("Temperature reached Low Error level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
            else
            {
                AlertWrapper.PrintOnConsole("Temperature reached Low Warning level:" + valueInInt.ToString() + "C at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
        }
        public void WhenAnalyzeHumidity(string[] values)
        {
            try
            {
                string valueInString = WhenWantValueOfProperty("Humidity", values);
                if (!valueInString.Equals("NA"))
                {
                    WhenHumidityIsOutOfLimitsThenAlert(valueInString, values);
                }
                else
                {
                    //NA means either temperature is not provided by CSV or its value is not in AnalyzerObj practical limits
                    AlertWrapper.PrintOnConsole("Humidity value not provided by sender.Possibility of error in humidity sensor.");
                }
            }
            catch (PropertyNotFoundException e)
            {
                AlertWrapper.PrintOnConsole(e.Message);
            }
        }
        void WhenHumidityIsOutOfLimitsThenAlert(string valueInString, string[] values)
        {
            int valueInInt = int.Parse((valueInString.Split('%'))[0]);
            if (valueInInt > 90)
            {
                AlertWrapper.PrintOnConsole("Humidity reached Error level:" + valueInString + " at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }
            else if (valueInInt > 70)
            {
                AlertWrapper.PrintOnConsole("Humidity reached Warning level:" + valueInString + " at " + WhenWantValueOfProperty("Time", values) + " on " + WhenWantValueOfProperty("Date", values));
            }

        }
    }
}
