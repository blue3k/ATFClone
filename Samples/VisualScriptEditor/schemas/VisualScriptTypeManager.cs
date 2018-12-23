//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Xml.Schema;

using Sce.Atf;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using PropertyDescriptor = System.ComponentModel.PropertyDescriptor;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;

using VisualScript.VisualScriptBasicSchema;
using System.Xml;

namespace VisualScript
{
    /// <summary>
    /// Loads the UI schema, registers data extensions on the DOM types and annotates
    /// the types with display information and PropertyDescriptors</summary>
    [Export(typeof(VisualScriptTypeManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class VisualScriptTypeManager
    {
        public string NameSpaceName = "VScript";

        /// <summary>Constructor that initialize the schema</summary>
        /// [ImportingConstructor]
        public VisualScriptTypeManager()
        {
            var nameSpaces = new XmlQualifiedName[] {
                    new XmlQualifiedName(NameSpaceName, "http://www.blue3k.com/")
                };

            m_typeCollection = new XmlSchemaTypeCollection(
                nameSpaces,
                NameSpaceName,
                new DomNodeTypeCollection()
            );
            m_version = new Version("1.0");// string.IsNullOrEmpty(version) ? new Version("1.0") : new Version(version);

            m_ChildInfoOfTheRoot = new ChildInfo("VScript", visualScriptDocumentType.Type);

            Initialize();
        }

        public XmlSchemaTypeCollection TypeCollection => m_typeCollection;

        public DomNodeTypeCollection DomNodeTypeCollection => m_typeCollection.DomNodeTypeCollection;

        public ChildInfo ChildInfoOfTheRoot => m_ChildInfoOfTheRoot;

        /// <summary>
        /// Gets the schema version</summary>
        public Version Version
        {
            get { return m_version; }
        }
        private Version m_version;

        /// <summary>
        /// Method called after the schema set has been loaded and the DomNodeTypes have been created, but
        /// before the DomNodeTypes have been frozen. This means that DomNodeType.SetIdAttribute, for example, has
        /// not been called on the DomNodeTypes. Is called shortly before OnDomNodeTypesFrozen.
        /// Defines DOM adapters for types. Create PropertyDescriptors for types to use in property editors.</summary>
        protected void Initialize()
        {
            RegisterTypes();

            RegisterExtensions();

            // types are initialized, register property descriptors on module, folder types
            moduleType.Type.SetTag(
                new PropertyDescriptorCollection(
                    new PropertyDescriptor[] {
                        new AttributePropertyDescriptor(
                            "Name".Localize(),
                            moduleType.labelAttribute, // 'nameAttribute' is unique id, label is user visible name
                            null,
                            "Module name".Localize(),
                            false),
                        new AttributePropertyDescriptor(
                            "ID".Localize(),
                            moduleType.nameAttribute, // 'nameAttribute' is unique id, label is user visible name
                            null,
                            "Unique ID".Localize(),
                            true),
                        new AttributePropertyDescriptor(
                            "X".Localize(),
                            moduleType.xAttribute, 
                            null,
                            "location x".Localize(),
                            false),
                        new AttributePropertyDescriptor(
                            "Y".Localize(),
                            moduleType.yAttribute, 
                            null,
                            "location Y".Localize(),
                            false),

                }));

            layerFolderType.Type.SetTag(
                new PropertyDescriptorCollection(
                    new PropertyDescriptor[] {
                        new AttributePropertyDescriptor(
                            "Name".Localize(),
                            layerFolderType.nameAttribute,
                            null,
                            "Layer name".Localize(),
                            false)
                }));

            prototypeFolderType.Type.SetTag(
                new PropertyDescriptorCollection(
                    new PropertyDescriptor[] {
                        new AttributePropertyDescriptor(
                            "Name".Localize(),
                            prototypeFolderType.nameAttribute,
                            null,
                            "Prototype folder name".Localize(),
                            false)
                }));           
        } 

        void RegisterType(DomNodeType nodeType)
        {
            var xmlName = string.Format("{0}:{1}", NameSpaceName, nodeType.Name);
            DomNodeTypeCollection.AddNodeType(xmlName, nodeType);
        }

        void RegisterTypes()
        {
            RegisterType(visualScriptDocumentType.Type);
            RegisterType(visualScriptType.Type);
            RegisterType(moduleType.Type);
            RegisterType(groupType.Type);
            RegisterType(connectionType.Type);
            RegisterType(expressionType.Type);
            RegisterType(socketType.Type);
            RegisterType(groupSocketType.Type);
            RegisterType(prototypeFolderType.Type);
            RegisterType(prototypeType.Type);
            RegisterType(layerFolderType.Type);
            RegisterType(moduleRefType.Type);
            RegisterType(annotationType.Type);
            RegisterType(visualScriptType.Type);
            RegisterType(templateFolderType.Type);
            RegisterType(templateType.Type);
            RegisterType(moduleTemplateRefType.Type);
            RegisterType(groupTemplateRefType.Type);
            RegisterType(missingModuleType.Type);
            RegisterType(moduleTemplateRefType.Type);
            RegisterType(moduleTemplateRefType.Type);
            RegisterType(moduleTemplateRefType.Type);
        }

        private void RegisterExtensions()
        {
            // register extensions

            // decorate circuit document type
            visualScriptDocumentType.Type.Define(new ExtensionInfo<ScriptDocument>());                  // document info
            //circuitDocumentType.Type.Define(new ExtensionInfo<SampleCircuitEditingContext>());                  // document info
            visualScriptDocumentType.Type.Define(new ExtensionInfo<MultipleHistoryContext>());    // ties sub-context histories into document dirty bit
            visualScriptDocumentType.Type.Define(new ExtensionInfo<VisualScriptEditor.PrototypingContext>());        // document-wide prototype hierarchy
            visualScriptDocumentType.Type.Define(new ExtensionInfo<VisualScriptEditor.TemplatingContext>());         // document-wide template hierarchy
            //circuitDocumentType.Type.Define(new ExtensionInfo<UniqueIdValidator>());         // ensures all ids are unique throughout document
            visualScriptDocumentType.Type.Define(new ExtensionInfo<Sce.Atf.Dom.CategoryUniqueIdValidator>());   // ensures all ids are local unique in its category
            visualScriptDocumentType.Type.Define(new ExtensionInfo<ScriptValidator>());          // validate group hierarchy
            // ReferenceValidator should be the last validator attached to the root DomNode to fully track
            // all the DOM editings of all other validators to update references properly 
            visualScriptDocumentType.Type.Define(new ExtensionInfo<ReferenceValidator>());        // tracks references and targets


            // decorate circuit type
            visualScriptType.Type.Define(new ExtensionInfo<GlobalHistoryContext>());
            visualScriptType.Type.Define(new ExtensionInfo<ViewingContext>());                    // manages module and circuit bounds, efficient layout
            visualScriptType.Type.Define(new ExtensionInfo<VisualScriptEditor.LayeringContext>());                   // circuit layer hierarchy
            visualScriptType.Type.Define(new ExtensionInfo<PrintableDocument>());                 // printing
            visualScriptType.Type.Define(new ExtensionInfo<ExpressionManager>());                 // printing


            // decorate group type
            groupType.Type.Define(new ExtensionInfo<VisualScriptEditor.VisualScriptEditingContext>());                    // main editable circuit adapter
            groupType.Type.Define(new ExtensionInfo<ScriptGroup>());
            groupType.Type.Define(new ExtensionInfo<ViewingContext>());

            connectionType.Type.Define(new ExtensionInfo<WireStyleProvider<ScriptNode, ScriptNodeConnection, ICircuitPin>>());

            // register Expression.
            expressionType.Type.Define(new ExtensionInfo<Expression>());

            // adapts the default implementation  of circuit types
            moduleType.Type.Define(new ExtensionInfo<ScriptNode>());
            moduleType.Type.Define(new ExtensionInfo<ScriptNodeProperties>());
            connectionType.Type.Define(new ExtensionInfo<ScriptNodeConnection>());
            socketType.Type.Define(new ExtensionInfo<ScriptNodeSocket>());
            groupSocketType.Type.Define(new ExtensionInfo<ScriptGroupSocket>());
            prototypeFolderType.Type.Define(new ExtensionInfo<ScriptPrototypeFolder>());
            prototypeType.Type.Define(new ExtensionInfo<ScriptPrototype>());
            layerFolderType.Type.Define(new ExtensionInfo<ScriptLayerFolder>());
            moduleRefType.Type.Define(new ExtensionInfo<ScriptNodeRef>());
            annotationType.Type.Define(new ExtensionInfo<ScriptAnnotation>());
            visualScriptType.Type.Define(new ExtensionInfo<VisualScript>());
            visualScriptType.Type.Define(new ExtensionInfo<VisualScriptEditor.VisualScriptEditingContext>()); // main editable circuit adapter

            templateFolderType.Type.Define(new ExtensionInfo<ScriptTemplateFolder>());
            templateType.Type.Define(new ExtensionInfo<ScriptTemplate>());
            moduleTemplateRefType.Type.Define(new ExtensionInfo<ScriptNodeReference>());
            groupTemplateRefType.Type.Define(new ExtensionInfo<ScriptGroupReference>());
            missingModuleType.Type.Define(new ExtensionInfo<MissingScriptNode >());


            // set document editor information(DocumentClientInfo) for visual script editor editor:
            visualScriptDocumentType.Type.SetTag(VisualScriptEditor.VisualScriptEditor.EditorInfo);
        }


        private XmlSchemaTypeCollection m_typeCollection;
        private ChildInfo m_ChildInfoOfTheRoot;
    }
}
