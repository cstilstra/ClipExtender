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
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClipExtender
{
    public partial class Form1 : Form
    {
        //this boolean is used to control when the clipboard monitor messages are acted upon
        //without it any change in clipboard contents sends the program into an endless loop
        Boolean runOnce = false;

        //I don't know exactly how these next three entries work, but they add the program as a clipboard monitor
        //as well as remove it as a clipboard listener, but I have no idea what the third one is
        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        private static int WM_CLIPBOARDUPDATE = 0x031D;

        //when the program first loads it will clear the contents of the clipboard when the btnFrom is clicked...
        //if the clipboard has not been used since the program opens
        //for example, if you open the program, paste something from the clipboard (even in another program)...
        //and then click the button, it will recognize that content
        //but if you were to open the program and immediately click the button...
        //the contents of the clipboard will be cleared and the 'no text content' message box will be shown
        public Form1()
        {
            InitializeComponent();
            selectLast();
                                    
            //if for some reason we cannot add this window as a format listener, pop a message box
            if (!AddClipboardFormatListener(this.Handle))
            {
                MessageBox.Show("Failed to add clipboard format listener.");
            }
        }

       

        //this function will determine if a string already exists as an item in the listbox and return a boolean value
        private bool findString(string searchString)
        {
            int index = listBox1.FindStringExact(searchString);
            if (index != ListBox.NoMatches)
                return true;
            else
                return false;
        }

        //this function selects the last item in the list
        private void selectLast()
        {
            //variable to hold the number of items in the list
            int numOfEntries = listBox1.Items.Count;
            //variable to hold the index number of the last item in the list
            int lastItem = numOfEntries - 1;
            //if we have entries in the list, select the last one
            if (numOfEntries > 0)
                listBox1.SetSelected(lastItem, true);
        }

        //this handles the selection changing in the listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //create an instance of the selected item (this would be the item after the selection has changed)
            object item = listBox1.SelectedItem;
            //pull the text from that instanced object
            string toClipboard = listBox1.GetItemText(item);
            runOnce = true;
            //send it to the clipboard
            Clipboard.SetDataObject(toClipboard);
            timer1.Start();
        }

        //checks if the clipboard currently holds text, if so pulls that text and returns it as a string
        private string pullFromClip()
        {
            //create the string that will potentially hold the clipboard text
            String returnHtmlText = null;
            Boolean clipContainsText = Clipboard.ContainsText();
            //test if the clipboard containts text
            if (clipContainsText == true)
            
                //if it does, pull that text and assign to string returnHtmlText
                returnHtmlText = Clipboard.GetText();
            
            //the contents of returnHtmlText are returned, whether null or containing text
            else
            
                returnHtmlText = "empty";
            
            return returnHtmlText;
        }

        //this will pull the contents of the clipboard and add to the listbox
        private void btnFrom_Click(object sender, EventArgs e)
        {
            getClipboardData();
        }

        //this will clear the listbox as well as the clipboard
        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Clipboard.Clear();
        }

        //this will remove the current selection from the listbox and set the seletion to whatever was the next item
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int currentSelection = listBox1.SelectedIndex;
            listBox1.Items.Remove(listBox1.SelectedItem);
            selectNext(currentSelection);
        }

        private void selectNext(int currSelect)
        {
            //variable to hold the number of items in the list
            int numOfEntries = listBox1.Items.Count;
            //if we have entries in the list, select the next entry one
            if (numOfEntries > 0)
                listBox1.SetSelected(currSelect, true);
        }

        //this will pull the contents of the clipboard and add to the listbox
        private void getClipboardData()
        {
            //pull the text contents of the clipboard
            //if no text content exists, this will return a null value
            String textFromClip = pullFromClip();
            //declare a boolean and populate it the results of testing if textFromClip is found in the listbox
            Boolean textAlreadyInList = findString(textFromClip);

            //if the clipboard does not contain text...
            if (textFromClip == "empty")
                //then show us a message box saying so...
                MessageBox.Show("The Clipboard does not contain text at this time.");

                //otherwise (if the clipboard does contain text)...
            //there seems to be an issue with running this second if statement
            //when the second statement is run, this function returns both message boxes
            //"The Clipboard does not contain text at this time" and...
            //"This item is already copied to ClipExtender"...
            //and then puts the duplicate text in the listbox anyway
            else if (textFromClip != "empty")
                ////if the string is already in the listbox show us a message box
                //    if (textAlreadyInList == true);
                //{
                //    MessageBox.Show("This item is already copied to ClipExtender");
                //}
                ////if the string is not already in the listbox then add it and select it as the current copy
                //if (textAlreadyInList == false);
                //{
                listBox1.Items.Add(textFromClip);

            //set runOnce to true so that the clipboard change message will not be acted upon again...
            //which would result in an endless loop
                runOnce = true;

            //select the last item in the list, which causes sending of the clipboard change message
                selectLast();
            //start the timer
            //once the timer has ticked, runOnce will be set back to false...
            //enabling the clipboard change message to be acted upon again
                timer1.Start();                
            //}
            
        }

        //not sure how this works, but it is what handles the message that is sent when the clipboard updates
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {              
                if (runOnce == false)
                {
                    getClipboardData();
                }
                else if (runOnce == true)
                {
                }
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }

        //when we close the form, remove it as a clipboard monitor
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveClipboardFormatListener(this.Handle);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            runOnce = false;
        }

        private void aboutClipExtenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About AboutFRM = new About();
            Form1 ParentForm = new Form1();
            AboutFRM.StartPosition = ParentForm.StartPosition;
            AboutFRM.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
