//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to a pin in a circuit; used in mastering and as a base
    /// class for GroupPin</summary>
    public class ScriptNodeSocket : Sce.Atf.Controls.Adaptable.Graphs.Pin
    {
        /// <summary>
        /// Gets type attribute of Pin</summary>
        protected override AttributeInfo TypeAttribute
        {
            get { return socketType.typeAttribute; }
        }

        /// <summary>
        /// Gets name attribute of Pin</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return socketType.nameAttribute; }
        }

        protected override AttributeInfo IndexAttribute
        {
            get { return null; }
        }
    }
}
