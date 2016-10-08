using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sce.Atf.Controls
{

    public class TagListSet : HashSet<string>
    {
        public bool Matches(string testString)
        {
            testString = testString.ToLower();

            int searchAttempted = 0;
            foreach (var filterPattern in this)
            {
                if (string.IsNullOrEmpty(filterPattern)) continue;
                var filterPatternLwr = filterPattern.ToLower();
                searchAttempted++;
                if (!testString.Contains(filterPatternLwr)) continue;

                return true;
            }

            return searchAttempted == 0;
        }

        public int IndexOfTag(string testString, int searchStart, out int patternLength)
        {
            testString = testString.ToLower();

            int searchAttempted = 0;
            foreach (var filterPattern in this)
            {
                if (string.IsNullOrEmpty(filterPattern)) continue;
                var filterPatternLwr = filterPattern.ToLower();
                searchAttempted++;

                int index = testString.IndexOf(filterPatternLwr, searchStart, StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                {
                    patternLength = filterPatternLwr.Length;
                    return index;
                }
            }

            patternLength = 0;
            return -1;
        }

    }


    public partial class TagLabelListControl : FlowLayoutPanel
    {
        /// <summary>
        /// Tag list updated delegate
        /// </summary>
        public event EventHandler OnTagListUpdated;


        /// <summary>
        /// Tag Label list control constructor
        /// </summary>
        public TagLabelListControl()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowOnly;
            Name = "TagPanel";
            Size = new Size(50, 30);

            TagList = new TagListSet();
        }


        /// <summary>
        /// Tag List
        /// </summary>
        public TagListSet TagList { get; private set; }


        /// <summary>
        /// Add tag to the list
        /// </summary>
        /// <param name="tag">tag string to add</param>
        public void AddTag(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return;

            TagList.Add(tag);
            var newTagLabel = new TagLabelControl(tag);
            newTagLabel.OnRemoved += OnTagLabelControlRemoved;

            Controls.Add(newTagLabel);

            OnTagListUpdated.Raise(this,null);
        }

        /// <summary>
        /// Clear all tags
        /// </summary>
        public void ClearAllTags()
        {
            Controls.Clear();
            TagList.Clear();
            OnTagListUpdated.Raise(this,null);
        }
        

        /// <summary>
        /// Tag label removed handler
        /// </summary>
        /// <param name="control">Control to be removed</param>
        void OnTagLabelControlRemoved(TagLabelControl control)
        {
            TagList.Remove(control.TagText);
            OnTagListUpdated.Raise(this, null);
        }

        public bool Matches(string testString)
        {
            return TagList.Matches(testString);
        }
    }
}

