using System;
using System.Collections.Generic;
using System.IO;


namespace Receiver
{
    public struct Properties
    {
        public string property_Name;

    }

    public class Receiver
    {
        public List<Properties> Properties_list = new List<Properties>();
        static string[] Properties_names;
        public readonly TextReader input;
        public readonly TextWriter output;
        public Receiver() : this(Console.In, Console.Out)
        {

        }
        public Receiver(TextReader input, TextWriter output)
        {
            this.input = input;
            this.output = output;
        }
        public string[] SplitLine(string line)
        {
            string[] split = line.Split(',');
            return split;
        }
        public void GetPropertyNames()
        {
            string line = input.ReadLine();
            Properties_names = SplitLine(line);

        }
        public void AssignIndexToProperties()
        {
            for (int i = 0; i < Properties_names.Length; i++)
            {
                Properties temp;
                temp.property_Name = Properties_names[i];
                Properties_list.Add(temp);
            }

        }
        public bool GetReadingsFromSensorAndAnalyze()
        {
            string line = input.ReadLine();

            while (!line.Equals(""))
            {
                string[] values;
                values = SplitLine(line);
                PrintOnConsole(AnalyzeTemperature(values));
                PrintOnConsole(AnalyzeHumidity(values));
                line = input.ReadLine();
            }
            return true;
        }
        int GetIndex(string property_name)
        {
            int id = -1;
            for (uint i = 0; i < Properties_list.Count; i++)
            {
                if (Properties_list[(int)i].property_Name.Equals(property_name))
                {
                    id = (int)i;
                    break;
                }
            }
            return id;
        }
        public string AnalyzeTemperature(string[] values)
        {
            int index = GetIndex("Temperature");
            if (index >= 0)
            {
                return AlertTemperatureIfOutOfLimits(values, index);

            }
            else
            {
                return "CSV does not contain Temperature property.";
            }

        }
        public string AlertTemperatureIfOutOfLimits(string[] values, int index)
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
                return "Temperature reached High Error level:" + values[index].ToString();
            }
            else
            {
                return "Temperature reached High Warning level:" + values[index].ToString();
            }
        }
        string AlertForLowerLimitsForTemperature(string[] values, int index)
        {
            if (int.Parse((values[index].Split('C'))[0]) < 0)
            {
                return "Temperature reached Low Error level:" + values[index].ToString();
            }
            else
            {
                return "Temperature reached Low Warning level:" + values[index].ToString();
            }
        }

        public string AnalyzeHumidity(string[] values)
        {
            int index = GetIndex("Humidity");
            //Console.WriteLine(index);
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
                return "Humidity reached Error level:" + values[index].ToString();
            }
            else if (int.Parse((values[index].Split('%'))[0]) > 70)
            {
                return "Humidity reached Warnig level:" + values[index].ToString();
            }
            return null;
        }
        /*
        void PrintProperties()
        {
            for(int i=0;i<Properties_list.Count;i++)
            {
                Console.Write(Properties_list[i].property_Name+",");
            }
            Console.WriteLine();
        }*/
        public void PrintOnConsole(string message)
        {
            if (message != null)
                output.WriteLine(message);
        }

        private static void Main()
        {
            Receiver r = new Receiver();
            r.GetPropertyNames();
            r.AssignIndexToProperties();
            //PrintProperties();
            r.GetReadingsFromSensorAndAnalyze();

        }
    }
}
