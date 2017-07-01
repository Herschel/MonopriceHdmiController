/******************************************************************************
 * Monoprice HDMI Controller
 * Copyright 2017 Michael Welsh
 * Licensed under the MIT License.
 * See License.MD for more info.
 *****************************************************************************/

using System;
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
        private static int NUM_INPUTS = 8;

        private HotKeyManager hotKeyManager;
        private HdmiInput[] inputs;
        private int nextInput;
        private SerialPort serialConnection;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            hotKeyManager = new HotKeyManager();

            // Create an HdmiInput control for each input on the HDMI switch.
            inputs = new HdmiInput[NUM_INPUTS];
            inputsTable.RowCount = NUM_INPUTS;
            for (int i = 0; i < NUM_INPUTS; i++)
            {
                HdmiInput input = new HdmiInput()
                {
                    InputNumber = i + 1,
                    HotKeyManager = hotKeyManager,
                };
                if (i < inputsTable.RowStyles.Count)
                {
                    inputsTable.RowStyles[i] = new RowStyle(SizeType.AutoSize);
                }
                else
                {
                    inputsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
                input.Dock = DockStyle.Fill;
                inputsTable.Controls.Add(input);
                input.InputRequested += OnInputRequested;

                inputs[i] = input;
            }

            serialConnection = new SerialPort(
                "COM1",
                9600, Parity.None, 8, StopBits.One

            );

            // Populate the list of serial ports.
            foreach (string port in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(port);
            }

            if (portComboBox.Items.Count > 0)
            {
                portComboBox.SelectedIndex = 0;
            }

            // Load previous settings from XML.
            // If the user has no settings, load the defaults.
            LoadSettings();
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
        /// Commands the HDMI switch to change inputs.
        /// </summary>
        /// <remarks>
        /// The input is indexed starting from 1.
        /// 
        /// Sends data over the serial connection to control the HDMI switch.
        /// For the specs for controlling the Monoprice 8x1 HDMI switch, see:
        /// http://support.monoprice.com/link/portal/41053/41056/Article/233/What-are-the-RS-232-commands-for-the-8X1-Enhanced-Powered-HDMI-Switcher-w-Remote-PID-4067
        /// </remarks>
        /// <param name="input">The input to switch to. Must be 1 or higher.</param>
        private void ChangeInput(int input)
        {
            if (!serialConnection.IsOpen)
            {
                try
                {
                    serialConnection.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Unable to open serial connection:\n" + e.Message);
                    return;
                }
            }

            try
            {
                // Command to change inputs:
                // 0x1, 0x1, 0x1, 0x{input}
                byte[] data = { 1, 1, 1, (byte)input };
                serialConnection.Write(data, 0, 4);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to send data:\n" + e.Message);
            }
        }

        /// <summary>
        /// Loads the user's settings from settings.xml.
        /// </summary>
        private void LoadSettings()
        {
            var document = new XmlDocument();
            try
            {
                document.Load("settings.xml");

                delayStepper.Value = Int32.Parse(document.DocumentElement.SelectSingleNode("changedelay").InnerText);

                var inputNodes = document.DocumentElement.SelectNodes("input");
                for (int i = 0; i < inputNodes.Count; i++)
                {
                    var input = inputs[i];
                    foreach (XmlNode hotKeyNode in inputNodes[i].SelectNodes("hotkey"))
                    {
                        var hotKey = new HotKeyManager.HotKey((Keys)Int32.Parse(hotKeyNode.InnerText));
                        hotKey.isCtrl = hotKeyNode.Attributes["ctrl"] != null && Boolean.Parse(hotKeyNode.Attributes["ctrl"].Value);
                        hotKey.isShift = hotKeyNode.Attributes["shift"] != null && Boolean.Parse(hotKeyNode.Attributes["shift"].Value);
                        hotKey.isAlt = hotKeyNode.Attributes["alt"] != null && Boolean.Parse(hotKeyNode.Attributes["alt"].Value);
                        input.AddHotKey(hotKey);
                    }
                }
            }
            catch (Exception)
            {
                // Something blew up, fall back to default settings.
                SetDefaultSettings();
            }
        }

        /// <summary>
        /// Handles a request from an HdmiInput control to change inputs.
        /// </summary>
        /// <param name="s">The HdmiInput control.</param>
        /// <param name="ev">The requested input.</param>
        private void OnInputRequested(Object s, HdmiInput.InputRequestedEventArgs ev)
        {
            if (delayStepper.Value <= 0)
            {
                // No delay, change inputs immediately.
                ChangeInput(ev.inputNumber);
            }
            else
            {
                // Start a timer to change the input momentarily.
                nextInput = ev.inputNumber;
                changeInputTimer.Start();
            }
        }

        /// <summary>
        /// Saves the user's settings to settings.xml.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                // Just some dumb XML serialization.
                var writer = XmlWriter.Create("settings.xml");
                writer.WriteStartDocument();
                writer.WriteStartElement("settings");

                writer.WriteStartElement("changedelay");
                writer.WriteValue((int)delayStepper.Value);
                writer.WriteEndElement();

                // Save hotkeys for each input.
                foreach (var input in inputs)
                {
                    writer.WriteStartElement("input");

                    foreach (var hotKey in input.HotKeys)
                    {
                        writer.WriteStartElement("hotkey");

                        if (hotKey.isCtrl)
                        {
                            writer.WriteStartAttribute("ctrl");
                            writer.WriteValue(true);
                            writer.WriteEndAttribute();
                        }

                        if (hotKey.isAlt)
                        {
                            writer.WriteStartAttribute("alt");
                            writer.WriteValue(true);
                            writer.WriteEndAttribute();
                        }

                        if (hotKey.isShift)
                        {
                            writer.WriteStartAttribute("shift");
                            writer.WriteValue(true);
                            writer.WriteEndAttribute();
                        }

                        writer.WriteValue((int)hotKey.key);
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }
                writer.WriteEndDocument();

                writer.Flush();
            }
            catch (Exception)
            {
                // Failed to write settings. Fail silently.
            }
        }

        /// <summary>
        /// Sets default settings for the first launch of the app.
        /// By default, there is no delay in changing inputs, and the inputs
        /// are bound to the F1 - F8 keys.
        /// </summary>
        private void SetDefaultSettings()
        {
            delayStepper.Value = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].AddHotKey(new HotKeyManager.HotKey(Keys.F1 + i));
            }
        }

        private void changeInputTimer_Tick(object sender, EventArgs e)
        {
            // Delay elapsed -- change input now.
            changeInputTimer.Stop();
            ChangeInput(nextInput);
        }

        private void delayStepper_ValueChanged(object sender, EventArgs e)
        {
            if (delayStepper.Value > 0)
            {
                changeInputTimer.Interval = (int)delayStepper.Value;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Restore window from system tray.
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void portComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (portComboBox.SelectedItem != null)
            {
                if (serialConnection.IsOpen)
                {
                    serialConnection.Close();
                }
                serialConnection.PortName = (string)portComboBox.SelectedItem;
            }
        }
    }
}
