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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Library));
            this.favoriteHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.authorsHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yearHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.journalHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addDateHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteArticleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slidePanel = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnHideSlide = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAddManually = new System.Windows.Forms.Button();
            this.tvFolderTitle = new System.Windows.Forms.Label();
            this.btnSync = new System.Windows.Forms.Button();
            this.dataGridViewContextMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.treeViewContextMenu.SuspendLayout();
            this.slidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // favoriteHeader
            // 
            this.favoriteHeader.Text = "";
            this.favoriteHeader.Width = 50;
            // 
            // fileHeader
            // 
            this.fileHeader.Text = "";
            this.fileHeader.Width = 94;
            // 
            // authorsHeader
            // 
            this.authorsHeader.Text = "Authors";
            this.authorsHeader.Width = 53;
            // 
            // titleHeader
            // 
            this.titleHeader.Text = "Title";
            this.titleHeader.Width = 39;
            // 
            // yearHeader
            // 
            this.yearHeader.Text = "Year";
            this.yearHeader.Width = 41;
            // 
            // journalHeader
            // 
            this.journalHeader.Text = "Published In";
            this.journalHeader.Width = 105;
            // 
            // addDateHeader
            // 
            this.addDateHeader.Text = "Added";
            this.addDateHeader.Width = 52;
            // 
            // dataGridViewContextMenu
            // 
            this.dataGridViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteArticleToolStripMenuItem});
            this.dataGridViewContextMenu.Name = "listViewContextMenu";
            this.dataGridViewContextMenu.Size = new System.Drawing.Size(143, 26);
            // 
            // deleteArticleToolStripMenuItem
            // 
            this.deleteArticleToolStripMenuItem.Name = "deleteArticleToolStripMenuItem";
            this.deleteArticleToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteArticleToolStripMenuItem.Text = "Delete article";
            this.deleteArticleToolStripMenuItem.Click += new System.EventHandler(this.deleteArticleToolStripMenuItem_Click_1);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-star-50.png");
            this.imageList1.Images.SetKeyName(1, "star_fill.png");
            this.imageList1.Images.SetKeyName(2, "document.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(310, 726);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtTitle);
            this.tabPage1.Controls.Add(this.label15);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(302, 700);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(72, 41);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(216, 20);
            this.txtTitle.TabIndex = 45;
            this.txtTitle.Leave += new System.EventHandler(this.txtTitle_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "Title";
            // 
            // btnFiles
            // 
            this.btnFiles.Location = new System.Drawing.Point(13, 603);
            this.btnFiles.Name = "btnFiles";
            this.btnFiles.Size = new System.Drawing.Size(75, 23);
            this.btnFiles.TabIndex = 43;
            this.btnFiles.Text = "Add file";
            this.btnFiles.UseVisualStyleBackColor = true;
            this.btnFiles.Click += new System.EventHandler(this.btnFiles_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 587);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 42;
            this.label14.Text = "Files:";
            // 
            // txtAuthors
            // 
            this.txtAuthors.Location = new System.Drawing.Point(72, 67);
            this.txtAuthors.Name = "txtAuthors";
            this.txtAuthors.Size = new System.Drawing.Size(216, 20);
            this.txtAuthors.TabIndex = 41;
            this.txtAuthors.Leave += new System.EventHandler(this.txtAuthors_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
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
            this.txtPMID.Size = new System.Drawing.Size(216, 20);
            this.txtPMID.TabIndex = 37;
            this.txtPMID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPMID_KeyPress);
            this.txtPMID.Leave += new System.EventHandler(this.txtPMID_Leave);
            // 
            // txtDOI
            // 
            this.txtDOI.Location = new System.Drawing.Point(72, 525);
            this.txtDOI.Name = "txtDOI";
            this.txtDOI.Size = new System.Drawing.Size(216, 20);
            this.txtDOI.TabIndex = 36;
            this.txtDOI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDOI_KeyPress);
            this.txtDOI.Leave += new System.EventHandler(this.txtDOI_Leave);
            // 
            // txtArXivID
            // 
            this.txtArXivID.Location = new System.Drawing.Point(72, 499);
            this.txtArXivID.Name = "txtArXivID";
            this.txtArXivID.Size = new System.Drawing.Size(216, 20);
            this.txtArXivID.TabIndex = 35;
            this.txtArXivID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtArXivID_KeyPress);
            this.txtArXivID.Leave += new System.EventHandler(this.txtArXivID_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 554);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "PMID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 528);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "DOI:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 502);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
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
            this.maskedTextBox1.Size = new System.Drawing.Size(278, 20);
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
            this.txtAbstract.Leave += new System.EventHandler(this.txtAbstract_Leave);
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
            this.txtPages.Location = new System.Drawing.Point(72, 198);
            this.txtPages.Name = "txtPages";
            this.txtPages.Size = new System.Drawing.Size(216, 20);
            this.txtPages.TabIndex = 26;
            this.txtPages.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPages_KeyPress);
            this.txtPages.Leave += new System.EventHandler(this.txtPages_Leave);
            // 
            // txtIssue
            // 
            this.txtIssue.Location = new System.Drawing.Point(72, 172);
            this.txtIssue.Name = "txtIssue";
            this.txtIssue.Size = new System.Drawing.Size(216, 20);
            this.txtIssue.TabIndex = 25;
            this.txtIssue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIssue_KeyPress);
            this.txtIssue.Leave += new System.EventHandler(this.txtIssue_Leave);
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(72, 146);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(216, 20);
            this.txtVolume.TabIndex = 24;
            this.txtVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVolume_KeyPress);
            this.txtVolume.Leave += new System.EventHandler(this.txtVolume_Leave);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(72, 120);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(216, 20);
            this.txtYear.TabIndex = 23;
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            this.txtYear.Leave += new System.EventHandler(this.txtYear_Leave);
            // 
            // txtJournal
            // 
            this.txtJournal.Location = new System.Drawing.Point(72, 94);
            this.txtJournal.Name = "txtJournal";
            this.txtJournal.Size = new System.Drawing.Size(216, 20);
            this.txtJournal.TabIndex = 22;
            this.txtJournal.Leave += new System.EventHandler(this.txtJournal_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Pages";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Issue";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Volume";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Journal";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(302, 700);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Notes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.treeViewContextMenu;
            this.treeView1.Location = new System.Drawing.Point(12, 189);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(141, 205);
            this.treeView1.TabIndex = 13;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // treeViewContextMenu
            // 
            this.treeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFolderToolStripMenuItem,
            this.renameFolderToolStripMenuItem,
            this.deleteFolderToolStripMenuItem});
            this.treeViewContextMenu.Name = "treeViewContextMenu";
            this.treeViewContextMenu.Size = new System.Drawing.Size(152, 70);
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addFolderToolStripMenuItem.Text = "Add folder";
            this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.addFolderToolStripMenuItem_Click);
            // 
            // renameFolderToolStripMenuItem
            // 
            this.renameFolderToolStripMenuItem.Name = "renameFolderToolStripMenuItem";
            this.renameFolderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.renameFolderToolStripMenuItem.Text = "Rename folder";
            this.renameFolderToolStripMenuItem.Click += new System.EventHandler(this.renameFolderToolStripMenuItem_Click);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deleteFolderToolStripMenuItem.Text = "Delete folder";
            this.deleteFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteFolderToolStripMenuItem_Click);
            // 
            // slidePanel
            // 
            this.slidePanel.BackColor = System.Drawing.Color.White;
            this.slidePanel.Controls.Add(this.tabControl1);
            this.slidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.slidePanel.Location = new System.Drawing.Point(650, 0);
            this.slidePanel.Name = "slidePanel";
            this.slidePanel.Size = new System.Drawing.Size(301, 733);
            this.slidePanel.TabIndex = 15;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnHideSlide
            // 
            this.btnHideSlide.Location = new System.Drawing.Point(617, 136);
            this.btnHideSlide.Name = "btnHideSlide";
            this.btnHideSlide.Size = new System.Drawing.Size(10, 81);
            this.btnHideSlide.TabIndex = 0;
            this.btnHideSlide.Text = "H\r\ni\r\nd\r\ne";
            this.btnHideSlide.UseVisualStyleBackColor = true;
            this.btnHideSlide.Click += new System.EventHandler(this.btnHideSlide_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Mendeley",
            "    Literature search",
            "",
            "My library",
            "    All documents",
            "    My articles",
            "    Recently added",
            "    Favorite"});
            this.listBox1.Location = new System.Drawing.Point(12, 73);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(141, 108);
            this.listBox1.TabIndex = 16;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(159, 73);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(449, 321);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.dataGridView1.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseLeave);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnAddManually
            // 
            this.btnAddManually.Location = new System.Drawing.Point(13, 37);
            this.btnAddManually.Name = "btnAddManually";
            this.btnAddManually.Size = new System.Drawing.Size(140, 23);
            this.btnAddManually.TabIndex = 18;
            this.btnAddManually.Text = "Add Entry Manually";
            this.btnAddManually.UseVisualStyleBackColor = true;
            this.btnAddManually.Click += new System.EventHandler(this.btnAddManually_Click);
            // 
            // tvFolderTitle
            // 
            this.tvFolderTitle.AutoSize = true;
            this.tvFolderTitle.Location = new System.Drawing.Point(159, 37);
            this.tvFolderTitle.Name = "tvFolderTitle";
            this.tvFolderTitle.Size = new System.Drawing.Size(35, 13);
            this.tvFolderTitle.TabIndex = 20;
            this.tvFolderTitle.Text = "label1";
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(251, 13);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(75, 23);
            this.btnSync.TabIndex = 21;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // Library
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(951, 733);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.tvFolderTitle);
            this.Controls.Add(this.btnAddManually);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnHideSlide);
            this.Controls.Add(this.slidePanel);
            this.Controls.Add(this.treeView1);
            this.Name = "Library";
            this.Text = "My Library";
            this.Load += new System.EventHandler(this.Library_Load);
            this.dataGridViewContextMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.treeViewContextMenu.ResumeLayout(false);
            this.slidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColumnHeader authorsHeader;
        private System.Windows.Forms.ColumnHeader titleHeader;
        private System.Windows.Forms.ColumnHeader yearHeader;
        private System.Windows.Forms.ColumnHeader journalHeader;
        private System.Windows.Forms.ColumnHeader addDateHeader;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
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
        private System.Windows.Forms.Button btnFiles;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.ColumnHeader favoriteHeader;
        public System.Windows.Forms.ColumnHeader fileHeader;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnHideSlide;
        private System.Windows.Forms.Panel slidePanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip dataGridViewContextMenu;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip treeViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private System.Windows.Forms.Button btnAddManually;
        private System.Windows.Forms.Label tvFolderTitle;
        private System.Windows.Forms.ToolStripMenuItem deleteArticleToolStripMenuItem;
        private System.Windows.Forms.Button btnSync;
    }
}