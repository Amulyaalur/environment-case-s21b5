using System;
using System.Collections.Generic;
using System.IO;

namespace Sender

{
    public static class Program

    {
        public static List<string> WhenCreateDataSet(String dataInString)
        {
            List<string> dataSet = new List<string>();
            var splits = dataInString.Split('\n');

            foreach (var data in splits)
            {
                if (!(WhenCheckStringEmpty(data)))
                {
                    dataSet.Add(data);
                }

            }
            return dataSet;
        }

        public static bool WhenCheckStringEmpty(String dataInString)
        {
            if ((dataInString.Equals(",,,") || dataInString.Equals("")))
                return true;
            return false;
        }

        public static List<string> WhenSendDataToReceiver(List<string> data)
        {
            List<String> finalDataSentToReceiver = new List<string>();
            foreach (var stringInData in data)
            {
                Console.WriteLine(stringInData);
                finalDataSentToReceiver.Add(stringInData);
            }
            Console.WriteLine("\n");

            return finalDataSentToReceiver;
        }

        public static string WhenReturnStringFromCsv(string datafile)
        {
            string s = "";
            try
            {
                using (var rd = new StreamReader(datafile))
                {
                    while (!rd.EndOfStream)
                    {
                        var splits = rd.ReadLine();

                        s += splits + "\n";
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
            }

            return s;
        }
        static void Main()
        {
            string datafile = "data.csv"; string data1 = WhenReturnStringFromCsv(datafile); List<string> finalData = WhenCreateDataSet(data1); WhenSendDataToReceiver(finalData);
        }
    }
}
