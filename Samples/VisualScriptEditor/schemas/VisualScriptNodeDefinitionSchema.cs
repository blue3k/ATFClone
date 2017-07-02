﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 
namespace VisualScriptSchema {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://blue3k.com/1_0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://blue3k.com/1_0", IsNullable=false)]
    public partial class NodeTypeDefinitions {
        
        private NodeTypeInfo[] nodeTypeInfoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NodeTypeInfo")]
        public NodeTypeInfo[] NodeTypeInfo {
            get {
                return this.nodeTypeInfoField;
            }
            set {
                this.nodeTypeInfoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://blue3k.com/1_0")]
    public partial class NodeTypeInfo {
        
        private Property[] propertyField;
        
        private EditorSocket[] outputsField;
        
        private string nameField;
        
        private bool isAbstractField;
        
        private string baseField;
        
        private string categoryField;
        
        private string nodeTypeField;
        
        private string iconField;
        
        private string descriptionField;
        
        public NodeTypeInfo() {
            this.isAbstractField = false;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Property")]
        public Property[] Property {
            get {
                return this.propertyField;
            }
            set {
                this.propertyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Outputs")]
        public EditorSocket[] Outputs {
            get {
                return this.outputsField;
            }
            set {
                this.outputsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool IsAbstract {
            get {
                return this.isAbstractField;
            }
            set {
                this.isAbstractField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Base {
            get {
                return this.baseField;
            }
            set {
                this.baseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Category {
            get {
                return this.categoryField;
            }
            set {
                this.categoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NodeType {
            get {
                return this.nodeTypeField;
            }
            set {
                this.nodeTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Icon {
            get {
                return this.iconField;
            }
            set {
                this.iconField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://blue3k.com/1_0")]
    public partial class Property {
        
        private string nameField;
        
        private PropertyType typeField;
        
        private string defaultField;
        
        private bool allowMultipleInputField;
        
        private bool allowMultipleOutputField;
        
        private bool isArrayField;
        
        private string minLengthField;
        
        private SocketType socketField;
        
        public Property() {
            this.allowMultipleInputField = false;
            this.allowMultipleOutputField = true;
            this.isArrayField = false;
            this.minLengthField = "0";
            this.socketField = SocketType.None;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PropertyType Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Default {
            get {
                return this.defaultField;
            }
            set {
                this.defaultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool AllowMultipleInput {
            get {
                return this.allowMultipleInputField;
            }
            set {
                this.allowMultipleInputField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AllowMultipleOutput {
            get {
                return this.allowMultipleOutputField;
            }
            set {
                this.allowMultipleOutputField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool IsArray {
            get {
                return this.isArrayField;
            }
            set {
                this.isArrayField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("0")]
        public string MinLength {
            get {
                return this.minLengthField;
            }
            set {
                this.minLengthField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(SocketType.None)]
        public SocketType Socket {
            get {
                return this.socketField;
            }
            set {
                this.socketField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://blue3k.com/1_0")]
    public enum PropertyType {
        
        /// <remarks/>
        Event,
        
        /// <remarks/>
        boolean,
        
        /// <remarks/>
        @int,
        
        /// <remarks/>
        @float,
        
        /// <remarks/>
        @double,
        
        /// <remarks/>
        @string,
        
        /// <remarks/>
        FixedString,
        
        /// <remarks/>
        vector3,
        
        /// <remarks/>
        Socket,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://blue3k.com/1_0")]
    public enum SocketType {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Input,
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        InOut,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://blue3k.com/1_0")]
    public partial class EditorSocket {
        
        private EditorSocketLink[] linkField;
        
        private string textField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Link")]
        public EditorSocketLink[] Link {
            get {
                return this.linkField;
            }
            set {
                this.linkField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://blue3k.com/1_0")]
    public partial class EditorSocketLink {
        
        private string targetField;
        
        private string socketField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Target {
            get {
                return this.targetField;
            }
            set {
                this.targetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Socket {
            get {
                return this.socketField;
            }
            set {
                this.socketField = value;
            }
        }
    }
}
