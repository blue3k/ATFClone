//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Xml.Schema;

using Sce.Atf;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using PropertyDescriptor = System.ComponentModel.PropertyDescriptor;



namespace VisualScript
{
    /// <summary>
    /// Loads the UI schema, registers data extensions on the DOM types and annotates
    /// the types with display information and PropertyDescriptors</summary>
    [Export(typeof(BasicSchemaLoader))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BasicSchemaLoader : XmlSchemaTypeLoader
    {
        /// <summary>
        /// Constructor that loads the schema</summary>
        public BasicSchemaLoader()
        {
            // set resolver to locate embedded .xsd file
            SchemaResolver = new ResourceStreamResolver(System.Reflection.Assembly.GetExecutingAssembly(), "VisualScriptEditor/schemas");
            var myAssembly = System.Reflection.Assembly.GetAssembly(GetType());
            var schema = Load(myAssembly, "VisualScriptBasicSchema.xsd");
            var version = schema.Version; // Version will be null if the xsd has no version attribute
            m_version = string.IsNullOrEmpty(version) ? new Version("1.0") : new Version(version);

        }

        /// <summary>
        /// Gets the schema namespace</summary>
        public string NameSpace
        {
            get { return m_namespace; }
        }
        private string m_namespace;

        /// <summary>
        /// Gets the schema version</summary>
        public Version Version
        {
            get { return m_version; }
        }
        private Version m_version;

        /// <summary>
        /// Gets the schema type collection</summary>
        public XmlSchemaTypeCollection TypeCollection
        {
            get { return m_typeCollection; }
        }
        private XmlSchemaTypeCollection m_typeCollection;

        /// <summary>
        /// Method called after the schema set has been loaded and the DomNodeTypes have been created, but
        /// before the DomNodeTypes have been frozen. This means that DomNodeType.SetIdAttribute, for example, has
        /// not been called on the DomNodeTypes. Is called shortly before OnDomNodeTypesFrozen.
        /// Defines DOM adapters for types. Create PropertyDescriptors for types to use in property editors.</summary>
        /// <param name="schemaSet">XML schema sets being loaded</param>
        protected override void OnSchemaSetLoaded(XmlSchemaSet schemaSet)
        {
            foreach (XmlSchemaTypeCollection typeCollection in GetTypeCollections())
            {
                m_namespace = typeCollection.TargetNamespace;
                m_typeCollection = typeCollection;
                VisualScriptBasicSchema.Initialize(typeCollection);

                // register extensions

                // decorate circuit document type
                VisualScriptBasicSchema.visualScriptDocumentType.Type.Define(new ExtensionInfo<ScriptDocument>());                  // document info
                //VisualScriptBasicSchema.circuitDocumentType.Type.Define(new ExtensionInfo<SampleCircuitEditingContext>());                  // document info
                VisualScriptBasicSchema.visualScriptDocumentType.Type.Define(new ExtensionInfo<MultipleHistoryContext>());    // ties sub-context histories into document dirty bit
                VisualScriptBasicSchema.visualScriptDocumentType.Type.Define(new ExtensionInfo<VisualScriptEditor.PrototypingContext >());        // document-wide prototype hierarchy
                VisualScriptBasicSchema.visualScriptDocumentType.Type.Define(new ExtensionInfo<VisualScriptEditor.TemplatingContext>());         // document-wide template hierarchy
                //VisualScriptBasicSchema.circuitDocumentType.Type.Define(new ExtensionInfo<UniqueIdValidator>());         // ensures all ids are unique throughout document
                VisualScriptBasicSchema.visualScriptDocumentType.Type.Define(new ExtensionInfo<Sce.Atf.Dom.CategoryUniqueIdValidator>());   // ensures all ids are local unique in its category
                VisualScriptBasicSchema.visualScriptDocumentType.Type.Define(new ExtensionInfo<ScriptValidator>());          // validate group hierarchy
                // ReferenceValidator should be the last validator attached to the root DomNode to fully track
                // all the DOM editings of all other validators to update references properly 
                VisualScriptBasicSchema.visualScriptDocumentType.Type.Define(new ExtensionInfo<ReferenceValidator>());        // tracks references and targets
                

                // decorate circuit type
                VisualScriptBasicSchema.visualScriptType.Type.Define(new ExtensionInfo<GlobalHistoryContext>());
                VisualScriptBasicSchema.visualScriptType.Type.Define(new ExtensionInfo<ViewingContext>());                    // manages module and circuit bounds, efficient layout
                VisualScriptBasicSchema.visualScriptType.Type.Define(new ExtensionInfo<VisualScriptEditor.LayeringContext>());                   // circuit layer hierarchy
                VisualScriptBasicSchema.visualScriptType.Type.Define(new ExtensionInfo<PrintableDocument>());                 // printing
                VisualScriptBasicSchema.visualScriptType.Type.Define(new ExtensionInfo<ExpressionManager>());                 // printing


                // decorate group type
                VisualScriptBasicSchema.groupType.Type.Define(new ExtensionInfo<VisualScriptEditor.VisualScriptEditingContext>());                    // main editable circuit adapter
                VisualScriptBasicSchema.groupType.Type.Define(new ExtensionInfo<ScriptGroup>());
                VisualScriptBasicSchema.groupType.Type.Define(new ExtensionInfo<ViewingContext>());

                VisualScriptBasicSchema.connectionType.Type.Define(new ExtensionInfo<WireStyleProvider<ScriptNode, ScriptNodeConnection, ICircuitPin>>());

                // register Expression.
                VisualScriptBasicSchema.expressionType.Type.Define(new ExtensionInfo<Expression>());

                RegisterCircuitExtensions();

                // types are initialized, register property descriptors on module, folder types

                VisualScriptBasicSchema.moduleType.Type.SetTag(
                    new PropertyDescriptorCollection(
                        new PropertyDescriptor[] {
                            new AttributePropertyDescriptor(
                                "Name".Localize(),
                                VisualScriptBasicSchema.moduleType.labelAttribute, // 'nameAttribute' is unique id, label is user visible name
                                null,
                                "Module name".Localize(),
                                false),
                            new AttributePropertyDescriptor(
                                "ID".Localize(),
                                VisualScriptBasicSchema.moduleType.nameAttribute, // 'nameAttribute' is unique id, label is user visible name
                                null,
                                "Unique ID".Localize(),
                                true),
                            new AttributePropertyDescriptor(
                                "X".Localize(),
                                VisualScriptBasicSchema.moduleType.xAttribute, 
                                null,
                                "location x".Localize(),
                                false),
                            new AttributePropertyDescriptor(
                                "Y".Localize(),
                                VisualScriptBasicSchema.moduleType.yAttribute, 
                                null,
                                "location Y".Localize(),
                                false),

                    }));

                VisualScriptBasicSchema.layerFolderType.Type.SetTag(
                    new PropertyDescriptorCollection(
                        new PropertyDescriptor[] {
                            new AttributePropertyDescriptor(
                                "Name".Localize(),
                                VisualScriptBasicSchema.layerFolderType.nameAttribute,
                                null,
                                "Layer name".Localize(),
                                false)
                    }));

                VisualScriptBasicSchema.prototypeFolderType.Type.SetTag(
                    new PropertyDescriptorCollection(
                        new PropertyDescriptor[] {
                            new AttributePropertyDescriptor(
                                "Name".Localize(),
                                VisualScriptBasicSchema.prototypeFolderType.nameAttribute,
                                null,
                                "Prototype folder name".Localize(),
                                false)
                    }));           

                // the circuit schema defines only one type collection
                break;
            }
        } 

        static private void RegisterCircuitExtensions()
        {         
            // adapts the default implementation  of circuit types
            VisualScriptBasicSchema.moduleType.Type.Define(new ExtensionInfo<ScriptNode>());
            VisualScriptBasicSchema.moduleType.Type.Define(new ExtensionInfo<ScriptNodeProperties>());
            VisualScriptBasicSchema.connectionType.Type.Define(new ExtensionInfo<ScriptNodeConnection>());
            VisualScriptBasicSchema.socketType.Type.Define(new ExtensionInfo<ScriptNodeSocket>());
            VisualScriptBasicSchema.groupSocketType.Type.Define(new ExtensionInfo<ScriptGroupSocket>());
            VisualScriptBasicSchema.visualScriptType.Type.Define(new ExtensionInfo<VisualScript>());
            VisualScriptBasicSchema.prototypeFolderType.Type.Define(new ExtensionInfo<ScriptPrototypeFolder>());
            VisualScriptBasicSchema.prototypeType.Type.Define(new ExtensionInfo<ScriptPrototype>());
            VisualScriptBasicSchema.layerFolderType.Type.Define(new ExtensionInfo<ScriptLayerFolder>());
            VisualScriptBasicSchema.moduleRefType.Type.Define(new ExtensionInfo<ScriptNodeRef>());
            VisualScriptBasicSchema.annotationType.Type.Define(new ExtensionInfo<ScriptAnnotation>());
            VisualScriptBasicSchema.visualScriptType.Type.Define(new ExtensionInfo<VisualScriptEditor.VisualScriptEditingContext>()); // main editable circuit adapter

            VisualScriptBasicSchema.templateFolderType.Type.Define(new ExtensionInfo<ScriptTemplateFolder>());
            VisualScriptBasicSchema.templateType.Type.Define(new ExtensionInfo<ScriptTemplate>());
            VisualScriptBasicSchema.moduleTemplateRefType.Type.Define(new ExtensionInfo<ScriptNodeReference>());
            VisualScriptBasicSchema.groupTemplateRefType.Type.Define(new ExtensionInfo<ScriptGroupReference>());
            VisualScriptBasicSchema.missingModuleType.Type.Define(new ExtensionInfo<MissingScriptNode >());


            // set document editor information(DocumentClientInfo) for circuit editor:
            VisualScriptBasicSchema.visualScriptDocumentType.Type.SetTag(VisualScriptEditor.VisualScriptEditor.EditorInfo);
        }
    }
}
