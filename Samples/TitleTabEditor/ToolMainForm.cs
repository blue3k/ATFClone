using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

using Sce.Atf;
using Sce.Atf.Adaptation;
using Sce.Atf.Applications;
using Sce.Atf.Controls.CurveEditing;
using Sce.Atf.Dom;
using Sce.Atf.Gui.TitleBarTab;


using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Controls.Adaptable;
using System.ComponentModel.Composition.Hosting;
using Sce.Atf.Controls.PropertyEditing;


namespace TitleTabEditor
{
    [Export(typeof(Form))]
    [Export(typeof(IMainWindow))]
    [Export(typeof(IWin32Window))]
    [Export(typeof(ISynchronizeInvoke))]
    [Export(typeof(MainForm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ToolMainForm : TitleBarTabMainForm, IMainWindow
    {
        /// <summary>
        /// Gets and sets main form bounds</summary>
        public Rectangle MainFormBounds
        {
            get
            {
                return m_mainFormBounds;
            }
            set
            {
                // Make sure the MainForm is visible. Setting Bounds sets m_mainFormBounds via SetBounds()
                if (WinFormsUtil.IsOnScreen(value))
                {
                    m_mainFormBounds = value;
                    Bounds = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets main form window state</summary>
        public FormWindowState MainFormWindowState
        {
            get
            {
                FormWindowState state = WindowState;
                if (state == FormWindowState.Minimized)
                    state = FormWindowState.Normal; // only return normal or maximized

                return state;
            }
            set
            {
                if (value == FormWindowState.Maximized && !m_mainFormLoaded)
                {
                    m_maximizeWindow = true;
                    WindowState = FormWindowState.Normal;
                }
                else
                    WindowState = value;
            }
        }


        [Import(AllowDefault = true)]
        private ISettingsService m_settingsService = null;

        private Rectangle m_mainFormBounds;
        private bool m_maximizeWindow;
        private bool m_mainFormLoaded;

        // Note: in some cases OnShown method will be called while still 
        // inside OnLoad which will mess up the order of Loading and Loaded events.
        // The following two boolean variables 
        private bool m_loading = false;
        private bool m_showen = false; // shown is called.


        public ToolMainForm()
        {
            InitializeComponent();

            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);
            Text = "ToolMain".Localize();
            Name = "ToolMain";
            Icon = GdiUtil.CreateIcon(ResourceUtil.GetImage(Sce.Atf.Resources.AtfIconImage));

            StartPosition = FormStartPosition.Manual; // so we can persist bounds
            m_mainFormBounds = Bounds;

            Sce.Atf.Direct2D.D2dFactory.EnableResourceSharing(this.Handle);
        }


        // We don't support dynamic tab
        public override TitleBarTabItem CreateTab() { return null; }
        public override void AddNewTab() { }


        /// <summary>
        /// Raises the form Load event</summary>
        /// <param name="e">Event args</param>
        protected override void OnLoad(System.EventArgs e)
        {
            const string MainToolWindowCategory = "MainToolWindow";
            try
            {
                m_loading = true;
                m_showen = false;  // used to test if OnShown is called before finishing OnLoad.

                // TODO:  m_settingsService is intended to be initialized via MEF for ATF 3.0 applications.
                //        For legacy applications which don't use MEF, it appears these same settings are registered explicitly
                //        much earlier in ApplicationHostService.cs.  The problem is that we need to ensure we don't try to 
                //        apply settings until this point, which is an open issue for legacy applications, although the default 
                //        ISettingsService "gets lucky" and defers callbacks long enough to get by.
                //        See also other comments in this file labled SCREAM_TOOLBAR_STATE_ISSUE
                if (m_settingsService != null)
                {
                    m_settingsService.RegisterSettings(this,
                        new BoundPropertyDescriptor(this, () => MainFormBounds, "MainFormBounds", MainToolWindowCategory, null),
                        new BoundPropertyDescriptor(this, () => MainFormWindowState, "MainFormWindowState", MainToolWindowCategory, null));
                }

                // deserialize  mainform WindowState here works better with DockPanelSuite; 
                // this fixes an issue to restore window size causes the form to span dual monitors   
                // when the program starts maximized
                m_mainFormLoaded = true;
                if (m_maximizeWindow)
                    WindowState = FormWindowState.Maximized;

                // Call base.OnLoad(e) last to ensure Loading event to occur before Loaded event.            
                base.OnLoad(e);
                Loading.Raise(this, e);

                // if OnShown already called then we need raise OnLoaded event here.
                if (m_showen)
                    Loaded.Raise(this, e);
            }
            finally
            {
                m_loading = false;
            }
        }

        #region IMainWindow Members

        /// <summary>
        /// Gets or sets the main window text</summary>
        string IMainWindow.Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets a Win32 handle for displaying WinForms dialogs with an owner</summary>
        /// <summary>
        public object DialogOwner
        {
            get { return this; }
        }

        /// <summary>
        /// Closes the application's main window</summary>
        void IMainWindow.Close()
        {
            base.Close();
        }

        /// <summary>
        /// Event that is raised before the application is loaded</summary>
        public event EventHandler Loading;

        /// <summary>
        /// Event that is raised after the application is loaded</summary>
        public event EventHandler Loaded;

        #endregion



        /// <summary>
        /// Raises the form SizeChanged event</summary>
        /// <param name="e">Event args</param>
        protected override void OnSizeChanged(System.EventArgs e)
        {
            SetBounds();
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Raises the form LocationChanged event</summary>
        /// <param name="e">Event args</param>
        protected override void OnLocationChanged(System.EventArgs e)
        {
            SetBounds();
            base.OnLocationChanged(e);
        }

        private void SetBounds()
        {
            // Only set our shadow of the main form's bounds if in normal state
            if (WindowState == FormWindowState.Normal)
                m_mainFormBounds = Bounds;
        }


        /// <summary>
        /// Raises the form Shown event</summary>
        /// <param name="e">Event args</param>
        protected override void OnShown(EventArgs e)
        {
            m_showen = true;
            base.OnShown(e);
            // if m_loading is true then this is called before
            // OnLoad finshed, so in this case don't raise
            // Loaded event because we still processing Loading event.
            if (!m_loading)
                Loaded.Raise(this, e);
        }

    }
}
