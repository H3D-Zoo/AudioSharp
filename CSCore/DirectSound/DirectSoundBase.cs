using System;
using System.Runtime.InteropServices;
using System.Security;
using CSCore.Win32;

namespace CSCore.DirectSound
{
    /// <summary>
    /// IDirectSound interface
    /// </summary>
    [ComImport,
     Guid("279AFA83-4981-11CE-A521-0020AF0BE560"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     SuppressUnmanagedCodeSecurity]
    public interface IDirectSound
    {
        /// <summary>
        /// Creates a sound buffer object to manage audio samples. 
        /// </summary>
        /// <param name="desc">A <see cref="BufferDescription"/> structure that describes the sound buffer to create.</param>
        /// <param name="pUnkOuter">Must be <see cref="IntPtr.Zero"/>.</param>
        /// <returns>A variable that receives the IDirectSoundBuffer interface of the new buffer object.</returns>
        /// <remarks>For more information, see <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.idirectsound8.idirectsound8.createsoundbuffer%28v=vs.85%29.aspx"/>.</remarks>
        void CreateSoundBuffer([In] BufferDescription desc, [Out, MarshalAs(UnmanagedType.Interface)] out IDirectSoundBuffer dsDSoundBuffer, IntPtr pUnkOuter);
        /// <summary>
        /// Retrieves the capabilities of the hardware device that is represented by the device object. 
        /// </summary>
        /// <param name="caps">Receives the capabilities of this sound device.</param>
        /// <returns>DSResult</returns>
        void GetCaps([MarshalAs(UnmanagedType.LPStruct)] DirectSoundCapabilities caps);
        /// <summary>
        /// Creates a new secondary buffer that shares the original buffer's memory.
        /// </summary>
        /// <param name="bufferOriginal">The buffer to duplicate.</param>
        /// <returns>The duplicated buffer.</returns>
        /// <remarks>For more information, see <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.idirectsound8.idirectsound8.duplicatesoundbuffer(v=vs.85).aspx"/>.</remarks>        
        void DuplicateSoundBuffer([In, MarshalAs(UnmanagedType.Interface)] IDirectSoundBuffer bufferOriginal, [In, MarshalAs(UnmanagedType.Interface)] IDirectSoundBuffer bufferDuplicate);
        /// <summary>
        /// Sets the cooperative level of the application for this sound device. 
        /// </summary>
        /// <param name="HWND">Handle to the application window.</param>
        /// <param name="dwLevel">The requested level.</param>
        void SetCooperativeLevel(IntPtr HWND, [In, MarshalAs(UnmanagedType.U4)] DirectSoundCooperativeLevel dwLevel);
        /// <summary>
        /// Has no effect. See remarks.
        /// </summary>
        /// <remarks>This method was formerly used for compacting the on-board memory of ISA sound cards.</remarks>
        /// <returns>DSResult</returns>
        void Compact();
        /// <summary>
        /// Retrieves the speaker configuration. 
        /// </summary>
        /// <returns>The speaker configuration.</returns>
        void GetSpeakerConfig(IntPtr pdwSpeakerConfig);
        /// <summary>
        /// Specifies the speaker configuration of the device. 
        /// </summary>
        /// <param name="pdwSpeakerConfig">The speaker configuration.</param>
        /// <remarks>
        /// In Windows Vista and later versions of Windows, <see cref="GetSpeakerConfig"/> is a NOP. For Windows Vista and later versions, the speaker configuration is a system setting that should not be modified by an application. End users can set the speaker configuration through control panels.
        /// For more information, see <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.idirectsound8.idirectsound8.setspeakerconfig(v=vs.85).aspx"/>.
        /// </remarks>
        void SetSpeakerConfig(uint pdwSpeakerConfig);
        /// <summary>
        /// Initializes a device object that was created by using the CoCreateInstance function. 
        /// </summary>
        /// <param name="guid">The globally unique identifier (GUID) specifying the sound driver to which this device object binds. Pass null to select the primary sound driver.</param>
        void Initialize([In, MarshalAs(UnmanagedType.LPStruct)] Guid guid);
    }


    static class IDirectSoundExtension
    {
        /// <summary>
        /// Gets the capabilities.
        /// </summary>
        public static DirectSoundCapabilities GetCaps(this IDirectSound target)
        {
            DirectSoundCapabilities tmp = new DirectSoundCapabilities();
            tmp.Size = Marshal.SizeOf(tmp);
            target.GetCaps(tmp);
            return tmp;
        }

        /// <summary>
        /// Checks whether the specified <paramref name="format"/> is supported.
        /// </summary>
        /// <param name="format">The wave format.</param>
        /// <returns>A value indicating whether the specified <paramref name="format"/> is supported. If true, the <paramref name="format"/> is supported; Otherwise false.</returns>
        public static bool SupportsFormat(this IDirectSound target,WaveFormat format)
        {
            DirectSoundCapabilities caps = target.GetCaps();
            bool result = true;
            if (format.Channels == 2)
                result &= (caps.Flags & DSCapabilitiesFlags.SecondaryBufferStereo) == DSCapabilitiesFlags.SecondaryBufferStereo;
            else if (format.Channels == 1)
                result &= (caps.Flags & DSCapabilitiesFlags.SecondaryBufferMono) == DSCapabilitiesFlags.SecondaryBufferMono;

            if (format.BitsPerSample == 8)
                result &= (caps.Flags & DSCapabilitiesFlags.SecondaryBuffer8Bit) == DSCapabilitiesFlags.SecondaryBuffer8Bit;
            else if (format.BitsPerSample == 16)
                result &= (caps.Flags & DSCapabilitiesFlags.SecondaryBuffer16Bit) == DSCapabilitiesFlags.SecondaryBuffer16Bit;

            result &= format.IsPCM();
            return result;
        }

        /// <summary>
        /// Combines a <see cref="DSSpeakerGeometry"/> value with a <see cref="DSSpeakerConfigurations"/> value.
        /// </summary>
        /// <param name="speakerConfiguration">Must be <see cref="DSSpeakerConfigurations.Stereo"/>.</param>
        /// <param name="speakerGeometry">The <see cref="DSSpeakerGeometry"/> value to combine with the <paramref name="speakerConfiguration"/>.</param>
        /// <returns>Combination out of the <paramref name="speakerConfiguration"/> and the <paramref name="speakerGeometry"/> value.</returns>
        /// <exception cref="ArgumentException">Must be stereo.; speakerConfiguration</exception>
        public static DSSpeakerConfigurations CombineSpeakerConfiguration(this IDirectSound target,DSSpeakerConfigurations speakerConfiguration, DSSpeakerGeometry speakerGeometry)
        {
            if (speakerConfiguration != DSSpeakerConfigurations.Stereo)
                throw new ArgumentException("Must be stereo.", "speakerConfiguration");

            int c = (int)speakerConfiguration;
            int g = (int)speakerGeometry;
            return (DSSpeakerConfigurations)(((byte)c) | (byte)g << 16);
        }
    }
}