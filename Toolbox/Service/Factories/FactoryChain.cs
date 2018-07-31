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
namespace Fourspace.Toolbox.Service.Factories
{
    public class FactoryChain
    {
        public static IFactory<T> Create<T,F>(IFactory<F> firstFactory, IFactory<T, F> secondFactory) {
            return new FactoryChain<T, F>(firstFactory, secondFactory);
        }

        public static IFactory<T, F> Create<T, M, F>(IFactory<M, F> firstFactory, IFactory<T, M> secondFactory)
        {
            return new FactoryChain<T, M, F>(firstFactory, secondFactory);
        }
    }

    public class FactoryChain<T, F> : IFactory<T>
    {
        IFactory<F> firstFactory;
        IFactory<T, F> secondFactory;

        public FactoryChain(IFactory<F> firstFactory, IFactory<T, F> secondFactory)
        {
            this.firstFactory = firstFactory;
            this.secondFactory = secondFactory;
        }

        public T Create()
        {
            return secondFactory.Create(firstFactory.Create());
        }
    }

    public class FactoryChain<T, M, F> : IFactory<T, F>
    {
        IFactory<M, F> firstFactory;
        IFactory<T, M> secondFactory;

        public FactoryChain(IFactory<M, F> firstFactory, IFactory<T, M> secondFactory)
        {
            this.firstFactory = firstFactory;
            this.secondFactory = secondFactory;
        }

        public T Create(F input)
        {
            return secondFactory.Create(firstFactory.Create(input));
        }
    }


}
