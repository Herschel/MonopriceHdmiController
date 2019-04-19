namespace MonopriceHdmiController
{
    partial class HdmiSwitch
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
            this.inputsTable = new System.Windows.Forms.TableLayoutPanel();
            this.portLabel = new System.Windows.Forms.Label();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textDeviceName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // inputsTable
            // 
            this.inputsTable.AutoSize = true;
            this.inputsTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inputsTable.ColumnCount = 1;
            this.inputsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.inputsTable.Location = new System.Drawing.Point(2, 71);
            this.inputsTable.Margin = new System.Windows.Forms.Padding(2);
            this.inputsTable.Name = "inputsTable";
            this.inputsTable.RowCount = 1;
            this.inputsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inputsTable.Size = new System.Drawing.Size(0, 0);
            this.inputsTable.TabIndex = 27;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(9, 45);
            this.portLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(38, 17);
            this.portLabel.TabIndex = 29;
            this.portLabel.Text = "Port:";
            // 
            // portComboBox
            // 
            this.portComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(50, 43);
            this.portComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(81, 24);
            this.portComboBox.TabIndex = 28;
            this.portComboBox.SelectedIndexChanged += new System.EventHandler(this.portComboBox_SelectedIndexChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(9, 11);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 32;
            this.labelName.Text = "Name:";
            // 
            // textDeviceName
            // 
            this.textDeviceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textDeviceName.Location = new System.Drawing.Point(63, 8);
            this.textDeviceName.Name = "textDeviceName";
            this.textDeviceName.Size = new System.Drawing.Size(263, 22);
            this.textDeviceName.TabIndex = 33;
            this.textDeviceName.Text = "Switch 1";
            this.textDeviceName.TextChanged += new System.EventHandler(this.textDeviceName_TextChanged);
            // 
            // HdmiSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.textDeviceName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.inputsTable);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portComboBox);
            this.Name = "HdmiSwitch";
            this.Size = new System.Drawing.Size(333, 73);
            this.Load += new System.EventHandler(this.HdmiSwitch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel inputsTable;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textDeviceName;
    }
}
