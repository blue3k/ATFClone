

using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using Sce.Atf.Adaptation;
using Sce.Atf.Rendering;
using System.Drawing;
using System.Collections.Generic;
using System.Xml;

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
        /// Gets desired interior size, in pixels, of this element type</summary>
        public Size InteriorSize
        {
            get
            {
                return m_cachedInteriorSize;
            }
        }

        /// <summary>
        /// Gets image to draw for this element type</summary>
        public Image Image
        {
            get
            {
                return m_cachedImage;
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


        ICircuitElementType GetCachedType()
        {
            if (m_cachedType != null)
                return m_cachedType;

            // search in tags
            if (m_cachedType == null)
                m_cachedType = DomNode.GetTag(typeof(ICircuitElementType)) as ICircuitElementType;

            if (m_cachedType == null) // now try domNode type tag
                m_cachedType = DomNode.Type.GetTag<ICircuitElementType>();

            return m_cachedType;
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


            var elementType = GetCachedType();

            if (elementType != null)
            {
                m_cachedImage = elementType.Image;
                m_cachedInteriorSize = elementType.InteriorSize;
                m_Inputs.AddRange(elementType.Inputs);
                m_Outputs.AddRange(elementType.Outputs);
            }

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



        ICircuitElementType m_cachedType;
        Size m_cachedInteriorSize;
        Image m_cachedImage;

        List<ICircuitPin> m_Inputs = new List<ICircuitPin>();
        List<ICircuitPin> m_Outputs = new List<ICircuitPin>();
    }
}
