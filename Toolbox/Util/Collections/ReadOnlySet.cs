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
using System.Collections;
using System.Collections.Generic;

namespace Fourspace.Toolbox.Util
{
    /// <summary>
    /// Read only implementation of set.
    /// Wraps an existing set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReadOnlySet<T> : ISet<T>
    {
        private readonly ISet<T> set;
        public ReadOnlySet(ISet<T> set)
        {
            this.set = set;
        }

        public int Count
        {
            get
            {
                return set.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public bool Add(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        public void Clear()
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        public bool Contains(T item)
        {
            return set.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            set.CopyTo(array, arrayIndex);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return set.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return set.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return set.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return set.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return set.Overlaps(other);
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return set.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return set.GetEnumerator();
        }
    }
}
