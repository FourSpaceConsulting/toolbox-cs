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

namespace Fourspace.Toolbox.Service.Factories
{
    public class FactoryWatcher
    {
        public static IFactory<T> Create<T>(IFactory<T> factory, Action<T> action)
        {
            return new FactoryWatcher<T>(factory, action);
        }
    }
    public class FactoryWatcher<T> : IFactory<T>
    {
        private readonly IFactory<T> factory;
        private readonly Action<T> action;

        public FactoryWatcher(IFactory<T> factory1, Action<T> action1)
        {
            this.factory = factory1;
            this.action = action1;
        }

        public T Create()
        {
            var created = factory.Create();
            action(created);
            return created;
        }
    }
}
