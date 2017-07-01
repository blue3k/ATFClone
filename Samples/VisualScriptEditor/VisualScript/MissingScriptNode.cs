//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf;
using Sce.Atf.Controls.Adaptable.Graphs;

namespace VisualScript
{
    public class MissingScriptNode : ScriptNode
    {
        protected override void OnNodeSet()
        {
            base.OnNodeSet();
            m_missingElementType = new MissingElementType(MissingTypeName);
            m_missingElementType.Image = ResourceUtil.GetImage(Sce.Atf.Resources.PackageErrorImage);
        }

        /// <summary>
        /// Gets ICircuitElementType type</summary>
        public override ICircuitElementType ElementType
        {
            get { return m_missingElementType; }
        }

        public static string MissingTypeName = "?Missing?";
        private MissingElementType m_missingElementType;
    }
}
