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
using System.Collections.Generic;

namespace Fourspace.Toolbox.Util
{
    public class Pair<T, U> : IPair<T, U>
    {
        public T First { get; }
        public U Second { get; }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        #region Equality
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;
            var other = (Pair<T, U>)obj;
            return IsEqual(other);
        }

        public bool Equals(Pair<T, U> other)
        {
            if (other == null) return false;
            if (object.ReferenceEquals(other, this)) return true;
            return IsEqual(other);
        }

        private bool IsEqual(Pair<T, U> other)
        {
            return EqualityComparer<T>.Default.Equals(First, other.First) &&
                EqualityComparer<U>.Default.Equals(Second, other.Second);
        }
        #endregion

        #region hash code
        public override int GetHashCode()
        {
            return HashCodeUtil.HashCode(First, Second);
        }
        #endregion

    }
}
