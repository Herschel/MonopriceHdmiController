namespace MonopriceHdmiController
{
    partial class HdmiInput
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
            this.selectInputButton = new System.Windows.Forms.Button();
            this.hotKeysText = new System.Windows.Forms.TextBox();
            this.addHotKeyButton = new System.Windows.Forms.Button();
            this.clearHotKeysButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectInputButton
            // 
            this.selectInputButton.Location = new System.Drawing.Point(10, 10);
            this.selectInputButton.Name = "selectInputButton";
            this.selectInputButton.Size = new System.Drawing.Size(120, 44);
            this.selectInputButton.TabIndex = 0;
            this.selectInputButton.Text = "Input";
            this.selectInputButton.UseVisualStyleBackColor = true;
            this.selectInputButton.Click += new System.EventHandler(this.selectInputButton_Click);
            // 
            // hotKeysText
            // 
            this.hotKeysText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeysText.Location = new System.Drawing.Point(140, 14);
            this.hotKeysText.Name = "hotKeysText";
            this.hotKeysText.ReadOnly = true;
            this.hotKeysText.Size = new System.Drawing.Size(200, 31);
            this.hotKeysText.TabIndex = 1;
            this.hotKeysText.Text = "<no shortcut>";
            // 
            // addHotKeyButton
            // 
            this.addHotKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addHotKeyButton.Location = new System.Drawing.Point(350, 10);
            this.addHotKeyButton.Name = "addHotKeyButton";
            this.addHotKeyButton.Size = new System.Drawing.Size(44, 44);
            this.addHotKeyButton.TabIndex = 2;
            this.addHotKeyButton.Text = "+";
            this.addHotKeyButton.UseVisualStyleBackColor = true;
            this.addHotKeyButton.Click += new System.EventHandler(this.addHotKeyButton_Click);
            // 
            // clearHotKeysButton
            // 
            this.clearHotKeysButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearHotKeysButton.Location = new System.Drawing.Point(404, 10);
            this.clearHotKeysButton.Name = "clearHotKeysButton";
            this.clearHotKeysButton.Size = new System.Drawing.Size(44, 44);
            this.clearHotKeysButton.TabIndex = 3;
            this.clearHotKeysButton.Text = "-";
            this.clearHotKeysButton.UseVisualStyleBackColor = true;
            this.clearHotKeysButton.Click += new System.EventHandler(this.clearHotKeysButton_Click);
            // 
            // HdmiInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clearHotKeysButton);
            this.Controls.Add(this.addHotKeyButton);
            this.Controls.Add(this.hotKeysText);
            this.Controls.Add(this.selectInputButton);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HdmiInput";
            this.Size = new System.Drawing.Size(458, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectInputButton;
        private System.Windows.Forms.Button addHotKeyButton;
        private System.Windows.Forms.TextBox hotKeysText;
        private System.Windows.Forms.Button clearHotKeysButton;
    }
}
