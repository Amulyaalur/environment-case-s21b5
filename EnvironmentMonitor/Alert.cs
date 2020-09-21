/*
 * Class Alert is responsible for sending alerts to user
 * Here mode of alert is alerting on console
 */

using System;
using System.IO;

namespace EnvironmentMonitor
{
    public class Alert
    {
        private readonly TextWriter _output;
        protected string FinalStringPrinted = "";
        public Alert()
        {
            _output = Console.Out;
        }

        public Alert(TextWriter output)
        {
            _output = output;
        }
        public virtual void PrintOnConsole(string message)
        {
            if (message != null)
                _output.WriteLine(message);
        }

        public string FinalStringPrintedOnConsole => FinalStringPrinted;
    }

    //Alert Mock is created to send data to unit test module
    public class AlertMock : Alert
    {
        public override void PrintOnConsole(string message)
        {
            FinalStringPrinted = message;
        }
    }
}