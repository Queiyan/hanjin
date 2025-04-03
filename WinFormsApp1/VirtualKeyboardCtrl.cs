using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace WinFormsApp1
{
    public static class VirtualKeyboardCtrl
    {
        public static bool IsShowingKeyboard { get; private set; } = false;
        public static bool IsShowingNumpad { get; private set; } = false;
        public static CustomVirtualKeyboard KeyboardFormInstance { get; private set; }
        public static NumpadForm NumpadFormInstance { get; private set; }

        public static void OpenVirtualKeyboard(RichTextBox target)
        {
            if (!MsgWindow.IsShowing)
            {
                CloseVirtualNumpad(); // Ensure numpad is closed
                IsShowingKeyboard = true;
                //KeyboardFormInstance = new CustomVirtualKeyboard(target);
                KeyboardFormInstance.FormClosed += (s, e) => IsShowingKeyboard = false; // Reset flag on close
            }
        }

        public static void CloseVirtualKeyboard()
        {
            if (KeyboardFormInstance != null)
            {
                KeyboardFormInstance.Close();
                KeyboardFormInstance = null;
                IsShowingKeyboard = false;
            }
        }

        public static void OpenVirtualNumpad(RichTextBox target)
        {
            if (!MsgWindow.IsShowing)
            {
                CloseVirtualKeyboard(); // Ensure keyboard is closed
                IsShowingNumpad = true;
                NumpadFormInstance = new NumpadForm();
                NumpadFormInstance.FormClosed += (s, e) => IsShowingNumpad = false; // Reset flag on close
            }
        }

        public static void CloseVirtualNumpad()
        {
            if (NumpadFormInstance != null)
            {
                NumpadFormInstance.Close();
                NumpadFormInstance = null;
                IsShowingNumpad = false;
            }
        }
    }
}
