//Copyright ?2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Sce.Atf.Dom
{
    /// <summary>Dom node dictionary for referencing</summary>
    [Export(typeof(DomNodeRefDictionary))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DomNodeRefDictionary
    {
        static internal Dictionary<string, WeakReference> Dic => stm_NodeList;
        static Dictionary<string, WeakReference> stm_NodeList;

        [ImportingConstructor]
        public DomNodeRefDictionary()
        {
            stm_NodeList = new Dictionary<string, WeakReference>();

            AttributeType.DomNodeRefType.ValueStringParser = (string stringValue) =>
            {
                WeakReference weakValue;
                if (!Dic.TryGetValue(stringValue, out weakValue))
                    return null;

                if (!weakValue.IsAlive)
                    return null;

                return weakValue.Target;
            };
        }
    }
}
