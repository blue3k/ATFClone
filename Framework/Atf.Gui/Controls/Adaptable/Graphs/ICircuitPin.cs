//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;

namespace Sce.Atf.Controls.Adaptable.Graphs
{
    /// <summary>
    /// Interface for pins, which are the sources and destinations for
    /// wires between circuit elements</summary>
    public interface ICircuitPin : IEdgeRoute
    {
        /// <summary>
        /// Gets pin name</summary>
        NameString Name
        {
            get;
        }

        /// <summary>
        /// Gets pin type name</summary>
        NameString TypeName
        {
            get;
        }

        /// <summary>Gets pin attribute type</summary>
        AttributeType PinType
        {
            get;
        }

        /// <summary>
        /// Gets pin index. cached value, use only for visual property.
        /// </summary>
        int Index
        {
            get;
            set;
        }

    }
}
