//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Adaptation;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;


using VisualScript;
using VisualScript.VisualScriptBasicSchema;

namespace VisualScriptEditor
{
    /// <summary>
    /// Editing Context for templates library; this is the context that is bound to the TemplateLister 
    /// when a circuit document becomes the active context. </summary>
    /// <remarks>This context has its own independent Selection, 
    /// The ITreeView implementation controls the hierarchy in the TemplateLister's TreeControl.
    /// The IItemView implementation controls icons and labels in the TemplateLister's TreeControl.</remarks>
    public class TemplatingContext : Sce.Atf.Dom.TemplatingContext
    {
        /// <summary>
        /// Performs initialization when the adapter is connected to the circuit's DomNode:
        /// subscribe to DomNode tree change events</summary>
        protected override void OnNodeSet()
        {
            base.OnNodeSet();
            DomNode.ChildRemoved += DomNode_ChildRemoved;
        }

        /// <summary>
        /// Gets root template folder</summary>
        public override Sce.Atf.Dom.TemplateFolder RootFolder
        {
            get
            {
                if (m_rootFolder == null)
                {
                    m_rootFolder = GetChild<ScriptTemplateFolder>(visualScriptDocumentType.templateFolderChild);
                    if (m_rootFolder == null) // create one if no root folder is defined yet
                    {
                        var rootFolderNode = new DomNode(templateFolderType.Type);
                        rootFolderNode.Cast<ScriptTemplateFolder>().Name = "_TemplateRoot_";
                        DomNode.SetChild(visualScriptDocumentType.templateFolderChild, rootFolderNode);
                        m_rootFolder = rootFolderNode.Cast<ScriptTemplateFolder>();
                    }
                }
                return m_rootFolder;
            }
        }

        /// <summary>
        /// Gets type of template</summary>
        protected override DomNodeType TemplateType
        {
            get { return templateType.Type; }
        }

        /// <summary>
        /// Returns true iff the reference can reference the specified target item</summary>
        /// <param name="item">Template item to be referenced</param>
        /// <returns>True iff the reference can reference the specified target item</returns>
        public override bool CanReference(object item)
        {
            return item.Is<ScriptTemplate>() && item.Cast<ScriptTemplate>().Target.Is<ScriptNode>();
        }

        /// <summary>
        /// Creates a reference instance that references the specified target item</summary>
        /// <param name="item">Item to create reference for</param>
        public override object CreateReference(object item)
        {
            var template = item.Cast<ScriptTemplate>();
            if (template.Target.Is<VisualScript.ScriptGroup>())
            {
                var groupReference = new DomNode(groupTemplateRefType.Type).Cast<VisualScript.ScriptGroupReference>();
                groupReference.Template = template;
                groupReference.Id = template.Name;
                groupReference.Name = template.Name;
                groupReference.Group.SourceGuid = template.Guid;
                return groupReference;
            }
            if (template.Target.Is<ScriptNode>())
            {
                var moduleReference = new DomNode(moduleTemplateRefType.Type).Cast<ScriptNodeReference>();
                moduleReference.Template = template;
                moduleReference.Id = template.Name;
                moduleReference.Name = template.Name;
                moduleReference.Element.SourceGuid = template.Guid;
                return moduleReference;
            }
            return null;
        }

        private void DomNode_ChildRemoved(object sender, ChildEventArgs e)
        {
            // if a template is deleted, turn template references into copy-instances
            if (!IsMovingItems && e.Child.Is<ScriptTemplate>())
            {
                // we can use the ReferenceValidator which is attached to this (root) node to get all the references.
                // note reference validation will happen later at the end of the transaction to remove the dangling references
                var refValidator = this.As<ReferenceValidator>();
                DomNode target = e.Child.Cast<ScriptTemplate>().Target;

                foreach (var reference in refValidator.GetReferences(target))
                {
                    var targetCopies = DomNode.Copy(new[] { target }); // DOM deep copy
                    var copyInstance = targetCopies[0].Cast<Element>();

                    var templateInstance = reference.First.Cast<Element>();
                    copyInstance.Position = templateInstance.Position;
                    var circuitContainer = reference.First.Parent.Cast<ICircuitContainer>();
                    circuitContainer.Elements.Add(copyInstance);

                    // reroute original edges 
                    foreach (var wire in circuitContainer.Wires)
                    {
                        if (wire.InputElement == templateInstance)
                        {
                            wire.InputElement = copyInstance;
                            wire.InputPin = copyInstance.ElementType.GetInputPin(wire.InputPin.Index);
                            wire.SetPinTarget();
                        }
                        if (wire.OutputElement == templateInstance)
                        {
                            wire.OutputElement = copyInstance;
                            wire.OutputPin = copyInstance.ElementType.GetOutputPin(wire.OutputPin.Index);
                            wire.SetPinTarget();
                        }
                    }
                }
            }
        }

        private VisualScript.ScriptTemplateFolder m_rootFolder;
    }
}
