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
using System;
using System.Threading;

namespace Fourspace.Toolbox.Util.Threading
{
    public static class ThreadUtil
    {

        /// <summary>
        /// Stops the thread for until dueTime has passed
        /// </summary>
        /// <param name="dueTime"></param>
        public static void Await(int dueTime)
        {
            EventWaitHandle autoEvent = new AutoResetEvent(false);
            TimerCallback timerDelegate = new TimerCallback((e) => { ((EventWaitHandle)e).Set(); });
            using (Timer stateTimer = new Timer(timerDelegate, autoEvent, dueTime, Timeout.Infinite))
            {
                // wait for signal
                autoEvent.WaitOne();
            }
        }

        /// <summary>
        /// Await until function returns true
        /// </summary>
        /// <param name="function"></param>
        /// <param name="dueTime"></param>
        public static void AwaitTrue(Func<bool> function, int dueTime, int period)
        {
            EventWaitHandle autoEvent = new AutoResetEvent(false);
            TimerCallback timerDelegate = new TimerCallback((e) => { if (function()) ((EventWaitHandle)e).Set(); });
            using (Timer stateTimer = new Timer(timerDelegate, autoEvent, dueTime, period))
            {
                // wait for signal
                autoEvent.WaitOne();
            }
        }

    }
}
