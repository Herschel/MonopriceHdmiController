namespace MonopriceHdmiController
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.delayStepper = new System.Windows.Forms.NumericUpDown();
            this.changeInputTimer = new System.Windows.Forms.Timer(this.components);
            this.lblChangeDelay = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.inputsTable = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.delayStepper)).BeginInit();
            this.SuspendLayout();
            // 
            // portComboBox
            // 
            this.portComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(72, 10);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(120, 33);
            this.portComboBox.TabIndex = 1;
            this.portComboBox.SelectedIndexChanged += new System.EventHandler(this.portComboBox_SelectedIndexChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(10, 13);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(57, 25);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port:";
            // 
            // delayStepper
            // 
            this.delayStepper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delayStepper.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.delayStepper.Location = new System.Drawing.Point(364, 10);
            this.delayStepper.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.delayStepper.Name = "delayStepper";
            this.delayStepper.Size = new System.Drawing.Size(120, 31);
            this.delayStepper.TabIndex = 4;
            this.delayStepper.ValueChanged += new System.EventHandler(this.delayStepper_ValueChanged);
            // 
            // changeInputTimer
            // 
            this.changeInputTimer.Interval = 1;
            this.changeInputTimer.Tick += new System.EventHandler(this.changeInputTimer_Tick);
            // 
            // lblChangeDelay
            // 
            this.lblChangeDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChangeDelay.AutoSize = true;
            this.lblChangeDelay.Location = new System.Drawing.Point(238, 13);
            this.lblChangeDelay.Name = "lblChangeDelay";
            this.lblChangeDelay.Size = new System.Drawing.Size(121, 25);
            this.lblChangeDelay.TabIndex = 11;
            this.lblChangeDelay.Text = "Delay (ms):";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "HDMI Controller";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // inputsTable
            // 
            this.inputsTable.AutoSize = true;
            this.inputsTable.ColumnCount = 1;
            this.inputsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.inputsTable.Location = new System.Drawing.Point(0, 53);
            this.inputsTable.Name = "inputsTable";
            this.inputsTable.RowCount = 1;
            this.inputsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inputsTable.Size = new System.Drawing.Size(494, 0);
            this.inputsTable.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(494, 829);
            this.Controls.Add(this.inputsTable);
            this.Controls.Add(this.lblChangeDelay);
            this.Controls.Add(this.delayStepper);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "HDMI Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.delayStepper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Timer changeInputTimer;
        private System.Windows.Forms.Label lblChangeDelay;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TableLayoutPanel inputsTable;
        private System.Windows.Forms.NumericUpDown delayStepper;
    }
}

