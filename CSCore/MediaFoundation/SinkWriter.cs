using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDX.MediaFoundation
{
    public partial class SinkWriter
    {
        public int AddStream(MediaType targetMediaTypeRef)
        {
            int dwStreamIndexRef;
            AddStream(targetMediaTypeRef, out dwStreamIndexRef);
            return dwStreamIndexRef;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SinkWriter"/> class with a underlying <paramref name="byteStream"/>.
        /// </summary>
        /// <param name="byteStream">The underlying <see cref="ByteStream"/> to use.</param>
        /// <param name="attributes">Attributes to configure the <see cref="SinkWriter"/>. For more information, see <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/dd389284(v=vs.85).aspx"/>. Use null/nothing as the default value.</param>
        public static SinkWriter Create(ByteStream byteStream, MediaAttributes attributes = null)
	    {
            return MediaFactory.CreateSinkWriterFromURL(null, byteStream.NativePointer, attributes);
        }

        public SinkWriterStatistics GetStatistics(int dwStreamIndex)
        {
            SinkWriterStatistics statsRef = new SinkWriterStatistics();
            GetStatistics(dwStreamIndex, out statsRef);
            return statsRef;
        }
    }
}
