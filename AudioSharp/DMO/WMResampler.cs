using System;
using System.Runtime.InteropServices;

namespace AudioSharp.DMO
{
    internal sealed class WMResampler : IDisposable
    {
        private bool _disposed;
        private MediaObject _mediaObject;
        private WMResamplerObject _obj;
        private WMResamplerProps _resamplerprops;

        public WMResampler()
        {
            //create a resampler instance
            var obj = new WMResamplerObject();

            _mediaObject = new MediaObject(Marshal.GetComInterfaceForObject((IMediaObject)obj, typeof(IMediaObject)));
            _resamplerprops =
                new WMResamplerProps(Marshal.GetComInterfaceForObject(obj as IWMResamplerProps,
                    typeof(IWMResamplerProps)));

            _obj = obj;
        }

        public WMResamplerProps ResamplerProps
        {
            get { return _resamplerprops; }
        }

        public MediaObject MediaObject
        {
            get { return _mediaObject; }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                GC.SuppressFinalize(this);
                Dispose(true);
            }
        }

        private void Dispose(bool disposing)
        {
            if (_resamplerprops != null)
            {
                _resamplerprops.Dispose();
                _resamplerprops = null;
            }
            if (_mediaObject != null)
            {
                _mediaObject.Dispose();
                _mediaObject = null;
            }
            if (_obj != null)
            {
                Marshal.ReleaseComObject(_obj);
                _obj = null;
            }
        }

        ~WMResampler()
        {
            Dispose(false);
        }

        //object to create an instance of wmresampler
        [ComImport]
        [Guid("f447b69e-1884-4a7e-8055-346f74d6edb3")]
        private class WMResamplerObject
        {
        }
    }
}

namespace AudioSharp.DMO.MONO
{
    internal sealed class WMResampler : IDisposable
    {
        #region PINVOKE
        [DllImport("AudioSharpDMO")]
        static extern IntPtr DMOWMResamplerCreate();

        [DllImport("AudioSharpDMO")]
        static extern void DMOWMResamlerDestroy(IntPtr ptr);

        [DllImport("AudioSharpDMO")]
        static extern IntPtr DMOWMResamler_mediaObject(IntPtr ptr);

        [DllImport("AudioSharpDMO")]
        static extern IntPtr DMOWMResamler_resamplerprops(IntPtr ptr);

        #endregion

        IntPtr nativeptr;
        private MediaObject _mediaObject;
        private WMResamplerProps _resamplerprops;

        WMResampler()
        {
            nativeptr = DMOWMResamplerCreate();
            _mediaObject = new MediaObject(DMOWMResamler_mediaObject(nativeptr), true);
            _resamplerprops = new WMResamplerProps(DMOWMResamler_resamplerprops(nativeptr), true);
        }

        ~WMResampler()
        {
            Dispose(false);
        }

        public WMResamplerProps ResamplerProps
        {
            get { return _resamplerprops; }
        }

        public MediaObject MediaObject
        {
            get { return _mediaObject; }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (_resamplerprops != null)
                {
                    _resamplerprops.Dispose();
                    _resamplerprops = null;
                }
                if (_mediaObject != null)
                {
                    _mediaObject.Dispose();
                    _mediaObject = null;
                }

                DMOWMResamlerDestroy(nativeptr);

                disposedValue = true;
            }
        }

        
        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}