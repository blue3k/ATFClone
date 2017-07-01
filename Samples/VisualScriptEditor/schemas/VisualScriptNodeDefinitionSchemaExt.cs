

namespace VisualScriptSchema
{
    using System.Collections.Generic;
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


    public partial class NodeTypeInfo
    {
        public NodeTypeInfo Parent { get; set; }

        public string NodeDefinitionFile { get; set; }
    }


}


