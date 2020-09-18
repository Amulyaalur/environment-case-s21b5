using System.IO;


namespace Sender
{
    public static class ReadCsv
    {
        public static string WhenReturnStringFromCsv(string datafile,out bool success)
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
