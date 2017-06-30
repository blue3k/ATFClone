//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sce.Atf;
using Sce.Atf.Dom;


namespace VisualScript
{

    /// <summary>
    /// DomNode adapter for expression element</summary>
    class Expression : DomNodeAdapter
    {
        protected override void OnNodeSet()
        {
            if (!VisualScriptBasicSchema.expressionType.Type.IsAssignableFrom(DomNode.Type))
                throw new Exception("can be attached only to DomNode of type expressionType");
        }


        public string Label
        {
            get { return GetAttribute<string>(VisualScriptBasicSchema.expressionType.labelAttribute); }
            set { SetAttribute(VisualScriptBasicSchema.expressionType.labelAttribute, value); }

        }

        public string Id
        {
            get { return GetAttribute<string>(VisualScriptBasicSchema.expressionType.idAttribute); }
            set
            {
                string id = value ?? "Expression";
                SetAttribute(VisualScriptBasicSchema.expressionType.idAttribute, id);
            }
        }


        public string Script
        {
            get { return GetAttribute<string>(VisualScriptBasicSchema.expressionType.scriptAttribute); }
            set
            {
                string script = value ?? string.Empty;
                SetAttribute(VisualScriptBasicSchema.expressionType.scriptAttribute, script);
            }
        }

    }
}
