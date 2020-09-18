using System;
using System.Collections.Generic;


namespace Sender
{
  public static class FilterCsvData
    {
        public static List<string> WhenCreateDataSet(String dataInString)
        {
            List<string> dataSet = new List<string>();
            var splits = dataInString.Split('\n');

            foreach (var data in splits)
            {
                if (!(WhenCheckStringEmpty(data)))
                {
                    dataSet.Add(data);
                }

            }
            return dataSet;
        }

        public static bool WhenCheckStringEmpty(String dataInString)
        {
            if ((dataInString.Equals(",,,") || dataInString.Equals("")))
                return true;
            return false;
        }
    }
}
