using Xunit;

namespace Sender.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WhenCsVisReadThenCheckStringOutput()
        {
            var datafile = "D:/a/environment-case-s21b5/environment-case-s21b5/dataForUnitTest.csv";
            var stringOutput = Program.WhenReturnStringFromCsv(datafile);
            Assert.True(stringOutput.Equals(
                "Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n,,,\n32C,20%,15-09-2020,12:10pm\n,,,\n40C,70%,15-09-2020,1:10pm\n42C,20%,15-09-2020,2:10pm\n"));
        }

        [Fact]
        public void WhenParametersAreSentOnlyThenCheckOutput()
        {
            var finalData = Program.WhenCreateDataSet("Temperature, Humidity, Date, Time");

            Assert.True(finalData[0].Equals("Temperature, Humidity, Date, Time"));
        }

        [Fact]
        public void WhenInputIsEmptyThenDoNotAddItToOutput()
        {
            var finalDatas = Program.WhenCreateDataSet(",,,");

            Assert.True(finalDatas.Count == 0);
        }

        [Fact]
        public void WhenSendDataToReceiverThenValidateSentOutput()
        {
            var finalData =
                Program.WhenCreateDataSet("Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n,,,\n");
            var finalDataSent = Program.WhenSendDataToReceiver(finalData);
            Assert.True(finalDataSent[0].Equals("Temperature,Humidity,Date,Time"));
            Assert.True(finalDataSent[1].Equals("37C,50%,15-09-2020,11:10am"));
            Assert.True(finalDataSent.Count == 2);
        }

        [Fact]
        public void WhenStringIsEmptyThenReturnTrue()
        {
            var emptyOrNot = Program.WhenCheckStringEmpty(",,,");
            Assert.True(emptyOrNot);
        }
    }
}