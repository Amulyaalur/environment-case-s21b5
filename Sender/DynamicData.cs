﻿/*
 Includes a Method that sends Dynamic data to Receiver
 */
using System;
using System.Threading;


namespace Sender
{
   public static class DynamicData
    {
        /*
        Sends Randomly generated data to Receiver.
         */
        public static bool WhenSendDynamicDataToReceiver(int cycle,bool filepass)
        {
            Random randomData = new Random();
            if (filepass == false)
            {
                Console.WriteLine("Temperature,Humidity,Date,Time");
            }

            for (int i = 0; i < cycle; i++)
            {
                var temperature = randomData.Next(200)-100;
                var humidity = randomData.Next(100);
                Thread.Sleep(5000);
                Console.Write(temperature.ToString() + "C" + "," + humidity.ToString() + "%");
                Program.WhenFetchCurrentDateTime();
            }
            Console.WriteLine("\n");
            return true;
        }
    }
}
