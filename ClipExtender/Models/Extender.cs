using ClipExtender.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClipExtender
{
    public class Extender
    {

        IStorageCommunications storageComms;
        ClipboardCommunication clipboardComms;

        public Extender(IStorageCommunications storage, IntPtr viewHandle)
        {
            setUpReferences(storage);
            ClearClipboard();
            clipboardComms.beginListeningToClipboard(viewHandle);
        }


        public ClipboardCommunication GetClipboardCommunication()
        {
            return clipboardComms;
        }

        public void ClearClipboard()
        {
            //parentForm.ClearListboxItems();
            storageComms.ClearClipboard();
        }

        public void RemoveItemFromClipboard(int listboxIndex)
        {
            int clipboardLineId = listboxIndex + 1;
            storageComms.RemoveItemFromClipboard(clipboardLineId);
        }

        public IEnumerable<string> HandleMessage(int message)
        {
            if (clipboardComms.handleUpdateMessage(message))
            {
                return storageComms.GetStorageItems();
            }
            return null;
        }

        //public void createNewList(string listName)
        //{
        //    databaseCommunications.createNewList(listName);
        //}

        //public void openList(int listId)
        //{
        //    clearClipboard();
        //    List<string> copiesOnList = databaseCommunications.getCopyTextOnList(listId);
        //    parentForm.SetListboxItems(copiesOnList);
        //    databaseCommunications.addListItemsToClipboard(listId);
        //}

        private void setUpReferences(IStorageCommunications storage)
        {
            storageComms = storage;
            clipboardComms = new ClipboardCommunication(storageComms);
        }
    }
}
