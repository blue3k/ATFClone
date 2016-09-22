// -------------------------------------------------------------------------------------------------------------------
// Generated code, do not edit
// Command Line:  DomGen "DomGenVS" "Schema.xsd" "Schema.cs" "Schema" "SimpleDomEditorSample" "-enums"
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Sce.Atf;
using Sce.Atf.Dom;

namespace SimpleDomEditorSample
{
    public static class Schema
    {
        public const string NS = "eventSequence_1_0";

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
            eventSequenceType.Type = getNodeType("eventSequence_1_0", "eventSequenceType");
            eventSequenceType.eventChild = eventSequenceType.Type.GetChildInfo("event");
            eventSequenceType.Type.Define(new ExtensionInfo<EventSequenceDocument>());
            eventSequenceType.Type.Define(new ExtensionInfo<EventSequenceContext>());
            eventSequenceType.Type.Define(new ExtensionInfo<Sce.Atf.Dom.MultipleHistoryContext>());
            eventSequenceType.Type.Define(new ExtensionInfo<EventSequence>());
            eventSequenceType.Type.Define(new ExtensionInfo<Sce.Atf.Dom.ReferenceValidator>());
            eventSequenceType.Type.Define(new ExtensionInfo<Sce.Atf.Dom.UniqueIdValidator>());
            eventSequenceType.Type.Define(new ExtensionInfo<Sce.Atf.Dom.DomNodeQueryable>());

            eventType.Type = getNodeType("eventSequence_1_0", "eventType");
            eventType.nameAttribute = eventType.Type.GetAttributeInfo("name");
            eventType.EventTypeAttribute = eventType.Type.GetAttributeInfo("EventType");
            eventType.timeAttribute = eventType.Type.GetAttributeInfo("time");
            eventType.durationAttribute = eventType.Type.GetAttributeInfo("duration");
            eventType.ShaderIDAttribute = eventType.Type.GetAttributeInfo("ShaderID");
            eventType.resourceChild = eventType.Type.GetChildInfo("resource");
            eventType.Type.Define(new ExtensionInfo<Event>());
            eventType.Type.Define(new ExtensionInfo<EventContext>());
            eventType.Type.SetTag(new NodeTypePaletteItem(eventType.Type, "eventType", "Event in a sequence".Localize(), Resources.EventImage));
            eventType.nameAttribute.DefaultValue = "Event".Localize();
            eventType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("Name".Localize(), eventType.nameAttribute, null, "Name of item".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("event type".Localize(), eventType.EventTypeAttribute, null, "event type desc".Localize(), false, new Sce.Atf.Controls.PropertyEditing.LongEnumEditor(typeof(eventEnumType)), new Sce.Atf.Controls.PropertyEditing.ExclusiveEnumTypeConverter(typeof(eventEnumType)) ),
                 new AttributePropertyDescriptor("time".Localize(), eventType.timeAttribute, null, "time".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("duration".Localize(), eventType.durationAttribute, null, "duration".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("Shader ID".Localize(), eventType.ShaderIDAttribute, null, "Shader ID".Localize(), false, new Sce.Atf.Controls.PropertyEditing.BoundedIntEditor(10,1000), new Sce.Atf.Controls.PropertyEditing.BoundedIntConverter() ),
            }));

            resourceType.Type = getNodeType("eventSequence_1_0", "resourceType");
            resourceType.nameAttribute = resourceType.Type.GetAttributeInfo("name");
            resourceType.sizeAttribute = resourceType.Type.GetAttributeInfo("size");
            resourceType.compressedAttribute = resourceType.Type.GetAttributeInfo("compressed");
            resourceType.Type.Define(new ExtensionInfo<Resource>());
            resourceType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), resourceType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("size".Localize(), resourceType.sizeAttribute, null, "size".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("compressed".Localize(), resourceType.compressedAttribute, null, "compressed".Localize(), false, null, null ),
            }));

            animationResourceType.Type = getNodeType("eventSequence_1_0", "animationResourceType");
            animationResourceType.nameAttribute = animationResourceType.Type.GetAttributeInfo("name");
            animationResourceType.sizeAttribute = animationResourceType.Type.GetAttributeInfo("size");
            animationResourceType.compressedAttribute = animationResourceType.Type.GetAttributeInfo("compressed");
            animationResourceType.tracksAttribute = animationResourceType.Type.GetAttributeInfo("tracks");
            animationResourceType.durationAttribute = animationResourceType.Type.GetAttributeInfo("duration");
            animationResourceType.nameAttribute.DefaultValue = "Animation".Localize();
            animationResourceType.Type.SetTag(new NodeTypePaletteItem(animationResourceType.Type, "animationResourceType", "Animation resource".Localize(), Resources.AnimationImage));
            animationResourceType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), animationResourceType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("size".Localize(), animationResourceType.sizeAttribute, null, "size".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("compressed".Localize(), animationResourceType.compressedAttribute, null, "compressed".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("tracks".Localize(), animationResourceType.tracksAttribute, null, "tracks".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("duration".Localize(), animationResourceType.durationAttribute, null, "duration".Localize(), false, null, null ),
            }));

            geometryResourceType.Type = getNodeType("eventSequence_1_0", "geometryResourceType");
            geometryResourceType.nameAttribute = geometryResourceType.Type.GetAttributeInfo("name");
            geometryResourceType.sizeAttribute = geometryResourceType.Type.GetAttributeInfo("size");
            geometryResourceType.compressedAttribute = geometryResourceType.Type.GetAttributeInfo("compressed");
            geometryResourceType.bonesAttribute = geometryResourceType.Type.GetAttributeInfo("bones");
            geometryResourceType.verticesAttribute = geometryResourceType.Type.GetAttributeInfo("vertices");
            geometryResourceType.primitiveTypeAttribute = geometryResourceType.Type.GetAttributeInfo("primitiveType");
            geometryResourceType.nameAttribute.DefaultValue = "Geometry".Localize();
            geometryResourceType.Type.SetTag(new NodeTypePaletteItem(geometryResourceType.Type, "geometryResourceType", "Geometry resource".Localize(), Resources.GeometryImage));
            geometryResourceType.Type.SetTag(new System.ComponentModel.PropertyDescriptorCollection(new PropertyDescriptor[] {
                 new AttributePropertyDescriptor("name".Localize(), geometryResourceType.nameAttribute, null, "name".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("size".Localize(), geometryResourceType.sizeAttribute, null, "size".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("compressed".Localize(), geometryResourceType.compressedAttribute, null, "compressed".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("bones".Localize(), geometryResourceType.bonesAttribute, null, "bones".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("vertices".Localize(), geometryResourceType.verticesAttribute, null, "vertices".Localize(), false, null, null ),
                 new AttributePropertyDescriptor("primitiveType".Localize(), geometryResourceType.primitiveTypeAttribute, null, "primitiveType".Localize(), false, new Sce.Atf.Controls.PropertyEditing.LongEnumEditor(typeof(PrimitiveEnumType)), new Sce.Atf.Controls.PropertyEditing.ExclusiveEnumTypeConverter(typeof(PrimitiveEnumType)) ),
            }));

            eventSequenceRootElement = getRootElement(NS, "eventSequence");
        }

        public static class eventSequenceType
        {
            public static DomNodeType Type;
            public static ChildInfo eventChild;
        }

        public static class eventType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo EventTypeAttribute;
            public static AttributeInfo timeAttribute;
            public static AttributeInfo durationAttribute;
            public static AttributeInfo ShaderIDAttribute;
            public static ChildInfo resourceChild;
        }

        public static class resourceType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo sizeAttribute;
            public static AttributeInfo compressedAttribute;
        }

        public static class animationResourceType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo sizeAttribute;
            public static AttributeInfo compressedAttribute;
            public static AttributeInfo tracksAttribute;
            public static AttributeInfo durationAttribute;
        }

        public static class geometryResourceType
        {
            public static DomNodeType Type;
            public static AttributeInfo nameAttribute;
            public static AttributeInfo sizeAttribute;
            public static AttributeInfo compressedAttribute;
            public static AttributeInfo bonesAttribute;
            public static AttributeInfo verticesAttribute;
            public static AttributeInfo primitiveTypeAttribute;
        }

        public enum eventEnumType
        {
             Pixie1,
             Pixie2,
             Pixie3,
             Pixie4,
             Pixie5,
             Bigbird,
        }


        public enum PrimitiveEnumType
        {
             Lines,
             Line_Strips,
             Polygons,
             Polylist,
             Triangles,
             Triangle_Strips,
             Bezier_Curves,
             Bezier_Surfaces,
             Subdivision_Surfaces,
        }


        public static ChildInfo eventSequenceRootElement;
    }
}
