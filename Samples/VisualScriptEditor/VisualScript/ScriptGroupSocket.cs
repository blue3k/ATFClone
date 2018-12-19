//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;


namespace VisualScript
{
    /// <summary>
    /// Adapter for a pin on a group module, with extra information needed to associate the pin
    /// on the group with the internal module where it was connected before grouping.
    /// A group pin is a pin on a grouped sub-circuit; it extends the information
    /// of a pin to preserve the internal pin/module which is connected to the outside circuit.</summary>
    public class ScriptGroupSocket : Sce.Atf.Controls.Adaptable.Graphs.GroupPin, ICircuitGroupPin<ScriptNode>
    {
        // for bases class Pin
        /// <summary>
        /// Gets type attribute for group pin</summary>
        protected override AttributeInfo TypeAttribute
        {
            get { return socketType.typeAttribute; }
        }

        /// <summary>
        /// Gets name attribute for group pin</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return socketType.nameAttribute; }
        }

        /// <summary>
        /// Gets index (pin order in its sub-graph owner) attribute for group pin</summary>
        protected override AttributeInfo IndexAttribute
        {
            get { return groupSocketType.indexAttribute; }
        }

        /// <summary>
        /// Gets floating y-coordinate attribute for group pin. 
        /// Floating pin location y value is user defined (x value is auto-generated).</summary>
        protected override AttributeInfo PinYAttribute
        {
            get { return groupSocketType.pinYAttribute; }
        }

        /// <summary>
        /// Gets module (associated internal subelement) attribute for group pin</summary>
        protected override AttributeInfo ElementAttribute
        {
            get { return groupSocketType.moduleAttribute; }
        }

        /// <summary>
        /// Gets pin (associated internal subpin) attribute for group pin</summary>
        protected override AttributeInfo PinAttribute
        {
            get { return groupSocketType.pinAttribute; }
        }

        /// <summary>
        /// Gets pinned attribute for group pin</summary>
        protected override AttributeInfo PinnedAttribute
        {
            get { return groupSocketType.pinnedAttribute; }
        }

        /// <summary>
        /// Gets visible attribute for group pin</summary>
        protected override AttributeInfo VisibleAttribute
        {
            get { return groupSocketType.visibleAttribute; }
        }

        #region ICircuitGroupPin members (needed for drawing)

        /// <summary>
        /// Gets the internal module corresponding to this group pin</summary>
        ScriptNode ICircuitGroupPin<ScriptNode>.InternalElement
        {
            get { return GetReference<ScriptNode>(groupSocketType.moduleAttribute); }
        
        }    

        #endregion

       
    }
}
