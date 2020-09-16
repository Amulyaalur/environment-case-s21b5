using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    class Program
    {
         static void Main(string[] args)
        {
            var column1 = new List<string>();
            var column2 = new List<string>();
            var column3 = new List<string>();
            var column4 = new List<string>();

            using (var rd = new StreamReader("data.csv"))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(',');
                    column1.Add(splits[0]);
                    column2.Add(splits[1]);
                    column3.Add(splits[2]);
                    column4.Add(splits[3]);

                }
            }
            // print Temperature
            Console.WriteLine("Column 1: ");
            foreach (var element in column1)
                Console.WriteLine(element);

            // print Humidity
            Console.WriteLine("Column 2:");
            foreach (var element in column2)
                Console.WriteLine(element);

            //print Date
            Console.WriteLine("Column 3:");
            foreach (var element in column3)
                Console.WriteLine(element);

            //print Time
            Console.WriteLine("Column 4:");
            foreach (var element in column4)
                Console.WriteLine(element);

        }
    }
}
