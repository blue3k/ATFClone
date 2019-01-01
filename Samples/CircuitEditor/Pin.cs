//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;

namespace CircuitEditorSample
{
    /// <summary>
    /// Adapts DomNode to a pin in a circuit; used in mastering and as a base
    /// class for GroupPin</summary>
    class Pin : Sce.Atf.Controls.Adaptable.Graphs.Pin
    {
        /// <summary>
        /// Gets type attribute of Pin</summary>
        protected override AttributeInfo TypeNameAttribute => Schema.pinType.typeAttribute;

        /// <summary>
        /// Gets name attribute of Pin</summary>
        protected override AttributeInfo NameAttribute => Schema.pinType.nameAttribute;

        /// <summary>
        /// Pint Type, is this input pin?
        /// </summary>
        protected override AttributeInfo IsInputAttribute => Schema.pinType.isInputAttribute;


        protected override AttributeInfo IndexAttribute
        {
            get { return null; }
        }
    }
}
