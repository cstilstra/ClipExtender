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
        public OpenListForm()
        {
            InitializeComponent();
        }

        private void listsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.listsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void ListViewer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Lists' table. You can move, or remove it, as needed.
            this.listsTableAdapter.Fill(this.database1DataSet.Lists);

        }
    }
}
