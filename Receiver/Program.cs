using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Receiver
{
    public struct Properties
    {
        public string PropertyName;

    }

    public class Receiver
    {
        private static readonly int TimeOutMillisecond = 15000;

        public readonly List<Properties> PropertiesList = new List<Properties>();
        private static string[] _propertiesNames;
        private readonly TextReader _input;
        private readonly TextWriter _output;

        private readonly AutoResetEvent _getInput;
        private readonly AutoResetEvent _gotInput;
        private string _inputFromConsole;

        public Receiver() : this(Console.In, Console.Out)
        {
            _getInput = new AutoResetEvent(false);
            _gotInput = new AutoResetEvent(false);
            var inputThread = new Thread(WhenReader) { IsBackground = true };
            inputThread.Start();
        }
        public Receiver(TextReader input, TextWriter output)
        {
            this._input = input;
            this._output = output;
            _getInput = new AutoResetEvent(false);
            _gotInput = new AutoResetEvent(false);
            var inputThread = new Thread(WhenReader) { IsBackground = true };
            inputThread.Start();
        }
        private void WhenReader()
        {
            while (true)
            {
                _getInput.WaitOne();
                _inputFromConsole = _input.ReadLine();
                _gotInput.Set();
            }
        }

        private string WhenReadLine()
        {

            _getInput.Set();
            bool success = _gotInput.WaitOne(TimeOutMillisecond);

            if (success)
                return _inputFromConsole;
            else
                throw new TimeoutException("User did not provide input within the time limit.");


        }
        public string[] WhenSplitLine(string line)
        {
            string[] split = line.Split(',');
            return split;
        }
        public void WhenGetPropertyNames()
        {
            string line = WhenReadLine();
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
            string line = WhenReadLine();

            while (line != null && !line.Equals(""))
            {
                var values = WhenSplitLine(line);
                PrintOnConsole(WhenAnalyzeTemperature(values));
                PrintOnConsole(WhenAnalyzeHumidity(values));
                line = WhenReadLine();
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
            try
            {
                r.WhenGetPropertyNames();
                r.WhenAssignIndexToProperties();
                r.WhenGetReadingsFromSensorAndAnalyze();
            }
            catch (TimeoutException)
            { r._output.WriteLine("Sender is disconnected"); }
        }
    }
}
