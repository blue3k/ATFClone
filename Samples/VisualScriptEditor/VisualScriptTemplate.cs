//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;

using Sce.Atf.Adaptation;
using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to a template, which is is a module that can be referenced into a circuit</summary>
    public class VisualScriptTemplate : Sce.Atf.Dom.Template
    {
        /// <summary>
        /// Gets and sets the user-visible name of the template</summary>
        public override string Name
        {
            get { return (string)DomNode.GetAttribute(VisualScriptBasicSchema.templateType.labelAttribute); }
            set { DomNode.SetAttribute(VisualScriptBasicSchema.templateType.labelAttribute, value); }
        }

        /// <summary>
        /// Gets or sets DomNode module that represents the template</summary>
        public override DomNode Target
        {
            get { return GetChild<DomNode>(VisualScriptBasicSchema.templateType.moduleChild); }
            set
            {
                SetChild(VisualScriptBasicSchema.templateType.moduleChild, value);
                if (value != null) // initialize  model name
                {
                    var module = Target.Cast<VisualScriptModule>();
                    Name = module.Name;
                    if (string.IsNullOrEmpty(Name))
                        Name = module.ElementType.Name;

                }
            }
        }

        /// <summary>Gets and sets  a globally unique identifier (GUID) that represents this template</summary>
        public override Guid Guid
        {
            get { return new Guid((string)DomNode.GetAttribute(VisualScriptBasicSchema.templateType.guidAttribute)); }
            set { DomNode.SetAttribute(VisualScriptBasicSchema.templateType.guidAttribute, value.ToString()); }
        }

        /// <summary>
        /// Returns true iff the template can reference the specified target item</summary>
        public override bool CanReference(DomNode item)
        {
            return item.Is<VisualScriptModule>();
        }
    }
}
