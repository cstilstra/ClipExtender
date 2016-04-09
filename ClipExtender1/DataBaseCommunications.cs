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

        public void addToCopiesTable(String textToAdd)
        {
            Copy newCopy = new Copy();
            newCopy.Text = textToAdd;
            newCopy.DateTime = DateTime.Now;
            database.Copies.InsertOnSubmit(newCopy);
            database.SubmitChanges();

            Copy copy = database.Copies.Single(c => c.Text == textToAdd);
            Debug.WriteLine("DataBaseCommunications: Id of just-added entry = " + copy.Id);
            Debug.WriteLine("DataBaseCommunications: Datetime of just-added entry = " + copy.DateTime);

        }

    }
}
