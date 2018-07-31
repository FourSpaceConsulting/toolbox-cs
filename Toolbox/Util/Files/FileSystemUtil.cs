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
using System.IO;
using System.Text;

namespace Fourspace.Toolbox.Util.Files
{

    public static class FileSystemUtil
    {
        /// <summary>
        /// Create directory if it doesn't exist
        /// </summary>
        /// <param name="filepath"></param>
        public static void CreateDirectoryIfRequired(string filepath)
        {
            string dir = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// Copy a file if exists and overwrite
        /// </summary>
        /// <param name="fromFile"></param>
        /// <param name="toFile"></param>
        /// <returns></returns>
        public static bool CopyIfExists(string fromFile, string toFile)
        {
            bool copied = false;
            if (File.Exists(fromFile))
            {
                File.Copy(fromFile, toFile, true);
                copied = true;
            }
            return copied;
        }

        /// <summary>
        /// Move a file if exists and overwrite
        /// </summary>
        /// <param name="fromFile"></param>
        /// <param name="toFile"></param>
        /// <returns></returns>
        public static bool MoveIfExists(string fromFile, string toFile)
        {
            bool copied = false;
            if (File.Exists(fromFile))
            {
                File.Move(fromFile, toFile);
                copied = true;
            }
            return copied;
        }

        /// <summary>
        /// Replaces strings in the filename (but not the directory)
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="toReplace"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceInFileName(string filepath, string toReplace, string replace)
        {
            if (filepath == null) throw new ArgumentNullException(nameof(filepath));

            string directory = Path.GetDirectoryName(filepath);
            string filename = Path.GetFileName(filepath);
            string newFilename = filename.Replace(toReplace, replace);
            var sb = string.IsNullOrWhiteSpace(directory) ? new StringBuilder() : new StringBuilder(directory).Append(Path.DirectorySeparatorChar);
            return sb.Append(newFilename).ToString();
        }

        /// <summary>
        /// Append a string to a filepath before the extension
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="toAppend"></param>
        /// <returns></returns>
        public static string InsertPreExtension(string filepath, string toAppend)
        {
            if (filepath == null) throw new ArgumentNullException(nameof(filepath));
            if (toAppend == null) throw new ArgumentNullException(nameof(toAppend));

            var dir = Path.GetDirectoryName(filepath);
            var file = Path.GetFileNameWithoutExtension(filepath);
            var ext = Path.GetExtension(filepath);
            var sb = string.IsNullOrWhiteSpace(dir) ? new StringBuilder() : new StringBuilder(dir).Append(Path.DirectorySeparatorChar);
            return sb.Append(file).Append(toAppend).Append(ext).ToString();
        }


        /// <summary>
        /// Append a directory name to the file path
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        public static string AppendDirectory(string filepath, string directoryName)
        {
            var dir = Path.GetDirectoryName(filepath);
            var file = Path.GetFileName(filepath);
            var sb = string.IsNullOrWhiteSpace(dir) ? new StringBuilder() : new StringBuilder(dir).Append(Path.DirectorySeparatorChar);
            return sb.Append(directoryName).Append(Path.DirectorySeparatorChar).Append(file).ToString();
        }

    }
}
