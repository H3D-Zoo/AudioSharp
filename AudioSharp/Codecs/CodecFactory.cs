using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


namespace AudioSharp.Codecs
{
    /// <summary>
    ///     Helps to choose the right decoder for different codecs.
    /// </summary>
    public class CodecFactory
    {
        // ReSharper disable once InconsistentNaming
        private static readonly CodecFactory _instance = new CodecFactory();

        private CodecFactory()
        {
        }

        /// <summary>
        ///     Gets the default singleton instance of the <see cref="CodecFactory" /> class.
        /// </summary>
        public static CodecFactory Instance
        {
            get { return _instance; }
        }

        /// <summary>
        ///     Gets the file filter in English. This filter can be used e.g. in combination with an OpenFileDialog.
        /// </summary>
        public static string SupportedFilesFilterEn
        {
            get { return Instance.GenerateFilter(); }
        }



        /// <summary>
        ///     Returns all the common file extensions of all supported codecs. Note that some of these file extensions belong to
        ///     more than one codec.
        ///     That means that it can be possible that some files with the file extension abc can be decoded but other a few files
        ///     with the file extension abc can't be decoded.
        /// </summary>
        /// <returns>Supported file extensions.</returns>
        public string[] GetSupportedFileExtensions()
        {
            var extensions = new string[]
            {
               "mp3" ,"mpeg3",
               "wav","wave",
               "flac", "fla",
               "aiff", "aif", "aifc",
               "aac", "adt", "adts", "m2ts", "mp2", "3g2", "3gp2", "3gp", "3gpp", "m4a", "m4v", "mp4v", "mp4","mov",
               "asf", "wm", "wmv", "wma",
                "mp1", "m2ts",
                "mp2", "m2ts",
                "mp2", "m2ts", "m4a", "m4v", "mp4v", "mp4", "mov", "asf", "wm", "wmv", "wma", "avi", "ac3", "ec3"
            };
            return extensions;
        }

        private string GenerateFilter()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Supported Files|");
            stringBuilder.Append(String.Concat(GetSupportedFileExtensions().Select(x => "*." + x + ";").ToArray()));
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        private class DisposeFileStreamSource : WaveAggregatorBase
        {
            private Stream _stream;

            public DisposeFileStreamSource(IWaveSource source, Stream stream)
                : base(source)
            {
                _stream = stream;
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (_stream != null)
                {
                    try
                    {
                        _stream.Dispose();
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Stream was already disposed.");
                    }
                    finally
                    {
                        _stream = null;
                    }
                }
            }
        }
    }
}