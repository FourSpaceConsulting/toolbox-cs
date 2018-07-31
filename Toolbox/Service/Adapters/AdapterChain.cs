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

namespace Fourspace.Toolbox.Service.Adapters
{
    /// <summary>
    /// Adapts a collection of adapters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdapterChain<T> : IAdapter<T, T>
    {
        private readonly IReadOnlyList<IAdapter<T, T>> adapters;

        public AdapterChain(params IAdapter<T,T>[] adapters)
        {
            this.adapters = adapters;
        }

        public AdapterChain(IReadOnlyList<IAdapter<T, T>> adapters)
        {
            this.adapters = adapters;
        }

        public T Adapt(T item)
        {
            T value = item;
            foreach (var adapter in adapters)
            {
                value = adapter.Adapt(value);
            }
            return value;
        }
    }

    public class AdapterChain<I, M, O> : IAdapter<I, O>
    {
        private readonly IAdapter<I, M> inputAdaptor;
        private readonly IAdapter<M, O> outputAdapter;

        public AdapterChain(IAdapter<I, M> inputAdaptor, IAdapter<M, O> outputAdapter)
        {
            this.inputAdaptor = inputAdaptor;
            this.outputAdapter = outputAdapter;
        }

        public O Adapt(I item)
        {
            return outputAdapter.Adapt(inputAdaptor.Adapt(item));
        }
    }

    public class AdapterChain<I, M, O, C> : IAdapter<I, O>
    {
        private readonly IAdapter<I, M> inputAdaptor;
        private readonly IAdapter<M, O> outputAdapter;

        public AdapterChain(IAdapter<I, M> inputAdaptor, IAdapter<M, O> outputAdapter)
        {
            this.inputAdaptor = inputAdaptor;
            this.outputAdapter = outputAdapter;
        }

        public O Adapt(I item)
        {
            return outputAdapter.Adapt(inputAdaptor.Adapt(item));
        }
    }

    /// <summary>
    /// Util to create that infer the generic types
    /// </summary>
    public static class AdapterChain
    {
        /// <summary>
        /// Infer types
        /// </summary>
        /// <param name="inputAdaptor"></param>
        /// <param name="outputAdapter"></param>
        /// <returns></returns>
        public static AdapterChain<I, M, O> Create<I, M, O>(IAdapter<I, M> inputAdaptor, IAdapter<M, O> outputAdapter)
        {
            return new AdapterChain<I, M, O>(inputAdaptor, outputAdapter);
        }

        /// <summary>
        /// Infer types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="adapters"></param>
        /// <returns></returns>
        public static AdapterChain<T> Create<T>(IReadOnlyList<IAdapter<T, T>> adapters)
        {
            return new AdapterChain<T>(adapters);
        }


        /// <summary>
        /// Infer types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="adapters"></param>
        /// <returns></returns>
        public static AdapterChain<T> Create<T>(params IAdapter<T, T>[] adapters)
        {
            return new AdapterChain<T>(adapters);
        }

    }

}
