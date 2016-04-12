//This file is part of ClipExtender.

//ClipExtender is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//ClipExtender is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with ClipExtender.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClipExtender
{
    public class ClipboardCommunication
    {
        Form1 parentForm1;
        DataBaseCommunications dbCommunications;

        //required for sub/unsub to the clipboard listener list
        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        //required to recognize clipboard update messages
        private static int WM_CLIPBOARDUPDATE = 0x031D;

        public ClipboardCommunication(Form1 form, DataBaseCommunications incomingDBCommunications)
        {
            parentForm1 = form;
            dbCommunications = incomingDBCommunications;
        }

        public void beginListeningToClipboard(IntPtr windowHandle)
        {
            //if we cannot add the window as a format listener show a message box and abort
            if (!AddClipboardFormatListener(windowHandle))
            {
                MessageBox.Show("Failed to add clipboard format listener, closing program.");
                Application.Exit();
            }
        }

        public void endListeningToClipBoard(IntPtr windowHandle)
        {
            RemoveClipboardFormatListener(windowHandle);
        }

        public bool handleUpdateMessage(Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                //stops clipExtender from putting itself into an infinite loop every time something is copied
                if (parentForm1.getMessageHasBeenProcessed() == false)
                {
                    //changes the clipboard contents, which resends the clipboard update message
                    //which reruns this subroutine, etc, looping forever without a toggle variable
                    getClipboardData();
                }
                //messageHasBeenProcessed will only ever be true within the first 100ms of having copied something
                //because we are not able to react to the clipboard update message within this time
                //it stops the program from continually looping
                return true;
            }
            else
            {
                return false;
            }
        }

        //pulls the contents of the clipboard and adds to the listbox
        private void getClipboardData()
        {
            String textFromClipboard = pullFromClipboard();

            //if the clipboard does not contain text
            if (textFromClipboard != null)
            {
                //if the string is not already in the listbox, add it to the listbox and database
                if (!parentForm1.findStringInList(textFromClipboard))
                {
                    parentForm1.listBox1.Items.Add(textFromClipboard);
                    dbCommunications.addCopy(textFromClipboard);
                }
            }

            //set runOnce to true so that the clipboard change message will not be acted upon again
            parentForm1.setMessageHasBeenProcessed(true);
            //select the last item in the list, which triggers sending of the clipboard change message
            parentForm1.selectLastItem();
            //start the timer that will reset messageHasBeenProcessed to false
            parentForm1.hasRunOnceTimer.Start();
        }

        //checks if the clipboard currently holds text, if so pulls that text and returns it as a string
        private string pullFromClipboard()
        {
            String clipboardText = null;
            Boolean clipBoardContainsText = Clipboard.ContainsText();
            //test if the clipboard containts text
            if (clipBoardContainsText == true)
            {
                //pull text 
                clipboardText = Clipboard.GetText();
            }

            return clipboardText;
        }
    }
}
