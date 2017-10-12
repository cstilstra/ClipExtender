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
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClipExtender
{
    public partial class Form1 : Form
    {

        // used to determine if a clipboard change has already been acted upon
        private Boolean messageHasBeenProcessed = false;

        // the path that the application executable is installed to
        String appPath = Application.StartupPath;

        Extender extender;
        ClipboardCommunication clipboardCommunication;
       
        public Form1()
        {
            InitializeComponent();
            SetUp();
        }

        // sets up references and starting state
        private void SetUp()
        {
            SelectLastItem();
            extender = new Extender(this);
            clipboardCommunication = extender.getClipboardCommunication();
            clipboardCommunication.beginListeningToClipboard(this.Handle);
        }

        // selects the last item in the list
        public void SelectLastItem()
        {
            int numberOfItems = listBox1.Items.Count;
            int lastItemIndex = numberOfItems - 1;
            if (numberOfItems > 0)
            {
                listBox1.SetSelected(lastItemIndex, true);
            }
        }

        // handles the message that is sent when the clipboard updates
        protected override void DefWndProc(ref Message m)
        {
            // if the clipboard extension doesn't need the messge
            if (!clipboardCommunication.handleUpdateMessage(m))
            {
                base.DefWndProc(ref m);
            }            
        }        

        // determines if a string already exists as an item in the listbox
        public bool IsStringInList(string searchString)
        {
            int index = listBox1.FindStringExact(searchString);
            if (index == ListBox.NoMatches)
                return false;
            else
                return true;
        }

        // handles the selection changing in the listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the text from the newly selected item
            string toClipboard = listBox1.GetItemText(listBox1.SelectedItem);
            // indicate that the message has been processed
            SetMessageHasBeenProcessed(true);
            // send it to the clipboard
            Clipboard.SetDataObject(toClipboard);
            // start the timer that will reset messageHasBeenProcessed to false
            hasRunOnceTimer.Start();
        }

        // clears the listbox and clipboard
        private void btnClear_Click(object sender, EventArgs e)
        {
            extender.clearClipboard();
            Clipboard.Clear();
        }

        // removes the current selection from the listbox and sets the seletion to the next item
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int currentSelectionIndex = listBox1.SelectedIndex;
            listBox1.Items.Remove(listBox1.SelectedItem);
            extender.removeItemFromClipboard(currentSelectionIndex);
            SelectNextItemAfterDeletion(currentSelectionIndex);            
        }

        // selects the next item in the list after the just deleted item
        private void SelectNextItemAfterDeletion(int deletedIndex)
        {
            int numberOfItems = listBox1.Items.Count;
            if (numberOfItems > 0)
            {
                // if the deleted entry was the last in the list
                if (numberOfItems == deletedIndex)
                {
                    // decrement deletedIndex to select the new last item 
                    deletedIndex--;
                }
                listBox1.SetSelected(deletedIndex, true);
            }
        }

        private void aboutClipExtenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About AboutFRM = new About();
            Form1 ParentForm = this;
            AboutFRM.StartPosition = ParentForm.StartPosition;
            AboutFRM.Show();
        }

        // exits the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        // handles the boolean that keeps the program out of an infinite loop
        private void timer1_Tick(object sender, EventArgs e)
        {
           SetMessageHasBeenProcessed(false);
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // remove form as clipboard listener
            clipboardCommunication.endListeningToClipBoard(this.Handle);
        }

        // the messageHasBeenProcessed represents whether or not a new copy to the clipboard has been handled 
        public void SetMessageHasBeenProcessed(bool value)
        {
            messageHasBeenProcessed = value;
        }

        public bool getMessageHasBeenProcessed()
        {
            return messageHasBeenProcessed;
        }

        // opens the window to open an existing saved list
        private void ListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenListForm ListViewerFRM = new OpenListForm(extender);
            Form1 ParentForm = this;
            ListViewerFRM.StartPosition = ParentForm.StartPosition;
            ListViewerFRM.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.ClipboardLines' table. You can move, or remove it, as needed.
            this.clipboardLinesTableAdapter.Fill(this.database1DataSet.ClipboardLines);
        }

        // opens the window to name a new list
        private void SaveAsNewListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NameNewList NameNewListFRM = new NameNewList();
            NameNewListFRM.StartPosition = this.StartPosition;
            NameNewListFRM.setExtender(extender);
            NameNewListFRM.Show();
        }

        public void SetListboxItems(List<string> items)
        {
            foreach (string item in items)
            {
                listBox1.Items.Add(item);
            }
        }

        public void AddItemToListbox(string item)
        {
            listBox1.Items.Add(item);
            SelectLastItem();
        }

        public void SelectItemInListbox(string item)
        {
            int index = listBox1.FindStringExact(item);
            if(index != -1)
            {
                listBox1.SetSelected(index, true);
            }
        }

        public void ClearListboxItems()
        {
            listBox1.Items.Clear();
        }

        public void StartHasRunOnceTimer()
        {
            hasRunOnceTimer.Start();
        }
    }
}
  