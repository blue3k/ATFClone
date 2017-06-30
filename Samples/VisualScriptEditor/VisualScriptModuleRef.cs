//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapter for a module reference, used within layer folders to represent
    /// circuit modules that belong to that layer.
    /// </summary>
    public class VisualScriptModuleRef : ElementRef
    {
        /// <summary>
        /// Gets the AttributeInfo for a module reference</summary>
        protected override AttributeInfo RefAttribute
        {
            get { return VisualScriptBasicSchema.moduleRefType.refAttribute; }
        }
    }
}
