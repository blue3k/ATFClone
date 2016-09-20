using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sce.Atf.Controls.PropertyEditing
{
    public partial class TagLabelListControl : FlowLayoutPanel
    {
        /// <summary>
        /// Event delegate definition for list update
        /// </summary>
        public delegate void delOnTagListUpdated();

        /// <summary>
        /// Tag list updated delegate
        /// </summary>
        public delOnTagListUpdated OnTagListUpdated;


        /// <summary>
        /// Tag Label list control constructor
        /// </summary>
        public TagLabelListControl()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowOnly;
            Name = "TagPanel";
            Size = new Size(50, 30);

            TagList = new HashSet<string>();
        }

        /// <summary>
        /// Tag List
        /// </summary>
        public HashSet<string> TagList { get; private set; }


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

            if (OnTagListUpdated != null) OnTagListUpdated();
        }

        /// <summary>
        /// Clear all tags
        /// </summary>
        public void ClearAllTags()
        {
            Controls.Clear();
            TagList.Clear();
            if (OnTagListUpdated != null) OnTagListUpdated();
        }
        

        /// <summary>
        /// Tag label removed handler
        /// </summary>
        /// <param name="control">Control to be removed</param>
        void OnTagLabelControlRemoved(TagLabelControl control)
        {
            TagList.Remove(control.TagText);
            if (OnTagListUpdated != null) OnTagListUpdated();
        }
    }
}

