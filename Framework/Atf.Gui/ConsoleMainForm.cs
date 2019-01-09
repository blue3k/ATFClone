//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Linq;
using Sce.Atf.Adaptation;
using System.ComponentModel.Composition.Hosting;

namespace Sce.Atf
{
    /// <summary>Console main frame for console program</summary>
    [Export(typeof(IMainWindow))]
    [Export(typeof(IInitializable))]
    [Export(typeof(ISynchronizeInvoke))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConsoleMainForm : IMainWindow, IInitializable, IDisposable
    {
        /// <summary>
        /// Gets or sets the main window text</summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Default constructor</summary>
        public ConsoleMainForm()
        {
        }

        /// <summary>
        /// Closes the application's main window</summary>
        public void Close()
        {
            if(Closing != null)
                Closing(this, new CancelEventArgs(false));
            Closed.Raise(this, new EventArgs());
        }

        #region IMainWindow Members

        /// <summary>
        /// Gets a handle for displaying WinForms dialogs with an owner</summary>
        public object DialogOwner
        {
            get { return this; }
        }

        /// <summary>
        /// Event that is raised before the application is loaded</summary>
        public event EventHandler Loading;

        /// <summary>
        /// Event that is raised after the application is loaded</summary>
        public event EventHandler Loaded;


        /// <summary>
        /// Event that is raised before the main window closes. Subscribers can cancel
        /// the closing action.</summary>
        public event CancelEventHandler Closing;

        /// <summary>
        /// Event that is raised after the main window is closed</summary>
        public event EventHandler Closed;

        #endregion

        /// <summary>
        /// Raises the form Load event</summary>
        /// <param name="e">Event args</param>
        public void Initialize()
        {
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
                }

                // deserialize  mainform WindowState here works better with DockPanelSuite; 
                // this fixes an issue to restore window size causes the form to span dual monitors   
                // when the program starts maximized
                //m_mainFormLoaded = true;

                Loading.Raise(this, new EventArgs());

                // if OnShown already called then we need raise OnLoaded event here.
                if (m_showen)
                    Loaded.Raise(this, new EventArgs());
            }
            finally
            {
                m_loading = false;
            }
        }

        /// <summary>
        /// Raises the form Shown event</summary>
        /// <param name="e">Event args</param>
        protected void OnShown(EventArgs e)
        {
            m_showen = true;
            //base.OnShown(e);            
            // if m_loading is true then this is called before
            // OnLoad finshed, so in this case don't raise
            // Loaded event because we still processing Loading event.
            if (!m_loading)
                Loaded.Raise(this, e);
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Cleans up any resources being used</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources;
        /// false to release only unmanaged resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
        }



        /// <summary>
        /// Accessors for component container
        /// </summary>
        public CompositionContainer ComponentContainer {  get { return m_componentContainer; } }

        /// <summary>
        /// Initialize composition. 
        /// When you need to manage your components with this mainform, you can ask MainForm to initialize your components
        /// </summary>
        /// <param name="catalog">Type catalog to initialize</param>
        /// <param name="sharedComponent">Shared component or pre-created batch objects</param>
        public void InitializeComposition(TypeCatalog catalog, List<object> sharedComponent)
        {
            var container = new CompositionContainer(catalog);

            CompositionBatch compositionBatch = new CompositionBatch();
            if (sharedComponent != null)
            {
                foreach (var part in sharedComponent)
                {
                    compositionBatch.AddPart(part);
                }
            }
            compositionBatch.AddPart(this);

            container.Compose(compositionBatch);

            container.InitializeAll();

            m_componentContainer = container;
        }


        private class ElementSortComparer<T> : IComparer<XmlElement>
        {
            int IComparer<XmlElement>.Compare(XmlElement element1, XmlElement element2)
            {
                string[] coords1 = element1.GetAttribute("Location").Split(',');
                string[] coords2 = element2.GetAttribute("Location").Split(',');
                return int.Parse(coords1[0]) - int.Parse(coords2[0]);
            }
        }


        // Note: in some cases OnShown method will be called while still 
        // inside OnLoad which will mess up the order of Loading and Loaded events.
        // The following two boolean variables 
        private bool m_loading;
        private bool m_showen; // shown is called.

        /// <summary>
        /// Required designer variable</summary>
        private readonly System.ComponentModel.IContainer components = null;

        [Import(AllowDefault = true)]
        private Sce.Atf.Applications.ISettingsService m_settingsService = null;

        //private bool m_mainFormLoaded;

        private CompositionContainer m_componentContainer = null;
    }
}
