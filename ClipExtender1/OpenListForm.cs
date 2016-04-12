using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClipExtender
{
    public partial class OpenListForm : Form
    {
        Extender extender;

        public OpenListForm(Extender extenderReference)
        {
            InitializeComponent();
            extender = extenderReference;
        }

        private void ListViewer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.ListLines' table. You can move, or remove it, as needed.
            this.listLinesTableAdapter.Fill(this.database1DataSet.ListLines);
            // TODO: This line of code loads data into the 'database1DataSet.Lists' table. You can move, or remove it, as needed.
            this.listsTableAdapter.Fill(this.database1DataSet.Lists);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenList_Click(object sender, EventArgs e)
        {
            //List selectedList = (List)listBox1.SelectedItem;
            //int listId = selectedList.Id;

            DataRowView row = (DataRowView)listBox1.SelectedItem;
            int listId = Int32.Parse(row["Id"].ToString());

            extender.openList(listId);

            this.Close();
        }
    }
}
