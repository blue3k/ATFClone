//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

using Sce.Atf.Applications;

namespace Sce.Atf.Controls
{
    /// <summary>
    /// A Tree Control to display and edit hierarchical data in a tree view with details in columns. 
    /// The TreeListItemRenderer decides how to draw the columns.</summary>
    public class TreeListControl : TreeControl, IWindowsFormsEditorService
    {
        /// <summary>
        /// Constructor of a Visual Studio-like tree control, with default rendering using a single font
        /// for the entire control.</summary>
        public TreeListControl()
            : this(Style.Tree, null)
        {
        }

        /// <summary>
        /// Constructor of a tree control with the given style and default rendering using a single font
        /// for the entire control.</summary>
        /// <param name="style">Tree style</param>
        public TreeListControl(Style style)
            : this(style, null)
        {
        }

        /// <summary>
        /// Constructor of a tree control with a particular style and renderer.</summary>
        /// <param name="style">Tree style</param>
        /// <param name="itemRenderer">Renderer of a node in the tree. If null, then a new
        /// TreeItemRenderer is created and used</param>
        public TreeListControl(Style style, TreeListItemRenderer itemRenderer)
            : base(style, itemRenderer)
        {
            m_columns = new TreeListView.ColumnCollection();

            SuspendLayout();
            m_editTextBox = new TextBox();
            m_editTextBox.Visible = false;
            m_editTextBox.BorderStyle = BorderStyle.None;


            m_editTextBox.KeyDown += TextBoxKeyDown;
            m_editTextBox.KeyPress += TextBoxKeyPress;
            m_editTextBox.LostFocus += TextBoxLostFocus;

            ContentVerticalOffset = FontHeight+2;

            Controls.Add(m_editTextBox);
            m_seperatorPen = new Pen(Color.DarkGray,1);
            m_brush = new SolidBrush(Color.White);
            ResumeLayout();
        }

        /// <summary>
        /// Gets the column collection.</summary>
        public TreeListView.ColumnCollection Columns
        {
            get { return m_columns; }
        }

        /// <summary>
        /// Adjusts the width of all columns to fit the contents of all their cells, including the header cells.</summary>
        public bool AutoResizeColumns
        {
            get { return m_autoResizeColumns; }
            set { m_autoResizeColumns = value; }
        }

        /// <summary>Width of the left-side tree.</summary>
        public int TreeWidth
        {
            get { return m_treeWidth; }
            set { m_treeWidth = value; }
        }

        /// <summary>
        /// Raises the Paint event and performs a variety of operations on the tree.</summary>
        /// <param name="e">Event args</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Region oldClip = e.Graphics.Clip;
            Rectangle clip = e.ClipRectangle;
            clip.Width = TreeWidth;
            e.Graphics.SetClip(clip);
            base.OnPaint(e);

            e.Graphics.Clip = oldClip;

            // draw column names
            // This need to be after main draw otherwise tree contents will be drawn on the column names
            int leftOffset = TreeWidth;
            if (Columns.Count > 0)
            {
                e.Graphics.FillRectangle(m_brush, 0, 0, 
                    ActualClientSize.Width, ContentVerticalOffset + 2);

                for (int i = 0; i < Columns.Count; ++i)
                {
                    var column = Columns[i];
                    Rectangle textRect = new Rectangle(leftOffset + 3, Margin.Top, column.ActualWidth, ContentVerticalOffset);
                    if (i == Columns.Count - 1)
                        textRect.Width = ActualClientSize.Width - leftOffset; // extends last column 
                    e.Graphics.DrawString(column.Label, Font, TreeListItemRenderer.TextBrush, textRect);

                    // draw vertical separator 
                    e.Graphics.DrawLine(m_seperatorPen, leftOffset, Margin.Top, leftOffset, ContentVerticalOffset);
                    leftOffset += column.ActualWidth;

                }
                // draw horizontal separator 
                e.Graphics.DrawLine(m_seperatorPen, 0, ContentVerticalOffset + 2, ActualClientSize.Width,
                    ContentVerticalOffset + 2);
            }
        }

        /// <summary>
        /// Gets the bounding rectangle for label editing.</summary>
        /// <param name="info">Node layout information</param>
        /// <returns>
        /// Bounding rectangle for label editing</returns>
        protected override Rectangle GetLabelEditBounds(NodeLayoutInfo info)
        {
            // adjust the width to keep textbox for label editing away from the data columns
            var bounds = base.GetLabelEditBounds(info);
            bounds.Width = TreeWidth - bounds.Left;
            return bounds;
        }

        /// <summary>
        /// Disposes of unmanaged resources.</summary>
        /// <param name="disposing">Whether or not managed resources should be disposed</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_seperatorPen != null)
                {
                    m_seperatorPen.Dispose();
                    m_seperatorPen = null;
                }
            }
        }

        private void TextBoxLostFocus(object sender, EventArgs e)
        {
            EndDataEdit();
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EndDataEdit();
                e.Handled = true;
                m_handleKeyUp = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                AbortDataEdit();
                e.Handled = true;
                m_handleKeyUp = true;
            }
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (m_dataEditNode == null); // this suppresses the error sound from Keys.Enter, handled above
        }

        private void AbortDataEdit()
        {
            m_editData = null;
            m_dataEditNode = null;
            m_editTextBox.Hide();

        }

        /// <summary>
        /// Data edit event for Nodes.</summary>
        public class NodeEditEventArgs : EventArgs
        {
            /// <summary>
            /// Constructor</summary>
            /// <param name="node">Node that changed</param>
            /// <param name="editedData">Data that was edited</param>
            public NodeEditEventArgs(Node node, DataEditor editedData)
            {
                Node = node;
                EditedData = editedData;
            }
            /// <summary>
            /// Node that changed.</summary>
            public readonly Node Node;

            /// <summary>
            /// The edited data.</summary>
            public readonly DataEditor EditedData;
        }


        /// <summary>
        /// Event that is raised after a node's Label property is edited by the user.</summary>
        public event EventHandler<NodeEditEventArgs> NodeDataEdited;

        /// <summary>
        /// Raises the NodeLabelEdited event.</summary>
        /// <param name="e">Event args</param>
        protected virtual void OnNodeDataEdited(NodeEditEventArgs e)
        {
            NodeDataEdited.Raise(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event and performs configured mouse up processing.</summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if(m_columnResizing)
                AllowDrop = m_orgAllowDrop;
            m_columnResizing = false;
            Cursor = Cursors.Default;

            if (e.Button == MouseButtons.Left)
            {
                if (m_editData != null && 
                    (m_editData.EditingMode == DataEditor.EditMode.BySlider ||
                    m_editData.EditingMode == DataEditor.EditMode.ByClick))
                {
                    EndDataEdit();
                    return;
                }
                var p = new Point(e.X, e.Y);
                HitRecord hitRecord = Pick(p);
                if (hitRecord.Node != null)
                {
                    var dataEditor = GetDataEditor(hitRecord.Node, p);
                    if (dataEditor != null && (!dataEditor.ReadOnly))
                    {
                        if (dataEditor.EditingMode == DataEditor.EditMode.ByTextBox)
                        {
                            BeginDataEdit(hitRecord.Node, dataEditor);
                            return;
                        }
                    }
                }

            }
            base.OnMouseUp(e);
        }

        private TreeListItemRenderer TreeListItemRenderer
        {
            get
            {
                return (TreeListItemRenderer)ItemRenderer;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event and performs configured mouse down processing.</summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            EndDataEdit();
            if (e.Button == MouseButtons.Left)
            {
                m_firstPoint  = m_currentPoint = new Point(e.X, e.Y);
                m_currentColumn = HitColumnSeperator(e.X, e.Y);
                if (m_currentColumn >= 0)
                {
                    AutoResizeColumns = false;
                    m_columnResizing = true;
                    m_orgAllowDrop = AllowDrop;
                    AllowDrop = false;
                    m_oldColumnWidths = new int[Columns.Count];
                    for (int i = 0; i < Columns.Count; ++i)
                    {
                        m_oldColumnWidths[i] = Columns[i].ActualWidth;
                    }
                    ResizeColumn();
                }
                else
                {
                    var p = new Point(e.X, e.Y);
                    HitRecord hitRecord = Pick(p);
                    if (hitRecord.Node != null)
                    {
                        // always select editing node
                        SetSelection(hitRecord.Node);
                        OnSelectionChanged(EventArgs.Empty);

                        var dataEditor = GetDataEditor(hitRecord.Node, p);
                        if (dataEditor != null && (!dataEditor.ReadOnly))
                        {
                            TreeListItemRenderer.TrackingEditor = dataEditor;
                            // mouse down will change slider value, i.e. edit
                            if (dataEditor.EditingMode == DataEditor.EditMode.BySlider )
                                BeginDataEdit(hitRecord.Node, dataEditor);
                            else if (dataEditor.EditingMode == DataEditor.EditMode.ByClick)
                                BeginDataEdit(hitRecord.Node, dataEditor);
                            else if (dataEditor.EditingMode == DataEditor.EditMode.ByExternalControl)
                            {
                                dataEditor.FinishDataEdit = EndDataEdit;
                                BeginDataEdit(hitRecord.Node, dataEditor);
                            }

                            dataEditor.OnMouseDown(e);

                            Invalidate();
                            return;
                        }

                    }
                }
               
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event and performs configured mouse move processing.</summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            m_currentPoint = new Point(e.X, e.Y);

            if (HitColumnSeperator(e.X, e.Y) >=0)
                Cursor = Cursors.SizeWE;
            else
            {
                if (!m_columnResizing)
                    Cursor = Cursors.Default;
            }

            // test for dragging
            if (e.Button == MouseButtons.Left )
            {
                if (m_columnResizing)
                {
                    ResizeColumn();
                }
                else if (TreeListItemRenderer.TrackingEditor != null && (TreeListItemRenderer.TrackingEditor.WantsMouseTracking()))
                {
                    var p = new Point(e.X, e.Y);
                    var node = GetNodeAt(p);
                    if (node != null)
                    {
                        TreeListItemRenderer.TrackingEditor.OnMouseMove(e);
                        Invalidate();
                    }
                }
            }

            base.OnMouseMove(e);
        }

        private int HitColumnSeperator(int x, int y)
        {
            if (y < ContentVerticalOffset)
            {
                if (Columns.Count > 0) // hit a column separator 
                {
                    int left = TreeWidth;

                    for (int i = 0; i < Columns.Count; ++i)
                    {
                        var column = Columns[i];
                        if ( x <= (left + 2) && x >= (left - 2))
                            return i;
                        left += column.ActualWidth;
                    }
                }
            }
            return -1;
        }

        private void ResizeColumn()
        {
            if (m_currentPoint.X> ActualClientSize.Width)
                return;
            if (m_currentPoint.X < Margin.Left)
                return;
            if (m_currentColumn >= 0)
            {
                int delta = m_currentPoint.X - m_firstPoint.X;
                if (m_currentColumn == 0)
                    m_treeWidth = m_currentPoint.X;
                else
                {
                    if (Control.ModifierKeys == Keys.Control)
                    {
                        // change only that column
                        int newWitdh = m_oldColumnWidths[m_currentColumn] - delta;
                        const int minWidth = 8;
                        if (newWitdh >= minWidth)
                        {
                            // need to adjust preceding column width
                            int adjust = m_oldColumnWidths[m_currentColumn - 1] + delta;
                            if (adjust < minWidth)
                            {
                                newWitdh += adjust;
                                if (newWitdh < minWidth)
                                    return;
                                adjust = minWidth;
                            }
                            Columns[m_currentColumn - 1].ActualWidth = adjust;
                            Columns[m_currentColumn].ActualWidth = newWitdh;

                        }
                    }
                    else
                    {
                        // Move all subsequent column together 
                        Columns[m_currentColumn - 1].ActualWidth += delta;
                        m_firstPoint.X = m_currentPoint.X;
                    }
                }

                Invalidate();
             }
        }

        /// <summary>
        /// Get the data editor hit by point p; also set edit mode if the editor supports multiple controls.</summary>
        /// <param name="node">The node</param>
        /// <param name="p">The point</param>
        /// <returns>Data editor hit by point</returns>
        internal DataEditor GetDataEditor(TreeControl.Node node, Point p)
        {
            var treeListControl = node.TreeControl as TreeListControl;

            ItemInfo info = new WinFormsItemInfo();
            treeListControl.TreeListItemRenderer.ItemView.GetInfo(node.Tag, info);
            if (info.Properties.Length != treeListControl.Columns.Count)
                return null;

            int left = treeListControl.TreeWidth;

            for (int i = 0; i < treeListControl.Columns.Count; ++i)
            {
                var column = treeListControl.Columns[i];
                if (p.X >= left && p.X <= left + column.ActualWidth)
                {
                    var dataEditor = info.Properties[i] as DataEditor;
                    if (dataEditor != null)
                    {
                        foreach (NodeLayoutInfo nodeLayout in NodeLayout)
                        {
                            if (nodeLayout.Node == node)
                            {
                                dataEditor.TextBox = m_editTextBox;
                                dataEditor.Bounds = GetEditArea(nodeLayout, dataEditor);
                                dataEditor.SetEditingMode(p);
                                break;
                            }

                        }
                    }
                    return dataEditor;
                }

                left += column.ActualWidth;
            }

            return null;
        }

        /// <summary>
        /// Begins a data edit on the given node, if it is editable.
        /// Otherwise, does nothing.</summary>
        /// <param name="node">Tree node whose data is to be edited</param>
        /// <param name="editData"></param>
        private void BeginDataEdit(Node node, DataEditor editData)
        {
            EndDataEdit();

            if (editData.ReadOnly)
                return;

            m_editData = editData;
            m_dataEditNode = node;

            foreach (NodeLayoutInfo info in NodeLayout)
            {
                if (info.Node == m_dataEditNode)
                {
                    m_editData.TextBox = m_editTextBox;
                    m_editData.Bounds = GetEditArea(info, m_editData);
                    m_editData.BeginDataEdit(this);
                    Invalidate();
                    break;

                }
            }
        }

        private void EndDataEdit()
        {
            if (m_dataEditNode != null)
            {
                bool dataChanged = m_editData.EndDataEdit();
                if (dataChanged)
                {
                    OnNodeDataEdited(new NodeEditEventArgs(m_dataEditNode, m_editData));
                    m_editData.OnValueChanged();
                }

                m_dataEditNode = null;
                m_editData = null;
                TreeListItemRenderer.TrackingEditor = null;
            }
            m_editTextBox.Hide();
        }

        private Rectangle GetEditArea(NodeLayoutInfo nodeLayoutInfo, DataEditor dataEditor)
        {
            int height = GetRowHeight(nodeLayoutInfo.Node);
            int xLeft = TreeWidth;
            int width = -1;
            for (int i = 0; i < Columns.Count; ++i)
            {
                if (Columns[i].Label == dataEditor.Name)
                {
                    width = Columns[i].ActualWidth;
                    break;
                }
                xLeft += Columns[i].ActualWidth;
            }
            Debug.Assert(width>=0);
            return new Rectangle(xLeft, nodeLayoutInfo.Y, width, height);
        }



        #region IWindowsFormsEditorService Members


        private DropDownForm m_dropDownForm;
        private bool m_closingDropDown;

        /// <summary>
        /// Drops down editor control</summary>
        /// <param name="control">The control to drop down</param>
        public void DropDownControl(Control control)
        {
            if(m_dropDownForm == null) m_dropDownForm = new DropDownForm(this);
            m_dropDownForm.SetControl(control);

            if(m_editData != null)
            {
                Point location = new Point(m_editData.Bounds.Left, m_editData.Bounds.Bottom);
                location = PointToScreen(location);
                int width = Math.Max(control.Width, m_editData.Bounds.Width);
                int height = control.Height;
                Rectangle bounds;
                bounds = new Rectangle(location.X, location.Y, width, height);

                m_dropDownForm.Bounds = bounds;
            }
            else
            {
                Point rightBottom = new Point(base.Right, base.Bottom);
                rightBottom = base.Parent.PointToScreen(rightBottom);
                Rectangle bounds = new Rectangle(
                    rightBottom.X - control.Width, rightBottom.Y, control.Width, control.Height);

                m_dropDownForm.Bounds = bounds;
            }

            Applications.SkinService.ApplyActiveSkin(control);
            m_dropDownForm.Visible = true;

            control.Focus();

            while (m_dropDownForm.Visible)
            {
                Application.DoEvents();
                MsgWaitForMultipleObjects(0, 0, true, 250, 255);
            }
        }

        /// <summary>
        /// Closes the dropped down editor</summary>
        public void CloseDropDown()
        {
            if (m_closingDropDown)
                return;

            // set the focus back to the text box right here so this control never loses focus
            //EnableTextBox();

            try
            {
                m_closingDropDown = true;
                if (m_dropDownForm.Visible)
                {
                    m_dropDownForm.SetControl(null);
                    m_dropDownForm.Visible = false;
                }
            }
            finally
            {
                m_closingDropDown = false;
            }
        }

        /// <summary>
        /// Opens a dialog editor</summary>
        /// <param name="dialog">The dialog to open</param>
        /// <returns>Result of user interaction with dialog</returns>
        public DialogResult ShowDialog(Form dialog)
        {
            dialog.ShowDialog(this);
            return dialog.DialogResult;
        }

        [DllImport("user32.dll")]
        private static extern int MsgWaitForMultipleObjects(
            int nCount,           // number of handles in array
            int pHandles,         // object-handle array
            bool bWaitAll,        // wait option
            int dwMilliseconds,   // time-out interval
            int dwWakeMask        // input-event type
            );

        #endregion



        private class DropDownForm : Form
        {
            public DropDownForm(TreeListControl parent)
            {
                m_parent = parent;

                base.StartPosition = FormStartPosition.Manual;
                base.ShowInTaskbar = false;
                base.ControlBox = false;
                base.MinimizeBox = false;
                base.MaximizeBox = false;
                base.FormBorderStyle = FormBorderStyle.None;
                base.Visible = false;
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                    m_parent.CloseDropDown();

                base.OnMouseDown(e);
            }

            protected override void OnClosed(EventArgs e)
            {
                if (Visible)
                    m_parent.CloseDropDown();

                base.OnClosed(e);
            }

            protected override void OnDeactivate(EventArgs e)
            {
                if (Visible)
                    m_parent.CloseDropDown();

                base.OnDeactivate(e);
            }

            public void SetControl(Control control)
            {
                if (m_control != null)
                {
                    Controls.Remove(m_control);
                    m_control = null;
                }
                if (control != null)
                {
                    //control.Width = Math.Max(m_parent.Width, control.Width);
                    //Size = control.Size;

                    m_control = control;
                    Controls.Add(m_control);

                    m_control.Location = new Point(0, 0);
                    m_control.Visible = true;
                }
                Enabled = m_control != null;
            }

            private Control m_control;
            private readonly TreeListControl m_parent;
        }


        private Node m_dataEditNode;
        private readonly TextBox m_editTextBox;
        private DataEditor m_editData;

        private readonly TreeListView.ColumnCollection m_columns;
        private Pen m_seperatorPen;
        private SolidBrush m_brush;
        private Point m_firstPoint;
        private Point m_currentPoint;
        private int m_currentColumn;
        private int [] m_oldColumnWidths;
        private int m_treeWidth= 200;
        private bool m_columnResizing;
        private bool m_autoResizeColumns = true;
        private bool m_orgAllowDrop = false;
    }
}
