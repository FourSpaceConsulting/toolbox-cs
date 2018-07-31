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
namespace Fourspace.Toolbox.Service
{
    /// <summary>
    /// Write properties from one object to another, using a context.
    /// Particularly useful in CRUD updates of persisted objects
    /// </summary>
    /// <typeparam name="T">To write</typeparam>
    /// <typeparam name="F">Write from</typeparam>
    /// <typeparam name="C">write context</typeparam>
    public interface IPropertyWriter<in T,in F,in C>
    {
        /// <summary>
        /// Write properties
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="context"></param>
        /// <param name="isOverwrite"></param>
        void WriteProperties(T to, F from, C context, bool isOverwrite);
    }
}
