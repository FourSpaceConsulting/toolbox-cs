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

namespace Fourspace.Toolbox.DataSource
{
    public interface IKeyedSource<out T, in K> : ISource<T>
    {
        /// <summary>
        /// Get object corresponding to key.
        /// </summary>
        /// <param name="key">Look up key</param>
        /// <returns>Object with identifier.</returns>
        /// <exception cref="ResourceNotFoundException"/>
        T Get(K key);

        /// <summary>
        /// Get object corresponding to key if exists.
        /// </summary>
        /// <param name="key">look up key.</param>
        /// <returns>Object with identifier.</returns>
        T GetIfExists(K key);

        /// <summary>
        /// Get object corresponding to keys.
        /// </summary>
        /// <param name="key">Look up key</param>
        /// <returns>Object with identifier.</returns>
        /// <exception cref="ResourceNotFoundException"/>
        IReadOnlyList<T> Get(IEnumerable<K> keys);

        /// <summary>
        /// Get object corresponding to keys if exists.
        /// </summary>
        /// <param name="key">look up key.</param>
        /// <returns>Object with identifier.</returns>
        IReadOnlyList<T> GetIfExists(IEnumerable<K> keys);
    }
}
