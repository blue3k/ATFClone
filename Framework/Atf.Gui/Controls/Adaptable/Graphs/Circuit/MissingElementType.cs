//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Sce.Atf.Controls.Adaptable.Graphs
{
    /// <summary>
    /// Class for missing circuit element type</summary>
    public class MissingElementType : ICircuitElementType
    {
        /// <summary>
        /// Constructor with name</summary>
        /// <param name="name">Name</param>
        public MissingElementType(  string name)
        {
            Name = name;
        }

        /// <summary>
        /// Get missing element name</summary>
        public string Name { get; private set; }


        /// <summary>
        /// Gets the element display name</summary>
        public string DisplayName
        {
            get { return null; }
        }

        /// <summary>
        /// Get missing element interior size</summary>
        public Size InteriorSize
        {
            get { return (Image != null) ? new Size(Image.Width + 18, Image.Height +18) : new Size(); }
        }

        /// <summary>
        /// Get missing element image</summary>
        public Image Image { get;  set; }

        /// <summary>
        /// Get missing element input pins</summary>
        public PinList<ICircuitPin> Inputs
        {
            get { return m_inputs; }
        }

        /// <summary>
        /// Get missing element output pins</summary>
        public PinList<ICircuitPin> Outputs 
        {
            get
            {
                return m_outputs;                 
            } 
        }


        private PinList<ICircuitPin> m_inputs = new PinList<ICircuitPin>();
        private PinList<ICircuitPin> m_outputs = new PinList<ICircuitPin>();


    }
}
