![AudioSharp Logo](https://ss2.baidu.com/6ONYsjip0QIZ8tyhnq/it/u=3045597410,4080357840&fm=96)

# AudioSharp - .NET Audio Library #

AudioSharp is a free .NET audio library which is completely written in C#. Although it is still a rather young project, it offers tons of features like playing or capturing audio, en- or decoding many different codecs, effects and much more!

AudioSharp is based on a very extensible architecture which allows you to make it fit to your needs without any major effort. You can build music players, voice chats, audio recorders and so on!

For more details, take a look at the source or the [online documentation](http://filoe.github.io/AudioSharp/sharpDox/).

Feel free to download or clone the source code:

    https://github.com/H3D-Zoo/AudioSharp.git

You may prefer to install the [AudioSharp nuget package](https://www.nuget.org/packages/AudioSharp/):

    Install-Package AudioSharp
    
For **FFmpeg** support, install the [AudioSharp.FFmpeg nuget package](https://www.nuget.org/packages/AudioSharp.Ffmpeg/)

    Install-Package AudioSharp.Ffmpeg -Pre
    
### Why AudioSharp? ###
 - **Highly optimized PERFORMANCE** through usage of CLI instructions
 - **Designed for newbies and professionals** 
 - **Tons of features**
 - **Fast support on [github](https://github.com/H3D-Zoo/AudioSharp)
 - **High code coverage through unit tests** 
 - **Licensed under the MS-PL** (does not include the [AudioSharp.Ffmpeg](https://github.com/H3D-Zoo/AudioSharp/tree/master/AudioSharp.FFmpeg) project which is licensed under the LGPL)

### Supported Features ###

Currently the following features are implemented:

- **Realtime audio processing**
  - Process audio data in realtime
  - Apply any processors in any order you want in realtime
  - Create custom processors (e.g. effects, analyzes, decoders,...)
- **Codecs** *1
  - MP3
  - WAVE (PCM, IeeeFloat, GSM, ADPCM,...)
  - FLAC
  - AAC
  - AC3
  - WMA
  - Raw data
  - OGG-Vorbis (through NVorbis)
  - FFmpeg (lots of additional formats, see [AudioSharp.FFmpeg](https://github.com/H3D-Zoo/AudioSharp/tree/master/AudioSharp.Ffmpeg))
- **FFmpeg support**
  - Supported through [AudioSharp.FFmpeg](https://github.com/H3D-Zoo/AudioSharp/tree/master/AudioSharp.Ffmpeg))
- **Speaker Output**
  - WaveOut
  - DirectSoundOut (realtime streaming)
  - WASAPI (loop- and event-callback + exclusive mode available)
  - XAudio2
- **Recording**
  - WaveIn
  - WASAPI (loop- and event-callback + exclusive mode available)
  - WASAPI loopback capture (capture output from soundcard)
- **DSP Algorithms**
  - Fastfouriertransform (FFT)
  - Effects (Echo, Compressor, Reverb, Chorus, Gargle, Flanger,...)
  - Resampler
  - Channel-mixing using custom channel-matrices
  - Generic Equalizer
  - ...
- **XAudio2 support**
  - XAudio2.7 and XAudio2.8 support
  - 3D Audio support (see X3DAudio sample)
  - Streaming source voice implementation allowing
    the client to stream any codec, custom effect,... to XAudio2
  - Optimized for games
- **Mediafoundation encoding and decoding**
- **DirectX Media Objects Wrapper**
- **CoreAudioAPI Wrapper**
  - WASAPI
  - Windows Multimedia Devices
  - Windows Audio Session
  - Endpoint Volume,...
- **Multi-Channel support**
- **Flexible**
  - Configure and customize any parts of AudioSharp
  - Use low latency values for realtime performance, high latency values for stability
  - Adjust the audio quality
  - Configure custom channel matrices
  - Create custom effects
  - ...
  - Or simply: **Make AudioSharp fit your needs!**
- **Tags** (ID3v1, ID3v2, FLAC)
- **Sample Winforms Visualizations**
- **Optimized performance though the usage of CLI instructions provided by a custom post compiler**

**\*1** Some Codecs are only available on certain platforms.

#### Samples: ####

["AudioPlayerSample"](Samples/AudioPlayerSample) Sample:

For more samples see [Samples](Samples/)

