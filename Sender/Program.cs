using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Sender

{
    class Program

    {
        public static List<string> dataset = new List<string>();
        public static List<string> SimulateDataset(String datafile)
        {
            using (var rd = new StreamReader(datafile))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split('\n');
                    dataset.Add(splits[0]);
                }
            }
            return dataset;
        }

        public static void SendDataToReceiver()
        {
            foreach (var element in dataset)
                Console.WriteLine(element);
            Console.WriteLine("\n");
        }
        static void Main(string[] args)
        {
            string datafile = "data.csv";
            SimulateDataset(datafile);
            SendDataToReceiver();
        }
    }

}
