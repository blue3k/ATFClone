//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Adaptation;
using Sce.Atf.Controls.Adaptable.Graphs;
using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapter for connections in a circuit</summary>
    public class ScriptNodeConnection : Wire, IGraphEdge<ScriptNode, ICircuitPin>
    {
       
        #region IGraphEdge Members

        /// <summary>
        /// Gets edge's source node</summary>
        ScriptNode IGraphEdge<ScriptNode>.FromNode
        {
            get { return OutputElement.Cast<ScriptNode>(); }
        }

        /// <summary>
        /// Gets the route taken from the source node</summary>
        ICircuitPin IGraphEdge<ScriptNode, ICircuitPin>.FromRoute
        {
            get { return OutputPin; }
        }

        /// <summary>
        /// Gets edge's destination node</summary>
        ScriptNode IGraphEdge<ScriptNode>.ToNode
        {
            get { return InputElement.Cast<ScriptNode>(); }
        }

        /// <summary>
        /// Gets the route taken to the destination node</summary>
        ICircuitPin IGraphEdge<ScriptNode, ICircuitPin>.ToRoute
        {
            get { return InputPin; }
        }

        /// <summary>
        /// Gets label on connection</summary>
        string IGraphEdge<ScriptNode>.Label
        {
            get { return Label; }
        }

        #endregion

        /// <summary>
        /// Gets label attribute on connection</summary>
        protected override AttributeInfo LabelAttribute
        {
            get { return VisualScriptBasicSchema.connectionType.labelAttribute; }
        }

        /// <summary>
        /// Gets input module attribute for connection</summary>
        protected override AttributeInfo InputElementAttribute
        {
            get { return VisualScriptBasicSchema.connectionType.inputModuleAttribute; }
        }

        /// <summary>
        /// Gets output module attribute for connection</summary>
        protected override AttributeInfo OutputElementAttribute
        {
            get { return VisualScriptBasicSchema.connectionType.outputModuleAttribute; }
        }

        /// <summary>
        /// Gets input pin attribute for connection</summary>
        protected override AttributeInfo InputPinAttribute
        {
            get { return VisualScriptBasicSchema.connectionType.inputPinAttribute; }
        }

        /// <summary>
        /// Gets output pin attribute for connection</summary>
        protected override AttributeInfo OutputPinAttribute
        {
            get { return VisualScriptBasicSchema.connectionType.outputPinAttribute; }
        }

    }
}
