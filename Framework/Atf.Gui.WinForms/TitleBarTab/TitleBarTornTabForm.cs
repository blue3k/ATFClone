using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Sce.Atf.Windows;
using System.Drawing;

namespace Sce.Atf.Gui.TitleBarTab
{
	/// <summary>
	/// Contains a semi-transparent window with a thumbnail of a tab that has been torn away from its parent window.  This thumbnail will follow the cursor
	/// around as it's dragged around the screen.
	/// </summary>
	public class TitleBarTornTabForm : Form
	{
		/// <summary>Window that contains the actual thumbnail image data.</summary>
		private readonly LayeredWindow m_layeredWindow;

		/// <summary>Offset of the cursor within the torn tab representation while dragging.</summary>
		protected Point m_cursorOffset;

		/// <summary>Pointer to the low-level mouse hook callback (<see cref="MouseHookCallback" />).</summary>
		protected IntPtr m_hookId;

		/// <summary>Flag indicating whether <see cref="m_hookproc" /> is installed.</summary>
		protected bool m_hookInstalled = false;

		/// <summary>Delegate of <see cref="MouseHookCallback" />; declared as a member variable to keep it from being garbage collected.</summary>
		protected HOOKPROC m_hookproc = null;

		/// <summary>Flag indicating whether or not the constructor has finished running.</summary>
		private bool m_initialized;

		/// <summary>Thumbnail of the tab we are dragging.</summary>
		protected Bitmap m_tabThumbnail;

        /// <summary>
        /// Message filter to handle mouse event
        /// </summary>
        private IMessageFilter m_messageFilter;

        /// <summary>Constructor; initializes the window and constructs the tab thumbnail image to use when dragging.</summary>
        /// <param name="tab">Tab that was torn out of its parent window.</param>
        /// <param name="tabRenderer">Renderer instance to use when drawing the actual tab.</param>
        public TitleBarTornTabForm(TitleBarTabItem tab, BaseTabRenderer tabRenderer, IMessageFilter messageFilter)
		{
            m_messageFilter = messageFilter;

            m_layeredWindow = new LayeredWindow();
			m_initialized = false;

			// Set drawing styles
			SetStyle(ControlStyles.DoubleBuffer, true);

			// This should show up as a semi-transparent borderless window
			Opacity = 0.70;
			ShowInTaskbar = false;
			FormBorderStyle = FormBorderStyle.None;
// ReSharper disable DoNotCallOverridableMethodsInConstructor
			BackColor = Color.Fuchsia;
// ReSharper restore DoNotCallOverridableMethodsInConstructor
			TransparencyKey = Color.Fuchsia;
			AllowTransparency = true;

			Disposed += TornTabForm_Disposed;

			// Get the tab thumbnail (full size) and then draw the actual representation of the tab onto it as well
			Bitmap tabContents = tab.GetImage();
			Bitmap contentsAndTab = new Bitmap(tabContents.Width, tabContents.Height + tabRenderer.TabHeight, tabContents.PixelFormat);
			Graphics tabGraphics = Graphics.FromImage(contentsAndTab);

			tabGraphics.DrawImage(tabContents, 0, tabRenderer.TabHeight);

			bool oldShowAddButton = tabRenderer.ShowAddButton;

			tabRenderer.ShowAddButton = false;
			tabRenderer.Render(
				new ListWithEvents<TitleBarTabItem>
				{
					tab
				}, tabGraphics, new Point(0, 0), new Point(0, 0), true);
			tabRenderer.ShowAddButton = oldShowAddButton;

			// Scale the thumbnail down to half size
			m_tabThumbnail = new Bitmap(contentsAndTab.Width / 2, contentsAndTab.Height / 2, contentsAndTab.PixelFormat);
			Graphics thumbnailGraphics = Graphics.FromImage(m_tabThumbnail);

			thumbnailGraphics.InterpolationMode = InterpolationMode.High;
			thumbnailGraphics.CompositingQuality = CompositingQuality.HighQuality;
			thumbnailGraphics.SmoothingMode = SmoothingMode.AntiAlias;
			thumbnailGraphics.DrawImage(contentsAndTab, 0, 0, m_tabThumbnail.Width, m_tabThumbnail.Height);

			Width = m_tabThumbnail.Width - 1;
			Height = m_tabThumbnail.Height - 1;

			m_cursorOffset = new Point(tabRenderer.TabContentWidth / 4, tabRenderer.TabHeight / 4);

			SetWindowPosition(Cursor.Position);
		}

		/// <summary>
		/// Event handler that's called from <see cref="IDisposable.Dispose" />; calls <see cref="User32.UnhookWindowsHookEx" /> to unsubscribe from the mouse
		/// hook.
		/// </summary>
		/// <param name="sender">Object from which this event originated.</param>
		/// <param name="e">Arguments associated with this event.</param>
		private void TornTabForm_Disposed(object sender, EventArgs e)
		{
			User32.UnhookWindowsHookEx(m_hookId);
		}

		/// <summary>Hook callback to process <see cref="WM.WM_MOUSEMOVE" /> messages to move the thumbnail along with the cursor.</summary>
		/// <param name="nCode">The message being received.</param>
		/// <param name="wParam">Additional information about the message.</param>
		/// <param name="lParam">Additional information about the message.</param>
		/// <returns>A zero value if the procedure processes the message; a nonzero value if the procedure ignores the message.</returns>
		protected IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
            if (nCode >= 0)
            {
                switch ((WM)wParam)
                {
                    case WM.WM_MOUSEMOVE:
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                        Point cursorPosition = new Point(hookStruct.pt.x, hookStruct.pt.y);

                        SetWindowPosition(cursorPosition);
                        {
                            var msg = new Message()
                            {
                                HWnd = Handle,
                                LParam = lParam,
                                WParam = wParam,
                                Msg = (int)WM.WM_MOUSEMOVE,
                            };
                            Invoke(new Action(()=> m_messageFilter.PreFilterMessage(ref msg)));
                        }
                        break;
                    case WM.WM_LBUTTONUP:
                        {
                            var msg = new Message()
                            {
                                HWnd = Handle,
                                LParam = lParam,
                                WParam = wParam,
                                Msg = (int)WM.WM_LBUTTONUP,
                            };
                            Invoke(new Action(() => m_messageFilter.PreFilterMessage(ref msg)));
                        }
                        break;
                    default:
                        break;
                }
            }

            return User32.CallNextHookEx(m_hookId, nCode, wParam, lParam);
		}

		/// <summary>Updates the window position to keep up with the cursor's movement.</summary>
		/// <param name="cursorPosition">Current position of the cursor.</param>
		protected void SetWindowPosition(Point cursorPosition)
		{
			Left = cursorPosition.X - m_cursorOffset.X;
			Top = cursorPosition.Y - m_cursorOffset.Y;

			UpdateLayeredBackground();
		}

		/// <summary>
		/// Event handler that's called when the window is loaded; shows <see cref="m_layeredWindow" /> and installs the mouse hook via
		/// <see cref="User32.SetWindowsHookEx" />.
		/// </summary>
		/// <param name="e">Arguments associated with this event.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			m_initialized = true;

			UpdateLayeredBackground();

			m_layeredWindow.Show();
			m_layeredWindow.Enabled = false;

			// Installs the mouse hook
			if (!m_hookInstalled)
			{
				using (Process curProcess = Process.GetCurrentProcess())
				{
					using (ProcessModule curModule = curProcess.MainModule)
					{
						m_hookproc = MouseHookCallback;
						m_hookId = User32.SetWindowsHookEx(WH.WH_MOUSE_LL, m_hookproc, Kernel32.GetModuleHandle(curModule.ModuleName), 0);
					}
				}

				m_hookInstalled = true;
			}
		}

		/// <summary>Event handler that is called when the window is closing; closes <see cref="m_layeredWindow" /> as well.</summary>
		/// <param name="e">Arguments associated with this event.</param>
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			m_layeredWindow.Close();
		}

		/// <summary>
		/// Calls <see cref="LayeredWindow.UpdateWindow" /> to update the position of the thumbnail and blend it properly with the underlying desktop
		/// elements.
		/// </summary>
		public void UpdateLayeredBackground()
		{
			if (m_tabThumbnail == null || !m_initialized)
			{
				return;
			}

			byte opacity = (byte) (Opacity * 255);
			m_layeredWindow.UpdateWindow(
				m_tabThumbnail, opacity, Width, Height, new POINT
				                                       {
					                                       x = Location.X,
					                                       y = Location.Y
				                                       });
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TornTabForm
            // 
            this.ClientSize = new System.Drawing.Size(983, 529);
            this.Name = "TornTabForm";
            this.ResumeLayout(false);

        }
    }

    /// <summary>Form that actually displays the thumbnail content for <see cref="TitleBarTornTabForm" />.</summary>
    internal class LayeredWindow : Form
	{
		/// <summary>Default constructor.</summary>
		public LayeredWindow()
		{
			ShowInTaskbar = false;
			FormBorderStyle = FormBorderStyle.None;
		}

		/// <summary>
		/// Makes sure that the window is created with an <see cref="WS_EX.WS_EX_LAYERED" /> flag set so that it can be alpha-blended properly with the desktop
		/// contents underneath it.
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= (int) WS_EX.WS_EX_LAYERED;

				return createParams;
			}
		}

		/// <summary>
		/// Renders the tab thumbnail (<paramref name="image" />) using the given dimensions and coordinates and blends it properly with the underlying desktop
		/// elements.
		/// </summary>
		/// <param name="image">Thumbnail to display.</param>
		/// <param name="opacity">Opacity that <paramref name="image" /> should be displayed with.</param>
		/// <param name="width">Width of <paramref name="image" />.</param>
		/// <param name="height">Height of <paramref name="image" />.</param>
		/// <param name="position">Screen position that <paramref name="image" /> should be displayed at.</param>
		public void UpdateWindow(Bitmap image, byte opacity, int width, int height, POINT position)
		{
			IntPtr windowHandle = User32.GetWindowDC(Handle);
			IntPtr deviceContextHandle = Gdi32.CreateCompatibleDC(windowHandle);
			IntPtr bitmapHandle = image.GetHbitmap(Color.FromArgb(0));
			IntPtr oldBitmapHandle = Gdi32.SelectObject(deviceContextHandle, bitmapHandle);
			SIZE size = new SIZE
			            {
				            cx = 0,
				            cy = 0
			            };
			POINT destinationPosition = new POINT
			                            {
				                            x = 0,
				                            y = 0
			                            };

			if (width == -1 || height == -1)
			{
				// No width and height specified, use the size of the image
				size.cx = image.Width;
				size.cy = image.Height;
			}

			else
			{
				// Use whichever size is smallest, so that the image will be clipped if necessary
				size.cx = Math.Min(image.Width, width);
				size.cy = Math.Min(image.Height, height);
			}

			// Set the opacity and blend the image with the underlying desktop elements using User32.UpdateLayeredWindow
			BLENDFUNCTION blendFunction = new BLENDFUNCTION
			                              {
				                              BlendOp = Convert.ToByte((int) AC.AC_SRC_OVER),
				                              SourceConstantAlpha = opacity,
				                              AlphaFormat = Convert.ToByte((int) AC.AC_SRC_ALPHA),
				                              BlendFlags = 0
			                              };

            User32.UpdateLayeredWindow(Handle, windowHandle, ref position, ref size, deviceContextHandle, ref destinationPosition, 0, ref blendFunction, ULW.ULW_ALPHA);

			Gdi32.SelectObject(deviceContextHandle, oldBitmapHandle);
			Gdi32.DeleteObject(bitmapHandle);
			Gdi32.DeleteDC(deviceContextHandle);
			User32.ReleaseDC(Handle, windowHandle);
		}
	}
}