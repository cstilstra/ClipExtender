using System.Collections.Generic;

namespace ClipExtender
{
    public class Extender
    {

        Form1 parentForm;
        DataBaseCommunications databaseCommunications;
        ClipboardCommunication clipboardCommunication;

        public Extender(Form1 callingForm)
        {
            setUpReferences(callingForm);
            clearClipboard();
        }

        private void setUpReferences(Form1 callingForm)
        {
            parentForm = callingForm;
            databaseCommunications = new DataBaseCommunications();
            clipboardCommunication = new ClipboardCommunication(parentForm, databaseCommunications);
        }

        public ClipboardCommunication getClipboardCommunication()
        {
            return clipboardCommunication;
        }

        public void clearClipboard()
        {
            parentForm.clearListboxItems();
            databaseCommunications.clearClipboard();
        }

        public void removeItemFromClipboard(int listboxIndex)
        {
            int clipboardLineId = listboxIndex + 1;
            databaseCommunications.removeItemFromClipboard(clipboardLineId);
        }

        public void createNewList(string listName)
        {
            databaseCommunications.createNewList(listName);
        }

        public void openList(int listId)
        {
            clearClipboard();
            List<string> copiesOnList = databaseCommunications.getCopyTextOnList(listId);
            parentForm.setListboxItems(copiesOnList);
            databaseCommunications.addListItemsToClipboard(listId);
        }
    }
}
