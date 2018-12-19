//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
using Sce.Atf.Dom;

namespace Sce.Atf.Controls.Adaptable.Graphs
{
    /// <summary>
    /// Adapts DomNode to a pin in a circuit; used in mastering and as a base
    /// class for GroupPin</summary>
    public abstract class Pin : DomNodeAdapter, ICircuitPin
    {
        public Pin()
        {

        }

        /// <summary>
        /// Gets type attribute of Pin
        /// </summary>
        protected abstract AttributeInfo TypeAttribute { get; }

        /// <summary>
        /// Gets name attribute of Pin
        /// </summary>
        protected abstract AttributeInfo NameAttribute { get; }

        /// <summary>
        /// Gets index attribute of Pin
        /// </summary>
        protected abstract AttributeInfo IndexAttribute { get; }

        /// <summary>
        /// pin index used for cached order access
        /// </summary>
        public int Index
        {
            get { return GetAttribute<int>(IndexAttribute); }
            set
            {
                SetAttribute(IndexAttribute, value);
            }
        }

        #region ICircuitPin Members

        /// <summary>Gets pin name</summary>
        public virtual NameString Name
        {
            get { return GetAttribute<NameString>(NameAttribute); }
            set
            {
                SetAttribute(NameAttribute, value);
            }
        }

        /// <summary>
        /// Gets pin type name
        /// </summary>
        public virtual string TypeName
        {
            get { return GetAttribute<string>(TypeAttribute); }
            set
            {
                SetAttribute(TypeAttribute, value);
            }
        }

        #endregion

        #region ElementType.Pin Members

        // Actually overriding this property is way far away
        /// <summary>
        /// Gets whether connection fan in to pin is allowed</summary>
        public virtual bool AllowFanIn
        {
            get { return true; }
        }

        /// <summary>
        /// Gets whether connection fan out from pin is allowed</summary>
        public virtual bool AllowFanOut
        {
            get { return true; }
        }

        #endregion

    }
}