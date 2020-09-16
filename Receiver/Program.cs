using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = string.Empty;

            while (line != null)
            {
                line = Console.ReadLine();
                Console.WriteLine(line);
            }
        }
    }
}
