/******************************************************************************
 * Monoprice HDMI Controller
 * Copyright 2017 Michael Welsh
 * Licensed under the MIT License.
 * See License.MD for more info.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MonopriceHdmiController
{
    /// <summary>
    /// Manages a Win32 system hook to capture hotkey presses.
    /// </summary>
    public class HotKeyManager : IDisposable
    {
        // Win32 keyboard message constants
        private static int WH_KEYBOARD_LL = 13;

        private static int WM_KEYDOWN = 0x100;
        private static int WM_KEYUP = 0x101;
        private static int WM_SYSKEYDOWN = 0x104;
        private static int WM_SYSKEYUP = 0x105;

        private List<HotKeyBinding> bindings = new List<HotKeyBinding>();
        private HotKey currentHotKey;               // The current hotkey that the user pressed.
        private int hHotKeyHook = 0;                // Handle to Win32 system hook.
        HookProc hotKeyHook;                        // Win32 keyboard hook procedure.
        private HotKeyRebindDelegate rebindHandler; // Used when creating a new hotkey.

        // Win32 system hook imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// Creating a <c>HotKeyManager</c> will register a Win32 system hook.
        /// The system hook will remain until the HotKeyManager is disposed.
        /// </summary>
        public HotKeyManager()
        {
            hotKeyHook = new HookProc(this.HotKeyHook);
            hHotKeyHook = SetWindowsHookEx(WH_KEYBOARD_LL, hotKeyHook, (IntPtr)0, 0);
            if (hHotKeyHook == 0)
            {
                throw new System.Exception("Unable to create Windows keyboard hook.");
            }
        }

        // Win32 system hook signature.
        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        // Fires when a registered hotkey is pressed.
        public delegate void HotKeyDelegate();

        // Fires when listening for a new hotkey.
        public delegate void HotKeyRebindDelegate(bool wasRebindSuccessful, HotKey hotKey);

        /// <summary>
        /// Adds a binding for the specified hotkey.
        /// The handler will be called when the hotkey is pressed.
        /// </summary>
        /// <param name="binding">The hotkey and the handler to call when the hotkey is pressed.</param>
        public void AddHotKey(HotKeyBinding binding)
        {
            bindings.Add(binding);
        }

        /// <summary>
        /// Register a delegate to be called the next time the user presses
        /// any hotkey.
        /// Used to create a new binding by listening for user input.
        /// </summary>
        /// <remarks>
        /// All hotkeys registered with AddHotKey will be blocked until after
        /// this delegate is called.
        /// When any keyboard combination is pressed, the delegate will be
        /// called containing the pressed hotkey.
        /// The client is then free to register a new binding with this hotkey
        /// using <c>AddHotKey</c>.
        /// If the rebinding fails, the delegate will be called with
        /// <c>wasRebindSuccessful</c> set to <c>false</c>.
        /// </remarks>
        /// <param name="handler">The handler for the next hotkey press.</param>
        public void ListenForRebind(HotKeyRebindDelegate handler)
        {
            if (rebindHandler != null)
            {
                rebindHandler(false, new HotKey());
            }
            rebindHandler = handler;
        }

        /// <summary>
        /// Removes the specified binding.
        /// </summary>
        /// <param name="binding">The binding to remove.</param>
        /// <returns>Whether the binding was found and removed.</returns>
        public bool RemoveHotKey(HotKeyBinding binding)
        {
            return bindings.Remove(binding);
        }

        // Win32 system hook handler.
        private int HotKeyHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // See https://msdn.microsoft.com/en-us/library/windows/desktop/ms644985(v=vs.85).aspx
            // for info on Windows keyboard hooks.
            if (nCode == 0)
            {
                KBDLLHOOKSTRUCT keyboardData = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                Keys key = (Keys)keyboardData.vkCode;
                if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
                {
                    // If this was a modifier key press, then update the modifier state.
                    switch (key)
                    {
                        case Keys.Control:
                        case Keys.ControlKey:
                        case Keys.LControlKey:
                        case Keys.RControlKey:
                            currentHotKey.isCtrl = true;
                            break;

                        case Keys.Shift:
                        case Keys.ShiftKey:
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            currentHotKey.isShift = true;
                            break;

                        case Keys.Menu:
                        case Keys.LMenu:
                        case Keys.RMenu:
                            currentHotKey.isAlt = true;
                            break;

                        case Keys.LWin:
                        case Keys.RWin:
                            currentHotKey.isWindows = true;
                            break;

                        default:
                            // If it was a normal key press, fire any associated bindings.
                            currentHotKey.key = key;
                            if (rebindHandler != null)
                            {
                                // We are listening for a rebind.
                                // Fire the rebind handler and consume the input.
                                rebindHandler(true, currentHotKey);
                                rebindHandler = null;
                                return -1;
                            }
                            else
                            {
                                // Look for a matching binding and fire it.
                                foreach (var binding in bindings)
                                {
                                    if (binding.hotKey.Equals(currentHotKey))
                                    {
                                        binding.handler();
                                    }
                                }
                            }
                            break;
                    }
                }
                else if (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
                {
                    // If a modifier key is released, update modifier key state.
                    switch (key)
                    {
                        case Keys.Control:
                        case Keys.ControlKey:
                        case Keys.LControlKey:
                        case Keys.RControlKey:
                            currentHotKey.isCtrl = false;
                            break;

                        case Keys.Shift:
                        case Keys.ShiftKey:
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            currentHotKey.isShift = false;
                            break;

                        case Keys.Menu:
                        case Keys.LMenu:
                        case Keys.RMenu:
                            currentHotKey.isAlt = false;
                            break;

                        case Keys.LWin:
                        case Keys.RWin:
                            currentHotKey.isWindows = false;
                            break;
                    }
                }
            }

            return CallNextHookEx(hHotKeyHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// Unregisters the Win32 system hook and clears all bindings.
        /// </summary>
        public void Dispose()
        {
            if (hHotKeyHook != 0)
            {
                UnhookWindowsHookEx(hHotKeyHook);
                hHotKeyHook = 0;
            }
            bindings.Clear();
        }

        /// <summary>
        /// A hotkey is a keyboard combination of a normal key plus modifier keys.
        /// </summary>
        public struct HotKey
        {
            public bool isShift;
            public bool isCtrl;
            public bool isAlt;
            public bool isWindows;
            public Keys key;

            public HotKey(Keys key = Keys.None)
            {
                this.key = key;
                isShift = false;
                isCtrl = false;
                isAlt = false;
                isWindows = false;
            }

            public override String ToString()
            {
                String str = "";
                if (isCtrl)
                {
                    str += "Ctrl + ";
                }
                if (isShift)
                {
                    str += "Shift + ";
                }
                if (isAlt)
                {
                    str += "Alt + ";
                }
                if (isWindows)
                {
                    str += "Win + ";
                }
                str += key.ToString();
                return str;
            }
        }

        /// <summary>
        /// A bindining links a specific hotkey to a delegate.
        /// The delegate will be called when the hotkey is pressed.
        /// </summary>
        public class HotKeyBinding
        {
            public HotKey hotKey;
            public HotKeyDelegate handler;
        }

        // Win32 keyboard hook struct
        [StructLayout(LayoutKind.Sequential)]
        private class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
    }
}
