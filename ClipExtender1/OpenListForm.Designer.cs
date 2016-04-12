namespace ClipExtender
{
    partial class OpenListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.database1DataSet = new ClipExtender.Database1DataSet();
            this.listsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listsTableAdapter = new ClipExtender.Database1DataSetTableAdapters.ListsTableAdapter();
            this.tableAdapterManager = new ClipExtender.Database1DataSetTableAdapters.TableAdapterManager();
            this.btnOpenList = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.fKListLinesListsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listLinesTableAdapter = new ClipExtender.Database1DataSetTableAdapters.ListLinesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKListLinesListsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // database1DataSet
            // 
            this.database1DataSet.DataSetName = "Database1DataSet";
            this.database1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // listsBindingSource
            // 
            this.listsBindingSource.DataMember = "Lists";
            this.listsBindingSource.DataSource = this.database1DataSet;
            // 
            // listsTableAdapter
            // 
            this.listsTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ClipboardLinesTableAdapter = null;
            this.tableAdapterManager.CopiesTableAdapter = null;
            this.tableAdapterManager.ListLinesTableAdapter = null;
            this.tableAdapterManager.ListsTableAdapter = this.listsTableAdapter;
            this.tableAdapterManager.UpdateOrder = ClipExtender.Database1DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // btnOpenList
            // 
            this.btnOpenList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenList.Location = new System.Drawing.Point(12, 193);
            this.btnOpenList.Name = "btnOpenList";
            this.btnOpenList.Size = new System.Drawing.Size(253, 23);
            this.btnOpenList.TabIndex = 4;
            this.btnOpenList.Text = "Open List";
            this.btnOpenList.UseVisualStyleBackColor = true;
            this.btnOpenList.Click += new System.EventHandler(this.btnOpenList_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.DataSource = this.listsBindingSource;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(253, 160);
            this.listBox1.TabIndex = 5;
            // 
            // listsBindingSource1
            // 
            this.listsBindingSource1.DataMember = "Lists";
            this.listsBindingSource1.DataSource = this.database1DataSet;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(12, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(253, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fKListLinesListsBindingSource
            // 
            this.fKListLinesListsBindingSource.DataMember = "FK_ListLines_Lists";
            this.fKListLinesListsBindingSource.DataSource = this.listsBindingSource1;
            // 
            // listLinesTableAdapter
            // 
            this.listLinesTableAdapter.ClearBeforeFill = true;
            // 
            // OpenListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 257);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnOpenList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpenListForm";
            this.Text = "Your Saved Lists";
            this.Load += new System.EventHandler(this.ListViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.database1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKListLinesListsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Database1DataSet database1DataSet;
        private System.Windows.Forms.BindingSource listsBindingSource;
        private Database1DataSetTableAdapters.ListsTableAdapter listsTableAdapter;
        private Database1DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button btnOpenList;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.BindingSource listsBindingSource1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource fKListLinesListsBindingSource;
        private Database1DataSetTableAdapters.ListLinesTableAdapter listLinesTableAdapter;
    }
}