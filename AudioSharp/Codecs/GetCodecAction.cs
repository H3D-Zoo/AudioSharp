using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AudioSharp.Codecs
{
    /// <summary>
    /// Delegate which initializes a new decoder for a specific codec based on a <paramref name="stream"/>.
    /// </summary>
    /// <param name="url"><see cref="Uri"/> which contains the data that should be decoded by the codec decoder.</param>
    /// <returns>Decoder for a specific coded based on a <paramref name="stream"/>.</returns>
    public delegate IWaveSource GetCodecAction(string url);
}