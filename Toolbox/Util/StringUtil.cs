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

namespace Fourspace.Toolbox.Util
{
    /// <summary>
    /// String utilities.
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// Truncate a nullable string if it is longer than length
        /// </summary>
        /// <param name="v"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Truncate(string v, int length)
        {
            return v == null ? null : v.Length > length ? v.Substring(0, length) : v;
        }

        /// <summary>
        /// Test whether value is numeric.
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>is numeric.</returns>
        public static bool IsDecimal(object value)
        {
            decimal retNum;
            return value == null ? false : Decimal.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        /// <summary>
        /// Test whether value is an integer
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>is integer.</returns>
        public static bool IsInteger(object value)
        {
            long retNum;
            return value == null ? false : long.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        /// <summary>
        /// Determine whether string is base 64
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBase64(string value)
        {

            if (value == null || value.Length == 0 || value.Length % 4 != 0)
                return false;
            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Parse a string value into a decimal, returning null if not possible
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? TryParseDecimal(string value)
        {
            decimal? retValue = null;
            if (value != null)
            {
                decimal result;
                if (decimal.TryParse(value, out result))
                {
                    retValue = result;
                }
            }
            return retValue;
        }

        /// <summary>
        /// Parse a string value into a double, returning null if not possible
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double? TryParseDouble(string value)
        {
            double? retValue = null;
            if (value != null)
            {
                double result;
                if (double.TryParse(value, out result))
                {
                    retValue = result;
                }
            }
            return retValue;
        }


        /// <summary>
        /// Parse a string value into a decimal, returning null if not possible
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? TryParseInteger(string value)
        {
            int? retValue = null;
            if (value != null)
            {
                int result;
                if (int.TryParse(value, out result))
                {
                    retValue = result;
                }
            }
            return retValue;
        }

        /// <summary>
        /// Parse a string value into a decimal, returning null if not possible
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long? TryParseLong(string value)
        {
            long? retValue = null;
            if (value != null)
            {
                long result;
                if (long.TryParse(value, out result))
                {
                    retValue = result;
                }
            }
            return retValue;
        }


    }
}
