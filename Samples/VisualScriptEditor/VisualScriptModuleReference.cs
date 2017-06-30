//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
using System;

using Sce.Atf;
using Sce.Atf.Adaptation;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapter for a reference instance of a module template</summary>
    public class VisualScriptModuleReference : ElementReference, IReference<VisualScriptModule>
    {
        protected override AttributeInfo GuidRefAttribute
        {
            get { return VisualScriptBasicSchema.moduleTemplateRefType.guidRefAttribute; }
        }

        #region IReference<Module>  memebers

        bool IReference<VisualScriptModule>.CanReference(VisualScriptModule item)
        {
            return true;
        }

        VisualScriptModule IReference<VisualScriptModule>.Target
        {
            get { return Template.Target.As<VisualScriptModule>(); }
            set
            {
                throw new InvalidOperationException("The group template determines the target");
            }
        }

        #endregion

        /// <summary>
        /// Gets the name attribute for module instance</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.nameAttribute; }
        }

        /// <summary>
        /// Gets the label attribute for module instance</summary>
        protected override AttributeInfo LabelAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.labelAttribute; }
        }

        /// <summary>
        /// Gets the x-coordinate position attribute for module instance</summary>
        protected override AttributeInfo XAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.xAttribute; }
        }

        /// <summary>
        /// Gets the y-coordinate position attribute for module instance</summary>
        protected override AttributeInfo YAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.yAttribute; }
        }

        /// <summary>
        /// Gets the visible attribute for module instance</summary>
        protected override AttributeInfo VisibleAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.visibleAttribute; }
        }
    }
}
