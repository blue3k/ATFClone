//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;

namespace VisualScript
{
    /// <summary>
    /// Adapts DomNode to script annotation, that is, note on the script</summary>
    public class VisualScriptAnnotation : Sce.Atf.Controls.Adaptable.Graphs.Annotation
    {
        /// <summary>
        /// Gets annotation text attribute</summary>
        protected override AttributeInfo TextAttribute
        {
            get { return VisualScriptBasicSchema.annotationType.textAttribute; }
        }

        /// <summary>
        /// Gets annotation x-coordinate position attribute</summary>
        protected override AttributeInfo XAttribute
        {
            get { return VisualScriptBasicSchema.annotationType.xAttribute; }
        }

        /// <summary>
        /// Gets annotation y-coordinate position attribute</summary>
        protected override AttributeInfo YAttribute
        {
            get { return VisualScriptBasicSchema.annotationType.yAttribute; }
        }

        /// <summary>
        /// Gets annotation width attribute</summary>
        protected override AttributeInfo WidthAttribute
        {
            get { return VisualScriptBasicSchema.annotationType.widthAttribute; }
        }

        /// <summary>
        /// Gets annotation height attribute</summary>
        protected override AttributeInfo HeightAttribute
        {
            get { return VisualScriptBasicSchema.annotationType.heightAttribute; }
        }

        /// <summary>
        /// Gets annotation background color attribute</summary>
        protected override AttributeInfo BackColorAttribute
        {
            get { return VisualScriptBasicSchema.annotationType.backcolorAttribute; }
        }

        /// <summary>
        /// Gets annotation foreColorAttribute color attribute</summary>
        protected override AttributeInfo ForeColorAttribute
        {
            get { return VisualScriptBasicSchema.annotationType.foreColorAttribute; }
        }
    }
}
