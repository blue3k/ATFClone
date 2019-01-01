// -------------------------------------------------------------------------------------------------------------------
// Generated code, do not edit
// Command Line:  DomGen "DomGenVS" "VisualScriptBasicSchema.xsd" "VisualScriptBasicSchema.cs" "VisualScriptBasicSchema" "VisualScript" "-enums"
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Sce.Atf;
using Sce.Atf.Dom;

namespace Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema
{
    public static class circuitDocumentType
    {
        static circuitDocumentType()
        {
            Type = new DomNodeType("CircuitDocumentType", circuitType.Type);
            versionAttribute = Type.DefineNewAttributeInfo("version", AttributeType.StringType, "0.1");
            moduleChild = Type.DefineNewChildInfo("module", moduleType.Type, true);
            connectionChild = Type.DefineNewChildInfo("connection", connectionType.Type, true);
            layerFolderChild = Type.DefineNewChildInfo("layerFolder", layerFolderType.Type, true);
            expressionChild = Type.DefineNewChildInfo("expression", expressionType.Type, true);
            annotationChild = Type.DefineNewChildInfo("annotation", annotationType.Type, true);
            prototypeFolderChild = Type.DefineNewChildInfo("prototypeFolder", prototypeFolderType.Type, false);
            templateFolderChild = Type.DefineNewChildInfo("templateFolder", templateFolderType.Type, false);

            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                    new AttributePropertyDescriptor("version".Localize(), versionAttribute, null, "version".Localize(), false, null, null ),
                }));

        }

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

    public static class circuitType
    {
        static circuitType()
        {
            Type = new DomNodeType("circuitType", moduleType.Type);
            moduleChild = Type.DefineNewChildInfo("module", moduleType.Type, true);
            connectionChild = Type.DefineNewChildInfo("connection", connectionType.Type, true);
            layerFolderChild = Type.DefineNewChildInfo("layerFolder", layerFolderType.Type, true);
            expressionChild = Type.DefineNewChildInfo("expression", expressionType.Type, true);
            annotationChild = Type.DefineNewChildInfo("annotation", annotationType.Type, true);
        }

        public static DomNodeType Type;
        public static ChildInfo moduleChild;
        public static ChildInfo connectionChild;
        public static ChildInfo layerFolderChild;
        public static ChildInfo expressionChild;
        public static ChildInfo annotationChild;
    }

    public static class moduleType
    {
        static moduleType()
        {
            Type = new DomNodeType("moduleType");
            nameAttribute = Type.DefineNewAttributeInfo("name", AttributeType.StringType);
            labelAttribute = Type.DefineNewAttributeInfo("label", AttributeType.StringType);
            xAttribute = Type.DefineNewAttributeInfo("x", AttributeType.IntType);
            yAttribute = Type.DefineNewAttributeInfo("y", AttributeType.IntType);
            visibleAttribute = Type.DefineNewAttributeInfo("visible", AttributeType.BooleanType, defaultValue:true);
            showUnconnectedPinsAttribute = Type.DefineNewAttributeInfo("showUnconnectedPins", AttributeType.BooleanType, defaultValue: true);
            sourceGuidAttribute = Type.DefineNewAttributeInfo("sourceGuid", AttributeType.StringType);
            validatedAttribute = Type.DefineNewAttributeInfo("validated", AttributeType.BooleanType, true);
            dynamicPropertyChild = Type.DefineNewChildInfo("dynamicProperty", dynamicPropertyType.Type, true);

            Type.SetIdAttribute(nameAttribute);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("x".Localize(), xAttribute, null, "x".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("y".Localize(), yAttribute, null, "y".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("visible".Localize(), visibleAttribute, null, "visible".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("showUnconnectedPins".Localize(), showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("sourceGuid".Localize(), sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("validated".Localize(), validatedAttribute, null, "validated".Localize(), false, null, null ),
                }));

        }
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
        static dynamicPropertyType()
        {
            Type = new DomNodeType("dynamicPropertyType");
            nameAttribute = Type.DefineNewAttributeInfo("name", AttributeType.StringType);
            categoryAttribute = Type.DefineNewAttributeInfo("category", AttributeType.StringType);
            descriptionAttribute = Type.DefineNewAttributeInfo("description", AttributeType.StringType);
            converterAttribute = Type.DefineNewAttributeInfo("converter", AttributeType.StringType);
            editorAttribute = Type.DefineNewAttributeInfo("editor", AttributeType.StringType);
            valueTypeAttribute = Type.DefineNewAttributeInfo("valueType", AttributeType.StringType);
            stringValueAttribute = Type.DefineNewAttributeInfo("stringValue", AttributeType.StringType);
            floatValueAttribute = Type.DefineNewAttributeInfo("floatValue", AttributeType.FloatType);
            vector3ValueAttribute = Type.DefineNewAttributeInfo("vector3Value", AttributeType.FloatArrayType);
            boolValueAttribute = Type.DefineNewAttributeInfo("boolValue", AttributeType.BooleanType);
            intValueAttribute = Type.DefineNewAttributeInfo("intValue", AttributeType.IntType);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                new AttributePropertyDescriptor("category".Localize(), categoryAttribute, null, "category".Localize(), false, null, null ),
                new AttributePropertyDescriptor("description".Localize(), descriptionAttribute, null, "description".Localize(), false, null, null ),
                new AttributePropertyDescriptor("converter".Localize(), converterAttribute, null, "converter".Localize(), false, null, null ),
                new AttributePropertyDescriptor("editor".Localize(), editorAttribute, null, "editor".Localize(), false, null, null ),
                new AttributePropertyDescriptor("valueType".Localize(), valueTypeAttribute, null, "valueType".Localize(), false, null, null ),
                new AttributePropertyDescriptor("stringValue".Localize(), stringValueAttribute, null, "stringValue".Localize(), false, null, null ),
                new AttributePropertyDescriptor("floatValue".Localize(), floatValueAttribute, null, "floatValue".Localize(), false, null, null ),
                new AttributePropertyDescriptor("vector3Value".Localize(), vector3ValueAttribute, null, "vector3Value".Localize(), false, null, null ),
                new AttributePropertyDescriptor("boolValue".Localize(), boolValueAttribute, null, "boolValue".Localize(), false, null, null ),
                new AttributePropertyDescriptor("intValue".Localize(), intValueAttribute, null, "intValue".Localize(), false, null, null ),
        }));

        }

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
        static connectionType()
        {
            Type = new DomNodeType("connectionType");
            labelAttribute = Type.DefineNewAttributeInfo("label", AttributeType.StringType);
            outputModuleAttribute = Type.DefineNewAttributeInfo("outputModule", AttributeType.DomNodeRefType);
            outputPinAttribute = Type.DefineNewAttributeInfo("outputPin", AttributeType.NameStringType);
            inputModuleAttribute = Type.DefineNewAttributeInfo("inputModule", AttributeType.DomNodeRefType);
            inputPinAttribute = Type.DefineNewAttributeInfo("inputPin", AttributeType.NameStringType);

            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("outputModule".Localize(), outputModuleAttribute, null, "outputModule".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("outputPin".Localize(), outputPinAttribute, null, "outputPin".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("inputModule".Localize(), inputModuleAttribute, null, "inputModule".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("inputPin".Localize(), inputPinAttribute, null, "inputPin".Localize(), false, null, null ),
                }));

        }

        public static DomNodeType Type;
        public static AttributeInfo labelAttribute;
        public static AttributeInfo outputModuleAttribute;
        public static AttributeInfo outputPinAttribute;
        public static AttributeInfo inputModuleAttribute;
        public static AttributeInfo inputPinAttribute;
    }

    public static class layerFolderType
    {
        static layerFolderType()
        {
            Type = new DomNodeType("layerFolderType");
            nameAttribute = Type.DefineNewAttributeInfo("name", AttributeType.StringType);
            layerFolderChild = Type.DefineNewChildInfo("layerFolder", layerFolderType.Type, true);
            moduleRefChild = Type.DefineNewChildInfo("moduleRef", moduleRefType.Type, true);

            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                }));

        }

        public static DomNodeType Type;
        public static AttributeInfo nameAttribute;
        public static ChildInfo layerFolderChild;
        public static ChildInfo moduleRefChild;
    }

    public static class moduleRefType
    {
        static moduleRefType()
        {
            Type = new DomNodeType("moduleRefType");
            refAttribute = Type.DefineNewAttributeInfo("ref", AttributeType.DomNodeRefType);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("ref".Localize(), refAttribute, null, "ref".Localize(), false, null, null ),
                }));

        }

        public static DomNodeType Type;
        public static AttributeInfo refAttribute;
    }

    public static class expressionType
    {
        static expressionType()
        {
            Type = new DomNodeType("expressionType");
            idAttribute = Type.DefineNewAttributeInfo("id", AttributeType.StringType);
            labelAttribute = Type.DefineNewAttributeInfo("label", AttributeType.StringType);
            scriptAttribute = Type.DefineNewAttributeInfo("script", AttributeType.StringType);

            Type.SetIdAttribute(idAttribute);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("id".Localize(), idAttribute, null, "id".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("script".Localize(), scriptAttribute, null, "script".Localize(), false, null, null ),
                }));
        }

        public static DomNodeType Type;
        public static AttributeInfo idAttribute;
        public static AttributeInfo labelAttribute;
        public static AttributeInfo scriptAttribute;
    }

    public static class annotationType
    {
        static annotationType()
        {
            Type = new DomNodeType("annotationType");
            textAttribute = Type.DefineNewAttributeInfo("text", AttributeType.StringType);
            xAttribute = Type.DefineNewAttributeInfo("x", AttributeType.IntType);
            yAttribute = Type.DefineNewAttributeInfo("y", AttributeType.IntType);
            widthAttribute = Type.DefineNewAttributeInfo("width", AttributeType.IntType);
            heightAttribute = Type.DefineNewAttributeInfo("height", AttributeType.IntType);
            backcolorAttribute = Type.DefineNewAttributeInfo("backcolor", AttributeType.IntType, defaultValue:-31);
            foreColorAttribute = Type.DefineNewAttributeInfo("foreColor", AttributeType.IntType, defaultValue:-16777216);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("text".Localize(), textAttribute, null, "text".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("x".Localize(), xAttribute, null, "x".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("y".Localize(), yAttribute, null, "y".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("width".Localize(), widthAttribute, null, "width".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("height".Localize(), heightAttribute, null, "height".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("backcolor".Localize(), backcolorAttribute, null, "backcolor".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("foreColor".Localize(), foreColorAttribute, null, "foreColor".Localize(), false, null, null ),
                }));
        }

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
        static prototypeFolderType()
        {
            Type = new DomNodeType("prototypeFolderType");
            nameAttribute = Type.DefineNewAttributeInfo("name", AttributeType.StringType);
            prototypeFolderChild = Type.DefineNewChildInfo("prototypeFolder", prototypeFolderType.Type, true);
            prototypeChild = Type.DefineNewChildInfo("prototype", prototypeType.Type, true);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                }));


        }
        public static DomNodeType Type;
        public static AttributeInfo nameAttribute;
        public static ChildInfo prototypeFolderChild;
        public static ChildInfo prototypeChild;
    }

    public static class prototypeType
    {
        static prototypeType()
        {
            Type = new DomNodeType("prototypeType");
            nameAttribute = prototypeType.Type.DefineNewAttributeInfo("name", AttributeType.StringType);
            moduleChild = prototypeType.Type.DefineNewChildInfo("module", moduleType.Type, true);
            connectionChild = prototypeType.Type.DefineNewChildInfo("connection", connectionType.Type, true);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), prototypeType.nameAttribute, null, "name".Localize(), false, null, null ),
                }));


        }
        public static DomNodeType Type;
        public static AttributeInfo nameAttribute;
        public static ChildInfo moduleChild;
        public static ChildInfo connectionChild;
    }

    public static class templateFolderType
    {
        static templateFolderType()
        {
            Type = new DomNodeType("templateFolderType");
            nameAttribute = templateFolderType.Type.DefineNewAttributeInfo("name", AttributeType.StringType);
            referenceFileAttribute = templateFolderType.Type.DefineNewAttributeInfo("referenceFile", AttributeType.UriType);
            templateFolderChild = templateFolderType.Type.DefineNewChildInfo("templateFolder", templateFolderType.Type, true);
            templateChild = templateFolderType.Type.DefineNewChildInfo("template", templateType.Type, true);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), templateFolderType.nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("referenceFile".Localize(), templateFolderType.referenceFileAttribute, null, "referenceFile".Localize(), false, null, null ),
                }));

        }

        public static DomNodeType Type;
        public static AttributeInfo nameAttribute;
        public static AttributeInfo referenceFileAttribute;
        public static ChildInfo templateFolderChild;
        public static ChildInfo templateChild;
    }

    public static class templateType
    {
        static templateType()
        {
            Type = new DomNodeType("templateType");
            guidAttribute = Type.DefineNewAttributeInfo("guid", AttributeType.StringType);
            labelAttribute = Type.DefineNewAttributeInfo("label", AttributeType.StringType);
            moduleChild = Type.DefineNewChildInfo("module", moduleType.Type);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(
                new PropertyDescriptor[] {
                    new AttributePropertyDescriptor("guid".Localize(), templateType.guidAttribute, null, "guid".Localize(), false, null, null ),
                    new AttributePropertyDescriptor("label".Localize(), templateType.labelAttribute, null, "label".Localize(), false, null, null ),
                }));

        }

        public static DomNodeType Type;
        public static AttributeInfo guidAttribute;
        public static AttributeInfo labelAttribute;
        public static ChildInfo moduleChild;
    }

    public static class socketType
    {
        static socketType()
        {
            Type = new DomNodeType("socketType");
            nameAttribute = Type.DefineNewAttributeInfo("name", AttributeType.NameStringType);
            typeAttribute = Type.DefineNewAttributeInfo("type", AttributeType.NameStringType);
            isInputAttribute = Type.DefineNewAttributeInfo("isInput", AttributeType.BooleanType);

            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("type".Localize(), typeAttribute, null, "type".Localize(), false, null, null ),
                }));

        }
        public static DomNodeType Type;
        public static AttributeInfo nameAttribute;
        public static AttributeInfo typeAttribute;
        public static AttributeInfo isInputAttribute;
    }

    public static class groupSocketType
    {
        static groupSocketType()
        {
            Type = new DomNodeType("groupSocketType", socketType.Type);
            nameAttribute = socketType.nameAttribute;
            typeAttribute = socketType.typeAttribute;
            moduleAttribute = Type.DefineNewAttributeInfo("module", AttributeType.DomNodeRefType);
            pinAttribute = Type.DefineNewAttributeInfo("pin", AttributeType.NameStringType);
            visibleAttribute = Type.DefineNewAttributeInfo("visible", AttributeType.BooleanType, defaultValue: true);
            indexAttribute = Type.DefineNewAttributeInfo("index", AttributeType.IntType);
            pinNameAttribute = Type.DefineNewAttributeInfo("pinName", AttributeType.NameStringType);
            pinnedAttribute = Type.DefineNewAttributeInfo("pinned", AttributeType.BooleanType, defaultValue: true);
            pinYAttribute = Type.DefineNewAttributeInfo("pinY", AttributeType.IntType);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("type".Localize(), typeAttribute, null, "type".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("module".Localize(), moduleAttribute, null, "module".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("pin".Localize(), pinAttribute, null, "pin".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("visible".Localize(), visibleAttribute, null, "visible".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("index".Localize(), indexAttribute, null, "index".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("pinName".Localize(), pinNameAttribute, null, "pinName".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("pinned".Localize(), pinnedAttribute, null, "pinned".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("pinY".Localize(), pinYAttribute, null, "pinY".Localize(), false, null, null ),
                }));

        }

        public static DomNodeType Type;
        public static AttributeInfo nameAttribute;
        public static AttributeInfo typeAttribute;
        public static AttributeInfo moduleAttribute;
        public static AttributeInfo pinAttribute;
        public static AttributeInfo visibleAttribute;
        public static AttributeInfo indexAttribute;
        public static AttributeInfo pinNameAttribute;
        public static AttributeInfo pinnedAttribute;
        public static AttributeInfo pinYAttribute;
    }

    public static class groupType
    {
        static groupType()
        {

            Type = new DomNodeType("groupType", moduleType.Type);
            nameAttribute = moduleType.nameAttribute;
            labelAttribute = moduleType.labelAttribute;
            xAttribute = moduleType.xAttribute;
            yAttribute = moduleType.yAttribute;
            visibleAttribute = moduleType.visibleAttribute;
            showUnconnectedPinsAttribute = moduleType.showUnconnectedPinsAttribute;
            sourceGuidAttribute = moduleType.sourceGuidAttribute;
            validatedAttribute = moduleType.validatedAttribute;
            expandedAttribute = Type.DefineNewAttributeInfo("expanded", AttributeType.BooleanType, defaultValue: true);
            showExpandedGroupPinsAttribute = Type.DefineNewAttributeInfo("showExpandedGroupPins", AttributeType.BooleanType, defaultValue: true);
            autosizeAttribute = Type.DefineNewAttributeInfo("autosize", AttributeType.BooleanType, defaultValue:true);
            widthAttribute = Type.DefineNewAttributeInfo("width", AttributeType.IntType);
            heightAttribute = Type.DefineNewAttributeInfo("height", AttributeType.IntType);
            minwidthAttribute = Type.DefineNewAttributeInfo("minwidth", AttributeType.IntType);
            minheightAttribute = Type.DefineNewAttributeInfo("minheight", AttributeType.IntType);
            dynamicPropertyChild = moduleType.dynamicPropertyChild;
            inputChild = Type.DefineNewChildInfo("input", groupSocketType.Type, true);
            outputChild = Type.DefineNewChildInfo("output", groupSocketType.Type, true);
            moduleChild = Type.DefineNewChildInfo("module", moduleType.Type, true);
            connectionChild = Type.DefineNewChildInfo("connection", connectionType.Type, true);
            annotationChild = Type.DefineNewChildInfo("annotation", annotationType.Type, true);
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("x".Localize(), xAttribute, null, "x".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("y".Localize(), yAttribute, null, "y".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("visible".Localize(), visibleAttribute, null, "visible".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("showUnconnectedPins".Localize(), showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("sourceGuid".Localize(), sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("validated".Localize(), validatedAttribute, null, "validated".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("expanded".Localize(), expandedAttribute, null, "expanded".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("showExpandedGroupPins".Localize(), showExpandedGroupPinsAttribute, null, "showExpandedGroupPins".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("autosize".Localize(), autosizeAttribute, null, "autosize".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("width".Localize(), widthAttribute, null, "width".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("height".Localize(), heightAttribute, null, "height".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("minwidth".Localize(), minwidthAttribute, null, "minwidth".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("minheight".Localize(), minheightAttribute, null, "minheight".Localize(), false, null, null ),
                }));
        }

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
        static missingTemplateType()
        {
            Type = new DomNodeType("missingTemplateType", templateType.Type);
            guidAttribute = templateType.guidAttribute;
            labelAttribute = templateType.labelAttribute;
            moduleChild = templateType.moduleChild;
            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("guid".Localize(), guidAttribute, null, "guid".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                }));

        }

        public static DomNodeType Type;
        public static AttributeInfo guidAttribute;
        public static AttributeInfo labelAttribute;
        public static ChildInfo moduleChild;
    }

    public static class missingModuleType
    {
        static missingModuleType()
        {
            Type = new DomNodeType("missingModuleType", moduleType.Type);
            nameAttribute = moduleType.nameAttribute;
            labelAttribute = moduleType.labelAttribute;
            xAttribute = moduleType.xAttribute;
            yAttribute = moduleType.yAttribute;
            visibleAttribute = moduleType.visibleAttribute;
            showUnconnectedPinsAttribute = moduleType.showUnconnectedPinsAttribute;
            sourceGuidAttribute = moduleType.sourceGuidAttribute;
            validatedAttribute = moduleType.validatedAttribute;
            dynamicPropertyChild = moduleType.dynamicPropertyChild;

            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("x".Localize(), xAttribute, null, "x".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("y".Localize(), yAttribute, null, "y".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("visible".Localize(), visibleAttribute, null, "visible".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("showUnconnectedPins".Localize(), showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("sourceGuid".Localize(), sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("validated".Localize(), validatedAttribute, null, "validated".Localize(), false, null, null ),
                }));

        }

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
        static moduleTemplateRefType()
        {
            Type = new DomNodeType("moduleTemplateRefType");
            nameAttribute = moduleType.nameAttribute;
            labelAttribute = moduleType.labelAttribute;
            xAttribute = moduleType.xAttribute;
            yAttribute = moduleType.yAttribute;
            visibleAttribute = moduleType.visibleAttribute;
            showUnconnectedPinsAttribute = moduleType.showUnconnectedPinsAttribute;
            sourceGuidAttribute = moduleType.sourceGuidAttribute;
            validatedAttribute = moduleType.validatedAttribute;
            dynamicPropertyChild = moduleType.dynamicPropertyChild;

            guidRefAttribute = Type.DefineNewAttributeInfo("guidRef", AttributeType.DomNodeRefType);

            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("x".Localize(), xAttribute, null, "x".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("y".Localize(), yAttribute, null, "y".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("visible".Localize(), visibleAttribute, null, "visible".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("showUnconnectedPins".Localize(), showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("sourceGuid".Localize(), sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("validated".Localize(), validatedAttribute, null, "validated".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("guidRef".Localize(), guidRefAttribute, null, "guidRef".Localize(), false, null, null ),
                }));

        }

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
        static groupTemplateRefType()
        {
            Type = new DomNodeType("groupTemplateRefType", groupType.Type);
            nameAttribute = groupType.nameAttribute;
            labelAttribute = groupType.labelAttribute;
            xAttribute = groupType.xAttribute;
            yAttribute = groupType.yAttribute;
            visibleAttribute = groupType.visibleAttribute;
            showUnconnectedPinsAttribute = groupType.showUnconnectedPinsAttribute;
            sourceGuidAttribute = groupType.sourceGuidAttribute;
            validatedAttribute = groupType.validatedAttribute;
            dynamicPropertyChild = groupType.dynamicPropertyChild;

            expandedAttribute = groupType.expandedAttribute;
            showExpandedGroupPinsAttribute = groupType.showExpandedGroupPinsAttribute;
            autosizeAttribute = groupType.autosizeAttribute;
            widthAttribute = groupType.widthAttribute;
            heightAttribute = groupType.heightAttribute;
            minwidthAttribute = groupType.minwidthAttribute;
            minheightAttribute = groupType.minheightAttribute;
            inputChild = groupType.inputChild;
            outputChild = groupType.outputChild;
            moduleChild = groupType.moduleChild;
            connectionChild = groupType.connectionChild;
            annotationChild = groupType.annotationChild;

            guidRefAttribute = Type.DefineNewAttributeInfo("guidRef", AttributeType.DomNodeRefType);
            refExpandedAttribute = Type.DefineNewAttributeInfo("refExpanded", AttributeType.BooleanType, defaultValue:false);
            refShowExpandedGroupPinsAttribute = Type.DefineNewAttributeInfo("refShowExpandedGroupPins", AttributeType.BooleanType, defaultValue:true);


            Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                        new AttributePropertyDescriptor("name".Localize(), nameAttribute, null, "name".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("label".Localize(), labelAttribute, null, "label".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("x".Localize(), xAttribute, null, "x".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("y".Localize(), yAttribute, null, "y".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("visible".Localize(), visibleAttribute, null, "visible".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("showUnconnectedPins".Localize(), showUnconnectedPinsAttribute, null, "showUnconnectedPins".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("sourceGuid".Localize(), sourceGuidAttribute, null, "sourceGuid".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("validated".Localize(), validatedAttribute, null, "validated".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("expanded".Localize(), expandedAttribute, null, "expanded".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("showExpandedGroupPins".Localize(), showExpandedGroupPinsAttribute, null, "showExpandedGroupPins".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("autosize".Localize(), autosizeAttribute, null, "autosize".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("width".Localize(), widthAttribute, null, "width".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("height".Localize(), heightAttribute, null, "height".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("minwidth".Localize(), minwidthAttribute, null, "minwidth".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("minheight".Localize(), minheightAttribute, null, "minheight".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("guidRef".Localize(), guidRefAttribute, null, "guidRef".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("refExpanded".Localize(), refExpandedAttribute, null, "refExpanded".Localize(), false, null, null ),
                        new AttributePropertyDescriptor("refShowExpandedGroupPins".Localize(), refShowExpandedGroupPinsAttribute, null, "refShowExpandedGroupPins".Localize(), false, null, null ),
                }));

        }
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

    //public static ChildInfo circuitRootElement;
}
