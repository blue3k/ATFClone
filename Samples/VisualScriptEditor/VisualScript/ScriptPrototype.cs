//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to a circuit prototype, which contains modules and connections
    /// that can be instanced in a circuit</summary>
    class ScriptPrototype : Sce.Atf.Controls.Adaptable.Graphs.Prototype
    {
        /// <summary>
        /// Gets name attribute for prototype</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return VisualScriptBasicSchema.prototypeType.nameAttribute; }
        }

        /// <summary>
        /// Gets ChildInfo for modules in prototype</summary>
        protected override ChildInfo ElementChildInfo
        {
            get { return VisualScriptBasicSchema.prototypeType.moduleChild; }
        }

        /// <summary>
        /// Gets ChildInfo for connections (wires) in prototype</summary>
        protected override ChildInfo WireChildInfo
        {
            get { return VisualScriptBasicSchema.prototypeType.connectionChild; }
        }
    }
}
