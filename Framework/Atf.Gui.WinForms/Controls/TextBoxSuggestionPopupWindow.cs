using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sce.Atf.Applications
{
    /// <summary>
    /// This is implementation of suggestion list pop up windows.</summary>
    public class TextBoxSuggestionPopupWindow : System.Windows.Forms.Form
    {
        /// <summary>
        /// Linked text box control
        /// </summary>
        TextBox m_textBoxControl;
        /// <summary>
        /// Suggestion source
        /// </summary>
        List<string> m_suggestionSource = null;
        /// <summary>
        /// Suggestion list 
        /// </summary>
        ListBox m_suggestionListBox;



        public TextBoxSuggestionPopupWindow(TextBox textBoxControl)
        {
            m_textBoxControl = textBoxControl;

            // Hide on lost focus
            m_textBoxControl.LostFocus += (sender, e) =>
            {
                Hide();
            };

            // Hijack some inputs
            m_textBoxControl.PreviewKeyDown += OnKeyDown;


            //Basic setup...
            this.AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            m_suggestionListBox = new ListBox();
            m_suggestionListBox.Name = "Suggestion List";
            m_suggestionListBox.Font = this.Font; // use same font
            var textBoxSize = m_textBoxControl.Size;

            m_suggestionListBox.MinimumSize = textBoxSize;

            textBoxSize.Height *= 20;
            m_suggestionListBox.Size = textBoxSize;
            m_suggestionListBox.MaximumSize = textBoxSize;

            //Positioning and Sizing
            this.MinimumSize = m_suggestionListBox.MinimumSize;
            this.MaximumSize = m_suggestionListBox.Size;
            this.Size = m_suggestionListBox.Size;
            m_suggestionListBox.Location = Point.Empty;

            //Add the host to the list
            this.Controls.Add(m_suggestionListBox);
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;

                const int WS_EX_NOACTIVATE = 0x08000000;
                const int WS_EX_TOOLWINDOW = 0x00000080;
                const int WS_EX_TOPMOST = 0x00000008;
                baseParams.ExStyle |= (int)(WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);

                return baseParams;
            }
        }

        public List<string> SuggestionSource
        {
            get { return m_suggestionSource; }
            set { m_suggestionSource = value; OnSuggestionSourceUpdated(); }
        }

        void OnSuggestionSourceUpdated()
        {

        }

        public void OnKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (m_suggestionListBox.SelectedIndex > 0) m_suggestionListBox.SelectedIndex--;
                    break;
                case Keys.Down:
                    if ((m_suggestionListBox.SelectedIndex+1) < m_suggestionListBox.Items.Count) m_suggestionListBox.SelectedIndex++;
                    break;
                case Keys.Space:
                case Keys.Tab:
                case Keys.Enter:
                    if (m_suggestionListBox.SelectedIndex >= 0)
                        SelectSuggestion();
                    break;
            }

        }


        public void ShowSuggestionList()
        {
            if (string.IsNullOrEmpty(m_textBoxControl.Text)) return;
            if (m_suggestionSource == null || m_suggestionSource.Count == 0) return;


            var location = m_textBoxControl.PointToScreen(new Point(0, m_textBoxControl.Bottom));
            Show();
            SetDesktopLocation(location.X, location.Y);

            UpdateSuggestionList();
        }
        public void SelectSuggestion()
        {
            // if the ListBox is not empty
            if (((m_suggestionListBox.Items.Count > 0) && (m_suggestionListBox.SelectedIndex > -1)))
            {
                m_textBoxControl.Text = m_suggestionListBox.SelectedItem.ToString();
                m_textBoxControl.SelectionStart = m_textBoxControl.Text.Length;
                Hide();
            }
        }

        public void UpdateSuggestionList()
        {
            if (m_suggestionListBox == null) return;

            string searchText = m_textBoxControl.Text.ToLower();

            m_suggestionListBox.BeginUpdate();
            m_suggestionListBox.Items.Clear();

            if (!string.IsNullOrEmpty(searchText))
            {
                foreach (var suggestionString in m_suggestionSource)
                {
                    if (!suggestionString.ToLower().Contains(searchText)) continue;

                    m_suggestionListBox.Items.Add(suggestionString);
                }

                if (m_suggestionListBox.Items.Count > 0)
                {
                    //m_suggestionListBox.SelectedIndex = 0;

                    var newSize = m_textBoxControl.Size;

                    var newSizeH = m_suggestionListBox.Items.Count * newSize.Height;
                    newSizeH = Math.Max(m_suggestionListBox.MinimumSize.Height, newSizeH);
                    newSizeH = Math.Min(m_suggestionListBox.MaximumSize.Height, newSizeH);
                    newSize.Height = newSizeH;
                    m_suggestionListBox.Size = newSize;
                }

            }

            m_suggestionListBox.EndUpdate();
        }

    }

}