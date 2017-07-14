using System;
using System.Runtime.InteropServices;
using System.Security;
using CSCore.Win32;

namespace CSCore.DirectSound
{
    /// <summary>
    /// Sets up notification events for a playback or capture buffer.
    /// </summary>
    /// 
    [ComImport,
     Guid("B0210783-89CD-11D0-AF08-00A0C925CD16"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
     SuppressUnmanagedCodeSecurity]
    [SuppressUnmanagedCodeSecurity]
    public interface IDirectSoundNotify 
    {
        /// <summary>
        /// Sets the notification positions. During capture or playback, whenever the read or play cursor reaches one of the specified offsets, the associated event is signaled. 
        /// </summary>
        /// <param name="notifies">An array of <see cref="DSBPositionNotify"/> structures.</param>
        void SetNotificationPositions(int cPositionNotifies, [MarshalAs(UnmanagedType.LPArray)] DSBPositionNotify[] notifies);
    }
}