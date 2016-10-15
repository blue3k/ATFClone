//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Sce.Atf.Controls
{
    /// <summary>
    /// Class to edit a enum value.</summary>
    public class EnumDataEditor : DataEditor
    {
        private string[] m_enumNames;
        private IWindowsFormsEditorService m_editorService;
        private int m_hoverIndex = -1;
        private bool m_listBoxMouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDataEditor"/> class.</summary>
        /// <param name="theme">The visual theme to use</param>
        public EnumDataEditor(DataEditorTheme theme, System.Type enumType)
            : this(theme, enumType.GetEnumNames())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDataEditor"/> class.</summary>
        /// <param name="theme">The visual theme to use</param>
        public EnumDataEditor(DataEditorTheme theme, string[] enumNames)
            : base(theme)
        {
            m_enumNames = enumNames;
        }

        /// <summary>
        /// The string value to display and edit.
        /// </summary>
        public string Value;

        /// <summary>
        /// Layout: Measures the desired layout size of the data value and any child UI elements.</summary>
        /// <param name="g">Graphics that can be used for measuring strings</param>
        /// <param name="availableSize">The available size that this editor can give to child UI elements</param>
        /// <returns>
        /// The size that this editor determines it needs during layout, based on its calculations of child element sizes</returns>
        public override SizeF Measure(Graphics g,  SizeF availableSize)
        {
            SizeF size = new SizeF();
            foreach (var enumName in m_enumNames)
            {
                var nameSize = g.MeasureString(enumName, Theme.Font);
                size.Width = Math.Max(size.Width, nameSize.Width);
                size.Height = Math.Max(size.Height, nameSize.Height);
            }
            size.Width += Theme.Padding.Left;
            return size;
        }


        /// <summary>
        /// Implement the display of the value's representation.</summary>
        /// <param name="g">The Graphics object</param>
        /// <param name="area">Rectangle delimiting area to paint</param>
        public override void PaintValue(Graphics g, Rectangle area)
        {
            if (ReadOnly)
                g.DrawString(Value, Theme.Font, Theme.ReadonlyBrush, area.Left + Theme.Padding.Left, area.Top); 
            else
                g.DrawString(Value, Theme.Font, Theme.TextBrush, area.Left + Theme.Padding.Left, area.Top); 
        }

        /// <summary>
        /// Determines the editing mode from input position.</summary>
        /// <param name="p">Input position point</param>
        public override void SetEditingMode(Point p)
        {
            if (Bounds.Contains(p))
                EditingMode = EditMode.ByExternalControl;
        }

        /// <summary>
        /// When context is changed
        /// </summary>
        public override void OnContextChanged()
        {
            if (EditingContext == null) return;
            Value = EditingContext.GetValue().ToString();
        }

        /// <summary>
        /// Called when value is changed
        /// </summary>
        public override void OnValueChanged()
        {
            if (EditingContext == null) return;
            EditingContext.SetValue(Value);
        }

        /// <summary>
        /// Begins an edit operation.</summary>
        public override void BeginDataEdit(IWindowsFormsEditorService editorService)
        {
            if (EditingMode != EditMode.ByExternalControl)
                return;

            TextBox.Text = ToString();
            m_editorService = editorService;

            if (m_editorService == null)
                return;

            ListBox listBox = new ListBox();
            listBox.DrawMode = DrawMode.OwnerDrawFixed;
            listBox.DrawItem += listBox_DrawItem;
            listBox.MouseMove += listBox_MouseMove;

            for (int i = 0; i < m_enumNames.Length; i++)
            {
                listBox.Items.Add(m_enumNames[i]);
                if (m_enumNames[i].Equals(Value))
                {
                    listBox.SelectedIndex = i;
                }
            }

            // size control so all strings are completely visible
            using (Graphics g = listBox.CreateGraphics())
            {
                float width = 0f;

                foreach (string name in m_enumNames)
                {
                    float w = g.MeasureString(name, listBox.Font).Width;
                    width = Math.Max(width, w);
                }

                float height = m_enumNames.Length * listBox.ItemHeight;
                int scrollBarThickness = SystemInformation.VerticalScrollBarWidth;
                if (height > listBox.Height - 4) // vertical scrollbar?
                    width += SystemInformation.VerticalScrollBarWidth;

                if (width > listBox.Width)
                    listBox.Width = (int)width + 6;
            }

            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            listBox.MouseDown += listBox_OnMouseDown;
            listBox.MouseUp += listBox_OnMouseUp;
            listBox.MouseLeave += listBox_OnMouseLeave;
            listBox.PreviewKeyDown += listBox_OnPreviewKeyDown;

            m_editorService.DropDownControl(listBox);
            int index = listBox.SelectedIndex;
            if (index >= 0)
            {
                string newValue = null;
                newValue = m_enumNames[index];

                // be careful to return the same object if the value didn't change
                if (!newValue.Equals(Value))
                {
                    Value = newValue;
                    OnValueChanged();
                }
            }


            EditingMode = EditMode.None;
        }

        /// <summary>
        /// Ends an edit operation.</summary>
        /// <returns>
        /// 'true' if the change should be committed and 'false' if the change should be discarded</returns>
        public override bool EndDataEdit()
        {
            // We don't use regular data editing flow
            return false;
        }

        /// <summary>
        /// Performs custom actions on ListBox SelectedIndexChanged events</summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_listBoxMouseDown)
            {
                m_listBoxMouseDown = false;
                m_editorService.CloseDropDown();
            }
        }

        private void listBox_OnMouseLeave(object sender, EventArgs e)
        {
            m_listBoxMouseDown = false;
        }

        private void listBox_OnMouseUp(object sender, MouseEventArgs e)
        {
            m_listBoxMouseDown = false;
        }

        private void listBox_OnMouseDown(object sender, MouseEventArgs e)
        {
            m_listBoxMouseDown = true;
        }

        private void listBox_OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Return ||
                e.KeyData == Keys.Tab ||
                e.KeyData == (Keys.Tab | Keys.Shift))
            {
                m_editorService.CloseDropDown();
            }
            else if (e.KeyData == Keys.Escape)
            {
                ((ListBox)sender).ClearSelected();
                m_editorService.CloseDropDown();
            }
        }
        
        /// <summary>
        /// Performs custom actions on ListBox MouseMove events</summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        void listBox_MouseMove(object sender, MouseEventArgs e)
        {

            ListBox listBox = sender as ListBox;
            if (e.Y <= m_enumNames.Length * listBox.ItemHeight)
            {
                int hoverIndex = e.Y / listBox.ItemHeight + listBox.TopIndex;
                if (hoverIndex != m_hoverIndex)
                {
                    m_hoverIndex = hoverIndex;
                    listBox.Invalidate();
                }
            }
            else
                m_hoverIndex = -1;

        }

        /// <summary>
        /// Performs custom actions on ListBox DrawItem events</summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            // Determine the forecolor based on whether or not the item is selected.
            Brush brush;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                brush = Brushes.White;
            }
            else
            {
                brush = Brushes.Black;
            }

            if (e.Index >= 0)
            {
                if (e.Index == m_hoverIndex && (e.State & DrawItemState.Selected) == 0)
                    e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);

                // Get the item text.
                string text = m_enumNames[e.Index];
                // Draw the item text.
                e.Graphics.DrawString(text, ((Control)sender).Font, brush, e.Bounds.X, e.Bounds.Y);
            }
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.</summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Parses the specified string representation and sets the data value.</summary>
        /// <param name="s">String to parse</param>
        public override void Parse(string s)
        {
            Value = s;
        }

    }
}