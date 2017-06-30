//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Dom;

namespace VisualScript
{
    internal class VisualScriptCategoryUniqueIdValidator : Sce.Atf.Dom.CategoryUniqueIdValidator
    {

        internal interface IDocumentTag { }

        /// <summary>
        /// Get the id category of the given node</summary>
        protected override object GetIdCategory(DomNode node)
        {
	        return node.GetTag(typeof (VisualScriptCategoryUniqueIdValidator.IDocumentTag));
        }
    }
}
