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
    public partial class TagLabelControl : UserControl
    {
        /// <summary>
        /// Delete button icon image
        /// </summary>
        private static readonly Image s_deleteImage = Sce.Atf.ResourceUtil.GetImage16(Resources.RemoveImage);

        /// <summary>
        /// OnRemoved delegate definition
        /// </summary>
        /// <param name="control">Control which is removed</param>
        public delegate void delOnRemoved(TagLabelControl control);

        /// <summary>
        /// Remove event Delegate
        /// </summary>
        public delOnRemoved OnRemoved;


        /// <summary>
        /// Constructor creating TagLabelControl
        /// </summary>
        public TagLabelControl()
        {
            InitializeComponent();
            deleteButton.Image = s_deleteImage;
        }

        /// <summary>
        /// Constructor creating and initialize tag string
        /// </summary>
        /// <param name="tagString">Tag label string</param>
        public TagLabelControl(string tagString)
            : this()
        {
            TagText = tagString;
        }

        /// <summary>
        /// Tag label string accessor
        /// </summary>
        public string TagText
        {
            get { return TagLabel.Text; }
            set { TagLabel.Text = value; }
        }

        /// <summary>
        /// Delete button event handler
        /// </summary>
        private void DeleteButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (Parent != null)
            {
                Parent.Controls.Remove(this);
                if (OnRemoved != null)
                    OnRemoved(this);
                Dispose();
            }
        }

    }
}
