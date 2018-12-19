//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to prototype folders</summary>
    class ScriptPrototypeFolder : Sce.Atf.Controls.Adaptable.Graphs.PrototypeFolder
    {
        /// <summary>
        /// Gets name attribute for prototype folder</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return prototypeFolderType.nameAttribute; }
        }

        /// <summary>
        /// Gets ChildInfo for prototypes in prototype folder</summary>
        protected override ChildInfo PrototypeChildInfo
        {
            get { return prototypeFolderType.prototypeChild; }
        }

        /// <summary>
        /// Gets ChildInfo for prototype folders in prototype folder</summary>
        protected override ChildInfo PrototypeFolderChildInfo
        {
            get { return prototypeFolderType.prototypeFolderChild; }
        }
    }
}
