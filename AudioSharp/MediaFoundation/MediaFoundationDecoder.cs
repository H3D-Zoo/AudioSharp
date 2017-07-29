using System;
using System.IO;
using System.Runtime.InteropServices;
using SharpDX.MediaFoundation;
using SharpDX.Win32;

namespace AudioSharp.MediaFoundation
{
    /// <summary>
    ///     The <see cref="MediaFoundationDecoder" /> is a generic decoder for all installed Mediafoundation codecs.
    /// </summary>
    public class MediaFoundationDecoder : IWaveSource
    {
        private readonly bool _hasFixedLength;
        private readonly Object _lockObj = new Object();
        private ByteStream _byteStream;

        private byte[] _decoderBuffer;
        private int _decoderBufferCount;
        private int _decoderBufferOffset;
        private bool _disposed;
        private long _length;

        //could not find a possibility to find out the position -> we have to track the position ourselves.
        private long _position;

        private SourceReader _reader;
        private Stream _stream;
        private WaveFormat _waveFormat;
        private bool _positionChanged;

        static MediaFoundationDecoder()
        {
            MediaFoundationCore.Startup(); //make sure that the MediaFoundation is started up.
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaFoundationDecoder" /> class.
        /// </summary>
        /// <param name="url">Uri which points to an audio source which can be decoded.</param>
        public MediaFoundationDecoder(string url)
        {
            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            _hasFixedLength = true;

            _reader = Initialize(MediaFoundationCore.CreateSourceReaderFromUrl(url));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaFoundationDecoder" /> class.
        /// </summary>
        /// <param name="stream">Stream which provides the audio data to decode.</param>
        public MediaFoundationDecoder(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArgumentException("Stream is not readable.", "stream");

            _stream = stream;
            _byteStream = new ByteStream(stream); 
            _reader = Initialize(_byteStream);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaFoundationDecoder" /> class.
        /// </summary>
        /// <param name="byteStream">Stream which provides the audio data to decode.</param>
        public MediaFoundationDecoder(ByteStream byteStream)
        {
            if (byteStream == null)
                throw new ArgumentNullException("byteStream");
            _byteStream = byteStream;
            _reader = Initialize(_byteStream);
        }

        /// <summary>
        ///     Reads a sequence of bytes from the <see cref="MediaFoundationDecoder" /> and advances the position within the
        ///     stream by the
        ///     number of bytes read.
        /// </summary>
        /// <param name="buffer">
        ///     An array of bytes. When this method returns, the <paramref name="buffer" /> contains the specified
        ///     byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> +
        ///     <paramref name="count" /> - 1) replaced by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        ///     The zero-based byte offset in the <paramref name="buffer" /> at which to begin storing the data
        ///     read from the current stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to read from the current source.</param>
        /// <returns>The total number of bytes read into the buffer.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            CheckForDisposed();

            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (buffer.Length < count)
                throw new ArgumentException("Length is too small.", "buffer");

            lock (_lockObj)
            {
                int read = 0;

                if (_reader == null || _disposed)
                    return read;

                if (_decoderBufferCount > 0)
                    read += CopyDecoderBuffer(buffer, offset + read, count - read);

                while (read < count)
                {
                    SourceReaderFlags flags;
                    long timestamp;
                    int actualStreamIndex;
                    using (
                        Sample sample = _reader.ReadSample(SourceReaderIndex.FirstAudioStream, 0,
                            out actualStreamIndex, out flags, out timestamp))
                    {
                        if (flags != SourceReaderFlags.None)
                            break;
                        var sampleTime = sample.SampleTime;
                        if (_positionChanged && timestamp > 0)
                        {
                            long actualPosition = NanoSecond100UnitsToBytes(sampleTime);
                            int bytesToSkip = (int) (_position - actualPosition);
                            _position = actualPosition;
                            _positionChanged = false;

                            SkipBytes(bytesToSkip);
                        }

                        using (var mediaBuffer = sample.ConvertToContiguousBuffer())
                        {
                            using (MediaBuffer.LockDisposable @lock = mediaBuffer.Lock())
                            {
                                _decoderBuffer = _decoderBuffer.CheckBuffer(@lock.CurrentLength);
                                Marshal.Copy(@lock.Buffer, _decoderBuffer, 0, @lock.CurrentLength);
                                _decoderBufferCount = @lock.CurrentLength;
                                _decoderBufferOffset = 0;

                                int tmp = CopyDecoderBuffer(buffer, offset + read, count - read);
                                read += tmp;
                            }
                        }
                    }
                }

                _position += read;

                return read;
            }
        }

        private void SkipBytes(int numberOfBytes)
        {
            numberOfBytes += numberOfBytes % WaveFormat.BlockAlign;
            if (numberOfBytes <= 0)
                return;

            int read;
            byte[] buffer = new byte[Math.Min(WaveFormat.BytesPerSecond * 2, numberOfBytes)];

            while ((read = Read(buffer, 0, Math.Min(WaveFormat.BytesPerSecond * 2, numberOfBytes))) > 0)
            {
                numberOfBytes -= read;
                if (numberOfBytes <= 0)
                {
                    break;
                }
            }
        }

        /// <summary>
        ///     Disposes the <see cref="MediaFoundationDecoder" />.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        ///     Gets the format of the decoded audio data provided by the <see cref="Read" /> method.
        /// </summary>
        public WaveFormat WaveFormat
        {
            get { return _waveFormat; }
        }

        /// <summary>
        ///     Gets or sets the position of the output stream, in bytes.
        /// </summary>
        public long Position
        {
            get { return !_disposed ? _position : 0; }
            set
            {
                CheckForDisposed();
                SetPosition(value);
            }
        }

        /// <summary>
        ///     Gets the total length of the decoded audio, in bytes.
        /// </summary>
        public long Length
        {
            get
            {
                if (_disposed)
                    return 0;
                if (_hasFixedLength)
                    return _length;
                return GetLength(_reader);
            }
        }

        /// <summary>
        ///     Gets a value which indicates whether the seeking is supported. True means that seeking is supported. False means
        ///     that seeking is not supported.
        /// </summary>
        public bool CanSeek
        {
            get { return _reader.CanSeek; }
        }

        private SourceReader Initialize(ByteStream byteStream)
        {
            return Initialize(MediaFoundationCore.CreateSourceReaderFromByteStream(byteStream, null));
        }

        private SourceReader Initialize(SourceReader reader)
        {
            try
            {
                reader.SetStreamSelection(SourceReaderIndex.AllStreams, false);
                reader.SetStreamSelection(SourceReaderIndex.FirstAudioStream, true);

                using (var mediaType =new MediaType())
                {
                    mediaType.Set(MediaTypeAttributeKeys.MajorType, AudioSubTypes.MediaTypeAudio);
                    mediaType.Set(MediaTypeAttributeKeys.Subtype, AudioSubTypes.Pcm);

                    reader.SetCurrentMediaType(SourceReaderIndex.FirstAudioStream, mediaType);
                }

                using (
                    var currentMediaType =
                        reader.GetCurrentMediaType(SourceReaderIndex.FirstAudioStream))
                {
                    if (currentMediaType.MajorType != AudioSubTypes.MediaTypeAudio)
                    {
                        throw new InvalidOperationException(String.Format(
                            "Invalid Majortype set on sourcereader: {0}.", currentMediaType.MajorType));
                    }

                    AudioEncoding encoding = AudioSubTypes.EncodingFromSubType(currentMediaType.SubType);

                    ChannelMask channelMask;
                    if (currentMediaType.TryGet(MediaFoundationAttributes.MF_MT_AUDIO_CHANNEL_MASK, out channelMask))
                        //check whether the attribute is available
                    {
                        _waveFormat = new WaveFormatExtensible(currentMediaType.SampleRate,
                            currentMediaType.BitsPerSample, currentMediaType.Channels, currentMediaType.SubType,
                            channelMask);
                    }
                    else
                    {
                        _waveFormat = new WaveFormat(currentMediaType.SampleRate, currentMediaType.BitsPerSample,
                            currentMediaType.Channels, encoding);
                    }
                }

                reader.SetStreamSelection(SourceReaderIndex.FirstAudioStream, true);

                if (_hasFixedLength)
                    _length = GetLength(reader);

                return reader;
            }
            catch (Exception)
            {
                DisposeInternal();
                throw;
            }
        }

        private long GetLength(SourceReader reader)
        {
            lock (_lockObj)
            {
                try
                {
                    if (reader == null)
                        return 0;

                    var value =
                         reader.GetPresentationAttribute(SourceReaderIndex.MediaSource,
                             MediaFoundationAttributes.MF_PD_DURATION);
                        //bug: still, depending on the decoder, this returns imprecise values.
                        return NanoSecond100UnitsToBytes((long)(ulong)value.Value);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        private void SetPosition(long value)
        {
            if (CanSeek)
            {
                lock (_lockObj)
                {
                    value -= (value % WaveFormat.BlockAlign);
                    long hnsPos = BytesToNanoSecond100Units(value);
                    var propertyVariant = new Variant() { Value = hnsPos};
                    _reader.SetCurrentPosition(Guid.Empty, propertyVariant);
                    _decoderBufferCount = 0;
                    _decoderBufferOffset = 0;
                    _position = value;

                    _positionChanged = true;
                }
            }
        }

        private int CopyDecoderBuffer(byte[] destBuffer, int offset, int count)
        {
            count = Math.Min(count, _decoderBufferCount);
            Array.Copy(_decoderBuffer, _decoderBufferOffset, destBuffer, offset, count);
            _decoderBufferCount -= count;
            _decoderBufferOffset += count;

            if (_decoderBufferCount == 0)
                _decoderBufferOffset = 0;

            return count;
        }

        private long NanoSecond100UnitsToBytes(long nanoSeconds100Units)
        {
            long result = (nanoSeconds100Units * WaveFormat.BytesPerSecond) / 10000000L;
            result += result % WaveFormat.BlockAlign;
            return result;
        }

        private long BytesToNanoSecond100Units(long bytes)
        {
            return (10000000L * bytes) / WaveFormat.BytesPerSecond;
        }

        /// <summary>
        ///     Disposes the <see cref="MediaFoundationDecoder" /> and its internal resources.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            lock (_lockObj)
            {
                DisposeInternal();
            }
        }

        private void DisposeInternal()
        {
            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }
            if (_byteStream != null)
            {
                _byteStream.Dispose();
                _byteStream = null;
            }
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        private void CheckForDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="MediaFoundationDecoder" /> class.
        /// </summary>
        ~MediaFoundationDecoder()
        {
            Dispose(false);
        }
    }
}