

using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using Sce.Atf.Adaptation;
using Sce.Atf.Rendering;
using System.Drawing;
using System.Collections.Generic;
using System.Xml;
using Sce.Atf;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to circuit ModuleDynamicSocketss, which is the base circuit element with pins.
    /// It maintains local name and bounds for faster
    /// circuit rendering during editing operations, like dragging ModuleDynamicSocketss and wires.</summary>
    public class ScriptNodeElementType : DomNodeAdapter, ICircuitElementType
    {
        private AttributeInfo m_AttributeForTitle;

        /// <summary>
        /// Gets the element type name
        ///  This is going to be title
        /// </summary>
        public string Name
        {
            get
            {
                //return m_TitleText;
                return GetAttribute<string>(m_AttributeForTitle);
            }
        }

        /// <summary>
        /// Gets the element display name
        ///  This show up at the bottom of the box
        /// </summary>
        public string DisplayName
        {
            // I don't want to display bottom text
            get { return " "; }
        }

        /// <summary>
        /// Gets desired interior size, in pixels, of this element type
        /// </summary>
        public Size InteriorSize
        {
            get { return (m_Image != null) ? new Size(32, 32) : new Size(); }
        }

        /// <summary>
        /// Gets image to draw for this element type</summary>
        public Image Image
        {
            get
            {
                return m_Image;
            }
        }

        /// <summary>
        /// Gets a read-only list of input pins for this element type. For Groups, this list
        /// only includes pins whose Info.Visible property is true. Consider using GetAllInputPins()
        /// or GetInputPin() when using ICircuitGroupPin's InternalPinIndex to look for the
        /// corresponding pin.</summary>
        public IList<ICircuitPin> Inputs
        {
            get
            {
                return m_Inputs;
            }
        }

        /// <summary>
        /// Gets a read-only list of output pins for this element type. For Groups, this list
        /// only includes pins whose Info.Visible property is true. Consider using GetAllOutputPins()
        /// or GetOutputPin() when using ICircuitGroupPin's InternalPinIndex to look for the
        /// corresponding pin.</summary>
        public IList<ICircuitPin> Outputs
        {
            get
            {
                return m_Outputs;
            }
        }

        

        string GetBaseNameFor(ChildInfo childInfo, DomNode child)
        {
            string socketName = childInfo.Name;
            //var nameInfo = child.Type.GetAttributeInfo("Name");
            //if (nameInfo != null)
            //{
            //    var name = child.GetAttribute(nameInfo) as string;
            //    if (!string.IsNullOrEmpty(name))
            //        socketName = name;
            //}
            return socketName;
        }

        protected override void OnNodeSet()
        {
            base.OnNodeSet();

            //var index = DomNode.Type.Name.LastIndexOf(':');
            //m_TitleText = index < 0 ? DomNode.Type.Name : DomNode.Type.Name.Substring(index + 1);


            if(string.IsNullOrEmpty(GetAttribute<string>(VisualScriptBasicSchema.moduleType.labelAttribute)))
                SetAttribute(VisualScriptBasicSchema.moduleType.labelAttribute, GetAttribute<string>(VisualScriptBasicSchema.moduleType.nameAttribute));

            m_AttributeForTitle = VisualScriptBasicSchema.moduleType.labelAttribute;



            // Pull image icon image from node definition
            var nodeDef = DomNode.Type.GetTag<VisualScriptSchema.NodeTypeInfo>();
            m_Image = string.IsNullOrEmpty(nodeDef.Icon) ? null : ResourceUtil.GetImage32(nodeDef.Icon);

            // Create non-string child classes
            if (DomNode.Children != null)
            {
                foreach (var childInfo in DomNode.Type.Children)
                {
                    if (childInfo.IsList) continue;
                    if (DomNode.GetChild(childInfo) != null) continue;

                    var newChild = new DomNode(childInfo.Type);
                    DomNode.SetChild(childInfo, newChild);
                }
            }

            // Add sockets for attributes
            if (DomNode.Type.Attributes != null)
            {
                foreach (var attrInfo in DomNode.Type.Attributes)
                {
                    var attrRule = attrInfo.GetRule<GameDataAttributeRule>();
                    if (attrRule == null || attrRule.SchemaProperty == null) continue;

                    switch (attrRule.SchemaProperty.Socket)
                    {
                        case VisualScriptSchema.SocketType.Input:
                            m_Inputs.Add(new ElementType.Pin(attrInfo.Name, attrInfo.Type.Name, m_Outputs.Count));
                            break;
                        case VisualScriptSchema.SocketType.Output:
                            m_Outputs.Add(new ElementType.Pin(attrInfo.Name, attrInfo.Type.Name, m_Outputs.Count));
                            break;
                        case VisualScriptSchema.SocketType.InOut:
                            m_Inputs.Add(new ElementType.Pin(attrInfo.Name, attrInfo.Type.Name, m_Outputs.Count));
                            m_Outputs.Add(new ElementType.Pin(attrInfo.Name, attrInfo.Type.Name, m_Outputs.Count));
                            break;
                        default:
                            break;
                    }
                }
            }


            // Create child sockets
            if (DomNode.Children != null)
            {
                foreach (var child in DomNode.Children)
                {
                    if (child.ChildInfo.Type != socketType.Type) continue;

                    string socketName = GetBaseNameFor(child.ChildInfo, child);

                    // Only output can vary
                    var newPin = new ElementType.Pin(socketName, VisualScriptSchema.PropertyType.boolean.ToString(), m_Outputs.Count);
                    m_Outputs.Add(newPin);

                }
            }

            DomNode.ChildInserted += (sender, args) =>
            {
                if (DomNode != args.Parent) return;

                if(args.ChildInfo.Type == socketType.Type)
                {
                    string socketName = GetBaseNameFor(args.ChildInfo, args.Child);

                    // Only output can vary
                    var newPin = new ElementType.Pin(socketName, VisualScriptSchema.PropertyType.boolean.ToString(), m_Outputs.Count);
                    m_Outputs.Add(newPin);
                }
            };

            DomNode.ChildRemoved += (sender, args) =>
            {
                if (DomNode != args.Parent) return;

                if (args.ChildInfo.Type == socketType.Type)
                {
                    string socketName = GetBaseNameFor(args.ChildInfo, args.Child);

                    // Only output can vary
                    int iSocket = 0;
                    for (; iSocket < m_Outputs.Count; iSocket++)
                    {
                        var output = m_Outputs[iSocket];
                        if (output.Name == socketName)
                        {
                            m_Outputs.RemoveAt(iSocket);
                            break;
                        }
                    }

                    // fix up index
                    for (; iSocket < m_Outputs.Count; iSocket++)
                    {
                        var output = m_Outputs[iSocket];
                        output.Index = iSocket;
                    }
                }
            };
        }



        Image m_Image;

        List<ICircuitPin> m_Inputs = new List<ICircuitPin>();
        List<ICircuitPin> m_Outputs = new List<ICircuitPin>();
    }
}
