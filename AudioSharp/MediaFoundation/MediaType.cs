﻿// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
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
using System;

using SharpDX.Multimedia;
using AudioSharp.MediaFoundation;
using AudioSharp;

namespace SharpDX.MediaFoundation
{
    public partial class MediaType
    {
        /// <summary>	
        /// Creates an empty media type.	
        /// </summary>	
        /// <remarks>	
        /// <p> The media type is created without any attributes. </p>	
        /// </remarks>	
        /// <msdn-id>ms693861</msdn-id>	
        /// <unmanaged>HRESULT MFCreateMediaType([Out] IMFMediaType** ppMFType)</unmanaged>	
        /// <unmanaged-short>MFCreateMediaType</unmanaged-short>	
        public MediaType()
        {
            MediaFactory.CreateMediaType(this);
        }

        /// <summary>	
        /// <p><strong>Applies to: </strong>desktop apps | Metro style apps</p><p> </p><p>Converts a Media Foundation audio media type to a <strong><see cref="SharpDX.Multimedia.WaveFormat"/></strong> structure.</p>	
        /// </summary>	
        /// <param name="bufferSize"><dd> <p>Receives the size of the <strong><see cref="SharpDX.Multimedia.WaveFormat"/></strong> structure.</p> </dd></param>	
        /// <param name="flags"><dd> <p>Contains a flag from the <strong><see cref="SharpDX.MediaFoundation.WaveFormatExConvertFlags"/></strong> enumeration.</p> </dd></param>	
        /// <returns>a reference to the <strong><see cref="SharpDX.Multimedia.WaveFormat"/></strong> structure.</returns>	
        /// <remarks>	
        /// <p>If the <strong>wFormatTag</strong> member of the returned structure is <strong><see cref="SharpDX.Multimedia.WaveFormatEncoding.Extensible"/></strong>, you can cast the reference to a <strong><see cref="SharpDX.Multimedia.WaveFormatExtensible"/></strong> structure.</p>	
        /// </remarks>	
        /// <msdn-id>ms702177</msdn-id>	
        /// <unmanaged>HRESULT MFCreateWaveFormatExFromMFMediaType([In] IMFMediaType* pMFType,[Out] void** ppWF,[Out, Optional] unsigned int* pcbSize,[In] unsigned int Flags)</unmanaged>	
        /// <unmanaged-short>MFCreateWaveFormatExFromMFMediaType</unmanaged-short>	
        public Multimedia.WaveFormat ExtracttWaveFormat(out int bufferSize, WaveFormatExConvertFlags flags = WaveFormatExConvertFlags.Normal)
        {
            IntPtr waveFormat;
            MediaFactory.CreateWaveFormatExFromMFMediaType(this, out waveFormat, out bufferSize, (int)flags);
            return Multimedia.WaveFormat.MarshalFrom(waveFormat);
        }

        public Guid SubType
        {
            get {return Get(MediaTypeAttributeKeys.Subtype); }
            set { Set(MediaTypeAttributeKeys.Subtype, value); }
        }

        /// <summary>
        /// Gets or sets the number of channels.
        /// </summary>
        public int Channels
        {
            get { return GetInt(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS); }
            set { Set(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS, value); }
        }

        /// <summary>
        /// Gets or sets the number of bits per sample.
        /// </summary>
        public int BitsPerSample
        {
            get { return GetInt(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE); }
            set { Set(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE, value); }
        }

        /// <summary>
        /// Gets or sets the number of samples per second (for one channel each).
        /// </summary>
        public int SampleRate
        {
            get { return GetInt(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND); }
            set { Set(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND, value); }
        }

        /// <summary>
        /// Gets or sets the channelmask.
        /// </summary>
        public ChannelMask ChannelMask
        {
            get { return (ChannelMask)GetInt(MediaFoundationAttributes.MF_MT_AUDIO_CHANNEL_MASK); }
            set { Set(MediaFoundationAttributes.MF_MT_AUDIO_CHANNEL_MASK, (int)value); }
        }

        /// <summary>
        /// Gets or sets the average number of bytes per second.
        /// </summary>
        public int AverageBytesPerSecond
        {
            get { return GetInt(MediaFoundationAttributes.MF_MT_AUDIO_AVG_BYTES_PER_SECOND); }
            set { Set(MediaFoundationAttributes.MF_MT_AUDIO_AVG_BYTES_PER_SECOND, value); }
        }
    }
}