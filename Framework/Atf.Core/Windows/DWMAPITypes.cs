//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Runtime.InteropServices;

namespace Sce.Atf.Windows
{
    /// <summary>
    ///     Flags used by the <see cref="Dwmapi.DwmSetPresentParameters" /> function to specify the frame sampling type.
    /// </summary>
    public enum DWM_SOURCE_FRAME_SAMPLING
    {
        /// <summary>Use the first source frame that includes the first refresh of the output frame.</summary>
        DWM_SOURCE_FRAME_SAMPLING_POINT,

        /// <summary>
        ///     Use the source frame that includes the most refreshes of the output frame. In the case of multiple source frames with the same coverage, the last
        ///     frame is used.
        /// </summary>
        DWM_SOURCE_FRAME_SAMPLING_COVERAGE,

        /// <summary>
        ///     The maximum recognized <see cref="DWM_SOURCE_FRAME_SAMPLING" /> value, used for validation purposes.
        /// </summary>
        DWM_SOURCE_FRAME_SAMPLING_LAST
    }

    /// <summary>The display options for the thumbnail.</summary>
    public enum SIT
    {
        /// <summary>No frame is displayed around the provided thumbnail.</summary>
        DWM_SIT_NOFRAME = 0,

        /// <summary>Displays a frame around the provided thumbnail.</summary>
        DWM_SIT_DISPLAYFRAME = 1
    }

    /// <summary>
    ///     Flags used by the <see cref="Dwmapi.DwmGetWindowAttribute" /> and <see cref="Dwmapi.DwmSetWindowAttribute" /> functions to specify window
    ///     attributes for non-client rendering.
    /// </summary>
    public enum DWMWINDOWATTRIBUTE
    {
        /// <summary>
        ///     Use with<see cref="Dwmapi.DwmGetWindowAttribute" />. Discovers whether non-client rendering is enabled. The retrieved value is of type BOOL. TRUE
        ///     if non-client rendering is enabled; otherwise, FALSE.
        /// </summary>
        DWMWA_NCRENDERING_ENABLED = 1,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" />\. Sets the non-client rendering policy. The pvAttribute parameter points to a value from the
        ///     <see cref="DWMNCRENDERINGPOLICY" /> enumeration.
        /// </summary>
        DWMWA_NCRENDERING_POLICY,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" />. Enables or forcibly disables DWM transitions. The pvAttribute parameter points to a value
        ///     of TRUE to disable transitions or FALSE to enable transitions.
        /// </summary>
        DWMWA_TRANSITIONS_FORCEDISABLED,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" />. Enables content rendered in the non-client area to be visible on the frame drawn by DWM.
        ///     The pvAttribute parameter points to a value of TRUE to enable content rendered in the non-client area to be visible on the frame; otherwise, it
        ///     points to FALSE.
        /// </summary>
        DWMWA_ALLOW_NCPAINT,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmGetWindowAttribute" />. Retrieves the bounds of the caption button area in the window-relative space. The retrieved
        ///     value is of type RECT.
        /// </summary>
        DWMWA_CAPTION_BUTTON_BOUNDS,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" />. Specifies whether non-client content is right-to-left (RTL) mirrored. The pvAttribute
        ///     parameter points to a value of TRUE if the non-client content is right-to-left (RTL) mirrored; otherwise, it points to FALSE.
        /// </summary>
        DWMWA_NONCLIENT_RTL_LAYOUT,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" /> . Forces the window to display an iconic thumbnail or peek representation (a static bitmap),
        ///     even if a live or snapshot representation of the window is available. This value normally is set during a window's creation and not changed
        ///     throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points to a
        ///     value of TRUE to require a iconic thumbnail or peek representation; otherwise, it points to FALSE.
        /// </summary>
        DWMWA_FORCE_ICONIC_REPRESENTATION,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" />. Sets how Flip3D treats the window. The pvAttribute parameter points to a value from the
        ///     <see cref="DWMFLIP3DWINDOWPOLICY" /> enumeration.
        /// </summary>
        DWMWA_FLIP3D_POLICY,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmGetWindowAttribute" />. Retrieves the extended frame bounds rectangle in screen space. The retrieved value is of
        ///     type <see cref="RECT" />.
        /// </summary>
        DWMWA_EXTENDED_FRAME_BOUNDS,

        /// <summary>
        ///     Use with<see cref="Dwmapi.DwmSetWindowAttribute" />. The window will provide a bitmap for use by DWM as an iconic thumbnail or peek
        ///     representation (a static bitmap) for the window. <see cref="DWMWA_HAS_ICONIC_BITMAP" /> can be specified with
        ///     <see cref="DWMWA_FORCE_ICONIC_REPRESENTATION" />. <see cref="DWMWA_HAS_ICONIC_BITMAP" /> normally is set during a window's creation and not
        ///     changed throughout the window's lifetime. Some scenarios, however, might require the value to change over time. The pvAttribute parameter points
        ///     to a value of TRUE to inform DWM that the window will provide an iconic thumbnail or peek representation; otherwise, it points to FALSE.
        /// </summary>
        DWMWA_HAS_ICONIC_BITMAP,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" />. Do not show peek preview for the window. The peek view shows a full-sized preview of the
        ///     window when the mouse hovers over the window's thumbnail in the taskbar. If this attribute is set, hovering the mouse pointer over the window's
        ///     thumbnail dismisses peek (in case another window in the group has a peek preview showing). The pvAttribute parameter points to a value of TRUE to
        ///     prevent peek functionality or FALSE to allow it.
        /// </summary>
        DWMWA_DISALLOW_PEEK,

        /// <summary>
        ///     Use with <see cref="Dwmapi.DwmSetWindowAttribute" />. Prevents a window from fading to a glass sheet when peek is invoked. The pvAttribute
        ///     parameter points to a value of TRUE to prevent the window from fading during another window's peek or FALSE for normal behavior.
        /// </summary>
        DWMWA_EXCLUDED_FROM_PEEK,

        /// <summary>
        ///     The maximum recognized <see cref="DWMWINDOWATTRIBUTE" /> value, used for validation purposes.
        /// </summary>
        DWMWA_LAST
    };

    /// <summary>
    ///     Flags used by the <see cref="Dwmapi.DwmSetWindowAttribute" /> function to specify the non-client area rendering policy.
    /// </summary>
    public enum DWMNCRENDERINGPOLICY
    {
        /// <summary>The non-client rendering area is rendered based on the window style.</summary>
        DWMNCRP_USEWINDOWSTYLE,

        /// <summary>The non-client area rendering is disabled; the window style is ignored.</summary>
        DWMNCRP_DISABLED,

        /// <summary>The non-client area rendering is enabled; the window style is ignored.</summary>
        DWMNCRP_ENABLED,

        /// <summary>
        ///     The maximum recognized <see cref="DWMNCRENDERINGPOLICY" /> value, used for validation purposes.
        /// </summary>
        DWMNCRP_LAST
    };

    /// <summary>
    ///     Flags used by the <see cref="Dwmapi.DwmSetWindowAttribute" /> function to specify the Flip3D window policy.
    /// </summary>
    /// <remarks>
    ///     To use a <see cref="DWMFLIP3DWINDOWPOLICY" /> value, set the dwAttribute parameter of the <see cref="Dwmapi.DwmSetWindowAttribute" /> function to
    ///     <see cref="DWMWINDOWATTRIBUTE.DWMWA_FLIP3D_POLICY" />. Set the pvAttribute parameter to the <see cref="DWMFLIP3DWINDOWPOLICY" /> value.
    /// </remarks>
    public enum DWMFLIP3DWINDOWPOLICY
    {
        /// <summary>Use the window's style and visibility settings to determine whether to hide or include the window in Flip3D rendering.</summary>
        DWMFLIP3D_DEFAULT,

        /// <summary>Exclude the window from Flip3D and display it below the Flip3D rendering.</summary>
        DWMFLIP3D_EXCLUDEBELOW,

        /// <summary>Exclude the window from Flip3D and display it above the Flip3D rendering.</summary>
        DWMFLIP3D_EXCLUDEABOVE,

        /// <summary>
        ///     The maximum recognized <see cref="DWMFLIP3DWINDOWPOLICY" /> value, used for validation purposes.
        /// </summary>
        DWMFLIP3D_LAST
    }



    /// <summary>Specifies Desktop Window Manager (DWM) blur-behind properties. Used by the DwmEnableBlurBehindWindow function.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_BLURBEHIND
    {
        /// <summary>A bitwise combination of DWM Blur Behind constant values that indicates which of the members of this structure have been set.</summary>
        public uint dwFlags;

        /// <summary>TRUE to register the window handle to DWM blur behind; FALSE to unregister the window handle from DWM blur behind.</summary>
        public bool fEnable;

        /// <summary>The region within the client area where the blur behind will be applied. A NULL value will apply the blur behind the entire client area.</summary>
        public IntPtr hRgnBlur;

        /// <summary>TRUE if the window's colorization should transition to match the maximized windows; otherwise, FALSE.</summary>
        public bool fTransitionOnMaximized;
    }

    /// <summary>Specifies Desktop Window Manager (DWM) thumbnail properties. Used by the DwmUpdateThumbnailProperties function.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_THUMBNAIL_PROPERTIES
    {
        /// <summary>A bitwise combination of DWM thumbnail constant values that indicates which members of this structure are set.</summary>
        public uint dwFlags;

        /// <summary>The area in the destination window where the thumbnail will be rendered.</summary>
        public RECT rcDestination;

        /// <summary>The region of the source window to use as the thumbnail. By default, the entire window is used as the thumbnail.</summary>
        public RECT rcSource;

        /// <summary>The opacity with which to render the thumbnail. 0 is fully transparent while 255 is fully opaque. The default value is 255.</summary>
        public byte opacity;

        /// <summary>TRUE to make the thumbnail visible; otherwise, FALSE. The default is FALSE.</summary>
        public bool fVisible;

        /// <summary>TRUE to use only the thumbnail source's client area; otherwise, FALSE. The default is FALSE.</summary>
        public bool fSourceClientAreaOnly;
    }

    /// <summary>
    ///     Defines a data type used by the Desktop Window Manager (DWM) APIs. It represents a generic ratio and is used for different purposes and units
    ///     even within a single API.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct UNSIGNED_RATIO
    {
        /// <summary>The ratio numerator.</summary>
        public UInt32 uiNumerator;

        /// <summary>The ratio denominator.</summary>
        public UInt32 uiDenominator;
    }

    /// <summary>Specifies Desktop Window Manager (DWM) video frame parameters for frame composition. Used by the DwmSetPresentParameters function.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_PRESENT_PARAMETERS
    {
        /// <summary>
        ///     The size of the <see cref="DWM_PRESENT_PARAMETERS" /> structure.
        /// </summary>
        public UInt32 cbSize;

        /// <summary>
        ///     TRUE if the caller is requesting queued presents; otherwise, FALSE. If TRUE, the remaining parameters must be specified. If FALSE, queued
        ///     presentation is disabled for this window and present behavior returns to the default behavior.
        /// </summary>
        public bool fQueue;

        /// <summary>A ULONGLONG value that provides the refresh number that the next presented frame should begin to display.</summary>
        public UInt64 cRefreshStart;

        /// <summary>The number of frames the application is instructing DWM to queue. The valid range is 2-8.</summary>
        public uint cBuffer;

        /// <summary>
        ///     TRUE if the application wants DWM to schedule presentation based on source rate. FALSE if the application will decide how many refreshes to
        ///     display for each frame. If TRUE, <see cref="rateSource" /> must be specified. If FALSE, <see cref="cRefreshesPerFrame" /> must be specified.
        /// </summary>
        public bool fUseSourceRate;

        /// <summary>The rate, in frames per second, of the source material being displayed.</summary>
        public UNSIGNED_RATIO rateSource;

        /// <summary>The number of monitor refreshes through which each frame should be displayed on the screen.</summary>
        public uint cRefreshesPerFrame;

        /// <summary>The frame sampling type to use for composition.</summary>
        public DWM_SOURCE_FRAME_SAMPLING eSampling;
    }

    /// <summary>Specifies Desktop Window Manager (DWM) composition timing information. Used by the DwmGetCompositionTimingInfo function.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_TIMING_INFO
    {
        /// <summary>
        ///     The size of this <see cref="DWM_TIMING_INFO" /> structure.
        /// </summary>
        public UInt32 cbSize;

        /// <summary>The monitor refresh rate.</summary>
        public UNSIGNED_RATIO rateRefresh;

        /// <summary>The monitor refresh period.</summary>
        public UInt64 qpcRefreshPeriod;

        /// <summary>The composition rate.</summary>
        public UNSIGNED_RATIO rateCompose;

        /// <summary>The query performance counter value before the vertical blank.</summary>
        public UInt64 qpcVBlank;

        /// <summary>The DWM refresh counter.</summary>
        public UInt64 cRefresh;

        /// <summary>The DirectX refresh counter.</summary>
        public uint cDXRefresh;

        /// <summary>The query performance counter value for a frame composition.</summary>
        public UInt64 qpcCompose;

        /// <summary>
        ///     The frame number that was composed at <see cref="qpcCompose" />.
        /// </summary>
        public UInt64 cFrame;

        /// <summary>The DirectX present number used to identify rendering frames.</summary>
        public uint cDXPresent;

        /// <summary>
        ///     The refresh count of the frame that was composed at <see cref="qpcCompose" />.
        /// </summary>
        public UInt64 cRefreshFrame;

        /// <summary>The DWM frame number that was last submitted.</summary>
        public UInt64 cFrameSubmitted;

        /// <summary>The DirectX present number that was last submitted.</summary>
        public uint cDXPresentSubmitted;

        /// <summary>The DWM frame number that was last confirmed as presented.</summary>
        public UInt64 cFrameConfirmed;

        /// <summary>The DirectX present number that was last confirmed as presented.</summary>
        public uint cDXPresentConfirmed;

        /// <summary>The target refresh count of the last frame confirmed as completed by the GPU.</summary>
        public UInt64 cRefreshConfirmed;

        /// <summary>The DirectX refresh count when the frame was confirmed as presented.</summary>
        public uint cDXRefreshConfirmed;

        /// <summary>The number of frames the DWM presented late.</summary>
        public UInt64 cFramesLate;

        /// <summary>The number of composition frames that have been issued but have not been confirmed as completed.</summary>
        public uint cFramesOutstanding;

        /// <summary>The last frame displayed.</summary>
        public UInt64 cFrameDisplayed;

        /// <summary>The QPC time of the composition pass when the frame was displayed.</summary>
        public UInt64 qpcFrameDisplayed;

        /// <summary>The vertical refresh count when the frame should have become visible.</summary>
        public UInt64 cRefreshFrameDisplayed;

        /// <summary>The ID of the last frame marked as completed.</summary>
        public UInt64 cFrameComplete;

        /// <summary>The QPC time when the last frame was marked as completed.</summary>
        public UInt64 qpcFrameComplete;

        /// <summary>The ID of the last frame marked as pending.</summary>
        public UInt64 cFramePending;

        /// <summary>The QPC time when the last frame was marked as pending.</summary>
        public UInt64 qpcFramePending;

        /// <summary>
        ///     The number of unique frames displayed. This value is valid only after a second call to the <see cref="Dwmapi.DwmGetCompositionTimingInfo" />
        ///     function.
        /// </summary>
        public UInt64 cFramesDisplayed;

        /// <summary>The number of new completed frames that have been received.</summary>
        public UInt64 cFramesComplete;

        /// <summary>The number of new frames submitted to DirectX but not yet completed.</summary>
        public UInt64 cFramesPending;

        /// <summary>
        ///     The number of frames available but not displayed, used, or dropped. This value is valid only after a second call to
        ///     <see cref="Dwmapi.DwmGetCompositionTimingInfo" />.
        /// </summary>
        public UInt64 cFramesAvailable;

        /// <summary>
        ///     The number of rendered frames that were never displayed because composition occurred too late. This value is valid only after a second call to
        ///     <see cref="Dwmapi.DwmGetCompositionTimingInfo" />.
        /// </summary>
        public UInt64 cFramesDropped;

        /// <summary>The number of times an old frame was composed when a new frame should have been used but was not available.</summary>
        public UInt64 cFramesMissed;

        /// <summary>The frame count at which the next frame is scheduled to be displayed.</summary>
        public UInt64 cRefreshNextDisplayed;

        /// <summary>The frame count at which the next DirectX present is scheduled to be displayed.</summary>
        public UInt64 cRefreshNextPresented;

        /// <summary>
        ///     The total number of refreshes that have been displayed for the application since the <see cref="Dwmapi.DwmSetPresentParameters" /> function was
        ///     last called.
        /// </summary>
        public UInt64 cRefreshesDisplayed;

        /// <summary>
        ///     The total number of refreshes that have been presented by the application since <see cref="Dwmapi.DwmSetPresentParameters" /> was last called.
        /// </summary>
        public UInt64 cRefreshesPresented;

        /// <summary>The refresh number when content for this window started to be displayed.</summary>
        public UInt64 cRefreshStarted;

        /// <summary>The total number of pixels DirectX redirected to the DWM.</summary>
        public UInt64 cPixelsReceived;

        /// <summary>The number of pixels drawn.</summary>
        public UInt64 cPixelsDrawn;

        /// <summary>The number of empty buffers in the flip chain.</summary>
        public UInt64 cBuffersEmpty;
    }



}