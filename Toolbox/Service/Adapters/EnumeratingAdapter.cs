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
using Fourspace.Toolbox.Util;
using System.Collections.Generic;

namespace Fourspace.Toolbox.Service.Adapters
{
    /// <summary>
    /// Util for creating adapters that infer the generic types
    /// </summary>
    public static class EnumeratingAdapter
    {
        public static IAdapter<IEnumerable<I>, IReadOnlyList<O>> Create<I,O>(IAdapter<I, O> adapter)
        {
            return new EnumeratingAdapter<I,O>(adapter);
        }
        public static IAdapter<IEnumerable<I>, C, IReadOnlyList<O>> Create<I, C, O>(IAdapter<I, C, O> adapter)
        {
            return new EnumeratingAdapter<I, C, O>(adapter);
        }
    }

    /// <summary>
    /// Adapter for a list of items
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="O"></typeparam>
    public class EnumeratingAdapter<I, O> : IAdapter<IEnumerable<I>, IReadOnlyList<O>>
    {
        private readonly IAdapter<I, O> adapter;

        public EnumeratingAdapter(IAdapter<I, O> adapter)
        {
            this.adapter = adapter;
        }

        public IReadOnlyList<O> Adapt(IEnumerable<I> items)
        {
            IList<O> output = new List<O>();
            if (CollectionUtil.IsNotNullOrEmpty(items))
            {
                foreach (var item in items)
                {
                    output.Add(adapter.Adapt(item));
                }
            }
            return Immutable.ReadOnlyList(output);
        }
    }

    public class EnumeratingAdapter<I, C, O> : IAdapter<IEnumerable<I>, C, IReadOnlyList<O>>
    {
        private readonly IAdapter<I, C, O> adapter;

        public EnumeratingAdapter(IAdapter<I, C, O> adapter)
        {
            this.adapter = adapter;
        }

        public IReadOnlyList<O> Adapt(IEnumerable<I> items, C context)
        {
            IList<O> output = new List<O>();
            if (CollectionUtil.IsNotNullOrEmpty(items))
            {
                foreach (var item in items)
                {
                    output.Add(adapter.Adapt(item, context));
                }
            }
            return Immutable.ReadOnlyList(output);
        }
    }


}
