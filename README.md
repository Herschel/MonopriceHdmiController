# Monoprice HDMI Controller

[![Build Status](https://travis-ci.org/Herschel/MonopriceHdmiController.svg?branch=master)](https://travis-ci.org/Herschel/MonopriceHdmiController)

A Windows application for controlling the [Monoprice 8x1 Enhanced Powered HDMI Switcher](https://www.monoprice.com/product?p_id=4067) over RS-232.

I have not tested this with other brands or models of HDMI switch. The serial communication specs for this HDMI switch are found in the [Monoprice support database](http://support.monoprice.com/link/portal/41053/41056/Article/233/What-are-the-RS-232-commands-for-the-8X1-Enhanced-Powered-HDMI-Switcher-w-Remote-PID-4067).

# Usage
 - Requires [.NET Framework 4.7](https://www.microsoft.com/net/download/framework).
 - Connect the HDMI switch via serial connection to your computer. I use a [UGREEN RS 232 to USB](https://www.amazon.com/UGREEN-Converter-Adapter-Chipset-Windows/dp/B00QUZY4UG/) adapter.
 - Run HdmiController.exe. It will appear in the Windows system tray when minimized.
 - Ensure that the proper serial port is selected in the Port drop-down.
 - Click an Input button or press a hotkey to change the HDMI input. The hotkeys are F1 - F8 by default.
 - You can bind a new hotkey for an HDMI input by clicking the + button and pressing a key combination.
 - Multiple hotkeys can be bound to a single input.
 - Clear all hotkeys for an HDMI input by clicking the - button.
 - Use the Delay setting to delay input changes by the specified number of milliseconds. This is useful if you need to hide the change behind a transition.

# Licesnse
This application is licensed under the [MIT License](https://opensource.org/licenses/MIT). See LICENSE.MD for the complete license.