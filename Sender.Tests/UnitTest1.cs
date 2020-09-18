using Xunit;

namespace Sender.Tests
{
    public class UnitTest1
    {
        
        [Fact]
        public void WhenCsVisReadThenCheckStringOutput()
        {
            var datafile = "D:/a/environment-case-s21b5/environment-case-s21b5/dataForUnitTest.csv";
            var stringOutput = ReadCsv.WhenReturnStringFromCsv(datafile,out _);
            Assert.True(stringOutput.Equals(
                "Temperature,Humidity,Date,Time\n37C,50%\n,,,\n32C,20%\n,,,\n40C,70%\n42C,20%\n"));
        }
        
        [Fact]
        public void WhenParametersAreSentOnlyThenCheckOutput()
        {
            var finalData = FilterCsvData.WhenCreateDataSet("Temperature, Humidity, Date, Time");

            Assert.True(finalData[0].Equals("Temperature, Humidity, Date, Time"));
        }

        [Fact]
        public void WhenInputIsEmptyThenDoNotAddItToOutput()
        {
            var finalDatas = FilterCsvData.WhenCreateDataSet(",,,");

            Assert.True(finalDatas.Count == 0);
        }

        [Fact]
        public void WhenSendDataToReceiverThenValidateSentOutput()
        {
            var finalData =
                FilterCsvData.WhenCreateDataSet("Temperature,Humidity,Date,Time\n37C,50%,15-09-2020,11:10am\n,,,\n");
            var finalDataSent = Program.WhenSendDataToReceiver(finalData);
            Assert.True(finalDataSent[0].Equals("Temperature,Humidity,Date,Time"));
            Assert.True(finalDataSent[1].Equals("37C,50%,15-09-2020,11:10am"));
            Assert.True(finalDataSent.Count == 2);
        }

        [Fact]
        public void WhenStringIsEmptyThenReturnTrue()
        {
            var emptyOrNot = FilterCsvData.WhenCheckStringEmpty(",,,");
            Assert.True(emptyOrNot);
        }

        [Fact]
        public void WhenFileNotFoundThenThrowAnException()
        {

            string datafile = "FileNotPresent.csv";
            ReadCsv.WhenReturnStringFromCsv(datafile,out bool status);
            Assert.False(status);
            
        }

        [Fact]
        public void WhenDataIsCreatedDynamicallyThenCheckValidDataIsSent()
        {

            Assert.True(DynamicData.WhenSendDynamicDataToReceiver(2,true));
        }

        [Fact]
        public void WhenCsvFileNotPresentSendDynamicDataToReceiver()
        {
            Assert.True(DynamicData.WhenSendDynamicDataToReceiver(2, false));
        }
    }

}
