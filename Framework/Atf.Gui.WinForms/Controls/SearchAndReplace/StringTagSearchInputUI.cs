//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Controls;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sce.Atf.Applications
{
    /// <summary>
    /// Simple string input UI for initiating string-based searches. Users of the class match against
    /// the inputted string via the Matches() method.</summary>
    public class StringTagSearchInputUI : ToolStrip
    {
        /// <summary>
        /// Constructor</summary>
        public StringTagSearchInputUI(TagLabelListControl tagListControl)
        {
            m_tagListControl = tagListControl;

            Visible = true;
            GripStyle = ToolStripGripStyle.Hidden;
            RenderMode = ToolStripRenderMode.System;

            ToolStripDropDownButton dropDownButton = new ToolStripDropDownButton();
            dropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            dropDownButton.Image = ResourceUtil.GetImage16(Resources.SearchImage);
            dropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            dropDownButton.Name = "SearchButton";
            dropDownButton.Size = new System.Drawing.Size(29, 22);
            dropDownButton.Text = "Search".Localize("'Search' is a verb");


            m_patternTextBox = new ToolStripAutoFitTextBox();
            m_patternTextBox.KeyDown += patternTextBox_KeyDown;
            m_patternTextBox.TextChanged += patternTextBox_TextChanged;
            m_patternTextBox.TextBox.PreviewKeyDown += textBox_PreviewKeyDown;
            m_patternTextBox.MaximumWidth = 1080;

            Items.AddRange(new ToolStripItem[] { 
                    dropDownButton, 
                    m_patternTextBox,
                    //clearSearchButton
                    });
        }

        /// <summary>
        /// Event that is raised after text control is updated</summary>
        public event EventHandler Updated;

        /// <summary>
        /// Gets whether or not the textbox contains any input</summary>
        /// <returns>True if any sort of string is in the textbox</returns>
        public bool IsNullOrEmpty()
        {
            return m_textBoxEmpty;
        }


        /// <summary>
        /// Clears search results</summary>
        public void ClearSearch()
        {
            m_patternTextBox.Text = string.Empty;
            Updated.Raise(this, EventArgs.Empty);
        }



        /// <summary>
        /// Callback that performs custom actions when the 'clear' button has been pressed</summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments related to the event</param>
        private void clearSearchButton_Click(object sender, System.EventArgs e)
        {
            ClearSearch();
        }
        


        void patternTextBox_KeyDown(object sender, KeyEventArgs arg)
        {
            if(arg.KeyCode ==  Keys.Enter)
            {
                string text = m_patternTextBox.Text;
                m_patternTextBox.Text = string.Empty;
                if(m_tagListControl != null && !string.IsNullOrEmpty(text))
                {
                    m_tagListControl.AddTag(text);
                }
            }
        }

        /// <summary>
        /// Callback that performs custom actions after any text changed in the text box</summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments related to the event</param>
        void patternTextBox_TextChanged(object sender, EventArgs e)
        {
            m_textBoxEmpty = string.IsNullOrEmpty(m_patternTextBox.Text);
        }

        /// <summary>
        /// Callback that performs custom actions after the preview key is pressed</summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Arguments related to the event</param>
        void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                clearSearchButton_Click(sender, e);
        }


        #region Custom Auto Complition
        public List<string> SuggestionSource
        {
            get { return m_patternTextBox != null ? m_patternTextBox.SuggestionSource : null; }
            set
            {
                m_patternTextBox.SuggestionSource = value;
            }
        }

        #endregion

        private readonly ToolStripAutoFitTextBox m_patternTextBox;
        private bool m_textBoxEmpty=true;
        private TagLabelListControl m_tagListControl;
    }
}