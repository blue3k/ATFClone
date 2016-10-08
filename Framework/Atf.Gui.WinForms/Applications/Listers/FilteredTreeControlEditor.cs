//Copyright � 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.ComponentModel.Composition;
using System.Drawing;

using System.Windows.Forms;

using Sce.Atf.Adaptation;
using Sce.Atf.Controls;
using Sce.Atf.Controls.PropertyEditing;
using System.Collections.Generic;

namespace Sce.Atf.Applications
{
    /// <summary>
    /// Add filtering support for tree editors</summary>
    [Export(typeof(FilteredTreeControlEditor))]
    [PartCreationPolicy(CreationPolicy.Any)]
    public class FilteredTreeControlEditor: TreeControlEditor
    {
        /// <summary>
        /// Constructor</summary>
        /// <param name="commandService">Command service for opening right-click context menus</param>
        [ImportingConstructor]
        public FilteredTreeControlEditor(ICommandService commandService)
            : base(commandService)
        {
        }


        /// <summary>
        /// Gets or sets the filter patterns</summary>
        public HashSet<string> FilterPatterns
        {
            get { return m_TagPanel.TagList; }
            set
            {
                m_TagPanel.TagList.Clear();
                foreach(var tag in value)
                    m_TagPanel.TagList.Add(tag);

                UpdateFiltering(this,null);
            }
        }


        /// <summary>
        /// Create and configure TreeControl</summary>
        /// <param name="treeControl">New TreeControl</param>
        /// <param name="treeControlAdapter">Adapter for TreeControl</param>
        protected override void Configure(out TreeControl treeControl, out TreeControlAdapter treeControlAdapter)
        {
            base.Configure(out treeControl, out treeControlAdapter);

            m_control = new UserControl();
            m_control.Dock = DockStyle.Fill;
            m_control.SuspendLayout();
            m_control.Name = "Tree View".Localize();
            m_control.Text = "Tree View".Localize();


            // create tag panel
            m_TagPanel = new TagLabelListControl();
            m_TagPanel.Dock = DockStyle.Top;
            m_TagPanel.OnTagListUpdated += UpdateFiltering;
            m_control.Controls.Add(m_TagPanel);

            m_TagInput = new StringTagSearchInputUI(m_TagPanel);
            m_control.Controls.Add(m_TagInput);

            m_control.Controls.Add(TreeControl);
            m_control.Layout += controls_Layout;
            m_control.ResumeLayout();

            TreeControl.PreviewKeyDown += TreeControl_PreviewKeyDown;
            TreeControl.NodeExpandedChanging += TreeControl_NodeExpandedChanging;
            TreeControl.NodeExpandedChanged += TreeControl_NodeExpandedChanged;
            TreeControl.ItemRendererChanged += (sender, e) => UpdateTreeItemRenderer();
        }
        

        /// <summary>
        /// Gets the control that hosts the tree view</summary>
        /// <remarks>Use this control to register in ControlHostService if you need the filtering feature</remarks>
        public Control Control
        {
            get { return m_control; }
        }


        /// <summary>
        /// Callback to determine if an item in the tree is filtered in (return true) or out</summary>
        /// <param name="item">Item tested for filtering</param>
        /// <returns>True if filtered in, false if filtered out</returns>
        public bool DefaultFilter(object item)
        {
            bool result = true;
            IItemView itemView = TreeView.As<IItemView>();
            if(m_TagPanel.TagList.Count != 0)
            {
                ItemInfo info = new WinFormsItemInfo();
                itemView.GetInfo(item, info);
                result = info.Label != null && m_TagPanel.Matches(info.Label);
            }
            return result;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to restore sub-tree expansion state when collapsing a node
        /// false by default</summary>          
        public bool RestoreSubExpansion
        {
            get;
            set;
        }

        /// <summary>
        /// Performs custom actions after filtering finished</summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        protected void UpdateFiltering(object sender, EventArgs e)
        {
            var filteredTreeView = TreeView as FilteredTreeView;
            if (filteredTreeView == null || TreeControl == null || filteredTreeView.Root == null)
                return;

            // Layout need to be updated when tag input height is changed
            int yoffset = m_TagInput.Height;
            yoffset += m_TagPanel.Visible ? m_TagPanel.Height /*+ TreeControl.Margin.Top*/ : 0;
            if (m_LastTagInputHeight != yoffset)
                UpdateLayout();

            UpdateTreeItemRenderer();            

            try
            {
                m_updating = true;
                bool search = FilterPatterns.Count != 0;
                if (search)
                {
                    if (!m_searching)
                    {// Search started: remember expansion
                        m_searching = true;
                        filteredTreeView.SaveExpansion(TreeControl.Root);
                    }

                    filteredTreeView.ClearCache();
                    filteredTreeView.IsFiltering = true;

                    // collapse then expand all, to show all the matched nodes 
                    Collapse();                    
                    TreeControl.ExpandAll();
                    TreeControl.Invalidate();
                }
                else
                {
                    filteredTreeView.IsFiltering = false;
                    if (m_searching)
                    {// Search stopped: restore expansion
                        m_searching = false;
                        Collapse();                        
                        TreeControl.Root.Expanded = true; 
                        filteredTreeView.RestoreExpansion(TreeControl.Root, TreeControlAdapter);
                        TreeControl.Invalidate();
                    }
                }
            }
            finally
            {
                m_updating = false;
            }
        }


        void UpdateLayout()
        {
            int yoffset = m_TagInput.Height;
            yoffset += m_TagPanel.Visible ? m_TagPanel.Height /*+ TreeControl.Margin.Top*/ : 0;
            m_LastTagInputHeight = yoffset;
            TreeControl.Bounds = new Rectangle(0, yoffset, m_control.Width, m_control.Height - yoffset);
        }

        private void controls_Layout(object sender, LayoutEventArgs e)
        {
            UpdateLayout();
        }

        private void UpdateTreeItemRenderer()
        {
            var filteredTreeView = TreeView as FilteredTreeView;
            if(filteredTreeView != null)
            {
                TreeItemRenderer itemRenderer = TreeControl.ItemRenderer;
                itemRenderer.FilteringPattern = m_TagPanel.TagList;
                itemRenderer.FilteringStatus = filteredTreeView.GetNodeFilteringStatus;
            }
        }
        // hold the node that need to be expanded         
        private TreeControl.Node m_toExpand;
        private void TreeControl_NodeExpandedChanging(object sender, TreeControl.CancelNodeEventArgs e)
        {            
            var treeView = TreeView.As<FilteredTreeView>();
            // early exit
            if (m_updating || !m_searching || e.Node.Tag == null
                 || treeView == null) return;

            // is the node or any of its decendants passed filter
            if (treeView.IsMatched(e.Node.Tag))
            {
                if (e.Node.Expanded)
                {
                    if (!treeView.IsFullyExpaned(e.Node.Tag))
                    {
                        // add all the children that did not pass
                        // the filtering to the exempted set.
                        treeView.AddToExemptSet(e.Node.Tag);

                        // save e.Node and expand it in NodeExpandedChanged event handler.
                        m_toExpand = e.Node;
                    }

                    if (RestoreSubExpansion)
                        treeView.SaveExpansion(e.Node);
                } // end of if (e.Node.Expanded )
                else
                { // e.Node about to expand. Remove all its children from exempted set.
                  // so normal filtering will be applied.
                    treeView.RemoveFromExemptSet(e.Node.Tag);
                }
            }//  end of if(treeView.IsMatched(e.Node.Tag))
            else
            {
                if(e.Node.Expanded)
                {
                    if (RestoreSubExpansion)
                        treeView.SaveExpansion(e.Node);
                }
                else
                {
                    treeView.AddToExemptSet(e.Node.Tag);
                }
            }
        }
        private void TreeControl_NodeExpandedChanged(object sender, TreeControl.NodeEventArgs e)
        {            
            var treeView = TreeView.As<FilteredTreeView>();
            // early exit
            if (m_updating || !m_searching || e.Node.Tag == null
                 || treeView == null) return;

            if(m_toExpand != null)
            {
                var node = m_toExpand;
                m_toExpand = null;
                try
                {
                    m_updating = true;
                    TreeControlAdapter.Expand(node.Tag);
                    treeView.RestoreExpansion(e.Node, TreeControlAdapter);
                }
                finally
                {
                    m_updating = false;
                }
            }
            else if(e.Node.Expanded && RestoreSubExpansion)
            {
                treeView.RestoreExpansion(e.Node, TreeControlAdapter);
            }
        }

        private void TreeControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        
        private void Collapse()
        {
            if (TreeControl.ShowRoot)
            {
                TreeControl.Root.Expanded = false;
                TreeControlAdapter.Refresh(TreeControl.Root.Tag);            
            }                
            else
            {
                TreeControl.Root.Children.ForEach(child => child.Expanded = false);
                TreeView.GetChildren(TreeControl.Root.Tag).ForEach(child => TreeControlAdapter.Refresh(child));
            }
        }
        private UserControl m_control;
        StringTagSearchInputUI m_TagInput;
        TagLabelListControl m_TagPanel;
        int m_LastTagInputHeight = 0;
        private bool m_searching = false;
        private bool m_updating = false;
    }
}
