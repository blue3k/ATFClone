//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;

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
            get { return VisualScriptBasicSchema.prototypeFolderType.nameAttribute; }
        }

        /// <summary>
        /// Gets ChildInfo for prototypes in prototype folder</summary>
        protected override ChildInfo PrototypeChildInfo
        {
            get { return VisualScriptBasicSchema.prototypeFolderType.prototypeChild; }
        }

        /// <summary>
        /// Gets ChildInfo for prototype folders in prototype folder</summary>
        protected override ChildInfo PrototypeFolderChildInfo
        {
            get { return VisualScriptBasicSchema.prototypeFolderType.prototypeFolderChild; }
        }
    }
}
