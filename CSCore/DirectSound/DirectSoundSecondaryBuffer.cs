using System;
using System.Runtime.InteropServices;
using System.Security;

namespace CSCore.DirectSound
{
    /// <summary>
    /// Represents a secondary directsound buffer.
    /// </summary>
    public class DirectSoundSecondaryBuffer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectSoundSecondaryBuffer"/> class.
        /// </summary>
        /// <param name="directSound">A <see cref="DirectSoundBase"/> instance which provides the <see cref="DirectSoundBase.CreateSoundBuffer"/> method.</param>
        /// <param name="waveFormat">The <see cref="WaveFormat"/> of the sound buffer.</param>
        /// <param name="bufferSize">The buffer size. Internally, the <see cref="BufferDescription.BufferBytes"/> will be set to <paramref name="bufferSize"/> * 2.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="directSound"/> or <paramref name="waveFormat"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bufferSize"/> must be a value between 4 and 0x0FFFFFFF.</exception>
        public static IDirectSoundBuffer Create(IDirectSound directSound, WaveFormat waveFormat, int bufferSize)
        {
            if (directSound == null) 
                throw new ArgumentNullException("directSound");
            if (waveFormat == null)
                throw new ArgumentNullException("waveFormat");
            if(bufferSize < 4 || bufferSize > 0x0FFFFFFF)
                throw new ArgumentOutOfRangeException("bufferSize");

            BufferDescription secondaryBufferDesc = new BufferDescription()
            {
                BufferBytes = bufferSize,
                Flags = DSBufferCapsFlags.ControlFrequency | DSBufferCapsFlags.ControlPan |
                          DSBufferCapsFlags.ControlVolume | DSBufferCapsFlags.ControlPositionNotify |
                          DSBufferCapsFlags.GetCurrentPosition2 | DSBufferCapsFlags.GlobalFocus |
                          DSBufferCapsFlags.StickyFocus,
                Reserved = 0,
                Guid3DAlgorithm = Guid.Empty
            };

            secondaryBufferDesc.Size = Marshal.SizeOf(secondaryBufferDesc);
            GCHandle hWaveFormat = GCHandle.Alloc(waveFormat, GCHandleType.Pinned);
            try
            {
                secondaryBufferDesc.PtrFormat = hWaveFormat.AddrOfPinnedObject();
                IDirectSoundBuffer output;
                directSound.CreateSoundBuffer(secondaryBufferDesc,out output, IntPtr.Zero);
                return output;
            }
            finally
            {
                hWaveFormat.Free();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectSoundSecondaryBuffer"/> class.
        /// </summary>
        /// <param name="directSound">A <see cref="DirectSoundBase"/> instance which provides the <see cref="DirectSoundBase.CreateSoundBuffer"/> method.</param>
        /// <param name="bufferDescription">The buffer description which describes the buffer to create.</param>        
        /// <exception cref="System.ArgumentNullException"><paramref name="directSound"/></exception>
        /// <exception cref="System.ArgumentException">
        /// The <paramref name="bufferDescription"/> is invalid.
        /// </exception>
        public static IDirectSoundBuffer Create(IDirectSound directSound, BufferDescription bufferDescription)
        {
            if (directSound == null)
                throw new ArgumentNullException("directSound");
            if((bufferDescription.Flags & DSBufferCapsFlags.PrimaryBuffer) == DSBufferCapsFlags.PrimaryBuffer)
                throw new ArgumentException("The PrimaryBuffer is set.", "bufferDescription");
            if (bufferDescription.BufferBytes < 4 || bufferDescription.BufferBytes > 0x0FFFFFFF)
                throw new ArgumentException("Invalid BufferBytes value.", "bufferDescription");

            IDirectSoundBuffer output;
            directSound.CreateSoundBuffer(bufferDescription, out output, IntPtr.Zero);
            return output;
        }
    }
}