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
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClipExtender
{
    public partial class Form1 : Form
    {

        //used to determine if a clipboard change has already been acted upon
        Boolean hasRunOnce = false;

        //the path that the application executable is installed to
        String appPath = Application.StartupPath;

        //required for sub/unsub to the clipboard listener list
        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        //required to recognize clipboard update messages
        private static int WM_CLIPBOARDUPDATE = 0x031D;

       
        public Form1()
        {
            InitializeComponent();
            selectLastItem();           

            //if we cannot add this window as a format listener show a message box and abort
            if (!AddClipboardFormatListener(this.Handle))
            {
                MessageBox.Show("Failed to add clipboard format listener, closing program.");
                Application.Exit();
            }
        }

        //selects the last item in the list
        private void selectLastItem()
        {
            //variable to hold the number of items in the list
            int numberOfItems = listBox1.Items.Count;
            //variable to hold the index number of the last item in the list
            int lastItem = numberOfItems - 1;
            //if we have entries in the list, select the last one
            if (numberOfItems > 0)
                listBox1.SetSelected(lastItem, true);
        }

        //selects the next item in the list after the currently selected item
        private void selectNextItem(int currentSelectedIndex)
        {
            //variable to hold the number of items in the list
            int numOfEntries = listBox1.Items.Count;

            //if we have entries in the list, select the next entry
            if (numOfEntries > 0)
            {
                //if the selected entry was the last in the list
                if (numOfEntries == currentSelectedIndex)
                {
                    //take one from currentSelectedIndex in order to avoid error
                    currentSelectedIndex--;
                }
                listBox1.SetSelected(currentSelectedIndex, true);
            }
        }

        //handles the message that is sent when the clipboard updates
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                //stops clipExtender from putting itself into an infinite loop every time something is copied
                if (hasRunOnce == false)
                {
                    //changes the clipboard contents, which resends the clipboard update message
                    //which reruns this subroutine, etc, looping forever without a toggle variable
                    getClipboardData();
                }
                //hasRunOnce will only ever be true within the first 100ms of having copied something
                //because we are not able to react to the clipboard update message within this time
                //it stops the program from continually looping
                else if (hasRunOnce == true) { }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        //this will pull the contents of the clipboard and add to the listbox
        private void getClipboardData()
        {
            //pull the text contents of the clipboard
            //if no text content exists, this will return a null value
            String textFromClipboard = pullFromClipboard();
            ////declare a boolean and populate it the results of testing if textFromClip is found in the listbox
            ////this is currently useless since the code resulting from this test is non functional
            //Boolean textAlreadyInList = findString(textFromClip);

            //if the clipboard does not contain text
            if (textFromClipboard == null)
            {
                //then show us a message box saying so
                MessageBox.Show("The Clipboard does not contain text at this time.");

                //otherwise (if the clipboard does contain text)
            }
            else {
                //if the string is not already in the listbox add it
                if (!findStringInList(textFromClipboard))
                {                    
                    listBox1.Items.Add(textFromClipboard);
                }
            }

            //set runOnce to true so that the clipboard change message will not be acted upon again
            //which would result in an endless loop
            hasRunOnce = true;

            //select the last item in the list, which causes sending of the clipboard change message
            selectLastItem();
            //start the timer
            //once the timer has ticked, runOnce will be set back to false
            //enabling the clipboard change message to be acted upon again
            timer1.Start();
            //}

        }

        //checks if the clipboard currently holds text, if so pulls that text and returns it as a string
        private string pullFromClipboard()
        {
            //create the string that will potentially hold the clipboard text
            String returnHtmlText = null;
            Boolean clipBoardContainsText = Clipboard.ContainsText();
            //test if the clipboard containts text
            if (clipBoardContainsText == true)
            {
                //pull text 
                returnHtmlText = Clipboard.GetText();
            }

            return returnHtmlText;
        }

        //determines if a string already exists as an item in the listbox and returns a boolean value
        private bool findStringInList(string searchString)
        {
            int index = listBox1.FindStringExact(searchString);
            //if index is NOT equal to listbox.nomatches = if there is a match
            if (index == ListBox.NoMatches)
                return false;
            else
                return true;
        }

        //handles the selection changing in the listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //create an instance of the selected item (this would be the item after the selection has changed)
            object item = listBox1.SelectedItem;
            //pull the text from that instanced object
            string toClipboard = listBox1.GetItemText(item);
            hasRunOnce = true;
            //send it to the clipboard
            Clipboard.SetDataObject(toClipboard);
            //start the timer
            //once the timer has ticked, runOnce will be set back to false
            //enabling the clipboard change message to be acted upon again
            timer1.Start();
        }

        //clears the listbox and clipboard
        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Clipboard.Clear();
        }

        //removes the current selection from the listbox and sets the seletion to the next item
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int currentSelection = listBox1.SelectedIndex;
            listBox1.Items.Remove(listBox1.SelectedItem);
            selectNextItem(currentSelection);
        } 

        //opens the "About" form
        private void aboutClipExtenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //About AboutFRM = new About();
            Form1 ParentForm = new Form1();
            //AboutFRM.StartPosition = ParentForm.StartPosition;
            //AboutFRM.Show();
        }

        //exits the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //this timer will be used to reset the runOnce variable to false
        //because this timer ticks at an interval of 100ms
        //it is not possible to copy something within 100ms of having copied before
        private void timer1_Tick(object sender, EventArgs e)
        {
            hasRunOnce = false;
        }

        //when we close the form, remove it as a clipboard monitor
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveClipboardFormatListener(this.Handle);
            Directory.Delete(appPath + "Temp");
        }
    }
}
  