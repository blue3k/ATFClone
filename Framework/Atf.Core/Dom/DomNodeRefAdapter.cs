//Copyright ?2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;

namespace Sce.Atf.Dom
{
    /// <summary>
    /// Adapter that ensures that every DOM node in the subtree has a unique id</summary>
    public class DomNodeRefAdapter : DomNodeAdapter
    {
        /// <summary>
        /// Performs initialization when the adapter's node is set</summary>
        /// <remarks>Checks ID uniqueness when the adapter's node is set and throws an
        /// InvalidOperationException if a violation is found</remarks>
        protected override void OnNodeSet()
        {
            DomNode.AttributeChanged += OnAttributeChanged;

            base.OnNodeSet();
        }

        protected virtual void OnAttributeChanged(object sender, AttributeEventArgs e)
        {
            if (e.AttributeInfo != DomNode.Type.IdAttribute)
                return;

            var dic = DomNodeRefDictionary.Dic;
            if (dic == null)
                return;

            WeakReference refValue;
            if(e.OldValue != null)
            {
                var oldValueStr = e.OldValue.ToString();
                if (dic.TryGetValue(oldValueStr, out refValue))
                {
                    dic.Remove(oldValueStr);
                }
            }

            if(e.NewValue != null)
            {
                var newValueStr = e.NewValue.ToString();
                if (newValueStr != null)
                {
                    if (dic.TryGetValue(newValueStr, out refValue) && refValue.IsAlive)
                    {
                        Outputs.WriteLine(OutputMessageType.Warning, "DomNodeReference DB has {0} already. ignoring new value", newValueStr);
                    }
                    else
                    {
                        dic[newValueStr] = new WeakReference(e.NewValue);
                    }
                }
            }
        }
    }
}
