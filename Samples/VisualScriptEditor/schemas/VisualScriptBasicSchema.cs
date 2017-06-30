// -------------------------------------------------------------------------------------------------------------------
// Generated code, do not edit
// Command Line:  DomGen "DomGenVS" "VisualScriptBasicSchema.xsd" "VisualScriptBasicSchema.cs" "VisualScriptBasicSchema" "VisualScript" "-enums"
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Sce.Atf;
using Sce.Atf.Dom;

namespace VisualScript
{
    public static class VisualScriptBasicSchema
    {
        public const string NS = "http://blue3k.com/1_0";

        public static void Initialize(XmlSchemaTypeCollection typeCollection)
        {
            Initialize((ns,name)=>typeCollection.GetNodeType(ns,name),
                (ns,name)=>typeCollection.GetRootElement(ns,name));
        }

        public static void Initialize(IDictionary<string, XmlSchemaTypeCollection> typeCollections)
        {
            Initialize((ns,name)=>typeCollections[ns].GetNodeType(name),
                (ns,name)=>typeCollections[ns].GetRootElement(name));
        }

        private static void Initialize(Func<string, string, DomNodeType> getNodeType, Func<string, string, ChildInfo> getRootElement)
        {
            visualScriptDocumentType.Type = getNodeType("http://blue3k.com/1_0", "visualScriptDocumentType");
            visualScriptDocumentType.versionAttribute = visualScriptDocumentType.Type.GetAttributeInfo("version");
            visualScriptDocumentType.moduleChild = visualScriptDocumentType.Type.GetChildInfo("module");
            visualScriptDocumentType.connectionChild = visualScriptDocumentType.Type.GetChildInfo("connection");
            visualScriptDocumentType.layerFolderChild = visualScriptDocumentType.Type.GetChildInfo("layerFolder");
            visualScriptDocumentType.expressionChild = visualScriptDocumentType.Type.GetChildInfo("expression");
            visualScriptDocumentType.annotationChild = visualScriptDocumentType.Type.GetChildInfo("annotation");
            visualScriptDocumentType.prototypeFolderChild = visualScriptDocumentType.Type.GetChildInfo("prototypeFolder");
            visualScriptDocumentType.templateFolderChild = visualScriptDocumentType.Type.GetChildInfo("templateFolder");
            visualScriptDocumentType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("version".Localize(), visualScriptDocumentType.versionAttribute, null, "version".Localize(), false, null, null ),
            }));

            visualScriptType.Type = getNodeType("http://blue3k.com/1_0", "visualScriptType");
            visualScriptType.moduleChild = visualScriptType.Type.GetChildInfo("module");
            visualScriptType.connectionChild = visualScriptType.Type.GetChildInfo("connection");
            visualScriptType.layerFolderChild = visualScriptType.Type.GetChildInfo("layerFolder");
            visualScriptType.expressionChild = visualScriptType.Type.GetChildInfo("expression");
            visualScriptType.annotationChild = visualScriptType.Type.GetChildInfo("annotation");

            moduleType.Type = getNodeType("http://blue3k.com/1_0", "moduleType");
            moduleType.nameAttribute = moduleType.Type.GetAttributeInfo("name");
            moduleType.labelAttribute = moduleType.Type.GetAttributeInfo("label");
            moduleType.xAttribute = moduleType.Type.GetAttributeInfo("x");
            moduleType.yAttribute = moduleType.Type.GetAttributeInfo("y");
            moduleType.visibleAttribute = moduleType.Type.GetAttributeInfo("visible");
            moduleType.showUnconnectedPinsAttribute = moduleType.Type.GetAttributeInfo("showUnconnectedPins");
            moduleType.sourceGuidAttribute = moduleType.Type.GetAttributeInfo("sourceGuid");
            moduleType.validatedAttribute = moduleType.Type.GetAttributeInfo("validated");
            moduleType.dynamicPropertyChild = moduleType.Type.GetChildInfo("dynamicProperty");
            moduleType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), moduleType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), moduleType.labelAttribute, null, "label".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("x".Localize(), moduleType.xAttribute, null, "x".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("y".Localize(), moduleType.yAttribute, null, "y".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("visible".Localize(), moduleType.visibleAttribute, null, "visible".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("showUnconnectedPins".Localize(), moduleType.showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("sourceGuid".Localize(), moduleType.sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("validated".Localize(), moduleType.validatedAttribute, null, "validated".Localize(), false, null, null ),
            }));

            dynamicPropertyType.Type = getNodeType("http://blue3k.com/1_0", "dynamicPropertyType");
            dynamicPropertyType.nameAttribute = dynamicPropertyType.Type.GetAttributeInfo("name");
            dynamicPropertyType.categoryAttribute = dynamicPropertyType.Type.GetAttributeInfo("category");
            dynamicPropertyType.descriptionAttribute = dynamicPropertyType.Type.GetAttributeInfo("description");
            dynamicPropertyType.converterAttribute = dynamicPropertyType.Type.GetAttributeInfo("converter");
            dynamicPropertyType.editorAttribute = dynamicPropertyType.Type.GetAttributeInfo("editor");
            dynamicPropertyType.valueTypeAttribute = dynamicPropertyType.Type.GetAttributeInfo("valueType");
            dynamicPropertyType.stringValueAttribute = dynamicPropertyType.Type.GetAttributeInfo("stringValue");
            dynamicPropertyType.floatValueAttribute = dynamicPropertyType.Type.GetAttributeInfo("floatValue");
            dynamicPropertyType.vector3ValueAttribute = dynamicPropertyType.Type.GetAttributeInfo("vector3Value");
            dynamicPropertyType.boolValueAttribute = dynamicPropertyType.Type.GetAttributeInfo("boolValue");
            dynamicPropertyType.intValueAttribute = dynamicPropertyType.Type.GetAttributeInfo("intValue");
            dynamicPropertyType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), dynamicPropertyType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("category".Localize(), dynamicPropertyType.categoryAttribute, null, "category".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("description".Localize(), dynamicPropertyType.descriptionAttribute, null, "description".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("converter".Localize(), dynamicPropertyType.converterAttribute, null, "converter".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("editor".Localize(), dynamicPropertyType.editorAttribute, null, "editor".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("valueType".Localize(), dynamicPropertyType.valueTypeAttribute, null, "valueType".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("stringValue".Localize(), dynamicPropertyType.stringValueAttribute, null, "stringValue".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("floatValue".Localize(), dynamicPropertyType.floatValueAttribute, null, "floatValue".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("vector3Value".Localize(), dynamicPropertyType.vector3ValueAttribute, null, "vector3Value".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("boolValue".Localize(), dynamicPropertyType.boolValueAttribute, null, "boolValue".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("intValue".Localize(), dynamicPropertyType.intValueAttribute, null, "intValue".Localize(), false, null, null ),
            }));

            connectionType.Type = getNodeType("http://blue3k.com/1_0", "connectionType");
            connectionType.labelAttribute = connectionType.Type.GetAttributeInfo("label");
            connectionType.outputModuleAttribute = connectionType.Type.GetAttributeInfo("outputModule");
            connectionType.outputPinAttribute = connectionType.Type.GetAttributeInfo("outputPin");
            connectionType.inputModuleAttribute = connectionType.Type.GetAttributeInfo("inputModule");
            connectionType.inputPinAttribute = connectionType.Type.GetAttributeInfo("inputPin");
            connectionType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("label".Localize(), connectionType.labelAttribute, null, "label".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("outputModule".Localize(), connectionType.outputModuleAttribute, null, "outputModule".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("outputPin".Localize(), connectionType.outputPinAttribute, null, "outputPin".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("inputModule".Localize(), connectionType.inputModuleAttribute, null, "inputModule".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("inputPin".Localize(), connectionType.inputPinAttribute, null, "inputPin".Localize(), false, null, null ),
            }));

            layerFolderType.Type = getNodeType("http://blue3k.com/1_0", "layerFolderType");
            layerFolderType.nameAttribute = layerFolderType.Type.GetAttributeInfo("name");
            layerFolderType.layerFolderChild = layerFolderType.Type.GetChildInfo("layerFolder");
            layerFolderType.moduleRefChild = layerFolderType.Type.GetChildInfo("moduleRef");
            layerFolderType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), layerFolderType.nameAttribute, null, "name".Localize(), false, null, null ),
            }));

            moduleRefType.Type = getNodeType("http://blue3k.com/1_0", "moduleRefType");
            moduleRefType.refAttribute = moduleRefType.Type.GetAttributeInfo("ref");
            moduleRefType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("ref".Localize(), moduleRefType.refAttribute, null, "ref".Localize(), false, null, null ),
            }));

            expressionType.Type = getNodeType("http://blue3k.com/1_0", "expressionType");
            expressionType.idAttribute = expressionType.Type.GetAttributeInfo("id");
            expressionType.labelAttribute = expressionType.Type.GetAttributeInfo("label");
            expressionType.scriptAttribute = expressionType.Type.GetAttributeInfo("script");
            expressionType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("id".Localize(), expressionType.idAttribute, null, "id".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), expressionType.labelAttribute, null, "label".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("script".Localize(), expressionType.scriptAttribute, null, "script".Localize(), false, null, null ),
            }));

            annotationType.Type = getNodeType("http://blue3k.com/1_0", "annotationType");
            annotationType.textAttribute = annotationType.Type.GetAttributeInfo("text");
            annotationType.xAttribute = annotationType.Type.GetAttributeInfo("x");
            annotationType.yAttribute = annotationType.Type.GetAttributeInfo("y");
            annotationType.widthAttribute = annotationType.Type.GetAttributeInfo("width");
            annotationType.heightAttribute = annotationType.Type.GetAttributeInfo("height");
            annotationType.backcolorAttribute = annotationType.Type.GetAttributeInfo("backcolor");
            annotationType.foreColorAttribute = annotationType.Type.GetAttributeInfo("foreColor");
            annotationType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("text".Localize(), annotationType.textAttribute, null, "text".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("x".Localize(), annotationType.xAttribute, null, "x".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("y".Localize(), annotationType.yAttribute, null, "y".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("width".Localize(), annotationType.widthAttribute, null, "width".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("height".Localize(), annotationType.heightAttribute, null, "height".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("backcolor".Localize(), annotationType.backcolorAttribute, null, "backcolor".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("foreColor".Localize(), annotationType.foreColorAttribute, null, "foreColor".Localize(), false, null, null ),
            }));

            prototypeFolderType.Type = getNodeType("http://blue3k.com/1_0", "prototypeFolderType");
            prototypeFolderType.nameAttribute = prototypeFolderType.Type.GetAttributeInfo("name");
            prototypeFolderType.prototypeFolderChild = prototypeFolderType.Type.GetChildInfo("prototypeFolder");
            prototypeFolderType.prototypeChild = prototypeFolderType.Type.GetChildInfo("prototype");
            prototypeFolderType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), prototypeFolderType.nameAttribute, null, "name".Localize(), false, null, null ),
            }));

            prototypeType.Type = getNodeType("http://blue3k.com/1_0", "prototypeType");
            prototypeType.nameAttribute = prototypeType.Type.GetAttributeInfo("name");
            prototypeType.moduleChild = prototypeType.Type.GetChildInfo("module");
            prototypeType.connectionChild = prototypeType.Type.GetChildInfo("connection");
            prototypeType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), prototypeType.nameAttribute, null, "name".Localize(), false, null, null ),
            }));

            templateFolderType.Type = getNodeType("http://blue3k.com/1_0", "templateFolderType");
            templateFolderType.nameAttribute = templateFolderType.Type.GetAttributeInfo("name");
            templateFolderType.referenceFileAttribute = templateFolderType.Type.GetAttributeInfo("referenceFile");
            templateFolderType.templateFolderChild = templateFolderType.Type.GetChildInfo("templateFolder");
            templateFolderType.templateChild = templateFolderType.Type.GetChildInfo("template");
            templateFolderType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), templateFolderType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("referenceFile".Localize(), templateFolderType.referenceFileAttribute, null, "referenceFile".Localize(), false, null, null ),
            }));

            templateType.Type = getNodeType("http://blue3k.com/1_0", "templateType");
            templateType.guidAttribute = templateType.Type.GetAttributeInfo("guid");
            templateType.labelAttribute = templateType.Type.GetAttributeInfo("label");
            templateType.moduleChild = templateType.Type.GetChildInfo("module");
            templateType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("guid".Localize(), templateType.guidAttribute, null, "guid".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), templateType.labelAttribute, null, "label".Localize(), false, null, null ),
            }));

            pinType.Type = getNodeType("http://blue3k.com/1_0", "pinType");
            pinType.nameAttribute = pinType.Type.GetAttributeInfo("name");
            pinType.typeAttribute = pinType.Type.GetAttributeInfo("type");
            pinType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), pinType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("type".Localize(), pinType.typeAttribute, null, "type".Localize(), false, null, null ),
            }));

            groupPinType.Type = getNodeType("http://blue3k.com/1_0", "groupPinType");
            groupPinType.nameAttribute = groupPinType.Type.GetAttributeInfo("name");
            groupPinType.typeAttribute = groupPinType.Type.GetAttributeInfo("type");
            groupPinType.moduleAttribute = groupPinType.Type.GetAttributeInfo("module");
            groupPinType.pinAttribute = groupPinType.Type.GetAttributeInfo("pin");
            groupPinType.visibleAttribute = groupPinType.Type.GetAttributeInfo("visible");
            groupPinType.indexAttribute = groupPinType.Type.GetAttributeInfo("index");
            groupPinType.pinnedAttribute = groupPinType.Type.GetAttributeInfo("pinned");
            groupPinType.pinYAttribute = groupPinType.Type.GetAttributeInfo("pinY");
            groupPinType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), groupPinType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("type".Localize(), groupPinType.typeAttribute, null, "type".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("module".Localize(), groupPinType.moduleAttribute, null, "module".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("pin".Localize(), groupPinType.pinAttribute, null, "pin".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("visible".Localize(), groupPinType.visibleAttribute, null, "visible".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("index".Localize(), groupPinType.indexAttribute, null, "index".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("pinned".Localize(), groupPinType.pinnedAttribute, null, "pinned".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("pinY".Localize(), groupPinType.pinYAttribute, null, "pinY".Localize(), false, null, null ),
            }));

            groupType.Type = getNodeType("http://blue3k.com/1_0", "groupType");
            groupType.nameAttribute = groupType.Type.GetAttributeInfo("name");
            groupType.labelAttribute = groupType.Type.GetAttributeInfo("label");
            groupType.xAttribute = groupType.Type.GetAttributeInfo("x");
            groupType.yAttribute = groupType.Type.GetAttributeInfo("y");
            groupType.visibleAttribute = groupType.Type.GetAttributeInfo("visible");
            groupType.showUnconnectedPinsAttribute = groupType.Type.GetAttributeInfo("showUnconnectedPins");
            groupType.sourceGuidAttribute = groupType.Type.GetAttributeInfo("sourceGuid");
            groupType.validatedAttribute = groupType.Type.GetAttributeInfo("validated");
            groupType.expandedAttribute = groupType.Type.GetAttributeInfo("expanded");
            groupType.showExpandedGroupPinsAttribute = groupType.Type.GetAttributeInfo("showExpandedGroupPins");
            groupType.autosizeAttribute = groupType.Type.GetAttributeInfo("autosize");
            groupType.widthAttribute = groupType.Type.GetAttributeInfo("width");
            groupType.heightAttribute = groupType.Type.GetAttributeInfo("height");
            groupType.minwidthAttribute = groupType.Type.GetAttributeInfo("minwidth");
            groupType.minheightAttribute = groupType.Type.GetAttributeInfo("minheight");
            groupType.dynamicPropertyChild = groupType.Type.GetChildInfo("dynamicProperty");
            groupType.inputChild = groupType.Type.GetChildInfo("input");
            groupType.outputChild = groupType.Type.GetChildInfo("output");
            groupType.moduleChild = groupType.Type.GetChildInfo("module");
            groupType.connectionChild = groupType.Type.GetChildInfo("connection");
            groupType.annotationChild = groupType.Type.GetChildInfo("annotation");
            groupType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), groupType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), groupType.labelAttribute, null, "label".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("x".Localize(), groupType.xAttribute, null, "x".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("y".Localize(), groupType.yAttribute, null, "y".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("visible".Localize(), groupType.visibleAttribute, null, "visible".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("showUnconnectedPins".Localize(), groupType.showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("sourceGuid".Localize(), groupType.sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("validated".Localize(), groupType.validatedAttribute, null, "validated".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("expanded".Localize(), groupType.expandedAttribute, null, "expanded".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("showExpandedGroupPins".Localize(), groupType.showExpandedGroupPinsAttribute, null, "showExpandedGroupPins".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("autosize".Localize(), groupType.autosizeAttribute, null, "autosize".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("width".Localize(), groupType.widthAttribute, null, "width".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("height".Localize(), groupType.heightAttribute, null, "height".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("minwidth".Localize(), groupType.minwidthAttribute, null, "minwidth".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("minheight".Localize(), groupType.minheightAttribute, null, "minheight".Localize(), false, null, null ),
            }));

            missingTemplateType.Type = getNodeType("http://blue3k.com/1_0", "missingTemplateType");
            missingTemplateType.guidAttribute = missingTemplateType.Type.GetAttributeInfo("guid");
            missingTemplateType.labelAttribute = missingTemplateType.Type.GetAttributeInfo("label");
            missingTemplateType.moduleChild = missingTemplateType.Type.GetChildInfo("module");
            missingTemplateType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("guid".Localize(), missingTemplateType.guidAttribute, null, "guid".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), missingTemplateType.labelAttribute, null, "label".Localize(), false, null, null ),
            }));

            missingModuleType.Type = getNodeType("http://blue3k.com/1_0", "missingModuleType");
            missingModuleType.nameAttribute = missingModuleType.Type.GetAttributeInfo("name");
            missingModuleType.labelAttribute = missingModuleType.Type.GetAttributeInfo("label");
            missingModuleType.xAttribute = missingModuleType.Type.GetAttributeInfo("x");
            missingModuleType.yAttribute = missingModuleType.Type.GetAttributeInfo("y");
            missingModuleType.visibleAttribute = missingModuleType.Type.GetAttributeInfo("visible");
            missingModuleType.showUnconnectedPinsAttribute = missingModuleType.Type.GetAttributeInfo("showUnconnectedPins");
            missingModuleType.sourceGuidAttribute = missingModuleType.Type.GetAttributeInfo("sourceGuid");
            missingModuleType.validatedAttribute = missingModuleType.Type.GetAttributeInfo("validated");
            missingModuleType.dynamicPropertyChild = missingModuleType.Type.GetChildInfo("dynamicProperty");
            missingModuleType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), missingModuleType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), missingModuleType.labelAttribute, null, "label".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("x".Localize(), missingModuleType.xAttribute, null, "x".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("y".Localize(), missingModuleType.yAttribute, null, "y".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("visible".Localize(), missingModuleType.visibleAttribute, null, "visible".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("showUnconnectedPins".Localize(), missingModuleType.showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("sourceGuid".Localize(), missingModuleType.sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("validated".Localize(), missingModuleType.validatedAttribute, null, "validated".Localize(), false, null, null ),
            }));

            moduleTemplateRefType.Type = getNodeType("http://blue3k.com/1_0", "moduleTemplateRefType");
            moduleTemplateRefType.nameAttribute = moduleTemplateRefType.Type.GetAttributeInfo("name");
            moduleTemplateRefType.labelAttribute = moduleTemplateRefType.Type.GetAttributeInfo("label");
            moduleTemplateRefType.xAttribute = moduleTemplateRefType.Type.GetAttributeInfo("x");
            moduleTemplateRefType.yAttribute = moduleTemplateRefType.Type.GetAttributeInfo("y");
            moduleTemplateRefType.visibleAttribute = moduleTemplateRefType.Type.GetAttributeInfo("visible");
            moduleTemplateRefType.showUnconnectedPinsAttribute = moduleTemplateRefType.Type.GetAttributeInfo("showUnconnectedPins");
            moduleTemplateRefType.sourceGuidAttribute = moduleTemplateRefType.Type.GetAttributeInfo("sourceGuid");
            moduleTemplateRefType.validatedAttribute = moduleTemplateRefType.Type.GetAttributeInfo("validated");
            moduleTemplateRefType.guidRefAttribute = moduleTemplateRefType.Type.GetAttributeInfo("guidRef");
            moduleTemplateRefType.dynamicPropertyChild = moduleTemplateRefType.Type.GetChildInfo("dynamicProperty");
            moduleTemplateRefType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), moduleTemplateRefType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), moduleTemplateRefType.labelAttribute, null, "label".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("x".Localize(), moduleTemplateRefType.xAttribute, null, "x".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("y".Localize(), moduleTemplateRefType.yAttribute, null, "y".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("visible".Localize(), moduleTemplateRefType.visibleAttribute, null, "visible".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("showUnconnectedPins".Localize(), moduleTemplateRefType.showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("sourceGuid".Localize(), moduleTemplateRefType.sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("validated".Localize(), moduleTemplateRefType.validatedAttribute, null, "validated".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("guidRef".Localize(), moduleTemplateRefType.guidRefAttribute, null, "guidRef".Localize(), false, null, null ),
            }));

            groupTemplateRefType.Type = getNodeType("http://blue3k.com/1_0", "groupTemplateRefType");
            groupTemplateRefType.nameAttribute = groupTemplateRefType.Type.GetAttributeInfo("name");
            groupTemplateRefType.labelAttribute = groupTemplateRefType.Type.GetAttributeInfo("label");
            groupTemplateRefType.xAttribute = groupTemplateRefType.Type.GetAttributeInfo("x");
            groupTemplateRefType.yAttribute = groupTemplateRefType.Type.GetAttributeInfo("y");
            groupTemplateRefType.visibleAttribute = groupTemplateRefType.Type.GetAttributeInfo("visible");
            groupTemplateRefType.showUnconnectedPinsAttribute = groupTemplateRefType.Type.GetAttributeInfo("showUnconnectedPins");
            groupTemplateRefType.sourceGuidAttribute = groupTemplateRefType.Type.GetAttributeInfo("sourceGuid");
            groupTemplateRefType.validatedAttribute = groupTemplateRefType.Type.GetAttributeInfo("validated");
            groupTemplateRefType.expandedAttribute = groupTemplateRefType.Type.GetAttributeInfo("expanded");
            groupTemplateRefType.showExpandedGroupPinsAttribute = groupTemplateRefType.Type.GetAttributeInfo("showExpandedGroupPins");
            groupTemplateRefType.autosizeAttribute = groupTemplateRefType.Type.GetAttributeInfo("autosize");
            groupTemplateRefType.widthAttribute = groupTemplateRefType.Type.GetAttributeInfo("width");
            groupTemplateRefType.heightAttribute = groupTemplateRefType.Type.GetAttributeInfo("height");
            groupTemplateRefType.minwidthAttribute = groupTemplateRefType.Type.GetAttributeInfo("minwidth");
            groupTemplateRefType.minheightAttribute = groupTemplateRefType.Type.GetAttributeInfo("minheight");
            groupTemplateRefType.guidRefAttribute = groupTemplateRefType.Type.GetAttributeInfo("guidRef");
            groupTemplateRefType.refExpandedAttribute = groupTemplateRefType.Type.GetAttributeInfo("refExpanded");
            groupTemplateRefType.refShowExpandedGroupPinsAttribute = groupTemplateRefType.Type.GetAttributeInfo("refShowExpandedGroupPins");
            groupTemplateRefType.dynamicPropertyChild = groupTemplateRefType.Type.GetChildInfo("dynamicProperty");
            groupTemplateRefType.inputChild = groupTemplateRefType.Type.GetChildInfo("input");
            groupTemplateRefType.outputChild = groupTemplateRefType.Type.GetChildInfo("output");
            groupTemplateRefType.moduleChild = groupTemplateRefType.Type.GetChildInfo("module");
            groupTemplateRefType.connectionChild = groupTemplateRefType.Type.GetChildInfo("connection");
            groupTemplateRefType.annotationChild = groupTemplateRefType.Type.GetChildInfo("annotation");
            groupTemplateRefType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), groupTemplateRefType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("label".Localize(), groupTemplateRefType.labelAttribute, null, "label".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("x".Localize(), groupTemplateRefType.xAttribute, null, "x".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("y".Localize(), groupTemplateRefType.yAttribute, null, "y".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("visible".Localize(), groupTemplateRefType.visibleAttribute, null, "visible".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("showUnconnectedPins".Localize(), groupTemplateRefType.showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("sourceGuid".Localize(), groupTemplateRefType.sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("validated".Localize(), groupTemplateRefType.validatedAttribute, null, "validated".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("expanded".Localize(), groupTemplateRefType.expandedAttribute, null, "expanded".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("showExpandedGroupPins".Localize(), groupTemplateRefType.showExpandedGroupPinsAttribute, null, "showExpandedGroupPins".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("autosize".Localize(), groupTemplateRefType.autosizeAttribute, null, "autosize".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("width".Localize(), groupTemplateRefType.widthAttribute, null, "width".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("height".Localize(), groupTemplateRefType.heightAttribute, null, "height".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("minwidth".Localize(), groupTemplateRefType.minwidthAttribute, null, "minwidth".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("minheight".Localize(), groupTemplateRefType.minheightAttribute, null, "minheight".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("guidRef".Localize(), groupTemplateRefType.guidRefAttribute, null, "guidRef".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("refExpanded".Localize(), groupTemplateRefType.refExpandedAttribute, null, "refExpanded".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("refShowExpandedGroupPins".Localize(), groupTemplateRefType.refShowExpandedGroupPinsAttribute, null, "refShowExpandedGroupPins".Localize(), false, null, null ),
            }));

            circuitRootElement = getRootElement(NS, "circuit");
        }

        public static class visualScriptDocumentType
        {
            public static DomNodeType Type;
            public static AttributeInfo versionAttribute;
            public static ChildInfo moduleChild;
            public static ChildInfo connectionChild;
            public static ChildInfo layerFolderChild;
            public static ChildInfo expressionChild;
            public static ChildInfo annotationChild;
            public static ChildInfo prototypeFolderChild;
            public static ChildInfo templateFolderChild;
        }

        public static class visualScriptType
        {
            public static DomNodeType Type;
            public static ChildInfo moduleChild;
            public static ChildInfo connectionChild;
            public static ChildInfo layerFolderChild;
            public static ChildInfo expressionChild;
            public static ChildInfo annotationChild;
        }

        public static class moduleType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo labelAttribute;
            public static AttributeInfo xAttribute;
            public static AttributeInfo yAttribute;
            public static AttributeInfo visibleAttribute;
            public static AttributeInfo showUnconnectedPinsAttribute;
            public static AttributeInfo sourceGuidAttribute;
            public static AttributeInfo validatedAttribute;
            public static ChildInfo dynamicPropertyChild;
        }

        public static class dynamicPropertyType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo categoryAttribute;
            public static AttributeInfo descriptionAttribute;
            public static AttributeInfo converterAttribute;
            public static AttributeInfo editorAttribute;
            public static AttributeInfo valueTypeAttribute;
            public static AttributeInfo stringValueAttribute;
            public static AttributeInfo floatValueAttribute;
            public static AttributeInfo vector3ValueAttribute;
            public static AttributeInfo boolValueAttribute;
            public static AttributeInfo intValueAttribute;
        }

        public static class connectionType
        {
            public static DomNodeType Type;
            public static AttributeInfo labelAttribute;
            public static AttributeInfo outputModuleAttribute;
            public static AttributeInfo outputPinAttribute;
            public static AttributeInfo inputModuleAttribute;
            public static AttributeInfo inputPinAttribute;
        }

        public static class layerFolderType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static ChildInfo layerFolderChild;
            public static ChildInfo moduleRefChild;
        }

        public static class moduleRefType
        {
            public static DomNodeType Type;
            public static AttributeInfo refAttribute;
        }

        public static class expressionType
        {
            public static DomNodeType Type;
            public static AttributeInfo idAttribute;
            public static AttributeInfo labelAttribute;
            public static AttributeInfo scriptAttribute;
        }

        public static class annotationType
        {
            public static DomNodeType Type;
            public static AttributeInfo textAttribute;
            public static AttributeInfo xAttribute;
            public static AttributeInfo yAttribute;
            public static AttributeInfo widthAttribute;
            public static AttributeInfo heightAttribute;
            public static AttributeInfo backcolorAttribute;
            public static AttributeInfo foreColorAttribute;
        }

        public static class prototypeFolderType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static ChildInfo prototypeFolderChild;
            public static ChildInfo prototypeChild;
        }

        public static class prototypeType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static ChildInfo moduleChild;
            public static ChildInfo connectionChild;
        }

        public static class templateFolderType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo referenceFileAttribute;
            public static ChildInfo templateFolderChild;
            public static ChildInfo templateChild;
        }

        public static class templateType
        {
            public static DomNodeType Type;
            public static AttributeInfo guidAttribute;
            public static AttributeInfo labelAttribute;
            public static ChildInfo moduleChild;
        }

        public static class pinType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo typeAttribute;
        }

        public static class groupPinType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo typeAttribute;
            public static AttributeInfo moduleAttribute;
            public static AttributeInfo pinAttribute;
            public static AttributeInfo visibleAttribute;
            public static AttributeInfo indexAttribute;
            public static AttributeInfo pinnedAttribute;
            public static AttributeInfo pinYAttribute;
        }

        public static class groupType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo labelAttribute;
            public static AttributeInfo xAttribute;
            public static AttributeInfo yAttribute;
            public static AttributeInfo visibleAttribute;
            public static AttributeInfo showUnconnectedPinsAttribute;
            public static AttributeInfo sourceGuidAttribute;
            public static AttributeInfo validatedAttribute;
            public static AttributeInfo expandedAttribute;
            public static AttributeInfo showExpandedGroupPinsAttribute;
            public static AttributeInfo autosizeAttribute;
            public static AttributeInfo widthAttribute;
            public static AttributeInfo heightAttribute;
            public static AttributeInfo minwidthAttribute;
            public static AttributeInfo minheightAttribute;
            public static ChildInfo dynamicPropertyChild;
            public static ChildInfo inputChild;
            public static ChildInfo outputChild;
            public static ChildInfo moduleChild;
            public static ChildInfo connectionChild;
            public static ChildInfo annotationChild;
        }

        public static class missingTemplateType
        {
            public static DomNodeType Type;
            public static AttributeInfo guidAttribute;
            public static AttributeInfo labelAttribute;
            public static ChildInfo moduleChild;
        }

        public static class missingModuleType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo labelAttribute;
            public static AttributeInfo xAttribute;
            public static AttributeInfo yAttribute;
            public static AttributeInfo visibleAttribute;
            public static AttributeInfo showUnconnectedPinsAttribute;
            public static AttributeInfo sourceGuidAttribute;
            public static AttributeInfo validatedAttribute;
            public static ChildInfo dynamicPropertyChild;
        }

        public static class moduleTemplateRefType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo labelAttribute;
            public static AttributeInfo xAttribute;
            public static AttributeInfo yAttribute;
            public static AttributeInfo visibleAttribute;
            public static AttributeInfo showUnconnectedPinsAttribute;
            public static AttributeInfo sourceGuidAttribute;
            public static AttributeInfo validatedAttribute;
            public static AttributeInfo guidRefAttribute;
            public static ChildInfo dynamicPropertyChild;
        }

        public static class groupTemplateRefType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo labelAttribute;
            public static AttributeInfo xAttribute;
            public static AttributeInfo yAttribute;
            public static AttributeInfo visibleAttribute;
            public static AttributeInfo showUnconnectedPinsAttribute;
            public static AttributeInfo sourceGuidAttribute;
            public static AttributeInfo validatedAttribute;
            public static AttributeInfo expandedAttribute;
            public static AttributeInfo showExpandedGroupPinsAttribute;
            public static AttributeInfo autosizeAttribute;
            public static AttributeInfo widthAttribute;
            public static AttributeInfo heightAttribute;
            public static AttributeInfo minwidthAttribute;
            public static AttributeInfo minheightAttribute;
            public static AttributeInfo guidRefAttribute;
            public static AttributeInfo refExpandedAttribute;
            public static AttributeInfo refShowExpandedGroupPinsAttribute;
            public static ChildInfo dynamicPropertyChild;
            public static ChildInfo inputChild;
            public static ChildInfo outputChild;
            public static ChildInfo moduleChild;
            public static ChildInfo connectionChild;
            public static ChildInfo annotationChild;
        }

        public static ChildInfo circuitRootElement;
    }
}
