/*
 Reads the CSV file and converts it to String
 */

using System.IO;

namespace Sensor
{
    public static class ReadCsv
    {
        /*
         return: Data in string format which is read from CSV file.
         */
        public static string WhenReturnStringFromCsv(string datafile, out bool success)
        {
            string s = "";
            try
            {
                using (var read = new StreamReader(datafile))
                {
                    while (!read.EndOfStream)
                    {
                        var splits = read.ReadLine();

                        s += splits + "\n";
                    }

                    success = true;
                }
            }
            catch (FileNotFoundException)
            {

                success = false;
            }

            return s;
        }
    }
}