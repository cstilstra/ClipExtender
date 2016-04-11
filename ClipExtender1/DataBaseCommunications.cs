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

    }
}
