//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Runtime.InteropServices;

namespace Sce.Atf.Windows
{
    public enum THEMESIZE
    {
        TS_MIN,             // minimum size
        TS_TRUE,            // size without stretching
        TS_DRAW             // size that theme mgr will use to draw part
    };

    public enum PROPERTYORIGIN
    {
        PO_STATE,           // property was found in the state section
        PO_PART,            // property was found in the part section
        PO_CLASS,           // property was found in the class section
        PO_GLOBAL,          // property was found in [globals] section
        PO_NOTFOUND         // property was not found
    };

    public enum WINDOWTHEMEATTRIBUTETYPE
    {
        WTA_NONCLIENT = 1
    };

    public enum BP_BUFFERFORMAT
    {
        BPBF_COMPATIBLEBITMAP,    // Compatible bitmap
        BPBF_DIB,                 // Device-independent bitmap
        BPBF_TOPDOWNDIB,          // Top-down device-independent bitmap
        BPBF_TOPDOWNMONODIB       // Top-down monochrome device-independent bitmap
    }

    public enum BP_ANIMATIONSTYLE
    {
        BPAS_NONE,                // No animation
        BPAS_LINEAR,              // Linear fade animation
        BPAS_CUBIC,               // Cubic fade animation
        BPAS_SINE                 // Sinusoid fade animation
    }

    [Flags]
    public enum WTNCA : uint
    {
        /// <summary>
        /// Prevents the window caption from being drawn.
        /// </summary>
        NODRAWCAPTION = 1,

        /// <summary>
        /// Prevents the system icon from being drawn.
        /// </summary>
        NODRAWICON = 2,

        /// <summary>
        /// Prevents the system icon menu from appearing.
        /// </summary>
        NOSYSMENU = 4,

        /// <summary>
        /// Prevents mirroring of the question mark, even in right-to-left (RTL) layout.
        /// </summary>
        NOMIRRORHELP = 8,

        /// <summary>
        /// A mask that contains all the valid bits.
        /// </summary>
        VALIDBITS = NODRAWCAPTION | NODRAWICON | NOSYSMENU | NOMIRRORHELP
    }

    

    [StructLayout(LayoutKind.Sequential)]
    public struct DTBGOPTS
    {
        public uint dwSize;           // size of the struct
        public uint dwFlags;          // which options have been specified
        public RECT rcClip;            // clipping rectangle
    }

    [StructLayout((LayoutKind.Sequential))]
    public struct MARGINS
    {
        public int cxLeftWidth;      // width of left border that retains its size
        public int cxRightWidth;     // width of right border that retains its size
        public int cyTopHeight;      // height of top border that retains its size
        public int cyBottomHeight;   // height of bottom border that retains its size
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INTLIST
    {
        public int iValueCount; // number of values in iValues
        public int[] iValues;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WTA_OPTIONS
    {
        public WTNCA dwFlags;          // values for each style option specified in the bitmask
        public WTNCA dwMask;           // bitmask for flags that are changing
        // valid options are: WTNCA_NODRAWCAPTION, WTNCA_NODRAWICON, WTNCA_NOSYSMENU
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DTTOPTS
    {
        public uint dwSize;              // size of the struct
        public uint dwFlags;             // which options have been specified
        public COLORREF crText;              // color to use for text fill
        public COLORREF crBorder;            // color to use for text outline
        public COLORREF crShadow;            // color to use for text shadow
        public int iTextShadowType;     // TST_SINGLE or TST_CONTINUOUS
        public POINT ptShadowOffset;      // where shadow is drawn (relative to text)
        public int iBorderSize;         // Border radius around text
        public int iFontPropId;         // Font property to use for the text instead of TMT_FONT
        public int iColorPropId;        // Color property to use for the text instead of TMT_TEXTCOLOR
        public int iStateId;            // Alternate state id
        public bool fApplyOverlay;       // Overlay text on top of any text effect?
        public int iGlowSize;           // Glow radious around text
        public Uxtheme.DTT_CALLBACK_PROC pfnDrawTextCallback; // Callback for DrawText
        public int lParam;              // Parameter for callback
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BP_ANIMATIONPARAMS
    {
        public uint cbSize;
        public uint dwFlags; // BPAF_ flags
        public BP_ANIMATIONSTYLE style;
        public uint dwDuration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BP_PAINTPARAMS
    {
        public uint cbSize;
        public uint dwFlags; // BPPF_ flags
        public IntPtr prcExclude;
        public IntPtr pBlendFunction;
    }


}
