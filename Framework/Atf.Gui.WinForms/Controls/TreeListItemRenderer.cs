//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Drawing;
using Sce.Atf.Applications;

namespace Sce.Atf.Controls
{
    /// <summary>
    /// Renders the items of a tree control with details in columns.
    /// To display node details in columns, fill the info.Properties from your actual node data 
    /// in IItemView.GetInfo(). For each node detail(column), you need to construct a DataEditor
    /// derived data editor to display/edit the node data.</summary>
    /// <remarks>This class is intended to be used in conjunction with TreeListControl.</remarks>
    public class TreeListItemRenderer : TreeItemRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeListItemRenderer"/> class.</summary>
        /// <param name="itemView">The item view</param>
        public TreeListItemRenderer(IItemView itemView)
        {
            m_itemView = itemView;
        }

        /// <summary>
        /// Gets the item view interface.</summary>
        public IItemView ItemView
        {
            get { return m_itemView; }
        }

        /// <summary>
        /// Active data editor that may want to tracking mouse movement, such as a slider.</summary>
        internal DataEditor TrackingEditor { get; set; }


        /// <summary>
        /// Draws the text of a tree node's label at the specified location and the same size
        /// that MeasureLabel() would calculate for this node</summary>
        /// <param name="node">The tree control's node whose label is to be drawn</param>
        /// <param name="g">The current GDI+ graphics object</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the drawn text</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the drawn text</param>
        public override void DrawLabel(TreeControl.Node node, Graphics g, int x, int y)
        {
            base.DrawLabel(node, g, x, y);
        }

        /// <summary>
        /// Draws the data of a tree node at the specified location.</summary>
        /// <param name="node">The tree control's node whose data is to be drawn</param>
        /// <param name="g">The current GDI+ graphics object</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the node label</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the node label</param>
        public override void DrawData(TreeControl.Node node, Graphics g, int x, int y)
        {
            var treeListControl = node.TreeControl as TreeListControl;

            ItemInfo info = new WinFormsItemInfo();
            m_itemView.GetInfo(node.Tag, info);
            if (info.Properties.Length ==0)
                return;

            Region oldClip = g.Clip;

            //UpdateColumnWidths(node, info, g);
            int xOffset = treeListControl.TreeWidth;
            for (int i = 0; i < info.Properties.Length; ++i)
            {
                var dataEditor = info.Properties[i] as DataEditor;
                if (dataEditor != null) // show object data details in columns
                {
                    if (TrackingEditor != null && (TrackingEditor.Owner == node.Tag) &&
                        (TrackingEditor.Name == dataEditor.Name))
                        dataEditor = TrackingEditor;
                    var clip = new Rectangle(xOffset, y, treeListControl.Columns[i].ActualWidth, treeListControl.GetRowHeight(node));
                    if (i == info.Properties.Length - 1) // extends last column 
                        clip.Width = node.TreeControl.ActualClientSize.Width - xOffset;
                    g.SetClip(clip);
                    dataEditor.PaintValue(g, clip);
                }
                xOffset += treeListControl.Columns[i].ActualWidth;
            }

            if (node.Selected)
            {
                g.SetClip(new Rectangle(0, y, treeListControl.Width+1, node.LabelHeight+1));
                g.DrawRectangle(s_BorderPen, 0, y, treeListControl.Width, node.LabelHeight);
            }

            g.Clip = oldClip;
        }

        /// <summary>
        /// Update column widths only under auto-resizing mode.</summary>
        /// <param name="node">Tree node</param>
        /// <param name="info">Mode info</param>
        /// <param name="g">Graphics object used for drawing</param>
        private void UpdateColumnWidths(TreeControl.Node node, ItemInfo info, Graphics g)
        {
            var treeListControl = node.TreeControl as TreeListControl;
            if (treeListControl.AutoResizeColumns)
            {
                bool updateColumnWidths = treeListControl.Columns.Count == info.Properties.Length;
                for (int i = 0; i < info.Properties.Length; ++i)
                {
                    var dataEditor = info.Properties[i] as DataEditor;
                    if (dataEditor != null) // show object data details in columns
                    {

                        var size = dataEditor.Measure(g, SizeF.Empty);
                        if (updateColumnWidths)
                        {
                            treeListControl.Columns[i].ActualWidth = Math.Max(treeListControl.Columns[i].Width,
                                (int)size.Width);
                        }
                    }
                }
            }
        }

        private readonly IItemView m_itemView;
        private static readonly Pen s_BorderPen = Pens.Black;
    }
}