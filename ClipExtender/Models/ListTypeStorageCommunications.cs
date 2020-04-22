using ClipExtender.Models;
using System.Collections.Generic;

namespace ClipExtender
{
    public class ListTypeStorageCommunications : IStorageCommunications
    {
        //DataClasses1DataContext database;
        List<string> storage;

        public ListTypeStorageCommunications()
        {
            ClearClipboard();
        }

        public void AddCopy(string textToAdd)
        {
            storage.Add(textToAdd);

        }

        //private Copy addNewCopy(String textToAdd)
        //{
        //    Copy newCopy = new Copy();
        //    newCopy.Text = textToAdd;
        //    newCopy.DateTime = DateTime.Now;
        //    database.Copies.InsertOnSubmit(newCopy);
        //    database.SubmitChanges();
        //    return newCopy;
        //}

        //private void addToClipboard(Copy copy)
        //{
        //    ClipboardLine newClipboardLine = new ClipboardLine();
        //    newClipboardLine.CopyId = copy.Id;
        //    database.ClipboardLines.InsertOnSubmit(newClipboardLine);
        //    database.SubmitChanges();
        //}

        public void ClearClipboard()
        {
            storage = new List<string>();
        }

        public IEnumerable<string> GetStorageItems()
        {
            return storage;
        }

        //private void refreshDatabaseConnection()
        //{
        //    // reinitialize the database connection to clear out references
        //    database = new DataClasses1DataContext();
        //}

        public void RemoveItemFromClipboard(int clipboardLineID)
        {
            //ClipboardLine clipboardLine = database.ClipboardLines.Single(l => l.Id == clipboardLineID);
            //int copyID = clipboardLine.CopyId;
            //database.ExecuteCommand("DELETE FROM ClipboardLines WHERE Id=" + clipboardLineID);
            //refreshDatabaseConnection();
        }

        //public void createNewList(string listName)
        //{
        //    int listEntryId = createNewListEntry(listName);
        //    createLinksToCurrentCopies(listEntryId);
        //}

        //private int createNewListEntry(string listName)
        //{
        //    List newList = new List();
        //    newList.Name = listName;
        //    database.Lists.InsertOnSubmit(newList);
        //    database.SubmitChanges();
        //    return newList.Id;
        //}

        //private void createLinksToCurrentCopies(int listID)
        //{
        //    List<int> copyIds = getCurrentCopyIds();

        //    foreach(int copyID in copyIds)
        //    {
        //        ListLine newListLine = new ListLine();
        //        newListLine.ListId = listID;
        //        newListLine.CopyId = copyID;
        //        database.ListLines.InsertOnSubmit(newListLine);
        //    }

        //    database.SubmitChanges();
        //}

        //private List<int> getCurrentCopyIds()
        //{
        //    List<int> copyIds = new List<int>();
        //    var clipboardLines = database.ClipboardLines;
        //    foreach(var line in clipboardLines)
        //    {
        //        copyIds.Add(line.CopyId);
        //    }
        //    return copyIds;
        //}

        //public List<string> getCopyTextOnList(int listId)
        //{
        //    List<string> copiesOnList = new List<string>();

        //    List<int> copyIdsOnList = getCopyIdsOnList(listId);
        //    foreach(int id in copyIdsOnList)
        //    {
        //        // get the copy from the database with a matching id
        //        Copy copyFromList = database.Copies.Single(c => c.Id == id);
        //        copiesOnList.Add(copyFromList.Text);
        //    }
        //    return copiesOnList;
        //}

        //private List<int> getCopyIdsOnList(int listId)
        //{
        //    List<int> copyIdsOnList = new List<int>();
        //    // get all of the list lines that match the list
        //    var listLinesFromList = database.ListLines.Where(l => l.ListId == listId);
        //    foreach(ListLine line in listLinesFromList)
        //    {
        //        copyIdsOnList.Add(line.CopyId);
        //    }

        //    return copyIdsOnList;
        //}

        //public void addListItemsToClipboard(int listId)
        //{
        //    List<int> copyIdsOnList = getCopyIdsOnList(listId);
        //    foreach (int id in copyIdsOnList)
        //    {
        //        ClipboardLine newLine = new ClipboardLine();
        //        newLine.CopyId = id;
        //        database.ClipboardLines.InsertOnSubmit(newLine);
        //    }
        //    database.SubmitChanges();
        //}
    }
}
