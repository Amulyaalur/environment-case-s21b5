/*
 * Receives data from sensor and analyzes it
 * by controlling all modules
 *
 * 
 */

using System;
using System.IO;


namespace EnvironmentMonitor
{
    
    public class EnvironmentMonitor
    {
        public readonly Analyzer AnalyzerObj=new Analyzer();

        //reader is class to handle read line method
        private readonly Reader _reader;

        public EnvironmentMonitor()
        {
            _reader = new Reader();
        }
        public EnvironmentMonitor(TextReader input)
        {
            _reader = new Reader(input);
        }
        
        public string[] WhenToSplitLine(string line)
        {
            string[] split = line.Split(',');
            return split;
        }
        //Adding property names to properties list
        public void WhenGetPropertyNamesThenSetPropertyNames()
        {
            string line = _reader.WhenReadLine();
            var propertiesNames = WhenToSplitLine(line);
            AnalyzerObj.SetProperties(propertiesNames);
           
        }
        public bool WhenGetReadingsFromSensorThenAnalyze()
        {
            string line = _reader.WhenReadLine();
            while (line != null && !line.Equals(""))
            {
                var values = WhenToSplitLine(line);
                AnalyzerObj.WhenAnalyzeTemperature(values);
                AnalyzerObj.WhenAnalyzeHumidity(values);
                line = _reader.WhenReadLine();
            }
            return true;
        }
        
        //Controls receiver
        static void Main()
        {
            EnvironmentMonitor r = new EnvironmentMonitor(); try
            { r.WhenGetPropertyNamesThenSetPropertyNames();
                r.WhenGetReadingsFromSensorThenAnalyze(); }
            catch (TimeoutException) { AlertWrapper.PrintOnConsole("Sensor is disconnected"); }
        }
    }
}
