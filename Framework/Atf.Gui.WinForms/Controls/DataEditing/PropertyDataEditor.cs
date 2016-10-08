//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Controls.PropertyEditing;
using Sce.Atf.Dom;
using System;
using System.Drawing;
using System.Windows.Forms;
using static Sce.Atf.Controls.PropertyEditing.PropertyView;

namespace Sce.Atf.Controls
{
    /// <summary>
    /// Class to edit a Enum set.</summary>
    public class PropertyDataEditor : DataEditor
    {
        public PropertyEditingControl EditingControl { get; set; }
        public Property Property { get; set; }
        private SizeF m_defaultSize = new SizeF(50,22);



        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDataEditor"/> class.
        /// </summary>
        /// <param name="theme">The visual theme to use</param>
        public PropertyDataEditor(DataEditorTheme theme)
            : base(theme)
        {
        }


        /// <summary>
        /// Layout: Measures the desired layout size of the data value and any child UI elements.</summary>
        /// <param name="g">Graphics that can be used for measuring strings</param>
        /// <param name="availableSize">The available size that this editor can give to child UI elements</param>
        /// <returns>
        /// The size that this editor determines it needs during layout, based on its calculations of child element sizes</returns>
        public override SizeF Measure(Graphics g,  SizeF availableSize)
        {
            Control control = null;
            if (Property.Control != null)
                control = Property.Control;
            else if (EditingControl != null)
                control = EditingControl;

            if (control == null)
                return m_defaultSize;

            return new SizeF(
                Math.Max(m_defaultSize.Width, control.Width),
                Math.Max(m_defaultSize.Height, control.Height + 1 + (Property.NameHasWholeRow ? m_defaultSize.Height : 0)));
        }


        /// <summary>
        /// Implement the display of the value's representation.</summary>
        /// <param name="g">The Graphics object</param>
        /// <param name="area">Rectangle delimiting area to paint</param>
        public override void PaintValue(Graphics g, Rectangle area)
        {
            
        }

        /// <summary>
        /// Determines the editing mode from input position.</summary>
        /// <param name="p">Input position point</param>
        public override void SetEditingMode(Point p)
        {
            if (Bounds.Contains(p))
                EditingMode = EditMode.ByPropertyEditor;
        }

        /// <summary>
        /// Begins an edit operation.</summary>
        public override void BeginDataEdit()
        {
        }

        /// <summary>
        /// Ends an edit operation.</summary>
        /// <returns>
        /// 'true' if the change should be committed and 'false' if the change should be discarded</returns>
        public override bool EndDataEdit()
        {
            return true;
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.</summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (Property.Context == null)
                return "None";

            return Property.Context.GetValue().ToString();
        }

        /// <summary>
        /// Parses the specified string representation and sets the data value.</summary>
        /// <param name="s">String to parse</param>
        public override void Parse(string s)
        {
            if (Property.Context == null)
                return;

            Property.Context.SetValue(s);
        }

    }
}