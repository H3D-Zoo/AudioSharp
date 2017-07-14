using System;
using System.Runtime.InteropServices;
using System.Security;

namespace CSCore.DirectSound
{
    /// <summary>
    /// Is used to create buffer objects, manage devices, and set up the environment. This object supersedes <see cref="DirectSoundBase"/> and adds new methods.
    /// Obtain a instance by calling the <see cref="DirectSoundBase.Create8"/> method.
    /// </summary>
    /// 
    [ComImport,
     Guid("C50A7E93-F395-4834-9EF6-7FA99DE50966"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     SuppressUnmanagedCodeSecurity]
    internal interface IDirectSound8 : IDirectSound
    {
        /// <summary>
        /// Ascertains whether the device driver is certified for DirectX. 
        /// </summary>
        /// <param name="certified">Receives a value which indicates whether the device driver is certified for DirectX.</param>
        /// <returns>DSResult</returns>
        DSResult VerifyCertificationNative(out DSCertification certified);
    }

    static class IDirectSound8Extension
    {
        /// <summary>
        /// Ascertains whether the device driver is certified for DirectX. 
        /// </summary>
        /// <returns>A value which indicates whether the device driver is certified for DirectX. On emulated devices, the method returns <see cref="DSCertification.Unsupported"/>.</returns>
        public static DSCertification VerifyCertification(this IDirectSound8 target)
        {
            DSCertification certification;
            var result = target.VerifyCertificationNative(out certification);
            if (result == DSResult.Unsupported)
                return DSCertification.Unsupported;
            DirectSoundException.Try(result, "IDirectSound8",
                "VerifyCertification");
            return certification;
        }
    }
}