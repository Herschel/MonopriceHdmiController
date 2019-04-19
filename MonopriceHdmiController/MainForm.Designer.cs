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
            this.changeInputTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.hdmiSwitchTabs = new System.Windows.Forms.TabControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.newSwitchButton = new System.Windows.Forms.Button();
            this.deleteSwitchButton = new System.Windows.Forms.Button();
            this.lblChangeDelay = new System.Windows.Forms.Label();
            this.delayStepper = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.delayStepper)).BeginInit();
            this.SuspendLayout();
            // 
            // changeInputTimer
            // 
            this.changeInputTimer.Interval = 1;
            this.changeInputTimer.Tick += new System.EventHandler(this.changeInputTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "HDMI Controller";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // hdmiSwitchTabs
            // 
            this.hdmiSwitchTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdmiSwitchTabs.Location = new System.Drawing.Point(12, 12);
            this.hdmiSwitchTabs.Name = "hdmiSwitchTabs";
            this.hdmiSwitchTabs.SelectedIndex = 0;
            this.hdmiSwitchTabs.Size = new System.Drawing.Size(355, 480);
            this.hdmiSwitchTabs.TabIndex = 0;
            // 
            // newSwitchButton
            // 
            this.newSwitchButton.Location = new System.Drawing.Point(12, 541);
            this.newSwitchButton.Name = "newSwitchButton";
            this.newSwitchButton.Size = new System.Drawing.Size(98, 23);
            this.newSwitchButton.TabIndex = 1;
            this.newSwitchButton.Text = "New Switch";
            this.newSwitchButton.UseVisualStyleBackColor = true;
            this.newSwitchButton.Click += new System.EventHandler(this.newSwitchButton_Click);
            // 
            // deleteSwitchButton
            // 
            this.deleteSwitchButton.Location = new System.Drawing.Point(214, 541);
            this.deleteSwitchButton.Name = "deleteSwitchButton";
            this.deleteSwitchButton.Size = new System.Drawing.Size(130, 23);
            this.deleteSwitchButton.TabIndex = 2;
            this.deleteSwitchButton.Text = "Delete Switch";
            this.deleteSwitchButton.UseVisualStyleBackColor = true;
            this.deleteSwitchButton.Click += new System.EventHandler(this.deleteSwitchButton_Click);
            // 
            // lblChangeDelay
            // 
            this.lblChangeDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChangeDelay.AutoSize = true;
            this.lblChangeDelay.Location = new System.Drawing.Point(34, 507);
            this.lblChangeDelay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeDelay.Name = "lblChangeDelay";
            this.lblChangeDelay.Size = new System.Drawing.Size(80, 17);
            this.lblChangeDelay.TabIndex = 33;
            this.lblChangeDelay.Text = "Delay (ms):";
            // 
            // delayStepper
            // 
            this.delayStepper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delayStepper.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.delayStepper.Location = new System.Drawing.Point(118, 505);
            this.delayStepper.Margin = new System.Windows.Forms.Padding(2);
            this.delayStepper.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.delayStepper.Name = "delayStepper";
            this.delayStepper.Size = new System.Drawing.Size(80, 22);
            this.delayStepper.TabIndex = 32;
            this.delayStepper.ValueChanged += new System.EventHandler(this.delayStepper_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(379, 584);
            this.Controls.Add(this.lblChangeDelay);
            this.Controls.Add(this.delayStepper);
            this.Controls.Add(this.deleteSwitchButton);
            this.Controls.Add(this.newSwitchButton);
            this.Controls.Add(this.hdmiSwitchTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Timer changeInputTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TabControl hdmiSwitchTabs;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button newSwitchButton;
        private System.Windows.Forms.Button deleteSwitchButton;
        private System.Windows.Forms.Label lblChangeDelay;
        private System.Windows.Forms.NumericUpDown delayStepper;
    }
}

