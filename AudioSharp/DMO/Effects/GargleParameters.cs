using System.Runtime.InteropServices;

namespace AudioSharp.DMO.Effects
{
    /// <summary>
    /// Internal parameter structure for the <see cref="DirectSoundFXGargle"/> effect.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GargleParameters
    {
        /// <summary>
        /// The rate hz.
        /// </summary>
        public int RateHz;
        /// <summary>
        /// The wave shape.
        /// </summary>
        public int WaveShape;
    }
}