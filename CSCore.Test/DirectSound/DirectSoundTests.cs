using CSCore.DirectSound;
using CSCore.SoundOut;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace CSCore.Test.DirectSound
{
    [TestClass]
    public class DirectSoundTests
    {
        [TestMethod]
        [TestCategory("DirectSound")]
        public void EnumerateDirectSoundDeviceTest()
        {
            var devices = DirectSoundDeviceEnumerator.EnumerateDevices();
            foreach (var device in devices)
            {
                Debug.WriteLine(device.ToString());
            }
        }

        [TestMethod]
        [TestCategory("DirectSound")]
        public void OpenDirectSoundDevice()
        {
            DirectSoundOut dsoundOut = new DirectSoundOut();
            dsoundOut.Initialize(new CSCore.Streams.SineGenerator().ToWaveSource(16));
            dsoundOut.Dispose();
        }

       
        private short[] GenerateData(int bufferSize, WaveFormat waveFormat)
        {
            int samples = bufferSize / waveFormat.BlockAlign;
            short[] data = new short[2 * samples];
            int dataIndex = 0;
            for (int i = 0; i < samples; i++)
            {
                double vibrato = Math.Cos(2 * Math.PI * 10.0 * i / waveFormat.SampleRate);
                short value = (short)(Math.Cos(2 * Math.PI * (220.0 + 4.0 * vibrato) * i / waveFormat.SampleRate) * 16384); // Not too loud
                data[dataIndex++] = value;
                data[dataIndex++] = value;
            }

            return data;
        }
    }
}