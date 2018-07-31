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
using System.Collections.Generic;

namespace Fourspace.Toolbox.Util
{
    public static class HashCodeUtil
    {

        /// <summary>
        /// Generate a hash code.
        /// </summary>
        /// <param name="obj1">object.</param>
        /// <returns>Hash code.</returns>
        public static int HashCode<T>(T obj1)
        {
            unchecked // Overflow is fine, just wrap
            {
                return 493 + EqualityComparer<T>.Default.GetHashCode(obj1);
            }
        }

        /// <summary>
        /// Generate a hash code.
        /// </summary>
        /// <param name="obj1">object.</param>
        /// <param name="obj2">object.</param>
        /// <returns>Hash code.</returns>
        public static int HashCode<T, U>(T obj1, U obj2)
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashcode = 17;
                hashcode = hashcode * 29 + EqualityComparer<T>.Default.GetHashCode(obj1);
                hashcode = hashcode * 29 + EqualityComparer<U>.Default.GetHashCode(obj2);
                return hashcode;
            }
        }

        /// <summary>
        /// Generate a hash code.
        /// </summary>
        /// <param name="obj1">object.</param>
        /// <param name="obj2">object.</param>
        /// <returns>Hash code.</returns>
        public static int HashCode<T, U, V>(T obj1, U obj2, V obj3)
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashcode = 17;
                hashcode = hashcode * 29 + EqualityComparer<T>.Default.GetHashCode(obj1);
                hashcode = hashcode * 29 + EqualityComparer<U>.Default.GetHashCode(obj2);
                hashcode = hashcode * 29 + EqualityComparer<V>.Default.GetHashCode(obj3);
                return hashcode;
            }
        }

        public static int HashCode<T>(params T[] array)
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashcode = 17;
                foreach (var obj in array)
                {
                    hashcode = hashcode * 29 + EqualityComparer<T>.Default.GetHashCode(obj);
                }
                return hashcode;
            }
        }


        /// <summary>
        /// hash code for an array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int HashCode(Array array)
        {
            if (array == null) return 0;
            unchecked // Overflow is fine, just wrap
            {
                int hashcode = 17;
                foreach (var obj in array)
                {
                    hashcode = hashcode * 29 + (obj == null ? 0 : obj.GetHashCode());
                }
                return hashcode;
            }
        }

    }
}
