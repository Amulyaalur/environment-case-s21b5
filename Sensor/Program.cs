/*
 Sender : Which acts as a Sensor and Simulates data to send to Receiver
 Modules included : ReadCsvData,ReadCsv,DynamicData
*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace Sensor
{
    public static class Program
    {
        public static int PeriodicTime = 2000;
        public static void WhenFetchCurrentDateTime()
        {
            DateTime now = DateTime.Now;
            Console.Write("," + now.ToShortDateString());
            Console.WriteLine("," + now.ToShortTimeString());
        }

        /*
          Data is Sent to the Receiver for every 5 seconds
          return: List of Data sent to Receiver
         */

        public static List<string> WhenSendDataToReceiver(List<string> data)
        {
            //count is to track line number if line number is 0 then send it directly
            int count = 0;
            List<String> finalDataSentToReceiver = new List<string>();
            foreach (var stringInData in data)
            {
                if (count > 0)
                {
                    Console.Write(stringInData);
                    WhenFetchCurrentDateTime();
                }
                else
                {
                    Console.WriteLine(stringInData);
                }

                count++;
                Thread.Sleep(PeriodicTime);
                finalDataSentToReceiver.Add(stringInData);
            }
            return finalDataSentToReceiver;
        }


        /*
            Main Function Controls all the Modules included
         */

        static void Main()
        {

            string datafile = "data.csv"; string filterData = ReadCsv.WhenReturnStringFromCsv(datafile, out bool csvpresentFlag); List<string> finalData = FilterCsvData.WhenCreateDataSet(filterData); WhenSendDataToReceiver(finalData);
            DynamicData.WhenSendDynamicDataToReceiver(2, csvpresentFlag);Thread.Sleep(12000);Console.WriteLine("\n");
        }
    }
}