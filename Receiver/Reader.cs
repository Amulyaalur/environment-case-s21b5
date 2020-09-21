using System;
using System.IO;
using System.Threading;
namespace Receiver
{
    class Reader
    {
        private static readonly int TimeOutMillisecond = 15000;
        private readonly TextReader _input;
        private readonly AutoResetEvent _getInput;
        private readonly AutoResetEvent _gotInput;
        private string _inputFromConsole;
        private Thread _inputThread;
        private static bool _running;
        public Reader()
        {
            _input = Console.In;
            _getInput = new AutoResetEvent(false);
            _gotInput = new AutoResetEvent(false);
        }
        public Reader(TextReader input)
        {
            _input = input;
            _getInput = new AutoResetEvent(false);
            _gotInput = new AutoResetEvent(false);
        }
        private void WhenReader()
        {
            while (_running)
            {
                _getInput.WaitOne();
                _inputFromConsole = _input.ReadLine();
                _gotInput.Set();
            }
        }
        public string WhenReadLine()
        {
            _running = true;
            _inputThread = new Thread(WhenReader) { IsBackground = true };
            _inputThread.Start();
            _getInput.Set();
            bool success = _gotInput.WaitOne(TimeOutMillisecond);

            _running = false;
            if (success)
                return _inputFromConsole;
            else
                throw new TimeoutException("User did not provide input within the time limit.");
        }
    }
}