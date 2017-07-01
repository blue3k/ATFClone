//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
using System.ComponentModel.Composition;

using Sce.Atf.Applications;
using Sce.Atf.Dom;
using VisualScript;

namespace VisualScriptEditor
{
    /// <summary>
    /// Component that defines circuit-specific commands for group and ungroup. Grouping takes
    /// modules and the connections between them and turns those into a single element that is equivalent.</summary>
    [Export(typeof(GroupingCommands))]
    public class GroupingCommands : Sce.Atf.Controls.Adaptable.Graphs.GroupingCommands
    {
        /// <summary>
        /// Constructor</summary>
        /// <param name="commandService">Command service</param>
        /// <param name="contextRegistry">Context registry</param>
        [ImportingConstructor]
        public GroupingCommands(ICommandService commandService, IContextRegistry contextRegistry) 
            : base(commandService, contextRegistry)           
        {
            CreationOptions = GroupCreationOptions.HideUnconnectedPins;
        }

        // provide required DomNodeType
        /// <summary>
        /// Gets type for Group</summary>
        protected override DomNodeType GroupType
        {
            get { return VisualScriptBasicSchema.groupType.Type; }
        }

        public override void DoCommand(object commandTag)
        {
            base.DoCommand(commandTag);

            // If the Create Group command was run, add in the default dynamic properties.
            if (commandTag is StandardCommand &&
                StandardCommand.EditGroup.Equals(commandTag))
            {
                var selectionContext = ContextRegistry.GetActiveContext<ISelectionContext>();
                var newGroup = selectionContext.GetLastSelected<ScriptGroup>();
                if (newGroup != null)
                {
                    // The "Emitter Vector" dynamic property.
                    var customAttrNode = new DomNode(VisualScriptBasicSchema.dynamicPropertyType.Type, VisualScriptBasicSchema.moduleType.dynamicPropertyChild);
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.nameAttribute,
                        "Emitter Vector");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.categoryAttribute,
                        "Custom Properties");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.descriptionAttribute,
                        "The speed and the direction that the emitter travels when it is created");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.converterAttribute,
                        "Sce.Atf.Controls.PropertyEditing.FloatArrayConverter, Atf.Gui");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.editorAttribute,
                        "Sce.Atf.Controls.PropertyEditing.NumericTupleEditor, Atf.Gui.WinForms:System.Single,x,y,z");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.valueTypeAttribute, "vector3Value");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.vector3ValueAttribute,
                        new [] {0.0f, 0.0f, 0.0f});
                    newGroup.DomNode.GetChildList(VisualScriptBasicSchema.moduleType.dynamicPropertyChild).Add(customAttrNode);

                    // The "Debug Mode" dynamic property.
                    customAttrNode = new DomNode(VisualScriptBasicSchema.dynamicPropertyType.Type, VisualScriptBasicSchema.moduleType.dynamicPropertyChild);
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.nameAttribute,
                        "Debug Mode");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.categoryAttribute,
                        "Custom Properties");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.descriptionAttribute,
                        "Whether or not debug mode visualizations should be used");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.editorAttribute,
                        "Sce.Atf.Controls.PropertyEditing.BoolEditor, Atf.Gui.WinForms");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.valueTypeAttribute, "boolValue");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.boolValueAttribute, false);
                    newGroup.DomNode.GetChildList(VisualScriptBasicSchema.moduleType.dynamicPropertyChild).Add(customAttrNode);

                    // The "Tester Name" dynamic property.
                    customAttrNode = new DomNode(VisualScriptBasicSchema.dynamicPropertyType.Type, VisualScriptBasicSchema.moduleType.dynamicPropertyChild);
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.nameAttribute,
                        "Tester Name");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.categoryAttribute,
                        "Custom Properties");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.descriptionAttribute,
                        "The name of the person who is testing this particle effect object");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.valueTypeAttribute, "stringValue");
                    newGroup.DomNode.GetChildList(VisualScriptBasicSchema.moduleType.dynamicPropertyChild).Add(customAttrNode);

                    // The "# of Emitters" dynamic property.
                    customAttrNode = new DomNode(VisualScriptBasicSchema.dynamicPropertyType.Type, VisualScriptBasicSchema.moduleType.dynamicPropertyChild);
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.nameAttribute,
                        "# of Emitters");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.categoryAttribute,
                        "Custom Properties");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.descriptionAttribute,
                        "The number of emitter objects spawned by this emitter");
                    customAttrNode.SetAttribute(VisualScriptBasicSchema.dynamicPropertyType.valueTypeAttribute, "intValue");
                    newGroup.DomNode.GetChildList(VisualScriptBasicSchema.moduleType.dynamicPropertyChild).Add(customAttrNode);
                }
            }
        }
    }
}
