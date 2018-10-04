//
// SecureDeleteExtensions.cs
// This file is part of Microsoft.WinAny.Helper library
//
// Author: Josip Habjan (habjan@gmail.com, http://www.linkedin.com/in/habjan) 
// Copyright (c) 2013 Josip Habjan. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDAPP BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.WinAny.Misc;

// src - https://archive.codeplex.com/?p=microsoftwinanyhelper

namespace Microsoft.WinAny.IO
{

    #region OverwriteAlgorithm

    public enum OverwriteAlgorithm : int
    {
        /// <summary>
        /// 1 pass.
        /// This method will simply overwrite a file with zeros before deleting it.
        /// It is not secure and should only be used for unimportant files and for quick free space locks.
        /// </summary>
        Quick = 1,
        /// <summary>
        /// 1 pass.
        /// This method will simply overwrite a file one time with random data before deleting it.
        /// It is not secure and should only be used for unimportant files.
        /// </summary>
        Random = 2,
        /// <summary>
        /// 3 passes.
        /// This method is based on the U.S. Department of Defense's standard 'National Industrial Security Program Operating Manual' (DoD 5220.22-M E).  
        /// It will overwrite a file 3 times.  This method offAPP medium security, use it only on files that do not contain sensitive information.
        /// </summary>
        DoD_3 = 4,
        /// <summary>
        /// 7 passes.
        /// This method is based on the U.S. Department of Defense's standard 'National Industrial Security Program Operating Manual' (US DoD 5220.22-M ECE).  
        /// It will overwrite a file 7 times.  This method incorporates the DoD-3 method.  It is secure and should be used for general files.
        /// </summary>
        DoD_7 = 8,
        /// <summary>
        /// 35 passes.
        /// This method is based on Peter Gutmann's article 'Secure Deletion of Data From Magnetic and Solid-State Memory.'  
        /// The data will be overwritten 35 times using the patterns and methods described in the article.  
        /// While this method takes the longest amount of time, it is the most secure method available and should be used for all files that contain sensitive information.
        /// </summary>
        Gutmann = 16
    }

    #endregion

    public static class SecureDeleteExtensions
    {
        #region Private constants

        private const int MAX_BUFFER_SIZE = 67108864;

        #endregion

        #region Delete - DirectoryInfo

        /// <summary>
        /// Safe delete this directory, subdirectories and all the files under this directory.
        /// </summary>
        /// <param name="directory">The DirectoryInfo.</param>
        /// <param name="overwriteAlgorithm">Overwrite algorithm.</param>
        public static void Delete(this DirectoryInfo directory, OverwriteAlgorithm overwriteAlgorithm)
        {
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                //hold any file generazted by the BOT
                if (file.Extension == ".html" || file.Extension == ".txt" || file.Extension == ".xlsx" || file.Extension == ".sql")
                    continue;

                file.Delete(overwriteAlgorithm);
            }

            DirectoryInfo[] subDirectories = directory.GetDirectories();

            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                subDirectory.Delete(overwriteAlgorithm);
            }

            //directory.Delete();
        }

        #endregion

        #region Delete - FileInfo

        public static void Delete(this FileInfo file, OverwriteAlgorithm overwriteAlgorithm)
        {
            if ((overwriteAlgorithm & OverwriteAlgorithm.Gutmann) == OverwriteAlgorithm.Gutmann)
            {
                OverwriteFile_Gutmann(file);
            }

            if ((overwriteAlgorithm & OverwriteAlgorithm.DoD_7) == OverwriteAlgorithm.DoD_7)
            {
                OverwriteFile_DoD_7(file);
            }

            if ((overwriteAlgorithm & OverwriteAlgorithm.DoD_3) == OverwriteAlgorithm.DoD_3)
            {
                OverwriteFile_DoD_3(file);
            }

            if ((overwriteAlgorithm & OverwriteAlgorithm.Random) == OverwriteAlgorithm.Random)
            {
                OverwriteFile_Random(file);
            }

            if ((overwriteAlgorithm & OverwriteAlgorithm.Quick) == OverwriteAlgorithm.Quick)
            {
                OverwriteFile_Quick(file);
            }

            file.Delete();
        }

        #endregion

        #region OverwriteFile_Quick

        /// <summary>
        /// Overwrite the file with zero bytes.
        /// </summary>
        /// <param name="file">The file.</param>
        internal static void OverwriteFile_Quick(FileInfo file)
        {
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Write, FileShare.None);

            for (long size = fs.Length; size > 0; size -= MAX_BUFFER_SIZE)
            {
                long bufferSize = (size < MAX_BUFFER_SIZE) ? size : MAX_BUFFER_SIZE;

                byte[] buffer = new byte[bufferSize];

                fs.Write(buffer, 0, buffer.Length);
                fs.Flush(true);
            }

            fs.Close(); fs.Dispose(); fs = null;
        }

        #endregion

        #region OverwriteFile_Random

        /// <summary>
        /// Overwrite the file with random data.
        /// </summary>
        /// <param name="file">The file.</param>
        internal static void OverwriteFile_Random(FileInfo file)
        {
            Random random = ThreadSafeRandom.Random;

            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Write, FileShare.None);

            for (long size = fs.Length; size > 0; size -= MAX_BUFFER_SIZE)
            {
                long bufferSize = (size < MAX_BUFFER_SIZE) ? size : MAX_BUFFER_SIZE;

                byte[] buffer = new byte[bufferSize];

                for (int bufferIndex = 0; bufferIndex < bufferSize; ++bufferIndex)
                {
                    buffer[bufferIndex] = (byte)(random.Next() % 256);
                }

                fs.Write(buffer, 0, buffer.Length);
                fs.Flush(true);
            }

            fs.Close(); fs.Dispose(); fs = null;
        }

        #endregion

        #region OverwriteFile_DoD_3

        /// <summary>
        /// Overwrite the file based on the U.S. Department of Defense's standard 'National Industrial Security Program Operating Manual' (DoD 5220.22-M E).
        /// </summary>
        /// <param name="file">The file.</param>
        internal static void OverwriteFile_DoD_3(FileInfo file)
        {
            byte[] pattern = new byte[] { 0x00, 0xFF, 0x72 };

            ThreadSafeRandom.Shuffle<byte>(pattern);

            Random random = ThreadSafeRandom.Random;

            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Write, FileShare.None);

            for (int pass = 1; pass <= 3; ++pass)
            {
                fs.Position = 0;

                for (long size = fs.Length; size > 0; size -= MAX_BUFFER_SIZE)
                {
                    long bufferSize = (size < MAX_BUFFER_SIZE) ? size : MAX_BUFFER_SIZE;

                    byte[] buffer = new byte[bufferSize];

                    if (pass != 2)
                    {
                        for (int bufferIndex = 0; bufferIndex < bufferSize; ++bufferIndex)
                        {
                            buffer[bufferIndex] = pattern[pass];
                        }
                    }
                    else
                    {
                        for (int bufferIndex = 0; bufferIndex < bufferSize; ++bufferIndex)
                        {
                            buffer[bufferIndex] = (byte)(random.Next() % 256);
                        }
                    }

                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush(true);
                }
            }

            fs.Close(); fs.Dispose(); fs = null;
        }

        #endregion

        #region OverwriteFile_DoD_7

        /// <summary>
        /// Overwrite the file based on the U.S. Department of Defense's standard 'National Industrial Security Program Operating Manual' (US DoD 5220.22-M ECE).
        /// </summary>
        /// <param name="file">The file.</param>
        internal static void OverwriteFile_DoD_7(FileInfo file)
        {
            byte[] pattern = new byte[] { 0x00, 0xFF, 0x72, 0x96, 0x00, 0xFF, 0x72 };

            ThreadSafeRandom.Shuffle<byte>(pattern);

            Random random = ThreadSafeRandom.Random;

            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Write, FileShare.None);

            for (int pass = 1; pass <= 7; ++pass)
            {
                fs.Position = 0;

                for (long size = fs.Length; size > 0; size -= MAX_BUFFER_SIZE)
                {
                    long bufferSize = (size < MAX_BUFFER_SIZE) ? size : MAX_BUFFER_SIZE;

                    byte[] buffer = new byte[bufferSize];

                    if (pass != 2 && pass != 6)
                    {
                        for (int bufferIndex = 0; bufferIndex < bufferSize; ++bufferIndex)
                        {
                            buffer[bufferIndex] = pattern[pass];
                        }
                    }
                    else
                    {
                        for (int bufferIndex = 0; bufferIndex < bufferSize; ++bufferIndex)
                        {
                            buffer[bufferIndex] = (byte)(random.Next() % 256);
                        }
                    }

                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush(true);
                }
            }

            fs.Close(); fs.Dispose(); fs = null;
        }

        #endregion

        #region OverwriteFile_Gutmann

        /// <summary>
        /// Overwrite the file based on the Peter Gutmann's algorithm.
        /// </summary>
        /// <param name="file">The file.</param>
        internal static void OverwriteFile_Gutmann(FileInfo file)
        {
            byte[][] pattern = new byte[][] { 
                new byte[] {0x55, 0x55, 0x55}, new byte[] {0xAA, 0xAA, 0xAA}, new byte[] {0x92, 0x49, 0x24}, new byte[] {0x49, 0x24, 0x92}, new byte[] {0x24, 0x92, 0x49}, 
                new byte[] {0x00, 0x00, 0x00}, new byte[] {0x11, 0x11, 0x11}, new byte[] {0x22, 0x22, 0x22}, new byte[] {0x33, 0x33, 0x33}, new byte[] {0x44, 0x44, 0x44}, 
                new byte[] {0x55, 0x55, 0x55}, new byte[] {0x66, 0x66, 0x66}, new byte[] {0x77, 0x77, 0x77}, new byte[] {0x88, 0x88, 0x88}, new byte[] {0x99, 0x99, 0x99}, 
                new byte[] {0xAA, 0xAA, 0xAA}, new byte[] {0xBB, 0xBB, 0xBB}, new byte[] {0xCC, 0xCC, 0xCC}, new byte[] {0xDD, 0xDD, 0xDD}, new byte[] {0xEE, 0xEE, 0xEE}, 
                new byte[] {0xFF, 0xFF, 0xFF}, new byte[] {0x92, 0x49, 0x24}, new byte[] {0x49, 0x24, 0x92}, new byte[] {0x24, 0x92, 0x49}, new byte[] {0x6D, 0xB6, 0xDB}, 
                new byte[] {0xB6, 0xDB, 0x6D}, new byte[] {0xDB, 0x6D, 0xB6} };

            ThreadSafeRandom.Shuffle<byte[]>(pattern);

            Random random = ThreadSafeRandom.Random;

            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Write, FileShare.None);

            for (int pass = 1; pass <= 35; ++pass)
            {
                for (int index = 0; index < 3; index++)
                {
                    fs.Position = 0;

                    for (long size = fs.Length; size > 0; size -= MAX_BUFFER_SIZE)
                    {
                        long bufferSize = (size < MAX_BUFFER_SIZE) ? size : MAX_BUFFER_SIZE;

                        byte[] buffer = new byte[bufferSize];

                        if (pass > 4 && pass < 32)
                        {
                            for (int bufferIndex = 0; bufferIndex < bufferSize; ++bufferIndex)
                            {
                                buffer[bufferIndex] = pattern[pass - 5][index];
                            }
                        }
                        else
                        {
                            for (int bufferIndex = 0; bufferIndex < bufferSize; ++bufferIndex)
                            {
                                buffer[bufferIndex] = (byte)(random.Next() % 256);
                            }
                        }

                        fs.Write(buffer, 0, buffer.Length);
                        fs.Flush(true);
                    }
                }
            }

            fs.Close(); fs.Dispose(); fs = null;
        }

        #endregion
    }
}
