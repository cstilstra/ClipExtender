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
    public partial class NameNewList : Form
    {
        Extender extender;

        public NameNewList()
        {
            InitializeComponent();
        }

        public void setExtender(Extender extenderReference)
        {
            extender = extenderReference;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            extender.createNewList(tbListName.Text);
            this.Close();
        }
    }
}
