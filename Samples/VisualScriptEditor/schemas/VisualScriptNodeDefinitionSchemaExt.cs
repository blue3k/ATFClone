

namespace VisualScriptSchema
{
    using Sce.Atf.Dom;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using VisualScript;

    public partial class Node
    {
        List<ScriptNodeSocket> m_SocketList = new List<ScriptNodeSocket>();

        public List<ScriptNodeSocket> GetSocketList()
        {
            return m_SocketList;
        }
    }

    public partial class EnumTypeInfo
    {
        [IgnoreDataMember]
        [System.Xml.Serialization.XmlIgnore]
        public string DefinitionFile { get; set; }

        [IgnoreDataMember]
        [System.Xml.Serialization.XmlIgnore]
        public AttributeType SingleValueType { get; set; }

        [IgnoreDataMember]
        [System.Xml.Serialization.XmlIgnore]
        public AttributeType ArrayValueType { get; set; }

        [IgnoreDataMember]
        [System.Xml.Serialization.XmlIgnore]
        public StringEnumRule EnumRule { get; set; }
    }

    public partial class NodeTypeInfo
    {
        [IgnoreDataMember]
        [System.Xml.Serialization.XmlIgnore]
        public NodeTypeInfo Parent { get; set; }

        [IgnoreDataMember]
        [System.Xml.Serialization.XmlIgnore]
        public string NodeDefinitionFile { get; set; }
    }


}


