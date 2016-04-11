using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ClipExtender
{
    class DataBaseCommunications
    {
        DataClasses1DataContext database;

        public DataBaseCommunications()
        {
            database = new DataClasses1DataContext();
        }

        public void addCopy(String textToAdd)
        {
            Copy copy = addNewCopy(textToAdd);
            addNewClipboardLine(copy);

        }

        private Copy addNewCopy(String textToAdd)
        {
            Copy newCopy = new Copy();
            newCopy.Text = textToAdd;
            newCopy.DateTime = DateTime.Now;
            database.Copies.InsertOnSubmit(newCopy);
            database.SubmitChanges();
            return newCopy;
        }

        private void addNewClipboardLine(Copy copy)
        {
            ClipboardLine newClipboardLine = new ClipboardLine();
            newClipboardLine.CopyId = copy.Id;
            database.ClipboardLines.InsertOnSubmit(newClipboardLine);
            database.SubmitChanges();
        }

        public void clearClipboard()
        {
            database.ExecuteCommand("TRUNCATE TABLE ClipboardLines");
        }

        public void removeItemFromClipboard(int clipboardLineID)
        {
            ClipboardLine clipboardLine = database.ClipboardLines.Single(l => l.Id == clipboardLineID);
            int copyID = clipboardLine.CopyId;
            database.ExecuteCommand("DELETE FROM ClipboardLines WHERE Id=" + clipboardLineID);
            database.ExecuteCommand("DELETE FROM Copies WHERE Id=" + copyID);
        }

        public void createNewList(string listName, List<string> copies)
        {
            int listEntryId = createNewListEntry(listName);
        }

        private int createNewListEntry(string listName)
        {
            List newList = new List();
            newList.Name = listName;
            database.Lists.InsertOnSubmit(newList);
            database.SubmitChanges();
            return newList.Id;
        }

    }
}
