using NuGet.Frameworks;
using System.Collections.Generic;
using Xunit;

namespace Sender.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WhenCsVisReadThenCheckStringOutput()
        {
            string datafile = "D:/a/environment-case-s21b5/environment-case-s21b5/dataForUnitTest.csv";
            string stringOutput = Program.ReturnStringFromCSV(datafile);
            Assert.True(stringOutput.Equals("Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n,,,\n32C,20%,15-09-2020,12:10pm\n,,,\n40C,70%,15-09-2020,1:10pm\n42C,20%,15-09-2020,2:10pm\n"));
        }
        [Fact]
        public void WhenParametersAreSentOnlyThenCheckOutput()
        {
            List<string> finalData = Program.CreateDataset("Temperature, Humidity, Date, Time");

            Assert.True(finalData[0].Equals("Temperature, Humidity, Date, Time"));
        }

        [Fact]
        public void WhenInputIsEmptyThenDoNotAddItToOutput()
        {
            List<string> finalData = Program.CreateDataset(",,,");

            Assert.True(finalData.Count == 0);
        }

        [Fact]
        public void WhenSendDataToReceiverThenValidateSentOutput()
        {
            List<string> finalData = Program.CreateDataset("Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n,,,\n");
            List<string> finalDataSent = Program.WhenSendDataToReceiver(finalData);
            Assert.True(finalDataSent[0].Equals("Temperature,Humidity,Date,Time"));
            Assert.True(finalDataSent[1].Equals("37C,50%,15-09-2020,11:10am"));
            Assert.True(finalDataSent.Count == 2);
        }
        [Fact]
        public void WhenStringisEmptyThenReturnTrue()
        {
           bool EmptyorNot = Program.WhenCheckStringEmpty(",,,");
            Assert.True(EmptyorNot);
        }
    }
}
