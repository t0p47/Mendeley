using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Mendeley.ModelResponse;
using System.Net.Http;
using Mono.Web;
using Newtonsoft.Json.Linq;

namespace Mendeley
{
    public partial class Library : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        Functions func = new Functions();
        //Types types = new Types();
        ArrayList al;
        JournalArticle[] journalArticle;

        public bool haveInternet { get; set; }

        private int dataGridRowIndex = 0;

        private static int FileAttachedInd = 1;
        private static int AuthorsInd = 2;
        private static int TitleInd = 3;
        private static int YearInd = 4;
        private static int JournalInd = 5;
        private static int CreatedAtInd = 6;

        SortedDictionary<int, TreeNode> folderTreeIds;
        //SortedDictionary<int ListViewItem> articleListId;

        List<JournalArticle> articlesList;
        List<Folder> foldersList;

        //User_library[] user_library;
        String fileDestPath;

        private void Library_Load(object sender, EventArgs e)
        {
            //TODO: move to login
            //How work reset on release debug versions
#if DEBUG
            //MessageBox.Show("DEBUG");
            Properties.Settings.Default.Reset();
#endif

#if !DEBUG
            //MessageBox.Show("Release");
#endif

            if (String.IsNullOrEmpty(Properties.Settings.Default.Workspace)) {
                FolderBrowserDialog fold = new FolderBrowserDialog();
                fold.Description = "Выберите папку для сохранения статей";
                if (fold.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.Workspace = fold.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }


                /*if (!ConfigurationManager.AppSettings.AllKeys.Contains("Workspace")) {
                MessageBox.Show("Checked good");
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                MessageBox.Show("Connected good");

                FolderBrowserDialog fold = new FolderBrowserDialog();
                fold.Description = "Выберите папку для сохранения статей";
                if (fold.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentConfig.AppSettings.Settings.Add("Workspace", fold.SelectedPath);

                    MessageBox.Show("Added good");

                    //Здесь ошибка
                    currentConfig.Save(ConfigurationSaveMode.Modified);

                    MessageBox.Show("Saved good");

                    ConfigurationManager.RefreshSection("appSettings");

                    MessageBox.Show("Refreshed good");
                }

                
            }*/

            func.recreateAllTables();

            foldersList = func.getAllFolders();
            articlesList = func.getRootFolderArticles();

            getFolders();
            getArticles();

            //func.recreateAllTables();


        }

        private void getFolders() {
            //MessageBox.Show(Convert.ToString(foldersList.Count));
            if (foldersList.Count == 0 && haveInternet)
            {
                //MessageBox.Show("First time folders");
                getFirstTimeFolders();
                
            }
            else {
                //MessageBox.Show("Folders exists");
                loadFolder();
            }
            tvFolderTitle.Text = foldersList[0].getTitle();
        }

        private void loadFolder() {

            folderTreeIds = new SortedDictionary<int, TreeNode>();

            foreach (Folder folder in foldersList)
            {

                /*Console.WriteLine("Folder id: "+folder.getLocal_id()
                    +", name: "+folder.getTitle()+", parent_id: "+folder.getParent_id());*/

                if (folder.getParent_id() == 0)
                {
                    TreeNode rootNode = new TreeNode(folder.getTitle());
                    folderTreeIds.Add(folder.getLocal_id(),rootNode);
                    rootNode.Tag = folder.getLocal_id();
                    treeView1.Nodes.Add(rootNode);
                }
                else {
                    TreeNode child = new TreeNode(folder.getTitle());
                    child.Tag = folder.getLocal_id();

                    TreeNode parent;
                    folderTreeIds.TryGetValue(folder.getParent_id(), out parent);
                    
                    parent.Nodes.Add(child);
                    folderTreeIds.Add(folder.getLocal_id(),child);
                }

            }

        }

        private void getArticles() {
            //TODO: haveInternet
            if (articlesList.Count == 0 && haveInternet)
            {
                getFirstTimeArticles();
            }
            else {
                initDataGrid();
            }
        }

        private void initDataGrid()
        {

            //articleTable.Columns.Add("favortie", typeof(int));
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            

            Image emptyStar = imageList1.Images[0];
            Image fillStar = imageList1.Images[1];
            Image document = imageList1.Images[2];

            //dataGridView1.Rows.Add("1",emptyStar);

            DataGridViewImageColumn dgvFavoriteCol = new DataGridViewImageColumn();
            dgvFavoriteCol.DefaultCellStyle.NullValue = null;

            DataGridViewImageColumn dgvFileAttachedCol = new DataGridViewImageColumn();
            dgvFileAttachedCol.DefaultCellStyle.NullValue = null;

            DataGridViewTextBoxColumn dgvAuthorsCol = new DataGridViewTextBoxColumn();
            dgvAuthorsCol.HeaderText = "Authors";

            DataGridViewTextBoxColumn dgvTitleCol = new DataGridViewTextBoxColumn();
            dgvTitleCol.HeaderText = "Title";

            DataGridViewTextBoxColumn dgvYearCol = new DataGridViewTextBoxColumn();
            dgvYearCol.HeaderText = "Year";

            DataGridViewTextBoxColumn dgvPublishedInCol = new DataGridViewTextBoxColumn();
            dgvPublishedInCol.HeaderText = "Published In";

            DataGridViewTextBoxColumn dgvCreatedAtCol = new DataGridViewTextBoxColumn();
            dgvCreatedAtCol.HeaderText = "Created At";

            dataGridView1.Columns.Add(dgvFavoriteCol);
            dataGridView1.Columns.Add(dgvFileAttachedCol);
            dataGridView1.Columns.Add(dgvAuthorsCol);
            dataGridView1.Columns.Add(dgvTitleCol);
            dataGridView1.Columns.Add(dgvYearCol);
            dataGridView1.Columns.Add(dgvPublishedInCol);
            dataGridView1.Columns.Add(dgvCreatedAtCol);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.RowTemplate.Height = 120;

            dataGridView1.AllowUserToAddRows = false;



            for (int i = 0; i < articlesList.Count; i++)
            {

                int favorite = articlesList[i].getFavorite();

                Image star;
                Image doc = null;

                if (favorite == 0)
                {
                    star = emptyStar;   
                }
                else {
                    star = fillStar;
                }

                string fileAttached = articlesList[i].getFilepath();
                if (!string.IsNullOrWhiteSpace(fileAttached)) {
                    doc = document;
                }


                string authors = articlesList[i].getAuthors();
                string title = articlesList[i].getTitle();
                int year = articlesList[i].getYear();
                
                string journal = articlesList[i].getJournal();
                string created_at = articlesList[i].getCreated_at();

                int rowIndex = dataGridView1.Rows.Add();

                DataGridViewRow newRow = dataGridView1.Rows[rowIndex];
                newRow.Cells[0].Value = star;
                newRow.Cells[1].Value = doc;
                newRow.Cells[2].Value = authors;
                newRow.Cells[3].Value = title;

                if (year != -1)
                {
                    newRow.Cells[4].Value = year;
                }
                

                newRow.Cells[5].Value = journal;
                newRow.Cells[6].Value = created_at;

                newRow.Tag = i;

                //articleTable.Rows.Add(star,fileAttached,authors,title,year,journal,created_at);
                //articleTable.Rows.Add(authors, title, year, journal, created_at);
            }

            //dataGridView1.DataSource = articleTable;

        }
        
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1) {

                //MessageBox.Show("Row mouse enter index: " + e.RowIndex + ", column index: " + e.ColumnIndex);

                Image emptyStar = imageList1.Images[0];
                Image fillStar = imageList1.Images[1];

                DataGridViewRow hoverRow = dataGridView1.Rows[e.RowIndex];
                int listIndex = Convert.ToInt32(hoverRow.Tag);

                if (articlesList[listIndex].getFavorite() == 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fillStar;
                }
                else {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = emptyStar;
                }
            }
        }
        
        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //MessageBox.Show("Row mouse leave index: "+e.RowIndex+", column index: "+e.ColumnIndex);

                Image emptyStar = imageList1.Images[0];
                Image fillStar = imageList1.Images[1];

                DataGridViewRow hoverRow = dataGridView1.Rows[e.RowIndex];
                int listIndex = Convert.ToInt32(hoverRow.Tag);

                if (articlesList[listIndex].getFavorite() == 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = emptyStar;
                }
                else {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fillStar;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //MessageBox.Show("Row mouse leave index: "+e.RowIndex+", column index: "+e.ColumnIndex);

                Image emptyStar = imageList1.Images[0];
                Image fillStar = imageList1.Images[1];

                DataGridViewRow hoverRow = dataGridView1.Rows[e.RowIndex];
                int listIndex = Convert.ToInt32(hoverRow.Tag);

                if (articlesList[listIndex].getFavorite() == 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fillStar;
                    articlesList[listIndex].setFavorite(1);
                    func.setFavorite(articlesList[listIndex].getLocal_id(), 1);
                }
                else {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = emptyStar;
                    articlesList[listIndex].setFavorite(0);
                    func.setFavorite(articlesList[listIndex].getLocal_id(), 0);
                }
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {

                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;

                int offset = (e.CellBounds.Width - imageList1.ImageSize.Width) / 2;
                pt.X += offset;
                pt.Y += 1;
                imageList1.Draw(e.Graphics, pt, 0);
                e.Handled = true;

            }

            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {

                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;

                int offset = (e.CellBounds.Width - imageList1.ImageSize.Width) / 2;
                pt.X += offset;
                pt.Y += 1;
                imageList1.Draw(e.Graphics, pt, 2);
                e.Handled = true;

            }
        }

        public Library()
        {
            InitializeComponent();
            PW = slidePanel.Width;
            Hided = false;
        }

        private void getFirstTimeFolders()
        {
            NameValueCollection formData = HttpUtility.ParseQueryString(String.Empty);
            formData.Add("type","windows");
            string postData = formData.ToString();

            string foldersResponse = RESTful.PostRequest(EndPoints.URL_SYNC_FOLDERS,postData);

            var folders = JsonConvert.DeserializeObject<List<FolderResponse>>(foldersResponse);

            foreach (FolderResponse responseFolder in folders)
            {
                /*Console.WriteLine("Folder response: id: "+responseFolder.id+", name: "
                    + responseFolder.name+", parent_id: "+responseFolder.parent_id);*/

                Folder folder = new Folder(responseFolder.id,responseFolder.name,responseFolder.parent_id);
                foldersList.Add(folder);
            }

            func.recreateAllFolders(foldersList);
            foldersList = func.getAllFolders();
            loadFolder();


            //Console.WriteLine("All folders: " + foldersResponse);


        }

        private void getFirstTimeArticles() {

            NameValueCollection formData = HttpUtility.ParseQueryString(String.Empty);
            formData.Add("type","windows");
            string postData = formData.ToString();

            string articlesResponse = RESTful.PostRequest(EndPoints.URL_SYNC_ARTICLES, postData);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var articles = JsonConvert.DeserializeObject<List<JournalArticleResponse>>(articlesResponse,settings);

            foreach (JournalArticleResponse responseArticle in articles)
            {

                /*Console.WriteLine("Article id: "+responseArticle.id+", title: "+responseArticle.title
                    +", pages: "+responseArticle.pages);*/

                int volume = responseArticle.volume == 0 ? -1 : responseArticle.volume;
                int issue = responseArticle.issue == 0 ? -1 : responseArticle.issue;
                int year = responseArticle.year == 0 ? -1 : responseArticle.year;
                int pages = responseArticle.pages == 0 ? -1 : responseArticle.pages;
                int ArXivID = responseArticle.ArXivID == 0 ? -1 : responseArticle.ArXivID;
                int DOI = responseArticle.DOI == 0 ? -1 : responseArticle.DOI;
                int PMID = responseArticle.PMID == 0 ? -1 : responseArticle.PMID;

                JournalArticle article = new JournalArticle(responseArticle.id, responseArticle.title,
                    responseArticle.authors, responseArticle.@abstract, responseArticle.journal_id,
                    volume, issue, year,pages,ArXivID,DOI, PMID, responseArticle.folder,
                    responseArticle.filepath, responseArticle.created_at, responseArticle.updated_at, responseArticle.favorite);

                articlesList.Add(article);

            }

            func.recreateAllArticles(articlesList);
            articlesList = func.getRootFolderArticles();
            
            initDataGrid();

            //Console.WriteLine("All articles: "+articlesResponse);

        }

        public void CreateFolder() {
            /*if (System.IO.File.Exists(uid))
            {

                //MessageBox.Show("File exists");
            }
            else {
                //System.IO.Directory.CreateDirectory(uid);
                //MessageBox.Show("File not exists");
            }*/

        }

        private void selectArticle(int listIndex) {
            txtTitle.Text = articlesList[listIndex].getTitle();
            txtAuthors.Text = articlesList[listIndex].getAuthors();
            txtJournal.Text = articlesList[listIndex].getJournal();

            if (articlesList[listIndex].getYear() != -1)
            {
                txtYear.Text = articlesList[listIndex].getYear().ToString();
            }
            else {
                txtYear.Text = "";
            }

            if (articlesList[listIndex].getVolume() != -1) {
                txtVolume.Text = articlesList[listIndex].getVolume().ToString();
            }
            else
            {
                txtVolume.Text = "";
            }


            if (articlesList[listIndex].getIssue() != -1)
            {
                txtIssue.Text = articlesList[listIndex].getIssue().ToString();
            }
            else
            {
                txtIssue.Text = "";
            }

            if (articlesList[listIndex].getPages() != -1)
            {
                txtPages.Text = articlesList[listIndex].getPages().ToString();
            }
            else
            {
                txtPages.Text = "";
            }


            if (articlesList[listIndex].getArXivID() != -1)
            {
                txtArXivID.Text = articlesList[listIndex].getArXivID().ToString();
            }
            else
            {
                txtArXivID.Text = "";
            }


            if (articlesList[listIndex].getDOI() != -1)
            {
                txtDOI.Text = articlesList[listIndex].getDOI().ToString();
            }
            else
            {
                txtDOI.Text = "";
            }


            if (articlesList[listIndex].getPMID() != -1)
            {
                txtPMID.Text = articlesList[listIndex].getPMID().ToString();
            }
            else
            {
                txtPMID.Text = "";
            }


            txtAbstract.Text = articlesList[listIndex].getAbstractField().ToString();
            FillDetails();

        }

        //TODO: row selection
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in selectedRows) {
                int listIndex = Convert.ToInt32(row.Tag);

                selectArticle(listIndex);
            }
        }

        private void FillDetails() {

            if (dataGridView1.SelectedRows[0].Cells[1].Value == null)
            {
                //MessageBox.Show("No attached file");
                btnFiles.Tag = null;
                btnFiles.Text = "Add file";
            }
            else {
                //MessageBox.Show("File attached");
                btnFiles.Text = "Open file";
                int listIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Tag);
                btnFiles.Tag = articlesList[listIndex].getFilepath();
            }

        }

        private void txtTitle_Leave(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtTitle.Text.Equals(articlesList[listIndex].getTitle()) && !String.IsNullOrEmpty(txtTitle.Text))
                {
                    string newText = txtTitle.Text;
                    articlesList[listIndex].setTitle(newText);
                    row.Cells[TitleInd].Value = newText;
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_TITLE, newText);
                }
                else if (String.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox.Show("Поле Название должно быть заполнено");
                    txtTitle.Text = articlesList[listIndex].getTitle();
                }
            }
        }

        private void txtJournal_Leave(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtJournal.Text.Equals(articlesList[listIndex].getJournal()) && !String.IsNullOrEmpty(txtJournal.Text))
                {
                    string newText = txtYear.Text;
                    articlesList[listIndex].setJournal(newText);
                    row.Cells[JournalInd].Value = newText;
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_JOURNAL_ID, newText);
                }
                else if (String.IsNullOrEmpty(txtJournal.Text))
                {
                    MessageBox.Show("Поле журнал должно быть заполнено");
                    txtJournal.Text = articlesList[listIndex].getJournal();
                }
            }
        }

        private void txtYear_Leave(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtYear.Text.Equals(articlesList[listIndex].getYear()) && !String.IsNullOrEmpty(txtYear.Text))
                {
                    string newText = txtYear.Text;
                    articlesList[listIndex].setYear(Convert.ToInt32(newText));
                    row.Cells[YearInd].Value = newText;
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_YEAR, newText);
                }
                //Empty field
                else {
                    articlesList[listIndex].setYear(-1);
                    row.Cells[YearInd].Value = "";
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_YEAR, -1);
                }
            }
        }

        private void txtAuthors_Leave(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtAuthors.Text.Equals(articlesList[listIndex].getAuthors()) && !String.IsNullOrEmpty(txtAuthors.Text))
                {
                    string newText = txtAuthors.Text;
                    articlesList[listIndex].setAuthors(newText);
                    row.Cells[AuthorsInd].Value = newText;
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_AUTHORS, newText);
                }
                else if (String.IsNullOrEmpty(txtAuthors.Text)) {
                    MessageBox.Show("Поле авторы должно быть заполнено");
                    txtAuthors.Text = articlesList[listIndex].getAuthors();
                }
            }
        }

        private void txtVolume_Leave(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtVolume.Text.Equals(articlesList[listIndex].getVolume()) && !String.IsNullOrEmpty(txtVolume.Text))
                {
                    string newText = txtVolume.Text;
                    articlesList[listIndex].setVolume(Convert.ToInt32(newText));
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_VOLUME, newText);
                }//Empty field
                else
                {
                    articlesList[listIndex].setVolume(-1);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_VOLUME, -1);
                }
            }
        }

        private void txtIssue_Leave(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtIssue.Text.Equals(articlesList[listIndex].getIssue()) && !String.IsNullOrEmpty(txtIssue.Text))
                {
                    string newText = txtIssue.Text;
                    articlesList[listIndex].setIssue(Convert.ToInt32(newText));
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_ISSUE, newText);
                }
                //Empty field
                else
                {
                    articlesList[listIndex].setIssue(-1);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_ISSUE, -1);
                }
            }
        }

        private void txtPages_Leave(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtPages.Text.Equals(articlesList[listIndex].getPages()) && !String.IsNullOrEmpty(txtPages.Text))
                {
                    string newText = txtPages.Text;
                    articlesList[listIndex].setPages(Convert.ToInt32(newText));
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_PAGES, newText);
                }
                //Empty field
                else
                {
                    articlesList[listIndex].setPages(-1);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_PAGES, -1);
                }
            }
        }

        private void txtAbstract_Leave(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtAbstract.Text.Equals(articlesList[listIndex].getAbstractField()))
                {
                    string newText = txtAbstract.Text;
                    articlesList[listIndex].setAbstractField(newText);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_ABSTRACT, newText);
                }
            }
        }

        private void txtArXivID_Leave(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtArXivID.Text.Equals(articlesList[listIndex].getArXivID()) && !String.IsNullOrEmpty(txtArXivID.Text))
                {
                    string newText = txtArXivID.Text;
                    articlesList[listIndex].setArXivID(Convert.ToInt32(newText));
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_ARXIVID, newText);
                }
                //Empty field
                else
                {
                    articlesList[listIndex].setArXivID(-1);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_ARXIVID, -1);
                }
            }
        }

        private void txtDOI_Leave(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtDOI.Text.Equals(articlesList[listIndex].getDOI()) && !String.IsNullOrEmpty(txtDOI.Text))
                {
                    string newText = txtDOI.Text;
                    articlesList[listIndex].setDOI(Convert.ToInt32(newText));
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_DOI, newText);
                }
                //Empty field
                else
                {
                    articlesList[listIndex].setDOI(-1);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_DOI, -1);
                }
            }
        }

        private void txtPMID_Leave(object sender, EventArgs e)
        {

            DataGridViewSelectedRowCollection rowColl = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in rowColl)
            {
                int listIndex = Convert.ToInt32(row.Tag);
                if (!txtPMID.Text.Equals(articlesList[listIndex].getPMID()) && !String.IsNullOrEmpty(txtPMID.Text))
                {
                    string newText = txtPMID.Text;
                    articlesList[listIndex].setPMID(Convert.ToInt32(newText));
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_PMID, newText);
                }
                //Empty field
                else
                {
                    articlesList[listIndex].setPMID(-1);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_PMID, -1);
                }
            }
        }

        private bool addFileToMainFolder() {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                float fileSize = new FileInfo(ofd.FileName).Length;
                fileSize = fileSize / 1024 / 1024;
                if (fileSize <= 10)
                {
                    string fileName = ofd.SafeFileName;
                    fileDestPath = Path.Combine(func.getWorkspacePath(), fileName);
                    File.Copy(ofd.FileName, fileDestPath, true);

                    DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

                    int listIndex = -1;
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        listIndex = Convert.ToInt32(row.Tag);
                        row.Cells[FileAttachedInd].Value = imageList1.Images[2];
                    }

                    articlesList[listIndex].setFilepath(fileDestPath);
                    func.editArticle(articlesList[listIndex].getLocal_id(), Functions.KEY_ARTICLE_FILEPATH, fileDestPath);

                    return true;
                }
                else
                {
                    MessageBox.Show("Размер файла превышает допустимый");
                    return false;
                }
            }
            else {
                return false;
            }
        }

        private void btnFiles_Click(object sender, EventArgs e)
        {
            if (btnFiles.Text.ToString().Equals("Add file"))
            {
                if (addFileToMainFolder()) {
                    //MessageBox.Show("File added");
                    
                    btnFiles.Text = "Open File";
                    btnFiles.Tag = fileDestPath;
                }
                
            }
            else
            {
                if (Path.GetExtension(btnFiles.Tag.ToString()).Equals(".pdf"))
                {
                    //MessageBox.Show("pdf file exists");
                    /*ReadFile read = new ReadFile();
                    read.SetFileToShow(btnFiles.Tag.ToString());
                    read.ShowDialog();*/
                }
                else {
                    MessageBox.Show("Это не pdf файл.");
                }
                


                //axAcroPDF1.src = btnFiles.Tag.ToString();
                //MessageBox.Show(btnFiles.Tag.ToString());

                /*char[] word = btnFiles.Tag.ToString().ToCharArray();
                char[] questions = new char[25];
                int counter = 0;

                foreach (char oneWord in word) {
                    if (oneWord.ToString().Equals("?")) {
                        questions[counter] = oneWord;
                        counter++;

                    }
                }

                foreach (char question in questions) {
                    //MessageBox.Show(question.ToString());
                }
                String text = "text";
                MessageBox.Show(detectTextEncoding(out text).ToString());*/

                /*string str = btnFiles.Tag.ToString();
                Encoding srcEncodingFormat = Encoding.Default;
                Encoding dstEncodingFormat = Encoding.GetEncoding("windows-1251");
                byte[] originalByteString = srcEncodingFormat.GetBytes(str);

                /*foreach (byte one in originalByteString) {
                    MessageBox.Show(one.ToString());
                }
                
                byte[] convertedByteString = Encoding.Convert(srcEncodingFormat,
                dstEncodingFormat, originalByteString);
                string finalString = dstEncodingFormat.GetString(convertedByteString);

                MessageBox.Show(finalString);*/

                //MessageBox.Show(UtfConv(btnFiles.Tag.ToString()));
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }
        }

        private void txtVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtIssue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtPages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtArXivID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtDOI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtPMID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            tvFolderTitle.Text = selectedNode.Text;

            articlesList = func.getArticlesInFolder(Convert.ToInt32(selectedNode.Tag));
            initDataGrid();
            
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {

                TreeNode clickedNode = e.Node;

                treeView1.SelectedNode = e.Node;

                //treeViewContextMenu.Show(treeView1, e.Location);

                //MessageBox.Show("Right mouse button click: "+clickedNode.Tag);
            }
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (sender as ToolStripMenuItem);
            TreeView v = (TreeView)(mi.Owner as ContextMenuStrip).SourceControl;
            TreeNode parentNode = v.SelectedNode;

            int parentId = Convert.ToInt32(parentNode.Tag);
            string newFolderTitle = Prompt.ShowDialog("Input new folder name", "Create folder","");
            int newId = func.addLocalFolder(newFolderTitle,parentId);

            Folder newFolder = new Folder(newFolderTitle, parentId);


            TreeNode newNode = new TreeNode(newFolderTitle);
            newNode.Tag = newId;

            parentNode.Nodes.Add(newNode);
            folderTreeIds.Add(newId, newNode);
        }

        private void renameFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (sender as ToolStripMenuItem);
            TreeView v = (TreeView)(mi.Owner as ContextMenuStrip).SourceControl;
            TreeNode node = v.SelectedNode;

            int local_id = Convert.ToInt32(node.Tag);

            string renamedFolderTitle = Prompt.ShowDialog("Rename folder", "Input folder title",node.Text);
            func.changeFolderTitle(local_id,renamedFolderTitle);

            node.Text = renamedFolderTitle;
        }

        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (sender as ToolStripMenuItem);
            TreeView v = (TreeView)(mi.Owner as ContextMenuStrip).SourceControl;
            TreeNode n = v.SelectedNode;
            func.deleteFolder(Convert.ToInt32(n.Tag));
            treeView1.SelectedNode.Remove();
        }

        int PW;
        bool Hided;

        private void btnHideSlide_Click(object sender, EventArgs e)
        {
            if (Hided) { btnHideSlide.Text = "H\nI\n\nD\nE"; }
            else { btnHideSlide.Text = "S\nH\nO\nW"; }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hided)
            {
                slidePanel.Width = slidePanel.Width + 20;
                if (slidePanel.Width >= PW) {
                    timer1.Stop();
                    Hided = false;
                    //this.Refresh();
                }
            }
            else {
                slidePanel.Width = slidePanel.Width - 20;
                if (slidePanel.Width <= 0) {
                    timer1.Stop();
                    Hided = true;
                    //this.Refresh();
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentIndex = listBox1.SelectedIndex;

            switch (currentIndex)
            {
                //Search
                case 0:
                    break;
                //Search
                case 1:
                    break;
                //All articles
                case 3:
                    articlesList = func.getAllArticles();
                    initDataGrid();
                    tvFolderTitle.Text = "All articles";
                    break;
                //All articles
                case 4:
                    articlesList = func.getAllArticles();
                    initDataGrid();
                    tvFolderTitle.Text = "All articles";
                    break;
                //My articles
                case 5:
                    break;
                //Recently added
                case 6:
                    break;
                //Favorite
                case 7:
                    articlesList = func.getFavoriteArticles();
                    initDataGrid();
                    tvFolderTitle.Text = "Favorites";
                    break;
            }
        }

        private void btnAddManually_Click(object sender, EventArgs e)
        {
            //string renamedFolderTitle = Prompt.ShowDialog("Rename folder", "Input folder title",node.Text);

            AddManually add = new AddManually();
            if (add.ShowDialog() == DialogResult.OK)
            {

                JournalArticle newArticle = add.article;

                TreeNode currentNode = treeView1.SelectedNode;
                if (currentNode == null)
                {
                    newArticle.setFolder(0);
                }
                else {
                    newArticle.setFolder(Convert.ToInt32(currentNode.Tag));
                }

                //PHP: 2017-09-28 06:57:34
                //JAVA: Tue Oct 24 16:41:50 GMT+07:00 2017
                newArticle.setCreated_at(DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));

                int newArticleId = func.addLocalArticle(newArticle);
                newArticle.setLocal_id(newArticleId);

                articlesList.Add(newArticle);
                initDataGrid();
            }
        }


        private void syncFolders() {
            string folderListStr = func.composeJSONFromFolders();

            NameValueCollection formData = HttpUtility.ParseQueryString(String.Empty);
            formData.Add("request",folderListStr);
            formData.Add("type","windows");
            string postData = formData.ToString();

            string response = RESTful.PostRequest(EndPoints.URL_SEND_FOLDERS, postData);


            MessageBox.Show("Folder response: "+response);

            JObject obj = null;
            obj = JObject.Parse(response);

            JArray globalIdNamesArr = obj["global_ids"] as JArray;
            JArray local_dataArr = obj["local_data"] as JArray;

            //MessageBox.Show("obj: "+obj.ToString());

                foreach (JObject localFolderObj in local_dataArr)
                {
                    int global_id = Convert.ToInt32(localFolderObj.GetValue("global_id"));
                    int local_id = Convert.ToInt32(localFolderObj.GetValue("local_id"));
                    int is_delete = Convert.ToInt32(localFolderObj.GetValue("is_delete"));

                    func.disableIsNewFolder();
                    func.disableIsRenameFolder();

                    //TODO: Если global_id вернувшейся папки равно 0, то родитель папки был удален и эту папку
                    //TODO: тоже нужно удалить
                    if (global_id == 0)
                    {
                        func.deleteParentFolder(local_id);
                        continue;
                        //TODO:Если global_id вернувшейся папки равен -1, то просто удалить папки
                    }
                    else if (global_id == -1)
                    {
                        func.deleteGlobalFolder(local_id);
                        continue;
                    }

                    //TODO: Если is_delete вернувшейся папки равно единице, то удаляем её
                    if (is_delete == 1)
                    {
                        func.deleteGlobalFolder(local_id);
                    }
                }

                //Вторая часть

                foreach (JObject globalFolderData in globalIdNamesArr)
                {
                    func.checkCreateRenameFolder(globalFolderData);
                }

                /*for (int i = 0; i < globalIdNamesArr.length(); i++)
                {



                    JSONObject globalFolderData = globalIdNamesArr.getJSONObject(i);
                    //int global_id = globalFolderData.getInt("id");

                    Log.d(TAG, "MainActivity: checkCreateRenameFolder global_id: " + globalFolderData.getInt("id")
                        + ", title: " + globalFolderData.getString("name"));

                    dbh.checkCreateRenameFolder(globalFolderData);

                    //TODO: Restart Activity when done all synchronization
                }*/
            /*}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            syncArticles();
            //Upload(EndPoints.URL_SEND_FILE, "params",);
        }

        private void syncArticles() {
            string articlesListStr = func.composeJSONFromArticles();

            NameValueCollection formData = HttpUtility.ParseQueryString(String.Empty);
            formData.Add("request", articlesListStr);
            formData.Add("type","windows");
            string postData = formData.ToString();

            string response = RESTful.PostRequest(EndPoints.URL_SEND_ARTICLES,postData);

            //MessageBox.Show("SyncArticles response: " + response);

            JObject obj = JObject.Parse(response);

            JArray serverCreatedArr = obj["serverCreated"] as JArray;
            JArray insertedArticleArr = obj["insertedArticles"] as JArray;
            JArray needToSyncArr = obj["needToSync"] as JArray;
            JArray needToDeleteArr = obj["needToDelete"] as JArray;

            foreach (JValue needToDeleteObj in needToDeleteArr)
            {
                //MessageBox.Show("NeedToDelete: "+needToDeleteObj.ToString());

                func.deleteGlobalArticle(Convert.ToInt32(needToDeleteObj));
                
                //func.deleteGlobalArticle(Convert.ToInt32(needToDeleteObj.GetValue("global_id")));
            }

            foreach (JObject insertedArticleObj in insertedArticleArr)
            {

                int local_id = Convert.ToInt32(insertedArticleObj.GetValue("local_id"));
                int global_id = Convert.ToInt32(insertedArticleObj.GetValue("global_id"));

                func.setGlobalIdToArticle(local_id,global_id);
            }

            foreach (JObject serverCreatedObj in serverCreatedArr)
            {
                func.addGlobalArticle(serverCreatedObj);
            }

            foreach (var needToSyncObj in needToSyncArr) {

                if(needToSyncObj.GetType() == typeof(JArray)) {
                    foreach (JObject needToSyncFinalObj in needToSyncObj) {
                        func.updateLocalArticleByServer(needToSyncFinalObj);
                    }
                }
                
            }


            //Databases are synched, refresh DataGridView
            /*
             func.recreateAllArticles(articlesList);
            articlesList = func.getRootFolderArticles();
            
            initDataGrid();
             */

            articlesList = func.getRootFolderArticles();
            initDataGrid();
            tvFolderTitle.Text = "All articles";
            MessageBox.Show("Синхронизация прошла успешно");

        }

        private Stream Upload(string actionUrl, string paramString, Stream paramFileString, byte[] paramFilesBytes) {
            HttpContent stringContent = new StringContent(paramString);
            HttpContent fileStreamContent = new StreamContent(paramFileString);
            HttpContent bytesContent = new ByteArrayContent(paramFilesBytes);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent()) {
                formData.Add(stringContent, "param1", "param1");
                formData.Add(fileStreamContent, "file1","file1");
                formData.Add(bytesContent, "file2","file2");
                var response = client.PostAsync(actionUrl, formData).Result;
                if (!response.IsSuccessStatusCode) {
                    return null;
                }
                return response.Content.ReadAsStreamAsync().Result;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //e.Button == MouseButtons.
            if (e.Button == MouseButtons.Right) {

                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridRowIndex = e.RowIndex;
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[1];
                dataGridViewContextMenu.Show(dataGridView1, e.Location);
                dataGridViewContextMenu.Show(Cursor.Position);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            syncFolders();
        }

        private void deleteArticleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            int listIndex = Convert.ToInt32(dataGridView1.Rows[dataGridRowIndex].Tag);
            dataGridView1.Rows.RemoveAt(dataGridRowIndex);
            func.deleteLocalArticle(articlesList[listIndex].getLocal_id());
            articlesList.RemoveAt(listIndex);
            //Слишком затратно(придумать по другому)
            initDataGrid();
        }
    }
}
