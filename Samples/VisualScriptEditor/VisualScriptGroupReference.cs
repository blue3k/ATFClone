//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using Sce.Atf;
using Sce.Atf.Adaptation;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;

namespace VisualScript
{
    public class VisualScriptGroupReference : Sce.Atf.Controls.Adaptable.Graphs.GroupReference,
         ICircuitGroupType<VisualScriptModule, VisualScriptConnection, ICircuitPin>, // for circuit render
         IReference<VisualScriptModule>
    {
        #region Fill required AttributeInfos 
        /// <summary>
        /// Gets name attribute for group instance</summary>
        protected override AttributeInfo NameAttribute
        {
            get { return VisualScriptBasicSchema.groupType.nameAttribute; }
        }

        /// <summary>
        /// Gets label attribute for group instance</summary>
        protected override AttributeInfo LabelAttribute
        {
            get { return VisualScriptBasicSchema.moduleType.labelAttribute; }
        }

        /// <summary>
        /// Gets x-coordinate position attribute for group instance</summary>
        protected override AttributeInfo XAttribute
        {
            get { return VisualScriptBasicSchema.groupType.xAttribute; }
        }

        /// <summary>
        /// Gets y-coordinate position attribute for group instance</summary>
        protected override AttributeInfo YAttribute
        {
            get { return VisualScriptBasicSchema.groupType.yAttribute; }
        }

        /// <summary>
        /// Gets visibility attribute for group instance</summary>
        protected override AttributeInfo VisibleAttribute
        {
            get { return VisualScriptBasicSchema.groupType.visibleAttribute; }
        }

        /// <summary>
        /// Gets minimum width (when the group is expanded) attribute for group</summary>
        protected override AttributeInfo MinWidthAttribute
        {
            get { return VisualScriptBasicSchema.groupType.minwidthAttribute; }
        }

        /// <summary>
        /// Gets minimum height (when the group is expanded) attribute for group</summary>
        protected override AttributeInfo MinHeightAttribute
        {
            get { return VisualScriptBasicSchema.groupType.minheightAttribute; }
        }

        /// <summary>
        /// Gets width (when the group is expanded) attribute for group</summary>
        protected override AttributeInfo WidthAttribute
        {
            get { return VisualScriptBasicSchema.groupType.widthAttribute; }
        }

        /// <summary>
        /// Gets height (when the group is expanded) attribute for group</summary>
        protected override AttributeInfo HeightAttribute
        {
            get { return VisualScriptBasicSchema.groupType.heightAttribute; }
        }

        /// <summary>
        /// Gets autosize attribute for group.
        /// When autosize is true, container size is computed.</summary>
        protected override AttributeInfo AutosizeAttribute
        {
            get { return VisualScriptBasicSchema.groupType.autosizeAttribute; }
        }

        protected override AttributeInfo ShowExpandedGroupPinsAttribute
        {
            get { return VisualScriptBasicSchema.groupTemplateRefType.refShowExpandedGroupPinsAttribute; }
        }

        /// <summary>
        /// Gets ChildInfo for Modules in group</summary>
        protected override ChildInfo ElementChildInfo
        {
            get { return VisualScriptBasicSchema.groupType.moduleChild; }
        }

        /// <summary>
        /// Gets ChildInfo for Wires in group</summary>
        protected override ChildInfo WireChildInfo
        {
            get { return VisualScriptBasicSchema.groupType.connectionChild; }
        }

        /// <summary>
        /// Gets ChildInfo for input group pins in group</summary>
        protected override ChildInfo InputChildInfo
        {
            get { return VisualScriptBasicSchema.groupType.inputChild; }
        }

        /// <summary>
        /// Gets ChildInfo for output group pins in group</summary>
        protected override ChildInfo OutputChildInfo
        {
            get { return VisualScriptBasicSchema.groupType.outputChild; }
        }

        /// <summary>
        /// Gets group pin type.
        /// A group pin is a pin on a grouped sub-circuit; it extends the information of a pin
        /// to preserve the internal pin/module which is connected to the outside circuit.</summary>
        protected override DomNodeType GroupPinType
        {
            get { return VisualScriptBasicSchema.groupPinType.Type; }
        }


        protected override AttributeInfo GuidRefAttribute
        {
            get { return VisualScriptBasicSchema.groupTemplateRefType.guidRefAttribute; }
        }

        #endregion

        #region ICircuitGroupType members

        IEnumerable<VisualScriptModule> IHierarchicalGraphNode<VisualScriptModule, VisualScriptConnection, ICircuitPin>.SubNodes
        {
            get
            {
                if (Group != null)
                    return Group.Elements.AsIEnumerable<VisualScriptModule>();
                return EmptyEnumerable<VisualScriptModule>.Instance;
            }
        }

        /// <summary>
        /// Gets the group's (subgraph's) internal edges</summary>
        IEnumerable<VisualScriptConnection> ICircuitGroupType<VisualScriptModule, VisualScriptConnection, ICircuitPin>.SubEdges
        {
            get
            {
                if (Group != null)
                    return Group.Wires.AsIEnumerable<VisualScriptConnection>();
                return EmptyEnumerable<VisualScriptConnection>.Instance;
            }
        }

        #endregion

        #region IReference<Module> members

        bool IReference<VisualScriptModule>.CanReference(VisualScriptModule item)
        {
            return item.Is<VisualScriptGroup>();
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

    }
}
