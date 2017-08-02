using System.IO;
using AudioSharp.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using SharpDX.Win32;
using SharpDX.MediaFoundation;

namespace AudioSharp.MediaFoundation
{
    internal static class MediaFoundationCore
    {
        public const int MF_SDK_VERSION = 0x2;
        public const int MF_API_VERSION = 0x70;
        public const int MF_VERSION = (MF_SDK_VERSION << 16) | MF_API_VERSION;

        public const int MF_SOURCE_READER_FIRST_AUDIO_STREAM = unchecked((int)0xFFFFFFFD);
        public const int MF_SOURCE_READER_ALL_STREAMS = unchecked((int)0xFFFFFFFE);

        public const int MF_SOURCE_READER_MEDIASOURCE = unchecked((int)0xFFFFFFFF); //pass this to mfattributes streamindex arguments

        public static bool IsSupported { get; private set; }

        static MediaFoundationCore()
        {
            try
            {
                Startup();
                IsSupported = true;

                AppDomain.CurrentDomain.ProcessExit += (s, e) => Shutdown();
            }
            catch (Exception)
            {
                IsSupported = false;
            }
        }

        public static SharpDX.MediaFoundation.SinkWriter CreateSinkWriterFromByteStream(ByteStream byteStream, MediaAttributes attributes)
        {
            return MediaFactory.CreateSinkWriterFromURL(null, byteStream.NativePointer, attributes);
        }

        public static bool IsTransformAvailable(IEnumerable<Activate> transforms, Guid transformGuid)
        {
            try
            {
                return
                    transforms.Select(t => (Guid) t.Get(MediaFoundationAttributes.MFT_TRANSFORM_CLSID_Attribute))
                        .Any(value => value == transformGuid);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsTransformAvailable(Guid category, Guid transformClsid)
        {
            var clsids = MFTEnumerator.EnumerateTransforms(category,null,null);
            return clsids.Any(x => x == transformClsid);
        }


        public static SourceReader CreateSourceReaderFromByteStream(ByteStream byteStream, MediaAttributes attributes)
        {
            return new SourceReader(byteStream, attributes);
        }

        private static bool _isstarted;

        public static void Startup()
        {
            if (!_isstarted)
            {
                MediaFactory.Startup(MediaFactory.Version, 0);
                _isstarted = true;
            }
        }

        public static void Shutdown()
        {
            if (_isstarted)
            {
                MediaFactory.Shutdown();
                _isstarted = false;
            }
        }

        public static MediaType MediaTypeFromWaveFormat(WaveFormat waveFormat)
        {
            var _waveFormat =
                    SharpDX.Multimedia.WaveFormat.CreateCustomFormat( (SharpDX.Multimedia.WaveFormatEncoding)(short)waveFormat.WaveFormatTag,
                waveFormat.SampleRate,
                waveFormat.Channels,
                waveFormat.BytesPerSecond,
                waveFormat.BlockAlign,
                waveFormat.BitsPerSample)
               ;
            var mediaType = new MediaType();
            MediaFactory.InitMediaTypeFromWaveFormatEx(mediaType,
                new SharpDX.Multimedia.WaveFormat[] {_waveFormat},
                Marshal.SizeOf(waveFormat));
            return mediaType;
        }
    }
}