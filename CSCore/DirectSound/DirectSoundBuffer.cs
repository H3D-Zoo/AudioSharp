using System;
using System.Runtime.InteropServices;
using System.Security;
using CSCore.Win32;

namespace CSCore.DirectSound
{
    /// <summary>
    /// IDirectSoundBuffer interface
    /// </summary>
    [ComImport,
     Guid("279AFA85-4981-11CE-A521-0020AF0BE560"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     SuppressUnmanagedCodeSecurity]
    public interface IDirectSoundBuffer
    {
        void GetCaps([MarshalAs(UnmanagedType.LPStruct)] BufferCaps pBufferCaps);
        /// <summary>
        /// Retrieves the position of the play and write cursors in the sound buffer. 
        /// </summary>
        /// <param name="playCursorPosition">Receives the offset, in bytes, of the play cursor.</param>
        /// <param name="writeCursorPosition">Receives the offset, in bytes, of the write cursor.</param>
        void GetCurrentPosition([Out] out uint currentPlayCursor, [Out] out uint currentWriteCursor);
        DSResult GetFormat(IntPtr pwfxFormat, int dwSize, out int pdwSizeWritten);
        [return: MarshalAs(UnmanagedType.I4)]
        /// <summary>
        /// Returns the attenuation of the sound. 
        /// </summary>
        /// <returns>The attenuation, in hundredths of a decibel.</returns>
        int GetVolume();
        /// <summary>
        /// Retrieves the relative volume of the left and right audio channels. 
        /// </summary>
        /// <returns>The relative volume, in hundredths of a decibel.</returns>
        void GetPan([Out] out uint pan);
        [return: MarshalAs(UnmanagedType.I4)]
        /// <summary>
        /// Gets the frequency, in samples per second, at which the buffer is playing. 
        /// </summary>
        /// <returns>The frequency at which the audio buffer is being played, in hertz.</returns>
        int GetFrequency();
        [return: MarshalAs(UnmanagedType.U4)]
        /// <summary>
        /// Retrieves the status of the sound buffer. 
        /// <seealso cref="Status"/>        
        /// </summary>
        /// <param name="status">Receives the status of the sound buffer.</param>
        /// <returns>DSResult</returns>
        /// <remarks>Use the <see cref="Status"/> property instead.</remarks>
        DirectSoundBufferStatus GetStatus();
        /// <summary>
        /// Initializes a sound buffer object if it has not yet been initialized. 
        /// </summary>
        /// <param name="directSound">The device object associated with this buffer.</param>
        /// <param name="bufferDescription">A <see cref="BufferDescription"/> structure that contains the values used to initialize this sound buffer.</param>
        void Initialize([In, MarshalAs(UnmanagedType.Interface)] IDirectSound directSound, [In] BufferDescription desc);
        /// <summary>
        /// Readies all or part of the buffer for a data write and returns pointers to which data can be written. 
        /// </summary>
        /// <param name="offset">Offset, in bytes, from the start of the buffer to the point where the lock begins. This parameter is ignored if <see cref="DirectSoundBufferLockFlag.FromWriteCursor"/> is specified in the <paramref name="lockFlags"/> parameter.</param>
        /// <param name="bytes">Size, in bytes, of the portion of the buffer to lock. The buffer is conceptually circular, so this number can exceed the number of bytes between <paramref name="offset"/> and the end of the buffer.</param>
        /// <param name="audioPtr1">Receives a pointer to the first locked part of the buffer.</param>
        /// <param name="audioBytes1">Receives the number of bytes in the block at <paramref name="audioPtr1"/>. If this value is less than <paramref name="bytes"/>, the lock has wrapped and <paramref name="audioPtr2"/> points to a second block of data at the beginning of the buffer.</param>
        /// <param name="audioPtr2">Receives a pointer to the second locked part of the capture buffer. If <see cref="IntPtr.Zero"/> is returned, the <paramref name="audioPtr1"/> parameter points to the entire locked portion of the capture buffer.</param>
        /// <param name="audioBytes2">Receives the number of bytes in the block at <paramref name="audioPtr2"/>. If <paramref name="audioPtr2"/> is <see cref="IntPtr.Zero"/>, this value is zero.</param>
        /// <param name="lockFlags">Flags modifying the lock event.</param>
        DSResult Lock(int dwOffset, uint dwBytes, [Out] out IntPtr audioPtr1, [Out] out int audioBytes1, [Out] out IntPtr audioPtr2, [Out] out int audioBytes2, [MarshalAs(UnmanagedType.U4)] DirectSoundBufferLockFlag dwFlags);
        void Play(uint dwReserved1, uint dwPriority, [In, MarshalAs(UnmanagedType.U4)] DirectSoundPlayFlags dwFlags);

        /// <summary>
        /// Sets the position of the play cursor, which is the point at which the next byte of data is read from the buffer. 
        /// </summary>
        /// <param name="playPosition">Offset of the play cursor, in bytes, from the beginning of the buffer.</param>
        void SetCurrentPosition(uint dwNewPosition);

        DSResult SetFormat([In] WaveFormat pcfxFormat);
        /// <summary>
        /// Sets the attenuation of the sound. 
        /// </summary>
        /// <param name="volume">Attenuation, in hundredths of a decibel (dB).</param>        
        void SetVolume(int volume);

        /// <summary>
        /// Sets the relative volume of the left and right channels. 
        /// </summary>
        /// <param name="pan">Relative volume between the left and right channels. Must be between <see cref="PanLeft"/> and <see cref="PanRight"/>.</param>
        /// <remarks>For more information, see <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.idirectsoundbuffer8.idirectsoundbuffer8.setpan(v=vs.85).aspx"/>.</remarks>
        void SetPan(uint pan);
        /// <summary>
        /// Sets the frequency at which the audio samples are played. 
        /// </summary>
        /// <param name="frequency">Frequency, in hertz (Hz), at which to play the audio samples. A value of <see cref="FrequencyOriginal"/> resets the frequency to the default value of the buffer format.</param>
        /// <remarks>Before setting the frequency, you should ascertain whether the frequency is supported by checking the <see cref="DirectSoundCapabilities.MinSecondarySampleRate"/> and <see cref="DirectSoundCapabilities.MaxSecondarySampleRate"/> members of the <see cref="DirectSoundCapabilities"/> structure for the device. Some operating systems do not support frequencies greater than 100,000 Hz.</remarks>
        void SetFrequency(uint frequency);
        void Stop();

        /// <summary>
        /// Releases a locked sound buffer. 
        /// </summary>
        /// <param name="audioPtr1">Address of the value retrieved in the <c>audioPtr1</c> parameter of the <see cref="Lock"/> method.</param>
        /// <param name="audioBytes1">Number of bytes written to the portion of the buffer at <c>audioPtr1</c>.</param>
        /// <param name="audioPtr2">Address of the value retrieved in the <c>audioPtr2</c> parameter of the <see cref="Lock"/> method.</param>
        /// <param name="audioBytes2">Number of bytes written to the portion of the buffer at <c>audioPtr2</c>.</param>
        DSResult Unlock(IntPtr pvAudioPtr1, int dwAudioBytes1, IntPtr pvAudioPtr2, int dwAudioBytes2);
        void Restore();
    }


    public static class IDirectSoundBufferExtension
    {
        private const int MinVolume = -10000;
        private const int MaxVolume = 0;

        /// <summary>
        /// Left only.
        /// </summary>
        public const int PanLeft = -10000;
        /// <summary>
        /// 50% left, 50% right.
        /// </summary>
        public const int PanCenter = 0;
        /// <summary>
        /// Right only.
        /// </summary>
        public const int PanRight = 10000;

        /// <summary>
        /// The default frequency. For more information, see <see cref="SetFrequency"/>.
        /// </summary>
        public const int FrequencyOriginal = 0;
        private const int FrequencyMin = 100;
        private const int FrequencyMax = 20000;

        public static BufferCaps GetCaps(this IDirectSoundBuffer target)
        {
            var bufferCaps = new BufferCaps();
            bufferCaps.Size = Marshal.SizeOf(bufferCaps);
            target.GetCaps(bufferCaps);
            return bufferCaps;
        }

        /// <summary>
        /// Causes the sound buffer to play, starting at the play cursor. 
        /// </summary>
        /// <param name="flags">Flags specifying how to play the buffer.</param>
        public static void Play(this IDirectSoundBuffer target, DirectSoundPlayFlags flags)
        {
            target.Play(flags, 0);
        }

        /// <summary>
        /// Causes the sound buffer to play, starting at the play cursor. 
        /// </summary>
        /// <param name="flags">Flags specifying how to play the buffer.</param>
        /// <param name="priority">Priority for the sound, used by the voice manager when assigning hardware mixing resources. The lowest priority is 0, and the highest priority is 0xFFFFFFFF. If the buffer was not created with the <see cref="DSBufferCapsFlags.LocDefer"/> flag, this value must be 0.</param>
        public static void Play(this IDirectSoundBuffer target, DirectSoundPlayFlags flags, uint priority)
        {
            target.Play(0, priority, flags);
        }

        /// <summary>
        /// Sets the relative volume of the left and right channels as a scalar value. 
        /// </summary>
        /// <param name="pan">Relative volume between the left and right channels. Must be between -1.0 and 1.0. 
        /// A value of -1.0 will set the volume of the left channel to 100% and the volume of the right channel to 0%. 
        /// A value of 1.0 will set the volume of the left channel to 0% and the volume of the right channel to 100%.</param>
        public static void SetPanScalar(this IDirectSoundBuffer target, float pan)
        {
            int pani = 0;
            if (pan < 0)
            {
                pani = (int)ScalarValueToDBValue(Math.Abs(Math.Abs(pan) - 1), PanLeft, 0);
            }
            else if (pan > 0)
            {
                pani = (int)ScalarValueToDBValue(Math.Abs(pan - 1), PanLeft, 0) * -1;
            }
            target.SetPan((uint)pani);
        }

        /// <summary>
        /// Gets the relative volume of the left and right channels as a scalar value.  
        /// </summary>
        /// <returns>The relative volume between the left and right channels. A value of -1.0 indicates that the volume of the left channel is set to 100% and the volume of the right channel to 0%.
        /// A value of 1.0 indicates that the volume of the left channel is set to 0% and the volume of the right channel is set to 100%.</returns>
        public static float GetPanScalar(this IDirectSoundBuffer target)
        {
            float pan = 0;
            uint ipani;
            target.GetPan(out ipani);
            int pani = (int)ipani;
            if (pani < 0)
            {
                pan = (float)(-1 + DBToScalarValue(pani));
            }
            else if (pani > 0)
            {
                pan = (float)(1 - DBToScalarValue(pani * -1));
            }
            return pan;
        }


        /// <summary>
        /// Sets the attenuation of the sound. 
        /// </summary>
        /// <param name="volume">The attenuation of the sound. The attenuation is expressed as a normalized value in the range from 0.0 to 1.0.</param>
        public static void SetVolumeScalar(this IDirectSoundBuffer target, double volume)
        {
            var value = (int)ScalarValueToDBValue(volume, MinVolume, MaxVolume);
            target.SetVolume(value);
        }

        /// <summary>
        /// Returns the attenuation of the sound.
        /// </summary>
        /// <returns>The attenuation of the sound. The attenuation is expressed as a normalized value in the range from 0.0 to 1.0.</returns>
        public static double GetVolumeScalar(this IDirectSoundBuffer target)
        {
            var volume = target.GetVolume();
            return DBToScalarValue(volume);
        }

        private static double ScalarValueToDBValue(double value, int minValue, int maxValue)
        {
            const double dv = 10.0;
            const double z0 = 0.69314718055994529; //ln(2)

            //assume that dv = 10.0
            const double minAttentuation = 9.766E-4; //(1/2)^(100/dv)

            double result = minValue;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (value != 0)
            {
                double attenuation = minAttentuation + value * (1 - minAttentuation);
                double db = dv * Math.Log(attenuation) / z0;
                result = (int)(db * 100);

                result = Math.Max(minValue, Math.Min(result, maxValue));
            }
            return result;
        }

        private static double DBToScalarValue(double db)
        {
            //assume that dv = 10.0
            const double minAttentuation = 9.766E-4; //(1/2)^(100/dv)
            const double z1 = 0.001; //(1/100)/(dv)
            double result = (minAttentuation - Math.Pow(2, z1 * db)) / (minAttentuation - 1);

            result = Math.Min(1, Math.Max(0, result));
            return result;
        }

        /// <summary>
        /// Returns a description of the format of the sound data in the buffer.
        /// </summary>
        /// <returns>A description of the format of the sound data in the buffer. The returned description is either of the type <see cref="WaveFormat"/> or of the type <see cref="WaveFormatExtensible"/>.</returns>
        public static WaveFormat GetWaveFormat(this IDirectSoundBuffer target)
        {
            int size;
            DSResult result = target.GetFormat(IntPtr.Zero, 0, out size);
            IntPtr ptr = Marshal.AllocCoTaskMem(size);
            try
            {
                int n;
                result = target.GetFormat(ptr, size, out n);
                return WaveFormatMarshaler.PointerToWaveFormat(ptr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(ptr);
            }
        }
        /// <summary>
        /// Gets a value indicating whether the buffer is lost. <c>True</c> means that the buffer is lost; Otherwise <c>False</c>.
        /// </summary>
        public static bool IsBufferLost(this IDirectSoundBuffer target)
        {
            var Status = target.GetStatus();
            return (Status & DirectSoundBufferStatus.BufferLost) == DirectSoundBufferStatus.BufferLost;
        }

        /// <summary>
        /// Writes data to the buffer by locking the buffer, copying data to the buffer and finally unlocking it.
        /// </summary>
        /// <param name="buffer">The data to write to the buffer.</param>
        /// <param name="offset">The zero-based offset in the <paramref name="buffer"/> at which to start copying data.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <returns>Returns <c>true</c> if writing data was successful; Otherwise <c>false</c>.</returns>
        public static bool Write(this IDirectSoundBuffer target, byte[] buffer, int offset, int count)
        {
            IntPtr ptr1, ptr2;
            int b1, b2;
            uint icount = (uint)count;
            if (target.Lock(offset, icount, out ptr1, out b1, out ptr2, out b2, DirectSoundBufferLockFlag.Default) == DSResult.Ok)
            {
                if (ptr1 != IntPtr.Zero)
                    Marshal.Copy(buffer, 0, ptr1, b1);
                if (ptr2 != IntPtr.Zero)
                    Marshal.Copy(buffer, b1 - 1, ptr2, b2);

                return target.Unlock(ptr1, b1, ptr2, b2) == DSResult.Ok;
            }
            return false;
        }

        /// <summary>
        /// Writes data to the buffer by locking the buffer, copying data to the buffer and finally unlocking it.
        /// </summary>
        /// <param name="buffer">The data to write to the buffer.</param>
        /// <param name="offset">The zero-based offset in the <paramref name="buffer"/> at which to start copying data.</param>
        /// <param name="count">The number of shorts to write.</param>
        /// <returns>Returns <c>true</c> if writing data was successful; Otherwise <c>false</c>.</returns>
        public static bool Write(this IDirectSoundBuffer target, short[] buffer, int offset, int count)
        {
            uint dscount =(uint)count * 2;

            IntPtr ptr1, ptr2;
            int b1, b2;
            if (target.Lock(offset, dscount, out ptr1, out b1, out ptr2, out b2, DirectSoundBufferLockFlag.Default) == DSResult.Ok)
            {
                if (ptr1 != IntPtr.Zero)
                    Marshal.Copy(buffer, 0, ptr1, Math.Min(b1, count));
                if (ptr2 != IntPtr.Zero)
                    Marshal.Copy(buffer, Math.Min(b1, count) - 2, ptr2, Math.Min(b2, count));

                return target.Unlock(ptr1, b1, ptr2, b2) == DSResult.Ok;
            }
            return false;
        }
    }
}