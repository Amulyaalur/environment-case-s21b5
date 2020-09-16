using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    public struct Properties
    {
        public string property_Name;

    }

    class Program
    {
        static List<Properties> Properties_list = new List<Properties>();
        static string[] Properties_names;

        static string[] SplitLine(string line)
        {
            string[] split = line.Split(',');
            return split;
        }
        static void GetPropertyNames()
        {
            string line = string.Empty;
            line = Console.ReadLine();
            Properties_names = SplitLine(line);

        }
        static void AssignIndexToProperties()
        {
            for (int i = 0; i < Properties_names.Length; i++)
            {
                Properties temp;
                temp.property_Name = Properties_names[i];
                Properties_list.Add(temp);
            }

        }
        static void GetReadingsFromSensorAndAnalyze()
        {
            string line = string.Empty;
            line = Console.ReadLine();

            while (!line.Equals(""))
            {

                string[] values;
                values = SplitLine(line);
                AnalyzeTemperature(values);
                AnalyzeHumidity(values);
                line = Console.ReadLine();
            }
        }
        static int getIndex(string property_name)
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
        static void AnalyzeTemperature(string[] values)
        {
            int index = getIndex("Temperature");
            if (index >= 0)
            {
                if (int.Parse((values[index].Split('C'))[0]) > 40)
                {
                    Console.WriteLine("Temperature reached High Error level:" + values[index]);
                }
                else if (int.Parse((values[index].Split('C'))[0]) > 37)
                {
                    Console.WriteLine("Temperature reached High Warning level:" + values[index]);
                }
                else if (int.Parse((values[index].Split('C'))[0]) < 0)
                {
                    Console.WriteLine("Temperature reached Low Error level:" + values[index]);
                }
                else if (int.Parse((values[index].Split('C'))[0]) < 4)
                {
                    Console.WriteLine("Temperature reached Low Warning level:" + values[index]);
                }
            }

        }
        static void AnalyzeHumidity(string[] values)
        {
            int index = getIndex("Humidity");
            //Console.WriteLine(index);
            if (index >= 0)
            {
                if (int.Parse((values[index].Split('%'))[0]) > 90)
                {
                    Console.WriteLine("Humidity reached Error level:" + values[index]);
                }
                else if (int.Parse((values[index].Split('%'))[0]) > 70)
                {
                    Console.WriteLine("Humidity reached Warnig level:" + values[index]);
                }
            }


        }
        static void PrintProperties()
        {
            for (int i = 0; i < Properties_list.Count; i++)
            {
                Console.Write(Properties_list[i].property_Name + ",");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            GetPropertyNames();
            AssignIndexToProperties();
            //PrintProperties();
            GetReadingsFromSensorAndAnalyze();

        }
    }
}
