using System;
using System.Collections.Generic;
using System.IO;
namespace Receiver
{
    public struct Properties
    {
        public string PropertyName;

    }

    public class Receiver
    {
        public readonly List<Properties> PropertiesList = new List<Properties>();
        private static string[] _propertiesNames;
        private readonly TextReader _input;
        private readonly TextWriter _output;
        public Receiver() : this(Console.In, Console.Out)
        {

        }
        public Receiver(TextReader input, TextWriter output)
        {
            this._input = input;
            this._output = output;
        }
        public string[] WhenSplitLine(string line)
        {
            string[] split = line.Split(',');
            return split;
        }
        public void WhenGetPropertyNames()
        {
            string line = _input.ReadLine();
            _propertiesNames = WhenSplitLine(line);

        }
        public void WhenAssignIndexToProperties()
        {
            foreach (var t in _propertiesNames)
            {
                Properties temp;
                temp.PropertyName = t;
                PropertiesList.Add(temp);
            }
        }
        public bool WhenGetReadingsFromSensorAndAnalyze()
        {
            string line = _input.ReadLine();

            while (line != null && !line.Equals(""))
            {
                var values = WhenSplitLine(line);
                PrintOnConsole(WhenAnalyzeTemperature(values));
                PrintOnConsole(WhenAnalyzeHumidity(values));
                line = _input.ReadLine();
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
        public string WhenAnalyzeTemperature(string[] values)
        {
            int index = WhenGetIndex("Temperature");
            if (index >= 0)
            {
                return WhenAlertTemperatureIfOutOfLimits(values, index);

            }
            else
            {
                return "CSV does not contain Temperature property.";
            }

        }
        private string WhenAlertTemperatureIfOutOfLimits(string[] values, int index)
        {
            if (int.Parse((values[index].Split('C'))[0]) > 37)
            {
                return AlertForHighLimitsForTemperature(values, index);
            }
            else if (int.Parse((values[index].Split('C'))[0]) < 4)
            {
                return AlertForLowerLimitsForTemperature(values, index);
            }
            return null;
        }
        string AlertForHighLimitsForTemperature(string[] values, int index)
        {
            if (int.Parse((values[index].Split('C'))[0]) > 40)
            {
                return "Temperature reached High Error level:" + values[index];
            }
            else
            {
                return "Temperature reached High Warning level:" + values[index];
            }
        }
        string AlertForLowerLimitsForTemperature(string[] values, int index)
        {
            if (int.Parse((values[index].Split('C'))[0]) < 0)
            {
                return "Temperature reached Low Error level:" + values[index];
            }
            else
            {
                return "Temperature reached Low Warning level:" + values[index];
            }
        }
        public string WhenAnalyzeHumidity(string[] values)
        {
            int index = WhenGetIndex("Humidity");
            if (index >= 0)
            {
                return AlertHumidityIfOutOfLimits(values, index);

            }
            else
            {
                return "CSV does not contain Humidity property.";
            }
        }
        string AlertHumidityIfOutOfLimits(string[] values, int index)
        {
            if (int.Parse((values[index].Split('%'))[0]) > 90)
            {
                return "Humidity reached Error level:" + values[index];
            }
            else if (int.Parse((values[index].Split('%'))[0]) > 70)
            {
                return "Humidity reached Warning level:" + values[index];
            }
            return null;
        }
        public void PrintOnConsole(string message)
        {
            if (message != null)
                _output.WriteLine(message);
        }

        static void Main()
        {
            Receiver r = new Receiver();
            r.WhenGetPropertyNames();
            r.WhenAssignIndexToProperties();
            r.WhenGetReadingsFromSensorAndAnalyze();
        }
    }
}
