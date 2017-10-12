using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
=======
using System.Diagnostics;
using System.Linq;
using System.Text;
>>>>>>> f32573743ed93f761f4c1aabaeb61d1b5b769085

namespace ClipExtender
{
    public class DataBaseCommunications
    {
        DataClasses1DataContext database;

        public DataBaseCommunications()
        {
            database = new DataClasses1DataContext();
        }

        public void addCopy(String textToAdd)
        {
            Copy line = addNewCopy(textToAdd);
            addToClipboard(line);

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

        private void addToClipboard(Copy copy)
        {
            ClipboardLine newClipboardLine = new ClipboardLine();
            newClipboardLine.CopyId = copy.Id;
            database.ClipboardLines.InsertOnSubmit(newClipboardLine);
            database.SubmitChanges();
        }

        public void clearClipboard()
        {
            database.ExecuteCommand("TRUNCATE TABLE ClipboardLines");
            refreshDatabaseConnection();
        }

        private void refreshDatabaseConnection()
        {
<<<<<<< HEAD
            // reinitialize the database connection to clear out references
=======
            //reinitialize the database connection to clear out references
>>>>>>> f32573743ed93f761f4c1aabaeb61d1b5b769085
            database = new DataClasses1DataContext();
        }

        public void removeItemFromClipboard(int clipboardLineID)
        {
            ClipboardLine clipboardLine = database.ClipboardLines.Single(l => l.Id == clipboardLineID);
            int copyID = clipboardLine.CopyId;
            database.ExecuteCommand("DELETE FROM ClipboardLines WHERE Id=" + clipboardLineID);
            refreshDatabaseConnection();
        }

        public void createNewList(string listName)
        {
            int listEntryId = createNewListEntry(listName);
            createLinksToCurrentCopies(listEntryId);
        }

        private int createNewListEntry(string listName)
        {
            List newList = new List();
            newList.Name = listName;
            database.Lists.InsertOnSubmit(newList);
            database.SubmitChanges();
            return newList.Id;
        }

        private void createLinksToCurrentCopies(int listID)
        {
            List<int> copyIds = getCurrentCopyIds();

            foreach(int copyID in copyIds)
            {
                ListLine newListLine = new ListLine();
                newListLine.ListId = listID;
                newListLine.CopyId = copyID;
                database.ListLines.InsertOnSubmit(newListLine);
            }

            database.SubmitChanges();
        }

        private List<int> getCurrentCopyIds()
        {
            List<int> copyIds = new List<int>();
            var clipboardLines = database.ClipboardLines;
            foreach(var line in clipboardLines)
            {
                copyIds.Add(line.CopyId);
            }
            return copyIds;
        }

        public List<string> getCopyTextOnList(int listId)
        {
            List<string> copiesOnList = new List<string>();

            List<int> copyIdsOnList = getCopyIdsOnList(listId);
            foreach(int id in copyIdsOnList)
            {
<<<<<<< HEAD
                // get the copy from the database with a matching id
=======
                //get the copy from the database with a matching id
>>>>>>> f32573743ed93f761f4c1aabaeb61d1b5b769085
                Copy copyFromList = database.Copies.Single(c => c.Id == id);
                copiesOnList.Add(copyFromList.Text);
            }
            return copiesOnList;
        }

        private List<int> getCopyIdsOnList(int listId)
        {
            List<int> copyIdsOnList = new List<int>();
<<<<<<< HEAD
            // get all of the list lines that match the list
=======
            //get all of the list lines that match the list
>>>>>>> f32573743ed93f761f4c1aabaeb61d1b5b769085
            var listLinesFromList = database.ListLines.Where(l => l.ListId == listId);
            foreach(ListLine line in listLinesFromList)
            {
                copyIdsOnList.Add(line.CopyId);
            }

            return copyIdsOnList;
        }

        public void addListItemsToClipboard(int listId)
        {
            List<int> copyIdsOnList = getCopyIdsOnList(listId);
            foreach (int id in copyIdsOnList)
            {
                ClipboardLine newLine = new ClipboardLine();
                newLine.CopyId = id;
                database.ClipboardLines.InsertOnSubmit(newLine);
            }
            database.SubmitChanges();
        }
    }
}
