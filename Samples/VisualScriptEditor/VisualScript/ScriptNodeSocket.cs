//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to a pin in a circuit; used in mastering and as a base
    /// class for GroupPin</summary>
    class ScriptNodeSocket : Sce.Atf.Controls.Adaptable.Graphs.Pin
    {
        /// <summary>
        /// Gets type attribute of Pin</summary>
        protected override AttributeInfo TypeAttribute
        {
            get { return VisualScriptBasicSchema.socketType.typeAttribute; }
        }

        /// <summary>
        /// Gets name attribute of Pin</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return VisualScriptBasicSchema.socketType.nameAttribute; }
        }
    }
}
