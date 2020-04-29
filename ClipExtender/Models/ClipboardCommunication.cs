// This file is part of ClipExtender.

// ClipExtender is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// ClipExtender is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with ClipExtender.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace ClipExtender
{
    public class ClipboardCommunication
    {
        #region Constants

        // required to recognize clipboard update messages
        private static int WM_CLIPBOARDUPDATE = 0x031D;

        #endregion

        #region DLL Imports

        // required for sub/unsub to the clipboard listener list
        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        #endregion

        #region Public Functions

        public void BeginListeningToClipboard(IntPtr windowHandle)
        {
            if (!AddClipboardFormatListener(windowHandle))
            {
                MessageBox.Show("Failed to add clipboard format listener, closing program.");
                Application.Current.Shutdown();
            }
        }

        public void EndListeningToClipBoard(IntPtr windowHandle)
        {
            RemoveClipboardFormatListener(windowHandle);
        }

        public bool HandleUpdateMessage(int message, ref string text)
        {
            if (message == WM_CLIPBOARDUPDATE)
            {
                text = PullFromClipboard();
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Private Helpers

        private string PullFromClipboard()
        {
            string clipboardText = null;
            if (Clipboard.ContainsText())
            {
                clipboardText = Clipboard.GetText();
            }
            return clipboardText;
        }

        #endregion
    }
}
