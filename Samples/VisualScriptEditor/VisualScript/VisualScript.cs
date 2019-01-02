//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System.Collections.Generic;
using System.Linq;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;
using Sce.Atf.Controls.Adaptable.Graphs.CircuitBasicSchema;

using VisualScript.VisualScriptBasicSchema;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to a circuit and observable context with change notification events</summary>
    public class VisualScript : Sce.Atf.Controls.Adaptable.Graphs.Circuit, IGraph<ScriptNode, ScriptNodeConnection, ICircuitPin>
    {
        /// <summary>
        /// Performs initialization when the adapter is connected to the circuit's DomNode</summary>
        protected override void OnNodeSet()
        {
            // cache these list wrapper objects
            m_modules = new DomNodeListAdapter<ScriptNode>(DomNode, visualScriptType.moduleChild);
            m_connections = new DomNodeListAdapter<ScriptNodeConnection>(DomNode, visualScriptType.connectionChild);
            new DomNodeListAdapter<ScriptAnnotation>(DomNode, visualScriptType.annotationChild);
            base.OnNodeSet();
        }

        /// <summary>
        /// Gets ChildInfo for Elements (circuit elements with pins) in circuit</summary>
        protected override ChildInfo ElementChildInfo
        {
            get { return visualScriptType.moduleChild; }
        }

        /// <summary>
        /// Gets ChildInfo for Wires (connections) in circuit</summary>
        protected override ChildInfo WireChildInfo
        {
            get { return visualScriptType.connectionChild; }
        }

        // optional child info
        /// <summary>
        /// Gets ChildInfo for annotations (comments) in circuit.
        /// Return null if annotations are not supported.</summary>
        protected override ChildInfo AnnotationChildInfo
        {
            get { return visualScriptType.annotationChild; }
        }

        #region IGraph Members

        /// <summary>
        /// Gets all visible nodes in the circuit</summary>
        ///<remarks>IGraph.Nodes is called during circuit rendering, and picking(in reverse order).</remarks>
        IEnumerable<ScriptNode> IGraph<ScriptNode, ScriptNodeConnection, ICircuitPin>.Nodes
        {
            get
            {
                return m_modules.Where(x => x.Visible);
            }
        }

        /// <summary>
        /// Gets enumeration of all connections between visible nodes in the circuit</summary>
        IEnumerable<ScriptNodeConnection> IGraph<ScriptNode, ScriptNodeConnection, ICircuitPin>.Edges
        {
            get
            {
                  return m_connections.Where(x => x.InputElement != null && x.OutputElement != null && x.InputElement.Visible && x.OutputElement.Visible);
            }
        }


        #endregion
       

        private DomNodeListAdapter<ScriptNode> m_modules;
        private DomNodeListAdapter<ScriptNodeConnection> m_connections;
    }
}
