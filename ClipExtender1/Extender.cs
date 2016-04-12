using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ClipExtender
{
    public class Extender
    {

        Form1 parentForm;
        DataBaseCommunications dbCommunications;
        ClipboardCommunication clipboardCommunication;

        List<Copy> currentClipboard;

        public Extender(Form1 callingForm)
        {
            setUpReferences(callingForm);
            clearClipboard();
        }

        private void setUpReferences(Form1 callingForm)
        {
            parentForm = callingForm;
            dbCommunications = new DataBaseCommunications();
            clipboardCommunication = new ClipboardCommunication(parentForm, dbCommunications);
            currentClipboard = new List<Copy>();
        }

        public ClipboardCommunication getClipboardCommunication()
        {
            return clipboardCommunication;
        }

        public void clearClipboard()
        {
            parentForm.listBox1.Items.Clear();
            dbCommunications.clearClipboard();
        }

        public void removeItemFromClipboard(int listboxIndex)
        {
            int clipboardLineId = listboxIndex + 1;
            dbCommunications.removeItemFromClipboard(clipboardLineId);
        }

        public void createNewList(string listName)
        {
            dbCommunications.createNewList(listName);
        }

        public void openList(int listId)
        {
            clearClipboard();
            List<string> copiesOnList = dbCommunications.getCopyTextOnList(listId);
            parentForm.setListboxItems(copiesOnList);
        }
    }
}
