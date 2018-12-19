//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to a layer folder. A layer folder allows a hierarchy of layers to be defined. 
    /// Folders can be hidden by the user to allow layering.</summary>
    class ScriptLayerFolder : Sce.Atf.Controls.Adaptable.Graphs.LayerFolder
    {
        /// <summary>
        /// Gets name attribute for layer folder</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return layerFolderType.nameAttribute; }
        }

        /// <summary>
        /// Gets ChildInfo for folders in layer folder</summary>
        protected override ChildInfo LayerFolderChild
        {
            get { return layerFolderType.layerFolderChild; }
        }

        /// <summary>
        /// Gets ChildInfo for references to layers in layer folder</summary>
        protected override ChildInfo ElementRefChildInfo
        {
            get { return layerFolderType.moduleRefChild; }
        }
    }
}
