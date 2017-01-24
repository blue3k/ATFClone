//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

using Sce.Atf;
using Sce.Atf.Adaptation;
using Sce.Atf.Applications;
using Sce.Atf.Controls;

namespace Sce.Atf.Applications
{
    /// <summary>
    /// Displays a tree view of the DOM data. Uses the context registry to track
    /// the active UI data as documents are opened and closed.</summary>
    [Export(typeof(FilteredTreeListControlEditor))]
    [PartCreationPolicy(CreationPolicy.Any)]
    public class FilteredTreeListControlEditor : FilteredTreeControlEditor
    {
        public DataEditorTheme EditorTheme { get; private set; }


        protected override TreeControl CreateControl()
        {
            return new TreeListControl
            {
                ImageList = ResourceUtil.GetImageList16(),
                StateImageList = ResourceUtil.GetImageList16()
            };
        }

        protected override TreeControlAdapter CreateAdapter(TreeControl treeControl)
        {
            return new TreeControlAdapter(treeControl);
        }

        /// <summary>
        /// Gets or sets the tree view displayed by the editor</summary>
        public override ITreeView TreeView
        {
            set
            {
                base.TreeView = value;
                TreeControl.ItemRenderer = new TreeListItemRenderer(value as IItemView);
            }
        }

        protected override void Configure(out TreeControl treeControl, out TreeControlAdapter treeControlAdapter)
        {
            base.Configure(out treeControl, out treeControlAdapter);

            EditorTheme = new DataEditorTheme(treeControl.Font);
        }

        /// <summary>
        /// Constructor</summary>
        /// <param name="commandService">Command service for opening right-click context menus</param>
        [ImportingConstructor]
        public FilteredTreeListControlEditor(ICommandService commandService)
            : base(commandService)
        {
        }
    }
}
