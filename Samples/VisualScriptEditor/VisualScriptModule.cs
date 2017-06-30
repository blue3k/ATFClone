﻿//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to circuit modules, which is the base circuit element with pins.
    /// It maintains local name and bounds for faster
    /// circuit rendering during editing operations, like dragging modules and wires.</summary>
    public class VisualScriptModule : Element
    {
        /// <summary>
        /// Gets name attribute for module</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.nameAttribute; }
        }

        /// <summary>
        /// Gets label attribute on module</summary>
        protected override AttributeInfo LabelAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.labelAttribute; }
        }

        /// <summary>
        /// Gets x-coordinate position attribute for module</summary>
        protected override AttributeInfo XAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.xAttribute; }
        }

        /// <summary>
        /// Gets y-coordinate position attribute for module</summary>
        protected override AttributeInfo YAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.yAttribute; }
        }

        /// <summary>
        /// Gets visible attribute for module</summary>
        protected override AttributeInfo VisibleAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.visibleAttribute; }
        }

        /// <summary>
        /// Gets the optional AttributeInfo for the original GUID of template 
        /// if this module is a copy-instance of a template(and nothing else) </summary>
        protected override AttributeInfo SourceGuidAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.sourceGuidAttribute; }
        }

        /// <summary>
        /// Gets the optional AttributeInfo for storing whether or not unconnected
        /// pins should be displayed.</summary>
        protected override AttributeInfo ShowUnconnectedPinsAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.showUnconnectedPinsAttribute; }
        }

        protected override CircuitElementInfo CreateElementInfo()
        {
            return new VisualScriptModuleElementInfo();
        }
    }
}
