namespace Sce.Atf.Controls.PropertyEditing
{
    partial class TagLabelControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TagLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.BGPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.BGPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TagLabel
            // 
            this.TagLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TagLabel.AutoSize = true;
            this.TagLabel.Location = new System.Drawing.Point(3, 0);
            this.TagLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TagLabel.Name = "TagLabel";
            this.TagLabel.Size = new System.Drawing.Size(35, 13);
            this.TagLabel.TabIndex = 0;
            this.TagLabel.Text = "Tag 1";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.deleteButton.BackColor = System.Drawing.Color.Transparent;
            this.deleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Location = new System.Drawing.Point(38, 0);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(15, 14);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DeleteButton_MouseUp);
            // 
            // BGPanel
            // 
            this.BGPanel.AutoSize = true;
            this.BGPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BGPanel.BackColor = System.Drawing.Color.Transparent;
            this.BGPanel.Controls.Add(this.TagLabel);
            this.BGPanel.Controls.Add(this.deleteButton);
            this.BGPanel.Location = new System.Drawing.Point(0, 0);
            this.BGPanel.Name = "BGPanel";
            this.BGPanel.Size = new System.Drawing.Size(53, 14);
            this.BGPanel.TabIndex = 2;
            // 
            // TagLabelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.BGPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TagLabelControl";
            this.Size = new System.Drawing.Size(56, 17);
            this.BGPanel.ResumeLayout(false);
            this.BGPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TagLabel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.FlowLayoutPanel BGPanel;
    }
}
