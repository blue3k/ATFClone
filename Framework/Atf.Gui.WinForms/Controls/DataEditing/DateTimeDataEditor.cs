//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;

namespace Sce.Atf.Controls
{
    /// <summary>
    /// Class to edit a float value using a Textbox, optionally with a slider tool.</summary>
    public class DateTimeDataEditor : DataEditor
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatDataEditor"/> class.</summary>
        /// <param name="theme">The visual theme to use</param>
        public DateTimeDataEditor(DataEditorTheme theme)
            : base(theme)
        {
        }


        /// <summary>
        /// The float value to display and edit.</summary>
        public DateTime Value;

        DateTime m_startValue;

        /// <summary>
        /// Layout: Measures the desired layout size of the data value and any child UI elements.</summary>
        /// <param name="g">Graphics that can be used for measuring strings</param>
        /// <param name="availableSize">The available size that this editor can give to child UI elements</param>
        /// <returns>
        /// The size that this editor determines it needs during layout, based on its calculations of child element sizes</returns>
        public override SizeF Measure(Graphics g,  SizeF availableSize)
        {

            SizeF size = g.MeasureString(Value.ToString(), Theme.Font);
            size.Width += Theme.Padding.Left;
            return size;
        }

        /// <summary>
        /// Implement the display of the value's representation.</summary>
        /// <param name="g">The Graphics object</param>
        /// <param name="area">Rectangle delimiting area to paint</param>
        public override void PaintValue(Graphics g,  Rectangle area)
        {
            int textOffset = 0;
            int left = area.Left + Theme.Padding.Left;

            string valueString = ToString();
            g.DrawString(valueString, Theme.Font, Theme.TextBrush, left + textOffset, area.Top); 
        }

        private void DrawThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
        {
            s_thumbPoints[0] = bounds.Location;
            s_thumbPoints[1] = new Point(bounds.Right, bounds.Top);
            s_thumbPoints[2] = new Point(bounds.Right, bounds.Top + bounds.Height * 3 / 4);
            s_thumbPoints[3] = new Point((bounds.Left + bounds.Right) / 2, bounds.Top + bounds.Height);
            s_thumbPoints[4] = new Point(bounds.Left, bounds.Top + bounds.Height * 3 / 4);

            g.DrawPolygon(Theme.SliderTrackPen, s_thumbPoints);
        }

        private static Point[] s_thumbPoints = new Point[5];

        /// <summary>
        /// Determines the editing mode from input position.</summary>
        /// <param name="p">Input position point</param>
        public override void SetEditingMode(Point p)
        {
            EditingMode = EditMode.ByTextBox;
        }

        /// <summary>
        /// When context is changed
        /// </summary>
        public override void OnContextChanged()
        {
            if (EditingContext == null) return;
            Value = (DateTime)Convert.ChangeType(EditingContext.GetValue(), typeof(DateTime));
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
            m_startValue = Value;

            if (EditingMode == EditMode.ByTextBox)
            {
                int textBoxOffset = 0;
                TextBox.Text = Value.ToString();
                TextBox.Bounds = new Rectangle(Bounds.Left + textBoxOffset, Bounds.Top, Bounds.Width - textBoxOffset, Bounds.Height);
                TextBox.SelectAll();
                TextBox.Show();
                TextBox.Focus();
            }
        }


        /// <summary>
        /// Ends an edit operation.</summary>
        /// <returns>
        /// 'true' if the change should be committed and 'false' if the change should be discarded</returns>
        public override bool EndDataEdit()
        {
            if (EditingMode == EditMode.ByTextBox)
            {
                Parse(TextBox.Text);
            }
            return (!(m_startValue == Value || Value == m_startValue));
        }


        /// <summary>
        /// Performs custom actions when moving the mouse pointer over this editor.</summary>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data</param>
        public override void OnMouseMove(MouseEventArgs e)
        {
        }

        /// <summary>
        /// Performs custom actions when the user clicks the editor with either mouse button.</summary>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data</param>
        public override void OnMouseDown(MouseEventArgs e)
        {
        }
        
        /// <summary>
        /// Determines whether the data editor wants to track mouse movement.</summary>
        /// <returns>
        /// Whether data editor wants to track mouse movement</returns>
        public override bool WantsMouseTracking()
        {
            return EditingMode == EditMode.BySlider;
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.</summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Parses the specified string representation and sets the data value.</summary>
        /// <param name="s">String to parse</param>
        public override void Parse(string s)
        {
            DateTime singleResult;
            if (DateTime.TryParse(s, out singleResult))
            {
                Value = singleResult;
            }
        }

    }
}