/*
 Filters the Input data containing empty records and Replaces with "NA" 
 1. If one of the property is empty.
 2. If Temperature is out of limit. 
 
 */
using System;
using System.Collections.Generic;


namespace Sender
{
    public static class FilterCsvData
    {
        /*
         return: Filtered data 
         */
        public static List<string> WhenCreateDataSet(String dataInString)
        {
            List<string> dataSet = new List<string>();
            var splits = dataInString.Split('\n');
            dataSet.Add(splits[0]);
            for (var i = 1; i < splits.Length; i++)
            {
                if (!(WhenCheckStringEmpty(splits[i])))
                {
                    string datatosend = WhenEitherTemperatureOrHumidityIsEmpty(splits[i]);
                    string correcteddataset = WhenTemperatureExceedsIdealLimits(datatosend);
                    dataSet.Add(correcteddataset);
                }
            }
            return dataSet;
        }
/*
   return : True - If Input data is empty
            False - If Input data is not empty

 */
        public static bool WhenCheckStringEmpty(String dataInString)
        {
            if ((dataInString.Equals(",") || dataInString.Equals("")))
                return true;
            return false;
        }
/*

 return: Replaces with "NA" if one of the property is empty

 */
        public static string WhenEitherTemperatureOrHumidityIsEmpty(String dataInString)
        {
            string[] data = dataInString.Split(',');
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i].Equals(""))
                {
                    data[i] = "NA";
                }
            }

            return string.Join(",", data);
        }
/*

 return: Temperature replaced with "NA" if Temperature exceeds Ideal limits

 */
        public static string WhenTemperatureExceedsIdealLimits(string dataInString)
        {
            string[] data = dataInString.Split(',');
            if (!(data[0].Equals("NA")))
            {
                string d = data[0].Split('C')[0];
                if (int.Parse(d) > 500 || int.Parse(d) < -500)
                {
                    data[0] = "NA";
                }
            }
            return string.Join(",", data);
        }
    }
}