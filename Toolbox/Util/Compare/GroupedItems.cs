﻿/*
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

namespace Fourspace.Toolbox.Util.Compare
{
    public class GroupedItems<T, K, I>
    {
        public IReadOnlyList<I> NullItems { get; set; }
        public IReadOnlyList<Pair<I, T>> NullKeyItems { get; set; }
        public IDictionary<K, IList<Pair<I, T>>> GroupItems { get; set; }
    }

    public class GroupedItems<L, R, K, I>
    {
        public IReadOnlyList<I> NullLeft { get; set; }
        public IReadOnlyList<I> NullRight { get; set; }
        public IReadOnlyList<Pair<I, L>> NullKeyLeft { get; set; }
        public IReadOnlyList<Pair<I, R>> NullKeyRight { get; set; }
        public IDictionary<K, IList<Pair<I, L>>> NonMatchLeft { get; set; }
        public IDictionary<K, IList<Pair<I, R>>> NonMatchRight { get; set; }
        public IDictionary<K, Pair<IList<Pair<I, L>>, IList<Pair<I, R>>>> GroupItems { get; set; }
    }

}
