//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

namespace Sce.Atf.Wpf.Docking
{
    internal class Win32Calls
    {
        internal static Point GetPosition(Visual relativeTo)
        {
            var w32Mouse = new Windows.POINT();
            GetCursorPos(ref w32Mouse);
            return relativeTo.PointFromScreen(new Point(w32Mouse.x, w32Mouse.y));
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Windows.POINT pt);
    }
}
