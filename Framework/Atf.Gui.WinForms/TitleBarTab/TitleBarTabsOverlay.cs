using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using Sce.Atf.Windows;

namespace Sce.Atf.Gui.TitleBarTab
{
	/// <summary>
	/// Borderless overlay window that is moved with and rendered on top of the non-client area of a  <see cref="TitleBarTabMainForm" /> instance that's responsible
	/// for rendering the actual tab content and responding to click events for those tabs.
	/// </summary>
	public class TitleBarTabsOverlay : Form, IMessageFilter
    {
		/// <summary>All of the parent forms and their overlays so that we don't create duplicate overlays across the application domain.</summary>
		protected static Dictionary<TitleBarTabMainForm, TitleBarTabsOverlay> _parents = new Dictionary<TitleBarTabMainForm, TitleBarTabsOverlay>();

		/// <summary>Tab that has been torn off from this window and is being dragged.</summary>
		protected static TitleBarTabItem m_tornTab;

		/// <summary>Thumbnail representation of <see cref="m_tornTab" /> used when dragging.</summary>
		protected static TitleBarTornTabForm m_tornTabForm;

		/// <summary>
		/// Flag used in <see cref="WndProc" /> and <see cref="MouseHookCallback" /> to track whether the user was click/dragging when a particular event
		/// occurred.
		/// </summary>
		protected static bool m_wasDragging = false;

		/// <summary>Flag indicating whether or not <see cref="m_hookproc" /> has been installed as a hook.</summary>
		protected static bool m_hookProcInstalled;

		/// <summary>Semaphore to control access to <see cref="m_tornTab" />.</summary>
		protected static object m_tornTabLock = new object();

		/// <summary>Flag indicating whether or not the underlying window is active.</summary>
		protected bool m_active = false;

		/// <summary>Flag indicating whether we should draw the titlebar background (i.e. we are in a non-Aero environment).</summary>
		protected bool m_aeroEnabled = false;

		/// <summary>
		/// When a tab is torn from the window, this is where we store the areas on all open windows where tabs can be dropped to combine the tab with that
		/// window.
		/// </summary>
		protected Tuple<TitleBarTabMainForm, Rectangle>[] m_dropAreas = null;
        
		/// <summary>Index of the tab, if any, whose close button is being hovered over.</summary>
		protected int m_isOverCloseButtonForTab = -1;

		/// <summary>Parent form for the overlay.</summary>
		protected TitleBarTabMainForm m_parentForm;

		/// <summary>Blank default constructor to ensure that the overlays are only initialized through <see cref="GetInstance" />.</summary>
		protected TitleBarTabsOverlay()
		{
		}

		/// <summary>Creates the overlay window and attaches it to <paramref name="parentForm" />.</summary>
		/// <param name="parentForm">Parent form that the overlay should be rendered on top of.</param>
		protected TitleBarTabsOverlay(TitleBarTabMainForm parentForm)
		{
			m_parentForm = parentForm;

			// We don't want this window visible in the taskbar
			ShowInTaskbar = false;
			FormBorderStyle = FormBorderStyle.Sizable;
			MinimizeBox = false;
			MaximizeBox = false;
			m_aeroEnabled = m_parentForm.IsCompositionEnabled;

			Show(m_parentForm);
			AttachHandlers();
		}

		/// <summary>
		/// Makes sure that the window is created with an <see cref="WS_EX.WS_EX_LAYERED" /> flag set so that it can be alpha-blended properly with the content (
		/// <see cref="m_parentForm" />) underneath the overlay.
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= (int) (WS_EX.WS_EX_LAYERED | WS_EX.WS_EX_NOACTIVATE);

				return createParams;
			}
		}

		/// <summary>Primary color for the titlebar background.</summary>
		protected Color TitleBarColor
		{
			get
			{
				if (Application.RenderWithVisualStyles && Environment.OSVersion.Version.Major >= 6)
				{
					return m_active
						? SystemColors.GradientActiveCaption
						: SystemColors.GradientInactiveCaption;
				}

				return m_active
					? SystemColors.ActiveCaption
					: SystemColors.InactiveCaption;
			}
		}

		/// <summary>Type of theme being used by the OS to render the desktop.</summary>
		protected DisplayType DisplayType
		{
			get
			{
				if (m_aeroEnabled)
				{
					return DisplayType.Aero;
				}

				if (Application.RenderWithVisualStyles && Environment.OSVersion.Version.Major >= 6)
				{
					return DisplayType.Basic;
				}

				return DisplayType.Classic;
			}
		}

		/// <summary>Gradient color for the titlebar background.</summary>
		protected Color TitleBarGradientColor
		{
			get
			{
				return m_active
					? SystemInformation.IsTitleBarGradientEnabled
						? SystemColors.GradientActiveCaption
						: SystemColors.ActiveCaption
					: SystemInformation.IsTitleBarGradientEnabled
						? SystemColors.GradientInactiveCaption
						: SystemColors.InactiveCaption;
			}
		}

		/// <summary>Screen area in which tabs can be dragged to and dropped for this window.</summary>
		public Rectangle TabDropArea
		{
			get
			{
				RECT windowRectangle;
				User32.GetWindowRect(m_parentForm.Handle, out windowRectangle);

				return new Rectangle(
					windowRectangle.left + SystemInformation.HorizontalResizeBorderThickness, windowRectangle.top + SystemInformation.VerticalResizeBorderThickness,
					ClientRectangle.Width, m_parentForm.NonClientAreaHeight - SystemInformation.VerticalResizeBorderThickness);
			}
		}

		/// <summary>Retrieves or creates the overlay for <paramref name="parentForm" />.</summary>
		/// <param name="parentForm">Parent form that we are to create the overlay for.</param>
		/// <returns>Newly-created or previously existing overlay for <paramref name="parentForm" />.</returns>
		public static TitleBarTabsOverlay GetInstance(TitleBarTabMainForm parentForm)
		{
			if (!_parents.ContainsKey(parentForm))
			{
				_parents.Add(parentForm, new TitleBarTabsOverlay(parentForm));
			}

			return _parents[parentForm];
		}

        #region IMessageFilter

        public bool PreFilterMessage(ref Message m)
        {
            Point mousePosition = Control.MousePosition;

            switch ((WM)m.Msg)
            {
                case WM.WM_MOUSEMOVE:
                case WM.WM_NCMOUSEMOVE:
                    HandleApplicationMouseMove(mousePosition);
                    break;
                case WM.WM_LBUTTONDOWN:
                case WM.WM_NCLBUTTONDOWN:
                    HandleApplicationMouseLDown(mousePosition);
                    break;
                case WM.WM_LBUTTONUP:
                case WM.WM_NCLBUTTONUP:
                    HandleApplicationMouseLUp(mousePosition);
                    break;
            }

            //System.Diagnostics.Debug.Print("Windows message {0}", (WM)m.Msg);

            return false;
        }

        #endregion

        /// <summary>
        /// Attaches the various event handlers to <see cref="m_parentForm" /> so that the overlay is moved in synchronization to
        /// <see cref="m_parentForm" />.
        /// </summary>
        protected void AttachHandlers()
		{
			m_parentForm.Closing += _parentForm_Closing;
			m_parentForm.Disposed += _parentForm_Disposed;
			m_parentForm.Deactivate += _parentForm_Deactivate;
			m_parentForm.Activated += _parentForm_Activated;
			m_parentForm.SizeChanged += _parentForm_Refresh;
			m_parentForm.Shown += _parentForm_Refresh;
			m_parentForm.VisibleChanged += _parentForm_Refresh;
			m_parentForm.Move += _parentForm_Refresh;
			m_parentForm.SystemColorsChanged += _parentForm_SystemColorsChanged;

            Application.AddMessageFilter(this);
        }

		/// <summary>
		/// Event handler that is called when <see cref="m_parentForm" /> is in the process of closing.  This uninstalls <see cref="m_hookproc" /> from the low-
		/// level hooks list and stops the consumer thread that processes those events.
		/// </summary>
		/// <param name="sender">Object from which this event originated, <see cref="m_parentForm" /> in this case.</param>
		/// <param name="e">Arguments associated with this event.</param>
		private void _parentForm_Closing(object sender, CancelEventArgs e)
		{
			TitleBarTabMainForm form = (TitleBarTabMainForm) sender;

			if (form == null)
			{
				return;
			}

			if (_parents.ContainsKey(form))
			{
				_parents.Remove(form);
			}

            Application.RemoveMessageFilter(this);
        }

        protected void HandleApplicationMouseLDown(Point mousePos)
        {
            m_wasDragging = false;
        }

        protected void HandleApplicationMouseLUp(Point mousePos)
        {
            // If we released the mouse button while we were dragging a torn tab, put that tab into a new window
            if (m_tornTab != null)
            {
                TitleBarTabItem tabToRelease = null;

                lock (m_tornTabLock)
                {
                    if (m_tornTab != null)
                    {
                        tabToRelease = m_tornTab;
                        m_tornTab = null;
                    }
                }

                if (tabToRelease != null)
                {
                    TitleBarTabMainForm newWindow = (TitleBarTabMainForm)Activator.CreateInstance(m_parentForm.GetType());

                    // Set the initial window position and state properly
                    if (newWindow.WindowState == FormWindowState.Maximized)
                    {
                        Screen screen = Screen.AllScreens.First(s => s.WorkingArea.Contains(Cursor.Position));

                        newWindow.StartPosition = FormStartPosition.Manual;
                        newWindow.WindowState = FormWindowState.Normal;
                        newWindow.Left = screen.WorkingArea.Left;
                        newWindow.Top = screen.WorkingArea.Top;
                        newWindow.Width = screen.WorkingArea.Width;
                        newWindow.Height = screen.WorkingArea.Height;
                    }

                    else
                    {
                        newWindow.Left = Cursor.Position.X;
                        newWindow.Top = Cursor.Position.Y;
                    }

                    tabToRelease.Parent = newWindow;
                    m_parentForm.ApplicationContext.OpenWindow(newWindow);

                    newWindow.Show();
                    newWindow.Tabs.Add(tabToRelease);
                    newWindow.SelectedTabIndex = 0;
                    newWindow.ResizeTabContents();

                    m_tornTabForm.Close();
                    m_tornTabForm = null;

                    if (m_parentForm.Tabs.Count == 0)
                    {
                        m_parentForm.Close();
                    }
                }
            }

            OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, Cursor.Position.X, Cursor.Position.Y, 0));
        }

        protected void HandleApplicationMouseMove(Point mousePos)
        {
            bool reRender = false;


            if (m_tornTab != null && m_dropAreas != null)
            {
                // ReSharper disable ForCanBeConvertedToForeach
                for (int i = 0; i < m_dropAreas.Length; i++)
                // ReSharper restore ForCanBeConvertedToForeach
                {
                    // If the cursor is within the drop area, combine the tab for the window that belongs to that drop area
                    if (m_dropAreas[i].Item2.Contains(mousePos))
                    {
                        TitleBarTabItem tabToCombine = null;

                        lock (m_tornTabLock)
                        {
                            if (m_tornTab != null)
                            {
                                tabToCombine = m_tornTab;
                                m_tornTab = null;
                            }
                        }

                        if (tabToCombine != null)
                        {
                            int i1 = i;

                            // In all cases where we need to affect the UI, we call Invoke so that those changes are made on the main UI thread since
                            // we are on a separate processing thread in this case
                            m_dropAreas[i1].Item1.TabRenderer.CombineTab(tabToCombine, mousePos);

                            tabToCombine = null;
                            m_tornTabForm.Close();
                            m_tornTabForm = null;

                            if (m_parentForm.Tabs.Count == 0)
                            {
                                m_parentForm.Close();
                            }
                        }
                    }
                }
            }

            else if (!m_parentForm.TabRenderer.IsTabRepositioning)
            {
                // If we were over a close button previously, check to see if the cursor is still over that tab's
                // close button; if not, re-render
                if (m_isOverCloseButtonForTab != -1 &&
                    (m_isOverCloseButtonForTab >= m_parentForm.Tabs.Count ||
                     !m_parentForm.TabRenderer.IsOverCloseButton(m_parentForm.Tabs[m_isOverCloseButtonForTab], GetRelativeCursorPosition(mousePos))))
                {
                    reRender = true;
                    m_isOverCloseButtonForTab = -1;
                }

                // Otherwise, see if any tabs' close button is being hovered over
                else
                {
                    // ReSharper disable ForCanBeConvertedToForeach
                    for (int i = 0; i < m_parentForm.Tabs.Count; i++)
                    // ReSharper restore ForCanBeConvertedToForeach
                    {
                        if (m_parentForm.TabRenderer.IsOverCloseButton(m_parentForm.Tabs[i], GetRelativeCursorPosition(mousePos)))
                        {
                            m_isOverCloseButtonForTab = i;
                            reRender = true;

                            break;
                        }
                    }
                }
            }

            else
            {
                m_wasDragging = true;

                // When determining if a tab has been torn from the window while dragging, we take the drop area for this window and inflate it by the
                // TabTearDragDistance setting
                Rectangle dragArea = TabDropArea;
                dragArea.Inflate(m_parentForm.TabRenderer.TabTearDragDistance, m_parentForm.TabRenderer.TabTearDragDistance);

                // If the cursor is outside the tear area, tear it away from the current window
                if (!dragArea.Contains(mousePos) && m_tornTab == null)
                {
                    lock (m_tornTabLock)
                    {
                        if (m_tornTab == null)
                        {
                            m_parentForm.TabRenderer.IsTabRepositioning = false;

                            // Clear the event handler subscriptions from the tab and then create a thumbnail representation of it to use when dragging
                            m_tornTab = m_parentForm.SelectedTab;
                            m_tornTab.ClearSubscriptions();
                            m_tornTabForm = new TitleBarTornTabForm(m_tornTab, m_parentForm.TabRenderer, this);
                        }
                    }

                    if (m_tornTab != null)
                    {
                        m_parentForm.SelectedTabIndex = (m_parentForm.SelectedTabIndex == m_parentForm.Tabs.Count - 1
                            ? m_parentForm.SelectedTabIndex - 1
                            : m_parentForm.SelectedTabIndex + 1);
                        m_parentForm.Tabs.Remove(m_tornTab);

                        // If this tab was the only tab in the window, hide the parent window
                        if (m_parentForm.Tabs.Count == 0)
                        {
                            m_parentForm.Hide();
                        }

                        m_tornTabForm.Show();
                        m_dropAreas = (from window in m_parentForm.ApplicationContext.OpenWindows.Where(w => w.Tabs.Count > 0)
                                      select new Tuple<TitleBarTabMainForm, Rectangle>(window, window.TabDropArea)).ToArray();
                    }
                }
            }

            OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, mousePos.X, mousePos.Y, 0));

            if (m_parentForm.TabRenderer.IsTabRepositioning)
            {
                reRender = true;
            }

            if (reRender)
            {
                Render(mousePos, true);
            }
        }

		/// <summary>Draws the titlebar background behind the tabs if Aero glass is not enabled.</summary>
		/// <param name="graphics">Graphics context with which to draw the background.</param>
		protected virtual void DrawTitleBarBackground(Graphics graphics)
		{
			if (DisplayType == DisplayType.Aero)
			{
				return;
			}

			Rectangle fillArea;

			if (DisplayType == DisplayType.Basic)
			{
				fillArea = new Rectangle(
					new Point(
						1, Top == 0
							? SystemInformation.CaptionHeight - 1
							: (SystemInformation.CaptionHeight + SystemInformation.VerticalResizeBorderThickness) - (Top - m_parentForm.Top) - 1),
					new Size(Width - 2, m_parentForm.Padding.Top));
			}

			else
			{
				fillArea = new Rectangle(new Point(1, 0), new Size(Width - 2, Height - 1));
			}

			if (fillArea.Height <= 0)
			{
				return;
			}

			// Adjust the margin so that the gradient stops immediately prior to the control box in the titlebar
			int rightMargin = 3;

			if (m_parentForm.ControlBox && m_parentForm.MinimizeBox)
			{
				rightMargin += SystemInformation.CaptionButtonSize.Width;
			}

			if (m_parentForm.ControlBox && m_parentForm.MaximizeBox)
			{
				rightMargin += SystemInformation.CaptionButtonSize.Width;
			}

			if (m_parentForm.ControlBox)
			{
				rightMargin += SystemInformation.CaptionButtonSize.Width;
			}

			LinearGradientBrush gradient = new LinearGradientBrush(
				new Point(24, 0), new Point(fillArea.Width - rightMargin + 1, 0), TitleBarColor, TitleBarGradientColor);

			using (BufferedGraphics bufferedGraphics = BufferedGraphicsManager.Current.Allocate(graphics, fillArea))
			{
				bufferedGraphics.Graphics.FillRectangle(new SolidBrush(TitleBarColor), fillArea);
				bufferedGraphics.Graphics.FillRectangle(
					new SolidBrush(TitleBarGradientColor),
					new Rectangle(new Point(fillArea.Location.X + fillArea.Width - rightMargin, fillArea.Location.Y), new Size(rightMargin, fillArea.Height)));
				bufferedGraphics.Graphics.FillRectangle(
					gradient, new Rectangle(fillArea.Location, new Size(fillArea.Width - rightMargin, fillArea.Height)));
				bufferedGraphics.Graphics.FillRectangle(new SolidBrush(TitleBarColor), new Rectangle(fillArea.Location, new Size(24, fillArea.Height)));

				bufferedGraphics.Render(graphics);
			}
		}

		/// <summary>
		/// Event handler that is called when <see cref="m_parentForm" />'s <see cref="Control.SystemColorsChanged" /> event is fired which re-renders
		/// the tabs.
		/// </summary>
		/// <param name="sender">Object from which the event originated.</param>
		/// <param name="e">Arguments associated with the event.</param>
		private void _parentForm_SystemColorsChanged(object sender, EventArgs e)
		{
			m_aeroEnabled = m_parentForm.IsCompositionEnabled;
			OnPosition();
		}

		/// <summary>
		/// Event handler that is called when <see cref="m_parentForm" />'s <see cref="Control.SizeChanged" />, <see cref="Control.VisibleChanged" />, or
		/// <see cref="Control.Move" /> events are fired which re-renders the tabs.
		/// </summary>
		/// <param name="sender">Object from which the event originated.</param>
		/// <param name="e">Arguments associated with the event.</param>
		private void _parentForm_Refresh(object sender, EventArgs e)
		{
			if (m_parentForm.WindowState == FormWindowState.Minimized)
			{
				Visible = false;
			}

			else
			{
				OnPosition();
			}
		}

		/// <summary>Sets the position of the overlay window to match that of <see cref="m_parentForm" /> so that it moves in tandem with it.</summary>
		protected void OnPosition()
		{
			if (!IsDisposed)
			{
				// If the form is in a non-maximized state, we position the tabs below the minimize/maximize/close
				// buttons
				Top = m_parentForm.Top + (DisplayType == DisplayType.Classic
					? SystemInformation.VerticalResizeBorderThickness
					: m_parentForm.WindowState == FormWindowState.Maximized
						? SystemInformation.VerticalResizeBorderThickness
						: SystemInformation.CaptionHeight);
				Left = m_parentForm.Left + SystemInformation.HorizontalResizeBorderThickness - SystemInformation.BorderSize.Width;
				Width = m_parentForm.Width - (SystemInformation.VerticalResizeBorderThickness * 2) + (SystemInformation.BorderSize.Width * 2);
				Height = m_parentForm.TabRenderer.TabHeight + (DisplayType == DisplayType.Classic && m_parentForm.WindowState != FormWindowState.Maximized
					? SystemInformation.CaptionButtonSize.Height
					: 0);

				Render();
			}
		}

        /// <summary>
        /// Renders the tabs and then calls <see cref="User32.UpdateLayeredWindow" /> to blend the tab content with the underlying window (
        /// <see cref="m_parentForm" />).
        /// </summary>
        /// <param name="forceRedraw">Flag indicating whether a full render should be forced.</param>
        public void Render(bool forceRedraw = false)
		{
			Render(Cursor.Position, forceRedraw);
		}

		/// <summary>
		/// Renders the tabs and then calls <see cref="User32.UpdateLayeredWindow" /> to blend the tab content with the underlying window (
		/// <see cref="m_parentForm" />).
		/// </summary>
		/// <param name="cursorPosition">Current position of the cursor.</param>
		/// <param name="forceRedraw">Flag indicating whether a full render should be forced.</param>
		public void Render(Point cursorPosition, bool forceRedraw = false)
		{
			if (!IsDisposed && m_parentForm.TabRenderer != null && m_parentForm.WindowState != FormWindowState.Minimized && m_parentForm.ClientRectangle.Width > 0)
			{
				cursorPosition = GetRelativeCursorPosition(cursorPosition);

				using (Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						DrawTitleBarBackground(graphics);

						// Since classic mode themes draw over the *entire* titlebar, not just the area immediately behind the tabs, we have to offset the tabs
						// when rendering in the window
						Point offset = m_parentForm.WindowState != FormWindowState.Maximized && DisplayType == DisplayType.Classic
							? new Point(0, SystemInformation.CaptionButtonSize.Height)
							: m_parentForm.WindowState != FormWindowState.Maximized
								? new Point(0, SystemInformation.VerticalResizeBorderThickness - SystemInformation.BorderSize.Height)
								: new Point(0, 0);

						// Render the tabs into the bitmap
						m_parentForm.TabRenderer.Render(m_parentForm.Tabs, graphics, offset, cursorPosition, forceRedraw);

						// Cut out a hole in the background so that the control box on the underlying window can be shown
						if (DisplayType == DisplayType.Classic && (m_parentForm.ControlBox || m_parentForm.MaximizeBox || m_parentForm.MinimizeBox))
						{
							int boxWidth = 0;

							if (m_parentForm.ControlBox)
							{
								boxWidth += SystemInformation.CaptionButtonSize.Width;
							}

							if (m_parentForm.MinimizeBox)
							{
								boxWidth += SystemInformation.CaptionButtonSize.Width;
							}

							if (m_parentForm.MaximizeBox)
							{
								boxWidth += SystemInformation.CaptionButtonSize.Width;
							}

							CompositingMode oldCompositingMode = graphics.CompositingMode;

							graphics.CompositingMode = CompositingMode.SourceCopy;
							graphics.FillRectangle(
								new SolidBrush(Color.Transparent), Width - boxWidth, 0, boxWidth, SystemInformation.CaptionButtonSize.Height);
							graphics.CompositingMode = oldCompositingMode;
						}

						IntPtr screenDc = User32.GetDC(IntPtr.Zero);
						IntPtr memDc = Gdi32.CreateCompatibleDC(screenDc);
						IntPtr oldBitmap = IntPtr.Zero;
						IntPtr bitmapHandle = IntPtr.Zero;

						try
						{
							// Copy the contents of the bitmap into memDc
							bitmapHandle = bitmap.GetHbitmap(Color.FromArgb(0));
							oldBitmap = Gdi32.SelectObject(memDc, bitmapHandle);

							SIZE size = new SIZE
							            {
								            cx = bitmap.Width,
								            cy = bitmap.Height
							            };

							POINT pointSource = new POINT
							                    {
								                    x = 0,
								                    y = 0
							                    };
							POINT topPos = new POINT
							               {
								               x = Left,
								               y = Top
							               };
							BLENDFUNCTION blend = new BLENDFUNCTION
							                      {
								                      // We want to blend the bitmap's content with the screen content under it
								                      BlendOp = Convert.ToByte((int) AC.AC_SRC_OVER),
								                      BlendFlags = 0,
								                      SourceConstantAlpha = 255,
								                      // We use the bitmap's alpha channel for blending instead of a pre-defined transparency key
								                      AlphaFormat = Convert.ToByte((int) AC.AC_SRC_ALPHA)
							                      };

							// Blend the tab content with the underlying content
							if (!User32.UpdateLayeredWindow(
								Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, ULW.ULW_ALPHA))
							{
								int error = Marshal.GetLastWin32Error();
								throw new Win32Exception(error, "Error while calling UpdateLayeredWindow().");
							}
						}

							// Clean up after ourselves
						finally
						{
							User32.ReleaseDC(IntPtr.Zero, screenDc);

							if (bitmapHandle != IntPtr.Zero)
							{
								Gdi32.SelectObject(memDc, oldBitmap);
								Gdi32.DeleteObject(bitmapHandle);
							}

							Gdi32.DeleteDC(memDc);
						}
					}
				}
			}
		}

		/// <summary>Gets the relative location of the cursor within the overlay.</summary>
		/// <param name="cursorPosition">Cursor position that represents the absolute position of the cursor on the screen.</param>
		/// <returns>The relative location of the cursor within the overlay.</returns>
		public Point GetRelativeCursorPosition(Point cursorPosition)
		{
			return new Point(cursorPosition.X - Location.X, cursorPosition.Y - Location.Y);
		}

		/// <summary>Overrides the message pump for the window so that we can respond to click events on the tabs themselves.</summary>
		/// <param name="m">Message received by the pump.</param>
		protected override void WndProc(ref Message m)
		{
			switch ((WM) m.Msg)
			{
                case WM.WM_NCPAINT:
                case WM.WM_PAINT:
                    // If any of children need to be drawn, we need to draw title bar as well
                    Render();
                    System.Diagnostics.Debug.Print("Paint");
                    break;
                case WM.WM_NCLBUTTONDOWN:
				case WM.WM_LBUTTONDOWN:
					Point relativeCursorPosition = GetRelativeCursorPosition(Cursor.Position);

					// If we were over a tab, set the capture state for the window so that we'll actually receive a WM_LBUTTONUP message
					if (m_parentForm.TabRenderer.OverTab(m_parentForm.Tabs, relativeCursorPosition) == null &&
					    !m_parentForm.TabRenderer.IsOverAddButton(relativeCursorPosition))
					{
						m_parentForm.ForwardMessage(ref m);
					}

					else
					{
						// When the user clicks a mouse button, save the tab that the user was over so we can respond properly when the mouse button is released
						TitleBarTabItem clickedTab = m_parentForm.TabRenderer.OverTab(m_parentForm.Tabs, relativeCursorPosition);

						if (clickedTab != null)
						{
							// If the user clicked the close button, remove the tab from the list
							if (!m_parentForm.TabRenderer.IsOverCloseButton(clickedTab, relativeCursorPosition))
							{
								m_parentForm.ResizeTabContents(clickedTab);
								m_parentForm.SelectedTabIndex = m_parentForm.Tabs.IndexOf(clickedTab);

								Render();
							}

							OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, Cursor.Position.X, Cursor.Position.Y, 0));
						}

						m_parentForm.Activate();
					}

					break;

				case WM.WM_LBUTTONDBLCLK:
					m_parentForm.ForwardMessage(ref m);
					break;

					// We always return HTCAPTION for the hit test message so that the underlying window doesn't have its focus removed
				case WM.WM_NCHITTEST:
					m.Result = new IntPtr((int) HT.HTCAPTION);
					break;

				case WM.WM_LBUTTONUP:
				case WM.WM_NCLBUTTONUP:
					Point relativeCursorPosition2 = GetRelativeCursorPosition(Cursor.Position);

					if (m_parentForm.TabRenderer.OverTab(m_parentForm.Tabs, relativeCursorPosition2) == null &&
					    !m_parentForm.TabRenderer.IsOverAddButton(relativeCursorPosition2))
					{
						m_parentForm.ForwardMessage(ref m);
					}

					else
					{
						// When the user clicks a mouse button, save the tab that the user was over so we can respond properly when the mouse button is released
						TitleBarTabItem clickedTab = m_parentForm.TabRenderer.OverTab(m_parentForm.Tabs, relativeCursorPosition2);

						if (clickedTab != null)
						{
							// If the user clicked the close button, remove the tab from the list
							if (m_parentForm.TabRenderer.IsOverCloseButton(clickedTab, relativeCursorPosition2))
							{
								clickedTab.Content.Close();
								Render();
							}

							else
							{
								m_parentForm.OnTabClicked(
									new TitleBarTabEventArgs
									{
										Tab = clickedTab,
										TabIndex = m_parentForm.SelectedTabIndex,
										Action = TabControlAction.Selected,
										WasDragging = m_wasDragging
									});
							}
						}

							// Otherwise, if the user clicked the add button, call CreateTab to add a new tab to the list and select it
						else if (m_parentForm.TabRenderer.IsOverAddButton(relativeCursorPosition2))
						{
							m_parentForm.AddNewTab();
						}

						OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, Cursor.Position.X, Cursor.Position.Y, 0));
					}

					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		/// <summary>Event handler that is called when <see cref="m_parentForm" />'s <see cref="Form.Activated" /> event is fired.</summary>
		/// <param name="sender">Object from which this event originated.</param>
		/// <param name="e">Arguments associated with the event.</param>
		private void _parentForm_Activated(object sender, EventArgs e)
		{
			m_active = true;
			Render();
		}

		/// <summary>Event handler that is called when <see cref="m_parentForm" />'s <see cref="Form.Deactivate" /> event is fired.</summary>
		/// <param name="sender">Object from which this event originated.</param>
		/// <param name="e">Arguments associated with the event.</param>
		private void _parentForm_Deactivate(object sender, EventArgs e)
		{
			m_active = false;
			Render();
		}

		/// <summary>Event handler that is called when <see cref="m_parentForm" />'s <see cref="Component.Disposed" /> event is fired.</summary>
		/// <param name="sender">Object from which this event originated.</param>
		/// <param name="e">Arguments associated with the event.</param>
		private void _parentForm_Disposed(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Contains information on mouse events captured by <see cref="TitleBarTabsOverlay.MouseHookCallback" /> and processed by
		/// <see cref="TitleBarTabsOverlay.InterpretMouseEvents" />.
		/// </summary>
		protected class MouseEvent
		{
			/// <summary>Code for the event.</summary>
			// ReSharper disable InconsistentNaming
			public int nCode
			{
				get;
				set;
			}

			/// <summary>wParam value associated with the event.</summary>
			public IntPtr wParam
			{
				get;
				set;
			}

			/// <summary>lParam value associated with the event.</summary>
			public IntPtr lParam
			{
				get;
				set;
			}

			// ReSharper restore InconsistentNaming

			/// <summary>Data associated with the mouse event.</summary>
			public MSLLHOOKSTRUCT? MouseData
			{
				get;
				set;
			}
		}
	}
}