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

using Sce.Atf.Applications;


namespace Sce.Atf
{
    /// <summary>Console main frame for console program</summary>
    [Export(typeof(IMainWindow))]
    [Export(typeof(IInitializable))]
    [Export(typeof(IBatchTaskManager))]
    [Export(typeof(ISynchronizeInvoke))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConsoleMainForm : IMainWindow, IInitializable, IDisposable, IBatchTaskManager
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

        public void Run()
        {
            try
            {
                Loading.Raise(this, new EventArgs());
                Loaded.Raise(this, new EventArgs());
            }
            finally
            {
            }

            ExecuteBatchTask();
        }


        #region IBatchTaskManager

        // Batch task
        public void RegisterBatchTask(IBatchTask task)
        {
            m_batchTasks.Add(task);
        }

        // Execute all registered batch task
        public void ExecuteBatchTask()
        {
            foreach(var batch in m_batchTasks)
            {
                batch.Execute();
            }
        }


        #endregion

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
                if (m_components != null)
                {
                    m_components.Dispose();
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



        /// <summary>
        /// Required designer variable</summary>
        private readonly System.ComponentModel.IContainer m_components = null;

        //[Import(AllowDefault = true)]
        //private Sce.Atf.Applications.ISettingsService m_settingsService = null;

        private List<IBatchTask> m_batchTasks = new List<IBatchTask>();

        //private bool m_mainFormLoaded;

        private CompositionContainer m_componentContainer = null;
    }
}
