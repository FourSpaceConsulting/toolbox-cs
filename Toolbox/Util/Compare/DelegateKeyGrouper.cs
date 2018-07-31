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

namespace Fourspace.Toolbox.Util.Compare
{
    public class DelegateKeyGrouper<T,K,I> : IKeyGrouper<T,K,I>
    {
        private readonly Func<T, K> keyCreator;
        private readonly Func<T, int, I> indexCreator;

        public DelegateKeyGrouper(Func<T, K> keyCreator, Func<T, int, I> indexCreator)
        {
            this.keyCreator = keyCreator;
            this.indexCreator = indexCreator;
        }

        public GroupedItems<T, K, I> GroupItems(IEnumerable<T> items)
        {
            List<I> nullTo = null;
            List<Pair<I, T>> nonKeyTo = null;
            IDictionary<K, IList<Pair<I, T>>> groupItems = null;
            // Perform grouping
            if (CollectionUtil.IsNotNullOrEmpty(items))
            {
                int i = 0;
                foreach (var item in items)
                {
                    I index = indexCreator(item, i++);
                    // register any nulls
                    if (item == null)
                    {
                        if (nullTo == null) nullTo = new List<I>();
                        nullTo.Add(index);
                    }
                    else
                    {
                        K key = keyCreator(item);
                        if (key == null)
                        {
                            // register null key
                            if (nonKeyTo == null) nonKeyTo = new List<Pair<I, T>>();
                            nonKeyTo.Add(new Pair<I, T>(index, item));
                        }
                        else
                        {
                            // register keyed item
                            if (groupItems == null) groupItems = new Dictionary<K, IList<Pair<I, T>>>();
                            CollectionUtil.AddToMappedList(groupItems, key, new Pair<I, T>(index, item));
                        }
                    }
                }
            }
            return new GroupedItems<T, K, I>()
            {
                NullItems = nullTo ?? Immutable.ReadOnlyList<I>(),
                NullKeyItems = nonKeyTo ?? Immutable.ReadOnlyList<Pair<I, T>>(),
                GroupItems = groupItems ?? Immutable.Dictionary<K, IList<Pair<I, T>>>()
            };
        }
    }
}
