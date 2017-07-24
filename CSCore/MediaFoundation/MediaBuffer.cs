using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDX.MediaFoundation
{
    public partial class MediaBuffer
    {
        public LockDisposable Lock()
        {
            int currentLength;
            int maxLength;
            return new LockDisposable(this, Lock(out maxLength, out currentLength), maxLength, currentLength);
        }

        /// <summary>
        /// Used to unlock a <see cref="MFMediaBuffer"/> after locking it by calling the <see cref="MFMediaBuffer.Lock()"/> method.
        /// </summary>
        public class LockDisposable : IDisposable
        {
            private readonly MediaBuffer _mediaBuffer;
            private bool _disposed;

            /// <summary>
            /// Gets a pointer to the start of the buffer.
            /// </summary>
            public IntPtr Buffer { get; private set; }

            /// <summary>
            /// Gets the maximum amount of data that can be written to the buffer.
            /// </summary>
            public int MaxLength { get; private set; }

            /// <summary>
            /// Gets the length of the valid data in the buffer, in bytes.
            /// </summary>
            public int CurrentLength { get; private set; }

            internal LockDisposable(MediaBuffer mediaBuffer, IntPtr buffer, int maxLength, int currentLength)
            {
                _mediaBuffer = mediaBuffer;
                Buffer = buffer;
                MaxLength = maxLength;
                CurrentLength = currentLength;
            }

            /// <summary>
            /// Unlocks the locked <see cref="MFMediaBuffer"/>.
            /// </summary>
            public void Dispose()
            {
                if (!_disposed)
                {
                    _mediaBuffer.Dispose();
                    _disposed = true;
                }
            }
        }
    }
}
