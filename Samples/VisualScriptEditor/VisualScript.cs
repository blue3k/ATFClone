//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System.Collections.Generic;
using System.Linq;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to a circuit and observable context with change notification events</summary>
    public class VisualScript : Sce.Atf.Controls.Adaptable.Graphs.Circuit, IGraph<VisualScriptModule, VisualScriptConnection, ICircuitPin>
    {
        /// <summary>
        /// Performs initialization when the adapter is connected to the circuit's DomNode</summary>
        protected override void OnNodeSet()
        {
            // cache these list wrapper objects
            m_modules = new DomNodeListAdapter<VisualScriptModule>(DomNode, VisualScriptBasicSchema.visualScriptType.moduleChild);
            m_connections = new DomNodeListAdapter<VisualScriptConnection>(DomNode, VisualScriptBasicSchema.visualScriptType.connectionChild);
            new DomNodeListAdapter<VisualScriptAnnotation>(DomNode, VisualScriptBasicSchema.visualScriptType.annotationChild);
            base.OnNodeSet();
        }

        /// <summary>
        /// Gets ChildInfo for Elements (circuit elements with pins) in circuit</summary>
        protected override ChildInfo ElementChildInfo
        {
            get { return VisualScriptBasicSchema.visualScriptType.moduleChild; }
        }

        /// <summary>
        /// Gets ChildInfo for Wires (connections) in circuit</summary>
        protected override ChildInfo WireChildInfo
        {
            get { return VisualScriptBasicSchema.visualScriptType.connectionChild; }
        }

        // optional child info
        /// <summary>
        /// Gets ChildInfo for annotations (comments) in circuit.
        /// Return null if annotations are not supported.</summary>
        protected override ChildInfo AnnotationChildInfo
        {
            get { return VisualScriptBasicSchema.visualScriptType.annotationChild; }
        }

        #region IGraph Members

        /// <summary>
        /// Gets all visible nodes in the circuit</summary>
        ///<remarks>IGraph.Nodes is called during circuit rendering, and picking(in reverse order).</remarks>
        IEnumerable<VisualScriptModule> IGraph<VisualScriptModule, VisualScriptConnection, ICircuitPin>.Nodes
        {
            get
            {
                return m_modules.Where(x => x.Visible);
            }
        }

        /// <summary>
        /// Gets enumeration of all connections between visible nodes in the circuit</summary>
        IEnumerable<VisualScriptConnection> IGraph<VisualScriptModule, VisualScriptConnection, ICircuitPin>.Edges
        {
            get
            {
                  return m_connections.Where(x => x.InputElement.Visible && x.OutputElement.Visible);
            }
        }


        #endregion
       

        private DomNodeListAdapter<VisualScriptModule> m_modules;
        private DomNodeListAdapter<VisualScriptConnection> m_connections;
    }
}
