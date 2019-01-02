//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sce.Atf.Adaptation;
using Sce.Atf.Dom;

namespace Sce.Atf.Controls.Adaptable.Graphs
{
    /// <summary>
    /// Adapts DomNode to connection in a circuit</summary>
    public abstract class Wire : DomNodeAdapter, IGraphEdge<Element, ICircuitPin>
    {

        /// <summary>
        /// Gets label attribute on connection</summary>
        protected abstract AttributeInfo LabelAttribute { get; }
        /// <summary>
        /// Gets input module attribute for connection</summary>
        protected abstract AttributeInfo InputElementAttribute { get; }
        /// <summary>
        /// Gets output module attribute for connection</summary>
        protected abstract AttributeInfo OutputElementAttribute { get; }
        /// <summary>
        /// Gets input pin attribute for connection</summary>
        protected abstract AttributeInfo InputPinAttribute { get; }
        /// <summary>
        /// Gets output pin attribute for connection</summary>
        protected abstract AttributeInfo OutputPinAttribute { get; }


        /// <summary>
        /// Gets or sets the element whose output pin this wire connects to</summary>
        public virtual Element OutputElement
        {
            get { return GetReference<Element>(OutputElementAttribute); }
            set { SetReference(OutputElementAttribute, value); }
        }

        /// <summary>
        /// Gets or sets the output pin, i.e., pin on element that receives connection as output</summary>
        public virtual ICircuitPin OutputPin
        {
            get
            {
                NameString pinIndex = GetAttribute<NameString>(OutputPinAttribute);             
                return OutputElement.OutputPin(pinIndex);
            }
            set
            {
                DomNode.SetAttribute(OutputPinAttribute, value.Name);
            }
        }

        /// <summary>
        /// Gets or sets the element whose input pin this wire connects to</summary>
        public virtual Element InputElement
        {
            get { return GetReference<Element>(InputElementAttribute); }
            set { SetReference(InputElementAttribute, value); }
        }

        /// <summary>
        /// Gets or sets input pin, i.e., pin on element that receives connection as input</summary>
        public virtual ICircuitPin InputPin
        {
            get
            {
                NameString pinName = GetAttribute<NameString>(InputPinAttribute);
                var inputElement = InputElement;
                return inputElement != null ? inputElement.InputPin(pinName) : null;
            }
            set
            {
                DomNode.SetAttribute(InputPinAttribute, value.Name);
            }
        }

        /// <summary>
        /// Sets output pin for an element</summary>
        /// <param name="outputElement">Element</param>
        /// <param name="outputPin">Output pin</param>
        public virtual void SetOutput(Element outputElement, ICircuitPin outputPin)
        {
            OutputElement = outputElement;
            OutputPin = outputPin;
        }

        /// <summary>
        /// Sets input pin for an element</summary>
        /// <param name="inputElement">Element</param>
        /// <param name="inputPin">Input pin</param>
        public virtual void SetInput(Element inputElement, ICircuitPin inputPin)
        {
            InputElement = inputElement;
            InputPin = inputPin;
        }

        /// <summary>
        /// Gets or sets label on connection</summary>
        public virtual string Label
        {
            get { return GetAttribute<string>(LabelAttribute); }
            set { SetAttribute(LabelAttribute, value); }
        }

        #region IGraphEdge Members

        /// <summary>
        /// Gets edge's source node</summary>
        Element IGraphEdge<Element>.FromNode
        {
            get { return OutputElement; }
        }

        /// <summary>
        /// Gets the route taken from the source node</summary>
        ICircuitPin IGraphEdge<Element, ICircuitPin>.FromRoute
        {
            get { return OutputPin; }
        }

        /// <summary>
        /// Gets edge's destination node</summary>
        Element IGraphEdge<Element>.ToNode
        {
            get { return InputElement; }
        }

        /// <summary>
        /// Gets the route taken to the destination node</summary>
        ICircuitPin IGraphEdge<Element, ICircuitPin>.ToRoute
        {
            get { return InputPin; }
        }

        /// <summary>
        /// Gets edge's label</summary>
        string IGraphEdge<Element>.Label
        {
            get { return Label; }
        }

        #endregion

        /// <summary>
        /// Sets input and output PinTarget for this connection</summary>
        public void SetPinTarget()
        {
            if (InputPin != null)
            {
                bool isInputElementRef = InputElement.DomNode.Is<IReference<DomNode>>();

                if (InputPin.Is<GroupPin>())
                {
                    if (isInputElementRef)
                    {
                        var pinTarget = InputPin.Cast<GroupPin>().PinTarget;
                        InputPinTarget = new PinTarget(pinTarget.LeafDomNode, pinTarget.LeafPinName,
                                                       InputElement.DomNode);
                    }
                    else
                        InputPinTarget = InputPin.Cast<GroupPin>().PinTarget;
                }
                else
                {
                    if (isInputElementRef)
                    {
                        var reference = InputElement.As<IReference<DomNode>>();
                        InputPinTarget = new PinTarget(reference.Target, InputPin.Name, InputElement.DomNode);
                    }
                    else
                        InputPinTarget = new PinTarget(InputElement.DomNode, InputPin.Name, null);
                }
                Debug.Assert(InputPinTarget != null, "sanity check");
            }

            if (OutputPin != null)
            {
                bool isOutputElementRef = OutputElement.DomNode.Is<IReference<DomNode>>();
                if (OutputPin.Is<GroupPin>())
                {
                    if (isOutputElementRef)
                    {
                        var pinTarget = OutputPin.Cast<GroupPin>().PinTarget;
                        OutputPinTarget = new PinTarget(pinTarget.LeafDomNode, pinTarget.LeafPinName,
                                                        OutputElement.DomNode);
                    }
                    else
                        OutputPinTarget = OutputPin.Cast<GroupPin>().PinTarget;
                }
                else
                {
                    if (isOutputElementRef)
                    {
                        var reference = OutputElement.Cast<IReference<DomNode>>();
                        OutputPinTarget = new PinTarget(reference.Target, OutputPin.Name, OutputElement.DomNode);
                    }
                    else
                        OutputPinTarget = new PinTarget(OutputElement.DomNode, OutputPin.Name, null);


                }
                Debug.Assert(OutputPinTarget != null, "sanity check");
            }
        }

        /// <summary>
        /// Gets or sets the input pin target</summary>
        public PinTarget InputPinTarget
        {
            get
            {
                if (m_inputPinTarget == null)
                    SetPinTarget();
                
                return m_inputPinTarget;
            }
            set { m_inputPinTarget = value; }
        }

        /// <summary>
        /// Gets or sets the output pin target</summary>
        public PinTarget OutputPinTarget
        {
            get
            {
                if (m_outputPinTarget == null)
                    SetPinTarget();
                return m_outputPinTarget;
            }
            set { m_outputPinTarget = value; }
        }

        /// <summary>
        /// For input pin, gets enumeration of group pins down the chain before the leaf level</summary>
        public IEnumerable<GroupPin> InputPinSinkChain 
        {
            get
            {
                if (InputPin.Is<GroupPin>())
                    return InputPin.Cast<GroupPin>().SinkChain(true);
                return EmptyEnumerable<GroupPin>.Instance;
            } 
        }

        /// <summary>
        /// For output pin, gets enumeration of group pins down the chain before the leaf level</summary>
        public IEnumerable<GroupPin> OutputPinSinkChain
        {
            get
            {
                if (OutputPin.Is<GroupPin>())
                    return OutputPin.Cast<GroupPin>().SinkChain(false);
                return EmptyEnumerable<GroupPin>.Instance;
            }
        }

        internal bool IsValid(out NameString inputPinName, out NameString outputPinName)
        {
            outputPinName = OutputPin.Name; // GetAttribute<int>(OutputPinAttribute);
            inputPinName = InputPin.Name;   // GetAttribute<int>(InputPinAttribute);
            return OutputElement.ElementType.GetOutputPin(outputPinName) != null &&  InputElement.ElementType.GetInputPin(inputPinName) != null;
        }

        private PinTarget m_inputPinTarget;
        private PinTarget m_outputPinTarget;

    }
}
