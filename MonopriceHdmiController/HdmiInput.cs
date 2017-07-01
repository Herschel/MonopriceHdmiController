/******************************************************************************
 * Monoprice HDMI Controller
 * Copyright 2017 Michael Welsh
 * Licensed under the MIT License.
 * See License.MD for more info.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MonopriceHdmiController
{
    /// <summary>
    /// A user control representing an HDMI input.
    /// Contains UI for binding hotkeys to this input.
    /// </summary>
    public partial class HdmiInput : UserControl
    {
        private List<HotKeyManager.HotKeyBinding> bindings = new List<HotKeyManager.HotKeyBinding>();
        private HotKeyManager hotKeyManager;
        private int inputNumber = 1;
        private bool isListeningForNewHotKey;

        public HdmiInput()
        {
            InitializeComponent();
            Disposed += delegate (Object sender, EventArgs e) { UnregisterHotKeys(); };
        }

        public delegate void InputRequestedEventHandler(object sender, InputRequestedEventArgs e);

        /// <summary>
        /// Fired whenever the user requests this input by pressing the hotkey
        /// or clicking the Input button.
        /// </summary>
        public event InputRequestedEventHandler InputRequested;

        /// <summary>
        /// The HotKeyManager that controls the hotkeys.
        /// </summary>
        public HotKeyManager HotKeyManager
        {
            get
            {
                return hotKeyManager;
            }
            set
            {
                UnregisterHotKeys();
                hotKeyManager = value;
                RegisterHotKeys();
            }
        }

        /// <summary>
        /// The list of hotkeys bound to this HDMI input.
        /// </summary>
        public IEnumerable<HotKeyManager.HotKey> HotKeys
        {
            get
            {
                return bindings.Select(binding => binding.hotKey);
            }
        }

        /// <summary>
        /// The HDMI input that this control maps to.
        /// </summary>
        [Description("Sets the HDMI input that this button will switch to."), Category("HDMI"),
            DefaultValue(1), Browsable(true)]
        public int InputNumber
        {
            get { return inputNumber; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                inputNumber = value;
                selectInputButton.Text = $"Input {inputNumber}";
            }
        }

        /// <summary>
        /// Assigns a new hotkey to this input.
        /// Whenever the hotkey is pressed, the <c>InputRequested</c> event
        /// will fire.
        /// </summary>
        /// <param name="hotKey">The hotkey to bind to this input.</param>
        public void AddHotKey(HotKeyManager.HotKey hotKey)
        {
            var binding = new HotKeyManager.HotKeyBinding
            {
                hotKey = hotKey,
                handler = OnHotKeyPressed,
            };
            hotKeyManager.AddHotKey(binding);
            bindings.Add(binding);
            UpdateLabel();
        }

        /// <summary>
        /// Clears all hotkeys associated with this input.
        /// </summary>
        public void ClearHotKeys()
        {
            UnregisterHotKeys();
            bindings.Clear();
            UpdateLabel();
        }

        private void RegisterHotKeys()
        {
            if (hotKeyManager != null)
            {
                foreach (var binding in bindings)
                {
                    hotKeyManager.AddHotKey(binding);
                }
            }
        }

        private void OnHotKeyPressed()
        {
            InputRequested(this, new InputRequestedEventArgs(InputNumber));
        }

        private void OnNewHotKeyBound(bool wasRebindSuccessful, HotKeyManager.HotKey hotKey)
        {
            isListeningForNewHotKey = false;
            if (wasRebindSuccessful)
            {
                AddHotKey(hotKey);
            }
        }

        private void UnregisterHotKeys()
        {
            if (hotKeyManager != null)
            {
                foreach (var binding in bindings)
                {
                    hotKeyManager.RemoveHotKey(binding);
                }
            }
        }

        private void UpdateLabel()
        {
            if (bindings.Count > 0)
            {
                hotKeysText.Text = String.Join(", ", HotKeys);
                if (isListeningForNewHotKey)
                {
                    hotKeysText.Text += ", <press a key...>";
                }
            }
            else if (isListeningForNewHotKey)
            {
                hotKeysText.Text = "<press a key...>";
            }
            else
            {
                hotKeysText.Text = "<no hotkey>";
            }
        }

        private void addHotKeyButton_Click(object sender, EventArgs e)
        {
            if (hotKeyManager != null)
            {
                isListeningForNewHotKey = true;
                hotKeyManager.ListenForRebind(OnNewHotKeyBound);
                UpdateLabel();
            }
        }

        private void clearHotKeysButton_Click(object sender, EventArgs e)
        {
            ClearHotKeys();
        }
  
        private void selectInputButton_Click(object sender, EventArgs e)
        {
            InputRequested?.Invoke(this, new InputRequestedEventArgs(InputNumber));
        }

        /// <summary>
        /// Fired when the user request an input change by pressing a hotkey or
        /// clicking the Input button.
        /// </summary>
        public class InputRequestedEventArgs : EventArgs
        {
            public InputRequestedEventArgs(int inputNumber)
            {
                this.inputNumber = inputNumber;
            }

            public int inputNumber { get; }
        }
    }
}
