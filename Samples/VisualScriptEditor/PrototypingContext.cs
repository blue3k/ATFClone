//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
using Sce.Atf.Dom;

using VisualScript;

namespace VisualScriptEditor
{
    /// <summary>
    /// Editing context for prototypes in the circuit document; this is the context that is
    /// bound to the PrototypeLister when a circuit document becomes the active context. This
    /// context implements instancing differently than the CircuitContext. It can insert modules
    /// and connections, converting them into prototypes. It can only copy and delete prototypes.</summary>
    public class PrototypingContext : Sce.Atf.Controls.Adaptable.Graphs.PrototypingContext
    {
        /// <summary>
        /// Gets ChildInfo for prototype folders in prototype folder</summary>
        protected override ChildInfo PrototypeFolderChildInfo
        {
            get { return VisualScriptBasicSchema.visualScriptDocumentType.prototypeFolderChild; }
        }

        /// <summary>
        /// Gets type of prototype</summary>
        protected override DomNodeType PrototypeType
        {
            get { return VisualScriptBasicSchema.prototypeType.Type; }
        }
    }
}
