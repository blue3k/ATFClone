//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Xml;

using Sce.Atf;
using Sce.Atf.Applications;
using Sce.Atf.Controls.Adaptable;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;

using PropertyDescriptor = Sce.Atf.Dom.PropertyDescriptor;
using System.IO;
using Sce.Atf.Controls.PropertyEditing;
using System;
using Sce.Atf.Adaptation;

namespace VisualScript
{
    /// <summary>
    /// Component that adds module types to the editor. 
    /// This class adds some sample audio modules.</summary>
    [Export(typeof(IInitializable))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(ScriptNodeDefinitionManager))]
    public class ScriptNodeDefinitionManager : IPaletteClient, IInitializable
    {
        static readonly string stm_DefaultPath = "Data";
        static readonly string NS = "VScript";

        static readonly string[] stm_DefaultModuleDefinitions = new string[]
        {
            Path.Combine(stm_DefaultPath, "mathNodes.vsdef"),
            Path.Combine(stm_DefaultPath, "buildPipelineNodes.vsdef"),
        };
        public string[] DefaultModuleDefinitions
        {
            get
            {
                return stm_DefaultModuleDefinitions;
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        [ImportingConstructor]
        public ScriptNodeDefinitionManager()
        {
        }


        public VisualScriptTypeManager NodeTypeManager
        {
            get { return m_nodeTypeManager; }
        }


        [Import(AllowDefault = true)]
        private IPaletteService m_paletteService = null;

        [Import(AllowDefault = false)]
        private VisualScriptTypeManager m_nodeTypeManager = null;


        /// <summary>
        /// Gets the palette category string for the circuit modules</summary>
        public readonly string PaletteCategory = "ScriptNodes".Localize();

        /// <summary>
        /// Gets drawing resource key for boolean pin types</summary>
        public string BooleanPinTypeName
        {
            get { return BooleanPinType.Name; }
        }

        /// <summary>
        /// Gets boolean pin type</summary>
        public AttributeType BooleanPinType
        {
            get { return AttributeType.BooleanType; }
        }

        /// <summary>
        /// Gets float pin type name</summary>
        public string FloatPinTypeName
        {
            get { return FloatPinType.Name; }
        }

        /// <summary>
        /// Gets float pin type</summary>
        public AttributeType FloatPinType
        {
            get { return AttributeType.FloatType; }
        }

        #region IInitializable Members

        /// <summary>
        /// Finishes initializing component by adding palette information and defining module types</summary>
        public virtual void Initialize()
        {
            // add palette info to annotation type, and register with palette
            var annotationItem = new NodeTypePaletteItem(
                annotationType.Type,
                "Comment".Localize(),
                "Create a moveable resizable comment on the circuit canvas".Localize(),
                Resources.AnnotationImage);

            if (m_paletteService != null)
                m_paletteService.AddItem(annotationItem, "Misc".Localize("abbreviation for miscellaneous"), this);

            // define editable properties on annotation
            annotationType.Type.SetTag(
                new PropertyDescriptorCollection(
                    new PropertyDescriptor[] {
                            new AttributePropertyDescriptor(
                                "Text".Localize(),
                                annotationType.textAttribute,
                                null,
                                "Comment Text".Localize(),
                                false),
                            new AttributePropertyDescriptor(
                                "Background Color".Localize(),  // name
                                annotationType.backcolorAttribute, //AttributeInfo
                                null, // category
                                "Comment's background color".Localize(), //description
                                false, //isReadOnly
                                new Sce.Atf.Controls.PropertyEditing.ColorEditor(), // editor
                                new Sce.Atf.Controls.PropertyEditing.IntColorConverter() // typeConverter
                                ),
                           new AttributePropertyDescriptor(
                                "Foreground Color".Localize(),  // name
                                annotationType.foreColorAttribute, //AttributeInfo
                                null, // category
                                "Comment's foreground color".Localize(), //description
                                false, //isReadOnly
                                new Sce.Atf.Controls.PropertyEditing.ColorEditor(), // editor
                                new Sce.Atf.Controls.PropertyEditing.IntColorConverter() // typeConverter
                                ),
                   }));


            CreateChildCollectionEditor();

            InitializeProperties();

            LoadNodeDefinition();

        }

        #endregion

        #region IPaletteClient Members

        /// <summary>
        /// Gets display info for the item</summary>
        /// <param name="item">Item</param>
        /// <param name="info">Info object, which client can fill out</param>
        void IPaletteClient.GetInfo(object item, ItemInfo info)
        {
            var paletteItem = (NodeTypePaletteItem)item;
            if (paletteItem != null)
            {
                info.Label = paletteItem.Name;
                info.Description = paletteItem.Description;
                info.ImageIndex = info.GetImageList().Images.IndexOfKey(paletteItem.ImageName);
                info.HoverText = paletteItem.Description;
            }
        }

        /// <summary>
        /// Converts the palette item into an object that can be inserted into an IInstancingContext</summary>
        /// <param name="item">Item to convert</param>
        /// <returns>Object that can be inserted into an IInstancingContext</returns>
        object IPaletteClient.Convert(object item)
        {
            var paletteItem = (NodeTypePaletteItem)item;
            var node = new DomNode(paletteItem.NodeType);
            if (paletteItem.NodeType.IdAttribute != null)
                node.SetAttribute(paletteItem.NodeType.IdAttribute, paletteItem.Name);
            return node;
        }

        #endregion


        struct PropertyInformation
        {
            public AttributeInfo AttributeInfo;
            public AttributeInfo ListAttributeInfo;
            public ChildInfo ChildInfo;
            public object Editor;
            public TypeConverter Converter;
        };

        List<PropertyInformation> m_PropertyInfos = new List<PropertyInformation>();



        DomNodeType CreateDOMType(Type clrType)
        {
            var nodeType = new DomNodeType(clrType.Name);

            var propertyDescs = new PropertyDescriptorCollection(null);
            var properties = clrType.GetProperties();
            foreach (var prop in properties)
            {
                var propType = prop.PropertyType;
                AttributeInfo newAttr = null;
                PropertyDescriptor propDesc = null;

                if (propType.IsClass && propType != typeof(string))
                {
                    ChildInfo newChild = null;
                    object editor = null;

                    var childCLRType = propType.IsArray ? propType.GetElementType() : propType;
                    newChild = new ChildInfo(prop.Name, CreateDOMType(childCLRType), propType.IsArray);
                    nodeType.Define(newChild);
                    if (propType.IsArray && clrType != typeof(VisualScriptSchema.EditorSocket))
                    {
                        editor = m_ChildCollectionEditor;
                    }

                    propDesc = new ChildPropertyDescriptor(prop.Name, newChild, "Node", prop.Name, false, editor);
                }
                else if (propType.IsEnum)
                {
                    if (propType.IsArray)
                    {
                        newAttr = new AttributeInfo(prop.Name, new AttributeType(AttributeTypes.SingleArray.ToString(), typeof(string[])));
                        newAttr.AddRule(new StringEnumRule(Enum.GetNames(propType)));
                    }
                    else
                    {
                        newAttr = new AttributeInfo(prop.Name, AttributeType.StringType);
                        newAttr.AddRule(new StringEnumRule(Enum.GetNames(propType)));
                    }
                }
                else
                {
                    if (propType.IsArray)
                    {
                        switch (propType.Name)
                        {
                            case "String":
                                // Consider them as string crc always?
                                newAttr = new AttributeInfo(prop.Name, new AttributeType(AttributeTypes.SingleArray.ToString(), typeof(string[])));
                                newAttr.AddRule(new StringHashRule());
                                break;
                            case "int":
                                newAttr = new AttributeInfo(prop.Name, new AttributeType(AttributeTypes.Int32Array.ToString(), typeof(int[])));
                                break;
                            case "boolean":
                                newAttr = new AttributeInfo(prop.Name, new AttributeType(AttributeTypes.BooleanArray.ToString(), typeof(bool[])));
                                break;
                            case "Single":
                                newAttr = new AttributeInfo(prop.Name, new AttributeType(AttributeTypes.SingleArray.ToString(), typeof(float[])));
                                break;
                            default:
                                throw new NotSupportedException("Not supported data type for DOM type gen:" + prop.PropertyType.Name);
                        }
                    }
                    else
                    {
                        switch (propType.Name)
                        {
                            case "String":
                                // Consider them as string crc always?
                                newAttr = new AttributeInfo(prop.Name, AttributeType.StringType);
                                newAttr.AddRule(new StringHashRule());
                                break;
                            case "int":
                                newAttr = new AttributeInfo(prop.Name, AttributeType.IntType);
                                break;
                            case "boolean":
                                newAttr = new AttributeInfo(prop.Name, AttributeType.BooleanType);
                                break;
                            case "Single":
                                newAttr = new AttributeInfo(prop.Name, AttributeType.FloatType);
                                break;
                            default:
                                throw new NotSupportedException("Not supported data type for DOM type gen:" + prop.PropertyType.Name);
                        }
                    }

                }

                if (newAttr != null)
                {
                    propDesc = new AttributePropertyDescriptor(prop.Name, newAttr, "Node", prop.Name, false);
                    nodeType.Define(newAttr);
                }

                if (propDesc != null)
                    propertyDescs.Add(propDesc);
            }

            nodeType.SetTag(propertyDescs);

            return nodeType;
        }


        void CreateChildCollectionEditor()
        {
            //EmbeddedCollectionEditor edit children (edit, add, remove, move).
            // note: EmbeddedCollectionEditor needs some work (efficiency and implementation issues).
            m_ChildCollectionEditor = new EmbeddedCollectionEditor();

            // the following  lambda's handles (add, remove, move ) items.
            m_ChildCollectionEditor.GetItemInsertersFunc = (context) =>
            {
                var insertors
                    = new EmbeddedCollectionEditor.ItemInserter[1];

                var list = context.GetValue() as IList<DomNode>;
                if (list != null)
                {
                    var childDescriptor
                        = context.Descriptor as ChildPropertyDescriptor;
                    if (childDescriptor != null)
                    {
                        insertors[0] = new EmbeddedCollectionEditor.ItemInserter(childDescriptor.ChildInfo.Type.Name,
                    delegate
                    {
                        DomNode node = new DomNode(childDescriptor.ChildInfo.Type);
                        if (node.Type.IdAttribute != null)
                        {
                            node.SetAttribute(node.Type.IdAttribute, node.Type.Name);
                        }
                        list.Add(node);
                        return node;
                    });
                        return insertors;
                    }
                }
                return EmptyArray<EmbeddedCollectionEditor.ItemInserter>.Instance;
            };


            m_ChildCollectionEditor.RemoveItemFunc = (context, item) =>
            {
                var list = context.GetValue() as IList<DomNode>;
                if (list != null)
                    list.Remove(item.Cast<DomNode>());
            };


            m_ChildCollectionEditor.MoveItemFunc = (context, item, delta) =>
            {
                var list = context.GetValue() as IList<DomNode>;
                if (list != null)
                {
                    DomNode node = item.Cast<DomNode>();
                    int index = list.IndexOf(node);
                    int insertIndex = index + delta;
                    if (insertIndex < 0 || insertIndex >= list.Count)
                        return;
                    list.RemoveAt(index);
                    list.Insert(insertIndex, node);
                }

            };

        }

        void InitializeProperties()
        {
            m_PropertyInfos.Clear();
            foreach (var enumValueObj in Enum.GetValues(typeof(VisualScriptSchema.PropertyType)))
            {
                var enumValue = (VisualScriptSchema.PropertyType)enumValueObj;
                AttributeInfo newAttr = null, newListAttr = null;
                DomNodeType childNodeType = null;
                object editor = null;
                TypeConverter converter = null;

                switch (enumValue)
                {
                    case VisualScriptSchema.PropertyType.Event:
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.BooleanType);
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.BooleanArrayType);
                        break;
                    case VisualScriptSchema.PropertyType.boolean:
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.BooleanType);
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.BooleanArrayType);
                        break;
                    case VisualScriptSchema.PropertyType.@int:
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.IntType);
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.IntArrayType);
                        break;
                    case VisualScriptSchema.PropertyType.@float:
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.FloatType);
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.FloatArrayType);
                        break;
                    case VisualScriptSchema.PropertyType.@double:
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.DoubleType);
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.DoubleArrayType);
                        break;
                    case VisualScriptSchema.PropertyType.@string:
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.StringType);
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.StringArrayType);
                        break;
                    case VisualScriptSchema.PropertyType.FixedString:
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.StringType);
                        newAttr.AddRule(new StringHashRule());
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.StringArrayType);
                        newListAttr.AddRule(new StringHashRule());
                        break;
                    case VisualScriptSchema.PropertyType.vector3:
                        newAttr = new AttributeInfo(enumValue.ToString(), new AttributeType(enumValue.ToString(), typeof(float[]), 3));
                        editor = new Sce.Atf.Controls.PropertyEditing.NumericTupleEditor(typeof(float), new string[] { "x", "y", "z" });
                        break;
                    case VisualScriptSchema.PropertyType.Socket:
                        childNodeType = CreateDOMType(typeof(VisualScriptSchema.EditorSocket));
                        socketType.Type = childNodeType; // cache for global use
                        break;
                    case VisualScriptSchema.PropertyType.File:
                    case VisualScriptSchema.PropertyType.Asset:
                        // TODO: add new type
                        newAttr = new AttributeInfo(enumValue.ToString(), AttributeType.StringType);
                        newListAttr = new AttributeInfo(enumValue.ToString() + "[]", AttributeType.StringArrayType);
                        break;
                    default:
                        throw new InvalidDataException("There is a not-handled property type");
                }

                if (newAttr != null)
                {
                    var propInfo = new PropertyInformation()
                    {
                        AttributeInfo = newAttr,
                        ListAttributeInfo = newListAttr,
                        ChildInfo = null,
                        Editor = editor,
                        Converter = converter,
                    };
                    m_PropertyInfos.Add(propInfo);
                }
                else if (childNodeType != null)
                {
                    ChildInfo newChild = new ChildInfo(enumValue.ToString(), childNodeType);
                    var propInfo = new PropertyInformation()
                    {
                        AttributeInfo = null,
                        ListAttributeInfo = null,
                        ChildInfo = newChild,
                        Editor = null,
                        Converter = null,
                    };
                    m_PropertyInfos.Add(propInfo);
                }
                else
                {
                    Outputs.WriteLine(OutputMessageType.Warning, "Invalid property {0}", enumValue.ToString());
                }
            }
        }


        void AddProperty(DomNodeType domNodeType, VisualScriptSchema.Property prop, PropertyDescriptorCollection newDescs)
        {
            var propTypeInfo = m_PropertyInfos[(int)prop.Type];
            if (propTypeInfo.AttributeInfo != null)
            {
                AttributeInfo newAttr = null;
                if (prop.IsArray)
                {
                    newAttr = propTypeInfo.ListAttributeInfo.Clone(prop.Name);
                }
                else
                {
                    newAttr = propTypeInfo.AttributeInfo.Clone(prop.Name);
                }

                if (!string.IsNullOrEmpty(prop.Default))
                {
                    newAttr.DefaultValue = newAttr.Type.Convert(prop.Default);
                }

                newAttr.AddRule(new GameDataAttributeRule(prop));

                var newDesc = new AttributePropertyDescriptor(newAttr.Name, newAttr, "Node", newAttr.Name, false, propTypeInfo.Editor, propTypeInfo.Converter);
                newDescs.Add(newDesc);
                domNodeType.Define(newAttr);
            }
            else if (propTypeInfo.ChildInfo != null)
            {
                var newChild = propTypeInfo.ChildInfo.Clone(prop.Name, prop.IsArray);

                newChild.AddRule(new GameDataChildRule(prop));

                object editor = m_ChildCollectionEditor;
                var newDesc = new ChildPropertyDescriptor(newChild.Name, newChild, "Node", newChild.Name, false, editor, propTypeInfo.Converter);
                newDescs.Add(newDesc);
                domNodeType.Define(newChild);
            }

        }

        void RecursiveCreateProperties(VisualScriptSchema.NodeTypeInfo nodeDef, Dictionary<string, VisualScriptSchema.Property> properties)
        {
            if (!string.IsNullOrEmpty(nodeDef.Base))
            {
                VisualScriptSchema.NodeTypeInfo parentNodeDef = nodeDef.Parent;
                if (parentNodeDef != null)
                {
                    // TODO : maybe parent domnode type would be better?
                    RecursiveCreateProperties(parentNodeDef, properties);
                }
                else
                {
                    Outputs.Write(OutputMessageType.Error, "Can't find base node definition: {0} parent of {1}", nodeDef.Base, nodeDef.Name);
                }
            }

            if (nodeDef.Property != null)
            {
                foreach (var prop in nodeDef.Property)
                {
                    if (properties.ContainsKey(prop.Name))
                    {
                        // This is DFS travel. Child most property definition will be taken.
                        // override
                        properties[prop.Name] = prop;
                    }
                    else
                    {
                        properties.Add(prop.Name, prop);
                    }
                }
            }
        }

        void AddProperties(ICollection<VisualScriptSchema.Property> properties, DomNodeType domNodeType, PropertyDescriptorCollection newDescs)
        {
            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    AddProperty(domNodeType, prop, newDescs);
                }
            }
        }
        
        string GetNodeIcon(VisualScriptSchema.NodeTypeInfo nodeDef, string nodeIcon = null)
        {
            if (!string.IsNullOrEmpty(nodeDef.Icon))
                nodeIcon = typeof(Resources).FullName + "." + nodeDef.Icon;

            if (!string.IsNullOrEmpty(nodeIcon))
                return nodeIcon;

            if (!string.IsNullOrEmpty(nodeDef.Base))
            {
                VisualScriptSchema.NodeTypeInfo parentNodeDef = nodeDef.Parent;
                if (parentNodeDef != null)
                {
                    nodeIcon = GetNodeIcon(parentNodeDef, nodeIcon);
                }
            }

            return nodeIcon;
        }

        string GetNodeCategory(VisualScriptSchema.NodeTypeInfo nodeDef, string nodeCategory = null)
        {
            if (!string.IsNullOrEmpty(nodeDef.Category))
                nodeCategory = nodeDef.Category;

            if (!string.IsNullOrEmpty(nodeCategory))
                return nodeCategory;

            if (!string.IsNullOrEmpty(nodeDef.Base))
            {
                VisualScriptSchema.NodeTypeInfo parentNodeDef = nodeDef.Parent;
                if (parentNodeDef != null)
                {
                    nodeCategory = GetNodeCategory(parentNodeDef, nodeCategory);
                }
            }

            return nodeCategory;
        }

        Dictionary<string, VisualScriptSchema.NodeTypeInfo> m_NodeDefines = new Dictionary<string, VisualScriptSchema.NodeTypeInfo>();

        void LoadNodeDefinition()
        {
            m_NodeDefines.Clear();

            foreach(var definitionFile in stm_DefaultModuleDefinitions)
                AddNodeDefinition(definitionFile);
        }

        void AddNodeDefinition(string nodeDefinitionPath)
        {
            if (nodeDefinitionPath == null)
            {
                throw new Exception("Invalid script node definition: " + nodeDefinitionPath);
            }

            VisualScriptSchema.NodeTypeDefinitions nodeData = null;
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(VisualScriptSchema.NodeTypeDefinitions));

            using (var stream = new FileStream(nodeDefinitionPath, FileMode.Open, FileAccess.Read))
            {
                nodeData = xmlSerializer.Deserialize(stream) as VisualScriptSchema.NodeTypeDefinitions;
            }


            if (nodeData == null || nodeData.NodeTypeInfo == null)
                throw new InvalidDataException("Failed to load Node definition data");

            // cache all node types
            foreach (var nodeDef in nodeData.NodeTypeInfo)
            {
                VisualScriptSchema.NodeTypeInfo oldDef;
                if (m_NodeDefines.TryGetValue(nodeDef.Name, out oldDef))
                {
                    throw new InvalidDataException(string.Format("Duplicated type name, {0} is already taken in {1}", nodeDef.Name, oldDef.NodeDefinitionFile));
                }

                nodeDef.NodeDefinitionFile = nodeDefinitionPath;
                m_NodeDefines.Add(nodeDef.Name, nodeDef);
            }

            foreach (var nodeDef in nodeData.NodeTypeInfo)
            {
                VisualScriptSchema.NodeTypeInfo parent;
                if (string.IsNullOrEmpty(nodeDef.Base)) continue;
                if (m_NodeDefines.TryGetValue(nodeDef.Base, out parent))
                    nodeDef.Parent = parent;
            }


            foreach (var nodeDef in nodeData.NodeTypeInfo)
            {
                if (nodeDef.IsAbstract) continue;

                var inputs = new List<ElementType.Pin>();
                var outputs = new List<ElementType.Pin>();
                var properties = new Dictionary<string, VisualScriptSchema.Property>();
                RecursiveCreateProperties(nodeDef, properties);

                var nodeIcon = GetNodeIcon(nodeDef);
                var nodeCategory = GetNodeCategory(nodeDef);

                var domNodeType = DefineModuleType(new XmlQualifiedName(nodeDef.Name, NS),
                    nodeDef.Name,
                    nodeDef.Description,
                    nodeCategory,
                    string.IsNullOrEmpty(nodeIcon) ? Resources.ButtonImage : nodeIcon,
                    m_typeManager.DomNodeTypeCollection);

                // Use node name as node type when node type isn't assigned
                nodeDef.NodeType = string.IsNullOrEmpty(nodeDef.NodeType) ? nodeDef.Name : nodeDef.NodeType;

                // Add in-game type info
                var typeAttribute = new AttributeInfo("NodeType", AttributeType.StringType);
                typeAttribute.DefaultValue = nodeDef.NodeType;
                domNodeType.Define(typeAttribute);

                // Update Icon name to actual icon name
                nodeDef.Icon = string.IsNullOrEmpty(nodeIcon) ? Resources.ButtonImage : nodeIcon;

                // Add node def as tag
                domNodeType.SetTag(nodeDef);

                var newDescs = new PropertyDescriptorCollection(null);

                AddProperties(properties.Values, domNodeType, newDescs);

                domNodeType.SetTag(newDescs);
            }


        }


        /// <summary>
        /// Prepare metadata for the module type, to be used by the palette and circuit drawing engine
        /// </summary>
        /// <param name="name"> Schema full name of the DomNodeType for the module type</param>
        /// <param name="displayName">Display name for the module type</param>
        /// <param name="description"></param>
        /// <param name="imageName">Image name </param>
        /// <param name="inputs">Define input pins for the module type</param>
        /// <param name="outputs">Define output pins for the module type</param>
        /// <param name="loader">XML schema loader </param>
        /// <param name="domNodeType">optional DomNode type for the module type</param>
        /// <returns>DomNodeType that was created (or the domNodeType parameter, if it wasn't null)</returns>
        protected DomNodeType DefineModuleType(

            XmlQualifiedName name,
            string displayName,
            string description,
            string category,
            string imageName,
            DomNodeTypeCollection typeCollection,
            DomNodeType domNodeType = null)
        {
            if (domNodeType == null)
                // create the type
                domNodeType = new DomNodeType(
                name.ToString(),
                moduleType.Type,
                EmptyArray<AttributeInfo>.Instance,
                EmptyArray<ChildInfo>.Instance,
                new ExtensionInfo[] { new ExtensionInfo<ScriptNodeElementType>() });

            // add it to the schema-defined types
            typeCollection.AddNodeType(name.ToString(), domNodeType);

            // add the type to the palette
            if (m_paletteService != null)
            {
                m_paletteService.AddItem(
                    new NodeTypePaletteItem(
                        domNodeType,
                        displayName,
                        description,
                        imageName),
                    string.IsNullOrEmpty(category) ? PaletteCategory : category,
                    this);
            }

            return domNodeType;
        }


        [Import]
        VisualScriptTypeManager m_typeManager = null;

        EmbeddedCollectionEditor m_ChildCollectionEditor;
    }



    public class GameDataAttributeRule : AttributeRule
    {
        public VisualScriptSchema.Property SchemaProperty { get; private set; }

        public GameDataAttributeRule(VisualScriptSchema.Property property)
        {
            SchemaProperty = property;
        }

        public override bool Validate(object value, AttributeInfo info)
        {
            return true;
        }
    }

    public class GameDataChildRule : ChildRule
    {
        public VisualScriptSchema.Property SchemaProperty { get; private set; }

        public GameDataChildRule(VisualScriptSchema.Property property)
        {
            SchemaProperty = property;
        }

        public override bool Validate(DomNode parent, DomNode child, ChildInfo childInfo)
        {
            return true;
        }
    }


    static public class startNodeType
    {
        public static DomNodeType Type;
    };

    static public class eventNodeType
    {
        public static DomNodeType Type;
    };

    //static public class socketType
    //{
    //    public static DomNodeType Type;
    //};

}
