using System;
using System.Collections.Generic;
using System.Threading;

namespace Sender
{ 
    public static class Program
    { 
        public static void WhenFetchCurrentDateTime()
        {
            DateTime now = DateTime.Now;
            Console.Write("," + now.ToShortDateString());
            Console.WriteLine("," + now.ToShortTimeString());
        }

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
               Thread.Sleep(5000);
                finalDataSentToReceiver.Add(stringInData);
            }
            return finalDataSentToReceiver;
        }

     
    
        static void Main()
        {
           
                string datafile = "data.csv"; string filterdata = ReadCsv.WhenReturnStringFromCsv(datafile,out bool csvpresentflag); List<string> finalData = FilterCsvData.WhenCreateDataSet(filterdata); WhenSendDataToReceiver(finalData);
                DynamicData.WhenSendDynamicDataToReceiver(10, csvpresentflag);
        }
    }
}