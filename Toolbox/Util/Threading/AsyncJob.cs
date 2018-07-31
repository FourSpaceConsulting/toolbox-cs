/*
MIT License

Copyright (c) 2017 Richard Steward

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using log4net;
using System;
using System.Threading;

namespace Fourspace.Toolbox.Util.Threading
{
    public class AsyncJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly object Lock = new object();
        private readonly Action action;
        public bool running { get; private set; }
        public bool stopJob { get; set; }

        public AsyncJob(Action action)
        {
            this.action = action;
            registerShutdown();
        }

        public void Run(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            setStatus(autoEvent, true);
            if (!stopJob)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Logger.Error("Caught exception ", e);
                }
            }
            setStatus(autoEvent, false);
        }

        private void setStatus(AutoResetEvent autoEvent, bool status)
        {
            lock (Lock)
            {
                running = status;
                if (stopJob) autoEvent.Set();
            }
        }

        private void registerShutdown()
        {
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                if (e.SpecialKey == ConsoleSpecialKey.ControlBreak)
                {
                    Logger.Info("Ctrl-Break: cooperative shutdown");
                    Thread t = new Thread(ControlBreakShutdown);
                    t.Start();
                    t.Join();
                }
                if (e.SpecialKey == ConsoleSpecialKey.ControlC)
                {
                    e.Cancel = true; // tell the CLR to keep running
                    Logger.Info("Ctrl-C: cooperative shutdown");
                    new Thread(ControlCShutdown).Start();
                }
            };
        }

        private void ControlBreakShutdown()
        {
            stopJobWaitAndExit(1);
        }
        private void ControlCShutdown()
        {
            stopJobWaitAndExit(2);
        }
        private void stopJobWaitAndExit(int code)
        {
            stopJob = true;
            if (running)
            {
                Logger.Info("Waiting for job");
                ThreadUtil.AwaitTrue(() => running, 0, 500);
            }
            Logger.Info("Asynchronous shutdown started");
            Environment.Exit(code);
        }

    }
}
