/*
 Sender : Which acts as a Sensor and Simulates data to send to Receiver
 Modules included : ReadCsvData,ReadCsv,DynamicData
*/
 
using System;
using System.Collections.Generic;
using System.Threading;

namespace Sender
{ 
    public static class Program
    {
        public static int PeriodicTime = 5000;
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
           
                string datafile = "data.csv"; string filterData = ReadCsv.WhenReturnStringFromCsv(datafile,out bool csvpresentFlag); List<string> finalData = FilterCsvData.WhenCreateDataSet(filterData); WhenSendDataToReceiver(finalData);
                DynamicData.WhenSendDynamicDataToReceiver(10, csvpresentFlag);
        }
    }
}