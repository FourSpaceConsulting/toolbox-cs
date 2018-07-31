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
using Fourspace.Toolbox.Service.Adapters;
using Fourspace.Toolbox.Service.DateTimeProviders;
using Fourspace.Toolbox.Util.Files;
using NUnit.Framework;
using System;
using System.IO;

namespace Fourspace.Toolbox.Test.Files
{
    [TestFixture]
    public class FilePathAdapterTest
    {
        [Test]
        public void AdaptFileName()
        {
            var filePath = Path.GetFullPath("C:/a/long/path/to/pathfilename.txt");
            var dateTimeProvider = new ConstantDateTimeProvider(new DateTime(2010, 1, 1, 13, 30, 30));
            var adapter = AdapterChain.Create(
                new InsertPreExtensionTimestampFilePathAdapter(dateTimeProvider),
                new ReplaceInFileNameFilePathAdapter("path", "new"),
                new InsertPreExtensionFilePathAdapter("extra"),
                new AppendDirectoryFilePathAdapter("extend"),
                new ModifyExtensionFilePathAdapter("csv")
                );
            // act
            var result = adapter.Adapt(filePath);
            // assert 
            var expected = Path.GetFullPath("C:/a/long/path/to/extend/newfilename20100101133030000extra.csv");
            Assert.AreEqual(expected, result);
        }
    }
}
