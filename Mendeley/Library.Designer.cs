namespace Mendeley
{
    partial class Library
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Title"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("ListVIewItem1");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Library));
            this.listView1 = new System.Windows.Forms.ListView();
            this.favoriteHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.authorsHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yearHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.journalHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addDateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnFiles = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAuthors = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.materialtype = new System.Windows.Forms.Label();
            this.materialid = new System.Windows.Forms.Label();
            this.txtPMID = new System.Windows.Forms.TextBox();
            this.txtDOI = new System.Windows.Forms.TextBox();
            this.txtArXivID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAbstract = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.txtIssue = new System.Windows.Forms.TextBox();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtJournal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tvName = new System.Windows.Forms.Label();
            this.comboBoxTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.user_libraryDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_libraryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mendeleyLocalDataSet = new Mendeley.MendeleyLocalDataSet();
            this.btnSaveChange = new System.Windows.Forms.Button();
            this.journalArticlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.journalArticlesDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.journalArticlesTableAdapter = new Mendeley.MendeleyLocalDataSetTableAdapters.JournalArticlesTableAdapter();
            this.tableAdapterManager = new Mendeley.MendeleyLocalDataSetTableAdapters.TableAdapterManager();
            this.user_libraryTableAdapter = new Mendeley.MendeleyLocalDataSetTableAdapters.user_libraryTableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.user_libraryDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_libraryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mendeleyLocalDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.journalArticlesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.journalArticlesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.favoriteHeader,
            this.fileHeader,
            this.authorsHeader,
            this.titleHeader,
            this.yearHeader,
            this.journalHeader,
            this.addDateHeader});
            this.listView1.FullRowSelect = true;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(12, 44);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(464, 97);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // favoriteHeader
            // 
            this.favoriteHeader.Text = "favorite";
            // 
            // fileHeader
            // 
            this.fileHeader.Text = "File Attached";
            this.fileHeader.Width = 77;
            // 
            // authorsHeader
            // 
            this.authorsHeader.Text = "Authors";
            // 
            // titleHeader
            // 
            this.titleHeader.Text = "Title";
            // 
            // yearHeader
            // 
            this.yearHeader.Text = "Year";
            // 
            // journalHeader
            // 
            this.journalHeader.Text = "Published In";
            this.journalHeader.Width = 83;
            // 
            // addDateHeader
            // 
            this.addDateHeader.Text = "Added";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Get From Web DB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(483, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(305, 659);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnFiles);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtAuthors);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.materialtype);
            this.tabPage1.Controls.Add(this.materialid);
            this.tabPage1.Controls.Add(this.txtPMID);
            this.tabPage1.Controls.Add(this.txtDOI);
            this.tabPage1.Controls.Add(this.txtArXivID);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.maskedTextBox1);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtAbstract);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtPages);
            this.tabPage1.Controls.Add(this.txtIssue);
            this.tabPage1.Controls.Add(this.txtVolume);
            this.tabPage1.Controls.Add(this.txtYear);
            this.tabPage1.Controls.Add(this.txtJournal);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tvName);
            this.tabPage1.Controls.Add(this.comboBoxTypes);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(297, 633);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnFiles
            // 
            this.btnFiles.Location = new System.Drawing.Point(13, 603);
            this.btnFiles.Name = "btnFiles";
            this.btnFiles.Size = new System.Drawing.Size(75, 23);
            this.btnFiles.TabIndex = 43;
            this.btnFiles.Text = "button4";
            this.btnFiles.UseVisualStyleBackColor = true;
            this.btnFiles.Click += new System.EventHandler(this.btnFiles_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 587);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 42;
            this.label14.Text = "Files:";
            // 
            // txtAuthors
            // 
            this.txtAuthors.Location = new System.Drawing.Point(78, 58);
            this.txtAuthors.Name = "txtAuthors";
            this.txtAuthors.Size = new System.Drawing.Size(216, 21);
            this.txtAuthors.TabIndex = 41;
            this.txtAuthors.TextChanged += new System.EventHandler(this.txtAuthors_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "Authors";
            // 
            // materialtype
            // 
            this.materialtype.AutoSize = true;
            this.materialtype.Location = new System.Drawing.Point(124, 467);
            this.materialtype.Name = "materialtype";
            this.materialtype.Size = new System.Drawing.Size(41, 13);
            this.materialtype.TabIndex = 39;
            this.materialtype.Text = "label14";
            this.materialtype.Visible = false;
            // 
            // materialid
            // 
            this.materialid.AutoSize = true;
            this.materialid.Location = new System.Drawing.Point(180, 467);
            this.materialid.Name = "materialid";
            this.materialid.Size = new System.Drawing.Size(41, 13);
            this.materialid.TabIndex = 38;
            this.materialid.Text = "label13";
            this.materialid.Visible = false;
            // 
            // txtPMID
            // 
            this.txtPMID.Location = new System.Drawing.Point(72, 551);
            this.txtPMID.Name = "txtPMID";
            this.txtPMID.Size = new System.Drawing.Size(216, 21);
            this.txtPMID.TabIndex = 37;
            this.txtPMID.TextChanged += new System.EventHandler(this.txtPMID_TextChanged);
            // 
            // txtDOI
            // 
            this.txtDOI.Location = new System.Drawing.Point(72, 525);
            this.txtDOI.Name = "txtDOI";
            this.txtDOI.Size = new System.Drawing.Size(216, 21);
            this.txtDOI.TabIndex = 36;
            this.txtDOI.TextChanged += new System.EventHandler(this.txtDOI_TextChanged);
            // 
            // txtArXivID
            // 
            this.txtArXivID.Location = new System.Drawing.Point(72, 499);
            this.txtArXivID.Name = "txtArXivID";
            this.txtArXivID.Size = new System.Drawing.Size(216, 21);
            this.txtArXivID.TabIndex = 35;
            this.txtArXivID.TextChanged += new System.EventHandler(this.txtArXivID_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 554);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "PMID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 528);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "DOI:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 502);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "ArXiv ID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(13, 467);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "Catalog IDs";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(10, 419);
            this.maskedTextBox1.Mask = " ";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(278, 21);
            this.maskedTextBox1.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(13, 388);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 29;
            this.label8.Text = "Tags";
            // 
            // txtAbstract
            // 
            this.txtAbstract.Location = new System.Drawing.Point(10, 264);
            this.txtAbstract.Name = "txtAbstract";
            this.txtAbstract.Size = new System.Drawing.Size(281, 96);
            this.txtAbstract.TabIndex = 28;
            this.txtAbstract.Text = "";
            this.txtAbstract.TextChanged += new System.EventHandler(this.txtAbstract_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(13, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "Abstract";
            // 
            // txtPages
            // 
            this.txtPages.Location = new System.Drawing.Point(75, 189);
            this.txtPages.Name = "txtPages";
            this.txtPages.Size = new System.Drawing.Size(216, 21);
            this.txtPages.TabIndex = 26;
            this.txtPages.TextChanged += new System.EventHandler(this.txtPages_TextChanged);
            // 
            // txtIssue
            // 
            this.txtIssue.Location = new System.Drawing.Point(75, 163);
            this.txtIssue.Name = "txtIssue";
            this.txtIssue.Size = new System.Drawing.Size(216, 21);
            this.txtIssue.TabIndex = 25;
            this.txtIssue.TextChanged += new System.EventHandler(this.txtIssue_TextChanged);
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(75, 137);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(216, 21);
            this.txtVolume.TabIndex = 24;
            this.txtVolume.TextChanged += new System.EventHandler(this.txtVolume_TextChanged);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(75, 111);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(216, 21);
            this.txtYear.TabIndex = 23;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            // 
            // txtJournal
            // 
            this.txtJournal.Location = new System.Drawing.Point(75, 85);
            this.txtJournal.Name = "txtJournal";
            this.txtJournal.Size = new System.Drawing.Size(216, 21);
            this.txtJournal.TabIndex = 22;
            this.txtJournal.TextChanged += new System.EventHandler(this.txtJournal_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Pages";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Issue";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Volume";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Journal";
            // 
            // tvName
            // 
            this.tvName.AutoSize = true;
            this.tvName.Location = new System.Drawing.Point(13, 38);
            this.tvName.Name = "tvName";
            this.tvName.Size = new System.Drawing.Size(93, 13);
            this.tvName.TabIndex = 16;
            this.tvName.Text = "Название статьи";
            // 
            // comboBoxTypes
            // 
            this.comboBoxTypes.FormattingEnabled = true;
            this.comboBoxTypes.Items.AddRange(new object[] {
            "JournalArticle",
            "Book",
            "Film"});
            this.comboBoxTypes.Location = new System.Drawing.Point(51, 6);
            this.comboBoxTypes.Name = "comboBoxTypes";
            this.comboBoxTypes.Size = new System.Drawing.Size(243, 21);
            this.comboBoxTypes.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Type";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(297, 633);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Notes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(368, 272);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(13, 177);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(75, 23);
            this.btnSync.TabIndex = 5;
            this.btnSync.Text = "Synchronize";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Visible = false;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuTextToolStripMenuItem,
            this.addFileToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 48);
            // 
            // contextMenuTextToolStripMenuItem
            // 
            this.contextMenuTextToolStripMenuItem.Name = "contextMenuTextToolStripMenuItem";
            this.contextMenuTextToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.contextMenuTextToolStripMenuItem.Text = "AddManually";
            this.contextMenuTextToolStripMenuItem.Click += new System.EventHandler(this.contextMenuTextToolStripMenuItem_Click);
            // 
            // addFileToolStripMenuItem
            // 
            this.addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            this.addFileToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.addFileToolStripMenuItem.Text = "AddFile";
            this.addFileToolStripMenuItem.Click += new System.EventHandler(this.addFileToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(12, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 26);
            this.button2.TabIndex = 6;
            this.button2.Text = "Add article";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // user_libraryDataGridView
            // 
            this.user_libraryDataGridView.AutoGenerateColumns = false;
            this.user_libraryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.user_libraryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.user_libraryDataGridView.DataSource = this.user_libraryBindingSource;
            this.user_libraryDataGridView.Location = new System.Drawing.Point(27, 246);
            this.user_libraryDataGridView.Name = "user_libraryDataGridView";
            this.user_libraryDataGridView.RowTemplate.Height = 23;
            this.user_libraryDataGridView.Size = new System.Drawing.Size(300, 220);
            this.user_libraryDataGridView.TabIndex = 7;
            this.user_libraryDataGridView.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "uid";
            this.dataGridViewTextBoxColumn1.HeaderText = "uid";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "type";
            this.dataGridViewTextBoxColumn2.HeaderText = "type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "mid";
            this.dataGridViewTextBoxColumn3.HeaderText = "mid";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "favorite";
            this.dataGridViewTextBoxColumn4.HeaderText = "favorite";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // user_libraryBindingSource
            // 
            this.user_libraryBindingSource.DataMember = "user_library";
            this.user_libraryBindingSource.DataSource = this.mendeleyLocalDataSet;
            // 
            // mendeleyLocalDataSet
            // 
            this.mendeleyLocalDataSet.DataSetName = "MendeleyLocalDataSet";
            this.mendeleyLocalDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(109, 142);
            this.btnSaveChange.Name = "btnSaveChange";
            this.btnSaveChange.Size = new System.Drawing.Size(75, 23);
            this.btnSaveChange.TabIndex = 8;
            this.btnSaveChange.Text = "SaeLocalDBChanges";
            this.btnSaveChange.UseVisualStyleBackColor = true;
            this.btnSaveChange.Visible = false;
            this.btnSaveChange.Click += new System.EventHandler(this.btnSaveChange_Click);
            // 
            // journalArticlesBindingSource
            // 
            this.journalArticlesBindingSource.DataMember = "JournalArticles";
            this.journalArticlesBindingSource.DataSource = this.mendeleyLocalDataSet;
            // 
            // journalArticlesDataGridView
            // 
            this.journalArticlesDataGridView.AutoGenerateColumns = false;
            this.journalArticlesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.journalArticlesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20});
            this.journalArticlesDataGridView.DataSource = this.journalArticlesBindingSource;
            this.journalArticlesDataGridView.Location = new System.Drawing.Point(27, 475);
            this.journalArticlesDataGridView.Name = "journalArticlesDataGridView";
            this.journalArticlesDataGridView.RowTemplate.Height = 23;
            this.journalArticlesDataGridView.Size = new System.Drawing.Size(300, 220);
            this.journalArticlesDataGridView.TabIndex = 10;
            this.journalArticlesDataGridView.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn5.HeaderText = "Id";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn6.HeaderText = "title";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "authors";
            this.dataGridViewTextBoxColumn7.HeaderText = "authors";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "abstract";
            this.dataGridViewTextBoxColumn8.HeaderText = "abstract";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "journal";
            this.dataGridViewTextBoxColumn9.HeaderText = "journal";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "year";
            this.dataGridViewTextBoxColumn10.HeaderText = "year";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "volume";
            this.dataGridViewTextBoxColumn11.HeaderText = "volume";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "issue";
            this.dataGridViewTextBoxColumn12.HeaderText = "issue";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "pages";
            this.dataGridViewTextBoxColumn13.HeaderText = "pages";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "ArXivID";
            this.dataGridViewTextBoxColumn14.HeaderText = "ArXivID";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "DOI";
            this.dataGridViewTextBoxColumn15.HeaderText = "DOI";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "PMID";
            this.dataGridViewTextBoxColumn16.HeaderText = "PMID";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "add_date";
            this.dataGridViewTextBoxColumn18.HeaderText = "add_date";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "update_date";
            this.dataGridViewTextBoxColumn19.HeaderText = "update_date";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "delete_date";
            this.dataGridViewTextBoxColumn20.HeaderText = "delete_date";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(108, 176);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // journalArticlesTableAdapter
            // 
            this.journalArticlesTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.JournalArticlesTableAdapter = this.journalArticlesTableAdapter;
            this.tableAdapterManager.UpdateOrder = Mendeley.MendeleyLocalDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.user_libraryTableAdapter = this.user_libraryTableAdapter;
            // 
            // user_libraryTableAdapter
            // 
            this.user_libraryTableAdapter.ClearBeforeFill = true;
            // 
            // Library
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(825, 715);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.journalArticlesDataGridView);
            this.Controls.Add(this.btnSaveChange);
            this.Controls.Add(this.user_libraryDataGridView);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Name = "Library";
            this.Text = "My Library";
            this.Load += new System.EventHandler(this.Library_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.user_libraryDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_libraryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mendeleyLocalDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.journalArticlesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.journalArticlesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader favoriteHeader;
        private System.Windows.Forms.ColumnHeader fileHeader;
        private System.Windows.Forms.ColumnHeader authorsHeader;
        private System.Windows.Forms.ColumnHeader titleHeader;
        private System.Windows.Forms.ColumnHeader yearHeader;
        private System.Windows.Forms.ColumnHeader journalHeader;
        private System.Windows.Forms.ColumnHeader addDateHeader;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtPages;
        private System.Windows.Forms.TextBox txtIssue;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtJournal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tvName;
        private System.Windows.Forms.ComboBox comboBoxTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox txtAbstract;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPMID;
        private System.Windows.Forms.TextBox txtDOI;
        private System.Windows.Forms.TextBox txtArXivID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSync;
        private MendeleyLocalDataSet mendeleyLocalDataSet;
        private System.Windows.Forms.BindingSource journalArticlesBindingSource;
        private System.Windows.Forms.BindingSource user_libraryBindingSource;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem contextMenuTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem;
        private System.Windows.Forms.DataGridView user_libraryDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button btnSaveChange;
        private System.Windows.Forms.DataGridView journalArticlesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.Label materialtype;
        private System.Windows.Forms.Label materialid;
        private System.Windows.Forms.TextBox txtAuthors;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnFiles;
        private System.Windows.Forms.Label label14;
        private MendeleyLocalDataSetTableAdapters.JournalArticlesTableAdapter journalArticlesTableAdapter;
        private MendeleyLocalDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private MendeleyLocalDataSetTableAdapters.user_libraryTableAdapter user_libraryTableAdapter;
    }
}