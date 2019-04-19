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

namespace MonopriceHdmiController
{
    public partial class HdmiSwitch : UserControl
    {
        private String deviceName;
        private HotKeyManager hotKeyManager;
        private List<HdmiInput> inputs;
        private SerialPort serialConnection;

        public HdmiSwitch(HotKeyManager hotKeyManager)
        {
            deviceName = "Switch 1";

            this.hotKeyManager = hotKeyManager;
            InitializeComponent();
        }

        /// <summary>
        /// A user-specified name for this HDMI switch.
        /// </summary>
        public String DeviceName
        {
            get
            {
                return deviceName;
            }
            set
            {
                deviceName = value;
                if (textDeviceName != null)
                {
                    textDeviceName.Text = value;
                }
            }
        }

        public delegate void DeviceNameChangedHandler(object sender, String name);

        /// <summary>
        /// Fired whenever the name of this HDMI switch is changed by the user.
        /// </summary>
        public event DeviceNameChangedHandler DeviceNameChanged;

        public delegate void InputRequestedHandler(object sender, HdmiSwitch hdmiSwitch, int requestedInput);

        /// <summary>
        /// Fired whenever the user requests this input by pressing the hotkey
        /// or clicking the Input button.
        /// </summary>
        public event InputRequestedHandler InputRequestedEventHandler;

        private void HdmiSwitch_Load(object sender, EventArgs e)
        {
            textDeviceName.Text = deviceName;

            inputs = new List<HdmiInput>();
            inputsTable.RowCount = 8;
            for (int i = 0; i < 8; i++)
            {
                var input = new HdmiInput()
                {
                    InputNumber = i + 1,
                    HotKeyManager = hotKeyManager,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
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
                //input.InputRequested += OnInputRequested;

                inputs.Add(input);
            }

            foreach (string port in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(port);
            }

            if (portComboBox.Items.Count > 0)
            {
                portComboBox.SelectedIndex = 0;
            }
        }

        private void portComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (portComboBox.SelectedItem != null)
            //{
            //    if (serialConnection.IsOpen)
            //    {
            //        serialConnection.Close();
            //    }
            //    serialConnection.PortName = (string)portComboBox.SelectedItem;
            //}
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

        private void textDeviceName_TextChanged(object sender, EventArgs e)
        {
            deviceName = textDeviceName.Text;
            DeviceNameChanged?.Invoke(this, deviceName);
        }
    }
}
