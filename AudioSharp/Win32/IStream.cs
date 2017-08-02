// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using AudioSharp.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using comtypes = System.Runtime.InteropServices.ComTypes;

namespace SharpDX.Win32
{
    [Shadow(typeof(ComStreamShadow))]
    public partial interface IStream
    {
        /// <summary>
        /// Changes the seek pointer to a new location relative to the beginning of the stream, to the end of the stream, or to the current seek pointer.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="origin">The origin.</param>
        /// <returns>The offset of the seek pointer from the beginning of the stream.</returns>
        long Seek(long offset, SeekOrigin origin);

        /// <summary>
        /// Changes the size of the stream object.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        void SetSize(long newSize);

        /// <summary>
        /// Copies a specified number of bytes from the current seek pointer in the stream to the current seek pointer in another stream.
        /// </summary>
        /// <param name="streamDest">The stream destination.</param>
        /// <param name="numberOfBytesToCopy">The number of bytes to copy.</param>
        /// <param name="bytesWritten">The number of bytes written.</param>
        /// <returns>The number of bytes read</returns>
        long CopyTo(IStream streamDest, long numberOfBytesToCopy, out long bytesWritten);

        /// <summary>
        /// Commit method ensures that any changes made to a stream object open in transacted mode are reflected in the parent storage. If the stream object is open in direct mode, Commit has no effect other than flushing all memory buffers to the next-level storage object. The COM compound file implementation of streams does not support opening streams in transacted mode.
        /// </summary>
        /// <param name="commitFlags">The GRF commit flags.</param>
        void Commit(CommitFlags commitFlags);

        /// <summary>
        /// Discards all changes that have been made to a transacted stream since the last <see cref="Commit"/> call. 
        /// </summary>
        void Revert();

        /// <summary>
        /// Restricts access to a specified range of bytes in the stream.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfBytesToLock">The number of bytes to lock.</param>
        /// <param name="dwLockType">Type of the dw lock.</param>
        void LockRegion(long offset, long numberOfBytesToLock, LockType dwLockType);

        /// <summary>
        /// Unlocks access to a specified range of bytes in the stream.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="numberOfBytesToLock">The number of bytes to lock.</param>
        /// <param name="dwLockType">Type of the dw lock.</param>
        void UnlockRegion(long offset, long numberOfBytesToLock, LockType dwLockType);

        /// <summary>
        /// Gets the statistics.
        /// </summary>
        /// <param name="storageStatisticsFlags">The storage statistics flags.</param>
        /// <returns></returns>
        StorageStatistics GetStatistics(StorageStatisticsFlags storageStatisticsFlags);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IStream Clone();
    }
}

namespace CSCore.Win32
{
    /// <summary>
    /// Provides the managed definition of the IStream interface.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000000c-0000-0000-C000-000000000046")]
    public interface IStream
    {
        /// <summary>
        /// Reads a specified number of bytes from the stream object into memory starting at the current seek pointer.
        /// </summary>
        /// <param name="pv">When this method returns, contains the data read from the stream. This parameter is passed uninitialized.</param>
        /// <param name="cb">The number of bytes to read from the stream object. </param>
        /// <param name="pcbRead">A pointer to a ULONG variable that receives the actual number of bytes read from the stream object. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

        /// <summary>
        /// Writes a specified number of bytes into the stream object starting at the current seek pointer.
        /// </summary>
        /// <param name="pv">The buffer to write this stream to. </param>
        /// <param name="cb">he number of bytes to write to the stream. </param>
        /// <param name="pcbWritten">On successful return, contains the actual number of bytes written to the stream object. If the caller sets this pointer to Zero, this method does not provide the actual number of bytes written. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

        /// <summary>
        /// Changes the seek pointer to a new location relative to the beginning of the stream, to the end of the stream, or to the current seek pointer.
        /// </summary>
        /// <param name="dlibMove">The displacement to add to dwOrigin. </param>
        /// <param name="dwOrigin">The origin of the seek. The origin can be the beginning of the file, the current seek pointer, or the end of the file. </param>
        /// <param name="plibNewPosition">On successful return, contains the offset of the seek pointer from the beginning of the stream. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

        /// <summary>
        /// Changes the size of the stream object.
        /// </summary>
        /// <param name="libNewSize">The new size of the stream as a number of bytes. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult SetSize(long libNewSize);

        /// <summary>
        /// Copies a specified number of bytes from the current seek pointer in the stream to the current seek pointer in another stream.
        /// </summary>
        /// <param name="pstm">A reference to the destination stream. </param>
        /// <param name="cb">The number of bytes to copy from the source stream. </param>
        /// <param name="pcbRead">On successful return, contains the actual number of bytes read from the source. </param>
        /// <param name="pcbWritten">On successful return, contains the actual number of bytes written to the destination. </param>
        /// <returns>HRESULT</returns>
        HResult CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

        /// <summary>
        /// Ensures that any changes made to a stream object that is open in transacted mode are reflected in the parent storage.
        /// </summary>
        /// <param name="grfCommitFlags">A value that controls how the changes for the stream object are committed. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult Commit(int grfCommitFlags);

        /// <summary>
        /// Discards all changes that have been made to a transacted stream since the last Commit call.
        /// </summary>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult Revert();

        /// <summary>
        /// Restricts access to a specified range of bytes in the stream.
        /// </summary>
        /// <param name="libOffset">The byte offset for the beginning of the range. </param>
        /// <param name="cb">The length of the range, in bytes, to restrict. </param>
        /// <param name="dwLockType">The requested restrictions on accessing the range. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult LockRegion(long libOffset, long cb, int dwLockType);

        /// <summary>
        /// Removes the access restriction on a range of bytes previously restricted with the LockRegion method.
        /// </summary>
        /// <param name="libOffset">The byte offset for the beginning of the range. </param>
        /// <param name="cb">The length, in bytes, of the range to restrict. </param>
        /// <param name="dwLockType">The access restrictions previously placed on the range. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult UnlockRegion(long libOffset, long cb, int dwLockType);

        /// <summary>
        /// Retrieves the STATSTG structure for this stream.
        /// </summary>
        /// <param name="pstatstg">When this method returns, contains a STATSTG structure that describes this stream object. This parameter is passed uninitialized.</param>
        /// <param name="grfStatFlag">Members in the STATSTG structure that this method does not return, thus saving some memory allocation operations. </param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult Stat(out comtypes.STATSTG pstatstg, int grfStatFlag);

        /// <summary>
        /// Creates a new stream object with its own seek pointer that references the same bytes as the original stream.
        /// </summary>
        /// <param name="ppstm">When this method returns, contains the new stream object. This parameter is passed uninitialized.</param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HResult Clone(out IStream ppstm);
    }
}
