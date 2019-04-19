/******************************************************************************
 * Monoprice HDMI Controller
 * Copyright 2017 Michael Welsh
 * Licensed under the MIT License.
 * See License.MD for more info.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;
using System.Xml;

namespace MonopriceHdmiController
{
    /// <summary>
    /// The form of the HDMI Controller application.
    /// Provides UI for switching and binding hotkeys to HDMI inputs.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The number of HDMI inputs this switch supports.
        /// (Eight for the Monoprice 8x1 HDMI switch.)
        /// One HdmiInput control will be created for each input.
        /// </summary>
        /// 
        private HotKeyManager hotKeyManager;
        private List<HdmiSwitch> hdmiSwitches;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            hotKeyManager = new HotKeyManager();

            hdmiSwitches = new List<HdmiSwitch>();
            AddNewHdmiSwitch();

            //serialConnection = new SerialPort(
            //    "COM1",
            //    9600, Parity.None, 8, StopBits.One

            //);

            //// Populate the list of serial ports.
            //foreach (string port in SerialPort.GetPortNames())
            //{
            //    portComboBox.Items.Add(port);
            //}

            //if (portComboBox.Items.Count > 0)
            //{
            //    portComboBox.SelectedIndex = 0;
            //}

            //// Load previous settings from XML.
            //// If the user has no settings, load the defaults.
            //LoadSettings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save settings when the app exits.
            SaveSettings();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Minimize to system tray.
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                Hide();
            }
            else
            {
                notifyIcon.Visible = false;
            }
        }

        

        /// <summary>
        /// Loads the user's settings from settings.xml.
        /// </summary>
        private void LoadSettings()
        {
            //var document = new XmlDocument();
            //try
            //{
            //    document.Load("settings.xml");

            //    delayStepper.Value = Int32.Parse(document.DocumentElement.SelectSingleNode("changedelay").InnerText);

            //    var inputNodes = document.DocumentElement.SelectNodes("input");
            //    for (int i = 0; i < inputNodes.Count; i++)
            //    {
            //        var input = inputs[i];
            //        foreach (XmlNode hotKeyNode in inputNodes[i].SelectNodes("hotkey"))
            //        {
            //            var hotKey = new HotKeyManager.HotKey((Keys)Int32.Parse(hotKeyNode.InnerText));
            //            hotKey.isCtrl = hotKeyNode.Attributes["ctrl"] != null && Boolean.Parse(hotKeyNode.Attributes["ctrl"].Value);
            //            hotKey.isShift = hotKeyNode.Attributes["shift"] != null && Boolean.Parse(hotKeyNode.Attributes["shift"].Value);
            //            hotKey.isAlt = hotKeyNode.Attributes["alt"] != null && Boolean.Parse(hotKeyNode.Attributes["alt"].Value);
            //            input.AddHotKey(hotKey);
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    // Something blew up, fall back to default settings.
            //    SetDefaultSettings();
            //}
        }

        /// <summary>
        /// Handles a request from an HdmiInput control to change inputs.
        /// </summary>
        /// <param name="s">The HdmiInput control.</param>
        /// <param name="ev">The requested input.</param>
        private void OnInputRequested(Object s, HdmiInput.InputRequestedEventArgs ev)
        {
            //if (delayStepper.Value <= 0)
            //{
            //    // No delay, change inputs immediately.
            //    ChangeInput(ev.inputNumber);
            //}
            //else
            //{
            //    // Start a timer to change the input momentarily.
            //    nextInput = ev.inputNumber;
            //    changeInputTimer.Start();
            //}
        }

        /// <summary>
        /// Saves the user's settings to settings.xml.
        /// </summary>
        private void SaveSettings()
        {
            //try
            //{
            //    // Just some dumb XML serialization.
            //    var writer = XmlWriter.Create("settings.xml");
            //    writer.WriteStartDocument();
            //    writer.WriteStartElement("settings");

            //    writer.WriteStartElement("changedelay");
            //    writer.WriteValue((int)delayStepper.Value);
            //    writer.WriteEndElement();

            //    // Save hotkeys for each input.
            //    foreach (var input in inputs)
            //    {
            //        writer.WriteStartElement("input");

            //        foreach (var hotKey in input.HotKeys)
            //        {
            //            writer.WriteStartElement("hotkey");

            //            if (hotKey.isCtrl)
            //            {
            //                writer.WriteStartAttribute("ctrl");
            //                writer.WriteValue(true);
            //                writer.WriteEndAttribute();
            //            }

            //            if (hotKey.isAlt)
            //            {
            //                writer.WriteStartAttribute("alt");
            //                writer.WriteValue(true);
            //                writer.WriteEndAttribute();
            //            }

            //            if (hotKey.isShift)
            //            {
            //                writer.WriteStartAttribute("shift");
            //                writer.WriteValue(true);
            //                writer.WriteEndAttribute();
            //            }

            //            writer.WriteValue((int)hotKey.key);
            //            writer.WriteEndElement();
            //        }

            //        writer.WriteEndElement();
            //    }
            //    writer.WriteEndDocument();

            //    writer.Flush();
            //}
            //catch (Exception)
            //{
            //    // Failed to write settings. Fail silently.
            //}
        }

        /// <summary>
        /// Sets default settings for the first launch of the app.
        /// By default, there is no delay in changing inputs, and the inputs
        /// are bound to the F1 - F8 keys.
        /// </summary>
        private void SetDefaultSettings()
        {
            ////delayStepper.Value = 0;
            //for (int i = 0; i < inputs.Length; i++)
            //{
            //    inputs[i].AddHotKey(new HotKeyManager.HotKey(Keys.F1 + i));
            //}
        }

        private void AddNewHdmiSwitch()
        {
            var hdmiSwitch = new HdmiSwitch(hotKeyManager)
            {
                DeviceName = $"Switch {hdmiSwitches.Count + 1}",
                Dock = DockStyle.Fill,
            };
            hdmiSwitches.Add(hdmiSwitch);

            var hdmiSwitchPage = new TabPage(hdmiSwitch.DeviceName);
            hdmiSwitchTabs.TabPages.Add(hdmiSwitchPage);
            hdmiSwitchPage.Controls.Add(hdmiSwitch);

            hdmiSwitch.DeviceNameChanged += (o, name) => hdmiSwitchPage.Text = name;
        }

        private void changeInputTimer_Tick(object sender, EventArgs e)
        {
            // Delay elapsed -- change input now.
            changeInputTimer.Stop();
            //ChangeInput(nextInput);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Restore window from system tray.
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void newSwitchButton_Click(object sender, EventArgs e)
        {
            AddNewHdmiSwitch();
        }

    
        private void delayStepper_ValueChanged(object sender, EventArgs e)
        {
            if (delayStepper.Value > 0)
            {
                changeInputTimer.Interval = (int)delayStepper.Value;
            }
        }

        private void deleteSwitchButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete switch?", "Delete Switch", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                int i = hdmiSwitchTabs.SelectedIndex;
                hdmiSwitchTabs.TabPages.RemoveAt(i);
                hdmiSwitches[i].Dispose();
                hdmiSwitches.RemoveAt(i);
            }
        }
    }
}
