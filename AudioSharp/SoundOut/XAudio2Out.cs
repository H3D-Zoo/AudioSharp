using AudioSharp.XAudio2;
using System;


namespace AudioSharp.SoundOut
{
    public class XAudio2Out : ISoundOut
    {
        private bool _disposed;
        private XAudio2.XAudio2 _xaudio2;
        private XAudio2MasteringVoice _masteringVoice;
        private StreamingSourceVoice _streamingSourceVoice;
        private IWaveSource _source;
        private PlaybackState _playbackState;


        public float Volume {
            get
            {
                return _streamingSourceVoice.Volume;
            }
            set
            {
                _streamingSourceVoice.Volume = value;
            }
        }

        public IWaveSource WaveSource
        {
            get
            {
                return _source;
            }
        }

        public PlaybackState PlaybackState
        {
            get
            {
                return _playbackState;
            }
        }

        public event EventHandler<PlaybackStoppedEventArgs> Stopped;

        public XAudio2Out()
        {
            _xaudio2 = XAudio2.XAudio2.CreateXAudio2();
            _masteringVoice = _xaudio2.CreateMasteringVoice();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                Stop();
                _masteringVoice.Dispose();
                _masteringVoice = null;
                _xaudio2.Dispose();
                _xaudio2 = null;
            }
            _disposed = true;
        }

        public void Initialize(IWaveSource source)
        {
            _source = source;
            _streamingSourceVoice = new StreamingSourceVoice(_xaudio2, source);
            StreamingSourceVoiceListener.Default.Add(_streamingSourceVoice);
            _playbackState = PlaybackState.Stopped;
        }

        public void Pause()
        {
            if (_playbackState == PlaybackState.Playing)
            {
                _xaudio2.StopEngine();
            }
            _playbackState = PlaybackState.Paused;
        }

        public void Play()
        {
            if(_streamingSourceVoice == null)
            {
                _streamingSourceVoice = new StreamingSourceVoice(_xaudio2,_source);
                StreamingSourceVoiceListener.Default.Add(_streamingSourceVoice);
            }
            if (_playbackState == PlaybackState.Paused)
                Resume();
            if(_playbackState != PlaybackState.Playing)
                _streamingSourceVoice.Start();
            _playbackState = PlaybackState.Playing;
        }

        public void Resume()
        {
            if (_playbackState == PlaybackState.Paused)
            {
                _xaudio2.StartEngine();
            }
            _playbackState = PlaybackState.Playing;
        }

        public void Stop()
        {
            if (_streamingSourceVoice != null)
            {
                StreamingSourceVoiceListener.Default.Remove(_streamingSourceVoice);
                _streamingSourceVoice.Stop();
                _streamingSourceVoice.Dispose();
                _streamingSourceVoice = null;
            }
            _playbackState = PlaybackState.Stopped;
        }
    }
}
