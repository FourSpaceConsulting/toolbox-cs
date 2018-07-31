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

namespace Fourspace.Toolbox.Service
{
    public interface IDateTimeSerializer
    {
        /// <summary>
        /// Convert a date to a string.
        /// </summary>
        /// <param name="dt">date</param>
        /// <returns>string</returns>
        string DateToString(DateTime? dt);

        /// <summary>
        /// Convert a date to a string.
        /// </summary>
        /// <param name="dt">date</param>
        /// <returns>string</returns>
        string DateToString(DateTime dt);

        /// <summary>
        /// Convert a date to a string.
        /// </summary>
        /// <param name="dt">date</param>
        /// <returns>string</returns>
        string DateTimeToString(DateTime? dt);

        /// <summary>
        /// Convert a date to a string.
        /// </summary>
        /// <param name="dt">date</param>
        /// <returns>string</returns>
        string DateTimeToString(DateTime dt);

        /// <summary>
        /// Convert a string date to a DateTime.
        /// </summary>
        /// <param name="dt">date</param>
        /// <returns>string</returns>
        DateTime? StringToDate(string dt);

        /// <summary>
        /// Convert a string date to a DateTime.
        /// </summary>
        /// <param name="dt">date</param>
        /// <returns>string</returns>
        DateTime? StringToDateTime(string dt);

        /// <summary>
        /// Test whether can convert a string date to a Date.
        /// </summary>
        /// <param name="dt">date</param>
        /// <param name="converted">converted</param>
        /// <returns>string</returns>
        bool ValidStringToDate(string dt, out DateTime converted);

        /// <summary>
        /// Test whether can convert a string date to a DateTime.
        /// </summary>
        /// <param name="dt">date</param>
        /// <param name="converted">converted</param>
        /// <returns>string</returns>
        bool ValidStringToDateTime(string dt, out DateTime converted);

    }
}
