//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.Drawing;
using Sce.Atf.Dom;

namespace Sce.Atf.Controls.Adaptable.Graphs
{
    /// <summary>
    /// Adapts DomNode to sub-circuit, which is created when mastering</summary>
    [Obsolete("Circuit groups and circuit templates have replaced mastered circuits")]
    public abstract class SubCircuit : Circuit, ICircuitElementType
    {
        /// <summary>
        /// Gets name attribute for subcircuit</summary>
        protected abstract AttributeInfo NameAttribute { get; }

        /// <summary>
        /// Gets name attribute for subcircuit</summary>
        protected abstract AttributeInfo LabelAttribute { get; }

        // required  child info
        /// <summary>
        /// Gets ChildInfo for input pins in subcircuit</summary>
        protected abstract ChildInfo InputChildInfo { get; }
        /// <summary>
        /// Gets ChildInfo for output pins in subcircuit</summary>
        protected abstract ChildInfo OutputChildInfo { get; }

  
        /// <summary>
        /// Performs initialization when the adapter is connected to the sub-circuit's DomNode.
        /// Raises the DomNodeAdapter NodeSet event and performs custom processing.</summary>
        protected override void OnNodeSet()
        {
            m_inputPins = new PinList<ICircuitPin>(new DomNodeListAdapter<ICircuitPin>(DomNode, InputChildInfo));
            m_outputPins = new PinList<ICircuitPin>(new DomNodeListAdapter<ICircuitPin>(DomNode, OutputChildInfo));

            base.OnNodeSet();
        }

        #region StandardCircuitRenderer.IElementType Members

        /// <summary>
        /// Gets the sub-circuit type name</summary>
        public string TitleText
        {
            get { return GetAttribute<string>(NameAttribute); }
            set { SetAttribute(NameAttribute, value); }
        }

        /// <summary>
        /// Gets the element display name</summary>
        public string BottomDisplayName
        {
            get { return GetAttribute<string>(LabelAttribute); }
        }

        /// <summary>
        /// Gets desired size of sub-circuit interior, in pixels</summary>
        public Size InteriorSize
        {
            get { return Size.Empty; }
        }

        /// <summary>
        /// Gets image for this sub-circuit, to be displayed in interior</summary>
        public Image Image
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the list of input pins for this element type; the list is considered
        /// to be read-only</summary>
        public PinList<ICircuitPin> Inputs
        {
            get { return m_inputPins; }
        }

        /// <summary>
        /// Gets the list of output pins for this element type; the list is considered
        /// to be read-only</summary>
        public PinList<ICircuitPin> Outputs
        {
            get { return m_outputPins; }
        }

        #endregion

 

        private PinList<ICircuitPin> m_inputPins;
        private PinList<ICircuitPin> m_outputPins;
    }
}
