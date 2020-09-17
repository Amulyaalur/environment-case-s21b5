using System;
using System.Collections.Generic;
using System.IO;


namespace Sender

{
    public class Program

    {
        public static List<string> CreateDataset(String datainstring)
        {
            List<string> dataset = new List<string>();
            var splits = datainstring.Split('\n');

            for (int i = 0; i < splits.Length; i++)
            {
                if (!(WhenCheckStringEmpty(splits[i])))
                {
                    dataset.Add(splits[i]);
                }

            }

            return dataset;
        }

        public static bool WhenCheckStringEmpty(String datainstring)
        {
            if ((datainstring.Equals(",,,") || datainstring.Equals("")))
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

            return finalDataSentToReceiver;
        }

        public static string ReturnStringFromCSV(string datafile)
        {
            string s = "";
            using (var rd = new StreamReader(datafile))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine();

                    s += splits + "\n";
                }
            }
            return s;
        }
        static void Main()
        {
            string datafile = "data.csv";string data1 = ReturnStringFromCSV(datafile);List<string> finalData = CreateDataset(data1); WhenSendDataToReceiver(finalData);
        }
    }
}
