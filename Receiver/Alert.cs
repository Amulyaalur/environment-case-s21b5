using System;
using System.IO;
namespace Receiver
{
    public class Alert
    {
        private readonly TextWriter _output;
        public string FinalStringPrinted = "";
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
    }
    public class AlertChild : Alert
    {

        public override void PrintOnConsole(string message)
        {
            FinalStringPrinted = message;
        }
    }
}