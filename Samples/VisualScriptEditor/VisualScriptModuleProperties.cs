//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Sce.Atf;
using Sce.Atf.Controls.PropertyEditing;
using Sce.Atf.Dom;
using PropertyDescriptor = Sce.Atf.Dom.PropertyDescriptor;

namespace VisualScript
{
    /// <summary>
    /// Provides property descriptors on circuit elements, dynamically, using optional user-specified data.</summary>
    public class VisualScriptModuleProperties : CustomTypeDescriptorNodeAdapter, IDynamicTypeDescriptor
    {
        /// <summary>
        /// Creates an array of property descriptors that are associated with the adapted DomNode's
        /// DomNodeType. No duplicates are in the array (based on the property descriptor's Name
        /// property).</summary>
        /// <returns>Array of property descriptors</returns>
        protected override System.ComponentModel.PropertyDescriptor[] GetPropertyDescriptors()
        {
            System.ComponentModel.PropertyDescriptor[] baseDescriptors = base.GetPropertyDescriptors();

            IList<DomNode> dynamicPropertyChildren = DomNode.GetChildList(VisualScriptBasicSchema.groupType.dynamicPropertyChild);
            if (dynamicPropertyChildren.Count == 0)
                return baseDescriptors;

            var result = new List<System.ComponentModel.PropertyDescriptor>(baseDescriptors);
            int childIndex = 0;
            foreach (var child in dynamicPropertyChildren)
            {
                var displayName = (string)child.GetAttribute(VisualScriptBasicSchema.dynamicPropertyType.nameAttribute);
                var category = (string)child.GetAttribute(VisualScriptBasicSchema.dynamicPropertyType.categoryAttribute);
                var description = (string)child.GetAttribute(VisualScriptBasicSchema.dynamicPropertyType.descriptionAttribute);
                bool readOnly = false;
                var editorTypeAndParameters = (string)child.GetAttribute(VisualScriptBasicSchema.dynamicPropertyType.editorAttribute);
                object editor = CreateObject(editorTypeAndParameters);
                var typeConverterAndParameters = (string)child.GetAttribute(VisualScriptBasicSchema.dynamicPropertyType.converterAttribute);
                var typeConverter = (TypeConverter)CreateObject(typeConverterAndParameters);
                string valueType = (string)child.GetAttribute(VisualScriptBasicSchema.dynamicPropertyType.valueTypeAttribute);
                PropertyDescriptor newDescriptor;
                if (valueType == "stringValue")
                {
                    newDescriptor = new ChildAttributePropertyDescriptor(
                        displayName,
                        VisualScriptBasicSchema.dynamicPropertyType.stringValueAttribute,
                        VisualScriptBasicSchema.moduleType.dynamicPropertyChild,
                        childIndex,
                        category, description, readOnly, editor, typeConverter);
                }
                else if (valueType == "floatValue")
                {
                    newDescriptor = new ChildAttributePropertyDescriptor(
                        displayName,
                        VisualScriptBasicSchema.dynamicPropertyType.floatValueAttribute,
                        VisualScriptBasicSchema.moduleType.dynamicPropertyChild,
                        childIndex,
                        category, description, readOnly, editor, typeConverter);
                }
                else if (valueType == "vector3Value")
                {
                    newDescriptor = new ChildAttributePropertyDescriptor(
                        displayName,
                        VisualScriptBasicSchema.dynamicPropertyType.vector3ValueAttribute,
                        VisualScriptBasicSchema.moduleType.dynamicPropertyChild,
                        childIndex,
                        category, description, readOnly, editor, typeConverter);
                }
                else if (valueType == "boolValue")
                {
                    newDescriptor = new ChildAttributePropertyDescriptor(
                        displayName,
                        VisualScriptBasicSchema.dynamicPropertyType.boolValueAttribute,
                        VisualScriptBasicSchema.moduleType.dynamicPropertyChild,
                        childIndex,
                        category, description, readOnly, editor, typeConverter);
                }
                else if (valueType == "intValue")
                {
                    newDescriptor = new ChildAttributePropertyDescriptor(
                        displayName,
                        VisualScriptBasicSchema.dynamicPropertyType.intValueAttribute,
                        VisualScriptBasicSchema.moduleType.dynamicPropertyChild,
                        childIndex,
                        category, description, readOnly, editor, typeConverter);
                }
                else
                    throw new InvalidOperationException("Unknown valueType attribute '" + valueType +
                        "' for dynamic property: " + displayName);

                result.Add(newDescriptor);
                childIndex++;
            }

            return result.ToArray();
        }

        /// <summary>
        /// Returns true iff this custom type descriptor can provide a PropertyDescriptorCollection
        /// (via GetProperties) that is the same for all instances of this type of object
        /// and that can be permanently cached</summary>
        /// <remarks>Returning 'true' greatly improves performance.</remarks>
        public bool CacheableProperties { get { return false; } }

        // This code came from Sce.Atf.Dom.PropertyDescriptor.
        private object CreateObject(string typeName)
        {
            string paramString = string.Empty;
            if (!string.IsNullOrEmpty(typeName))
            {
                // check for params
                int colonIndex = typeName.IndexOf(':');
                if (colonIndex >= 0)
                {
                    int paramsIndex = colonIndex + 1;
                    paramString = typeName.Substring(paramsIndex, typeName.Length - paramsIndex);
                    typeName = typeName.Substring(0, colonIndex);
                }

                // create object from type name
                Type objectType = Type.GetType(typeName);
                if (objectType == null)
                    throw new InvalidOperationException("Couldn't find type " + typeName);

                // initialize with params
                object obj = Activator.CreateInstance(objectType);
                IAnnotatedParams annotatedObj = obj as IAnnotatedParams;
                if (annotatedObj != null)
                {
                    string[] parameters = null;

                    if (!string.IsNullOrEmpty(paramString))
                        parameters = paramString.Split(',');
                    //else
                    //    parameters = TryGetEnumeration(domNodeType, annotation);
                    if (parameters != null)
                        annotatedObj.Initialize(parameters);
                }

                return obj;
            }
            else
            {
                return null;
            }
        }
    }
}
