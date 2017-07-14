using System;

namespace CSCore.DirectSound
{
    internal static class DSUtils
    {
        public static readonly Guid AllObjects = new Guid("aa114de5-c262-4169-a1c8-23d698cc73b5");

       

        //public static IntPtr GetConsoleHandle()
        //{
        //    return FindWindow(Console.Title);
        //}

        public static IntPtr FindWindow(string atom, string windowTitle)
        {
            return NativeMethods.FindWindow(atom, windowTitle);
        }

        public static IntPtr FindWindow(string windowTitle)
        {
            return NativeMethods.FindWindow(null, windowTitle);
        }

        public static IntPtr GetDesktopWindow()
        {
            return NativeMethods.GetDesktopWindow();
        }
    }
}
