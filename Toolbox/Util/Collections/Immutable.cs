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
using System.Collections.ObjectModel;

namespace Fourspace.Toolbox.Util
{
    public static class Immutable
    {
        #region list
        public static IList<V> List<V>()
        {
            return EmptyReadOnlyIList<V>.Instance;
        }

        public static IReadOnlyList<V> ReadOnlyList<V>()
        {
            return EmptyReadOnlyList<V>.Instance;
        }

        public static IReadOnlyList<V> ReadOnlyList<V>(params V[] items)
        {
            return ReadOnlyList((IList<V>)items);
        }

        public static IReadOnlyList<V> ReadOnlyList<V>(IList<V> items)
        {
            return new ReadOnlyCollection<V>(items);
        }

        public static IReadOnlyList<V> ReadOnlyList<V>(IEnumerable<V> items)
        {
            return new ReadOnlyCollection<V>(new List<V>(items));
        }

        private static class EmptyReadOnlyIList<V>
        {
            private static volatile IList<V> _instance;

            public static IList<V> Instance
            {
                get { return _instance ?? (_instance = new ReadOnlyCollection<V>(new List<V>())); }
            }
        }

        private static class EmptyReadOnlyList<V>
        {
            private static volatile IReadOnlyList<V> _instance;

            public static IReadOnlyList<V> Instance
            {
                get { return _instance ?? (_instance = new ReadOnlyCollection<V>(new List<V>())); }
            }
        }
        #endregion

        #region dictionary
        public static IDictionary<K, V> Dictionary<K, V>()
        {
            return EmptyReadOnlyDictionary<K, V>.Instance;
        }

        public static IDictionary<K, V> Dictionary<K, V>(IDictionary<K, V> dict)
        {
            return new ReadOnlyDictionary<K, V>(dict);
        }

        public static IReadOnlyDictionary<K, V> ReadOnlyDictionary<K, V>(IDictionary<K, V> dict)
        {
            return new ReadOnlyDictionary<K,V>(dict);
        }

        private static class EmptyReadOnlyDictionary<K, V>
        {
            private static volatile IDictionary<K, V> _instance;

            public static IDictionary<K, V> Instance
            {
                get { return _instance ?? (_instance = new ReadOnlyDictionary<K, V>(new Dictionary<K, V>())); }
            }
        }
        #endregion

        #region set
        public static ISet<V> Set<V>()
        {
            return EmptyReadOnlySet<V>.Instance;
        }

        private static class EmptyReadOnlySet<V>
        {
            private static volatile ISet<V> _instance;

            public static ISet<V> Instance
            {
                get { return _instance ?? (_instance = new ReadOnlySet<V>(new HashSet<V>())); }
            }
        }
        #endregion

    }
}
