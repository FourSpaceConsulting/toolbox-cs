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

namespace Fourspace.Toolbox.Util.Threading
{
    public static class RetryUtil
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Default always retry function
        /// </summary>
        /// <param name="i"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool AlwaysRetry(int i, Exception e) { return true; }

        /// <summary>
        /// Timed Retry
        /// Calls tryFunction, if an exception is thrown it will re-attempt with a delay.
        /// Before each retry, the retryFunction is called with the retry number and exception and will throw if it returns false.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="tryFunction"></param>
        /// <param name="retryFunction"></param>
        /// <param name="retries"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static R TimedRetry<R>(Func<R> tryFunction, Func<int, Exception, bool> retryFunction, RetrySettings settings)
        {
            R result = default(R);
            int retryCount = 0;
            int delay = settings.Delay;
            bool retry = false;
            do
            {
                try
                {
                    result = tryFunction();
                    retry = false;
                }
                catch (Exception e)
                {
                    if (Logger.IsWarnEnabled) Logger.Warn("Exception caught with retry [" + retryCount + "/" + settings.NumberRetries + "]");
                    retry = retryCount++ < settings.NumberRetries;
                    // do retry if within limit and retry function returns true
                    if (!retry || !retryFunction(retryCount, e)) throw;
                    if (delay > 0) ThreadUtil.Await(delay);
                    // increment delay if factor supplied
                    if (settings.DelayFactor != null) delay *= settings.DelayFactor.Value;
                }

            } while (retry);
            return result;
        }

        /// <summary>
        /// Timed Retry
        /// Calls tryAction, if an exception is thrown it will re-attempt with a delay.
        /// Before each retry, the retryFunction is called with the retry number and exception and will throw if it returns false.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="tryAction"></param>
        /// <param name="retryFunction"></param>
        /// <param name="retries"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static void TimedRetry(Action tryAction, Func<int, Exception, bool> retryFunction, RetrySettings settings)
        {
            TimedRetry<object>(() => { tryAction(); return null; }, retryFunction, settings);
        }

    }
}
