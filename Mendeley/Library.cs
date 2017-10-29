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

namespace Mendeley
{
    public partial class Library : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        Functions func = new Functions();
        Types types = new Types();
        ArrayList al;
        JournalArticle[] journalArticle;
        User_library[] user_library;
        String fileDestPath;

        private string uid = "1";

        public string passUID {
            get { return uid; }
            set { uid = value; }
        }

        public Library()
        {
            InitializeComponent();
        }

        public void sendPostGetUserLibrary() {
            string URL = "http://a0021435.xsph.ru/Mendeley/visual/getUserLibrary.php";
            WebClient webClient = new WebClient();
            webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;

            NameValueCollection formData = new NameValueCollection();
            formData["uid"] = "1";

            byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            //MessageBox.Show("Remote user_library "+responsefromserver);
            webClient.Dispose();

            //MessageBox.Show("Remote user_library response " + responsefromserver);

            JavaScriptSerializer js = new JavaScriptSerializer();
            user_library = js.Deserialize<User_library[]>(responsefromserver);
            //MessageBox.Show("Remote user_library mid "+user_library[0].mid);
        }




        public void CreateFolder() {
            if (System.IO.File.Exists(uid))
            {

                //MessageBox.Show("File exists");
            }
            else {
                System.IO.Directory.CreateDirectory(uid);
                //MessageBox.Show("File not exists");
            }

        }



        public void tryFTP() {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://testingweb.ru/public_html/Mendeley/guice.pdf");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential("filezilla@a0021435.xsph.ru", "351942025500ghj");

            byte[] fileContents = File.ReadAllBytes("guice.pdf");


            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();
        }






        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            FillDetails();
        }

        private void FillDetails() {
            if (listView1.SelectedItems[0].SubItems[1].Text.ToString().Equals(""))
            {
                btnFiles.Tag = null;
                btnFiles.Text = "Add file";
            }
            else {
                btnFiles.Text = "Open file";
                btnFiles.Tag = listView1.SelectedItems[0].SubItems[1].Text.ToString();
            }
            foreach (MendeleyLocalDataSet.JournalArticlesRow article in mendeleyLocalDataSet.JournalArticles.Rows)
            {
                if (article.title.Equals(listView1.SelectedItems[0].SubItems[3].Text.ToString())) {
                    
                    txtJournal.Text = article.journal.ToString();
                    txtYear.Text = article.year.ToString();
                    txtAuthors.Text = article.authors;
                    materialid.Text = article.Id.ToString();
                    comboBoxTypes.SelectedIndex = 0;
                    materialtype.Text = "JournalArticle";
                    txtVolume.Text = article.volume.ToString();
                    txtIssue.Text = article.issue.ToString();
                    txtPages.Text =  article.pages.ToString();

                    if (!article.IstitleNull()) {
                        tvName.Text = article.title;
                    }

                    if (!article.Is_abstractNull())
                    {
                        txtAbstract.Text = article._abstract;
                    }   
                    
                    if (!article.IsArXivIDNull()) {
                    txtArXivID.Text = article.ArXivID;
                    }
                    if (!article.IsDOINull()) {
                        txtDOI.Text = article.DOI;
                    }
                    if (!article.IsPMIDNull()) {
                        txtPMID.Text = article.PMID;
                    }
                    break;
                }
            }

        }

        

        private void btnTest_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            //tabControl1.TabPages.Add(tabPage1);
            TabPage tabPageInsert = types.JournalArticle();
            tabControl1.TabPages.Insert(0, tabPageInsert);
            tabControl1.SelectedTab = tabPageInsert;
            //MessageBox.Show(tabControl1.Focused.ToString());


            NameValueCollection nvp = new NameValueCollection();
            KeyValuePair<String, String> kvp = new KeyValuePair<string, string>();

            /*kvp["id"] = "one";
            kvp.

            func.CompareDataToJson(nvp);*/

            /*String[] jsonArr = {"01", "02"};
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(jsonArr);
            MessageBox.Show(json);*/
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            Synchronize();
        }

        DataTable GetMyLib()
        {
            DataTable dt = new DataTable();

            MySqlConnectionStringBuilder mysqlSB = new MySqlConnectionStringBuilder();

            List<int> mid = new List<int>();
            List<String> type = new List<String>();
            List<int> favourite = new List<int>();

            mysqlSB.Server = "141.8.195.41";
            mysqlSB.Database = "a0021435_mendeley";
            mysqlSB.UserID = "a0021435_user";
            mysqlSB.Password = "351942025500ghj";

            //MessageBox.Show(uid);

            //uid = "\'" + uid + "\'";
            uid = "1";

            string queryString = @"SELECT type,mid,favorite FROM user_library WHERE uid=" + uid;

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlSB.ConnectionString;
                MySqlCommand com = new MySqlCommand(queryString, con);

                try
                {
                    con.Open();

                    using (MySqlDataReader dr = com.ExecuteReader())
                    {


                        al = new ArrayList();
                        
                        while (dr.Read())
                        {

                            object[] values = new object[dr.FieldCount];
                            dr.GetValues(values);
                            al.Add(values);
                        }
                        //MessageBox.Show(al.Count.ToString());
                        dr.Close();
                        con.Close();

                        foreach (object[] row in al)
                        {
                            foreach (object column in row)
                            {

                                //MessageBox.Show(column.ToString());
                            }
                        }
                    }

                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }

        public int idCounter = 0;

        public void Synchronize()
        {

            sendPostGetUserLibrary();

            //MessageBox.Show(GetMyLib().ToString());
            sendPostGetJournalArticles();
            //MessageBox.Show("Remote library rows count "+journalArticle.Length);
            
            //MessageBox.Show("Local library rows count "+ mendeleyLocalDataSet.user_library.Count);
            DataRow[] rowsUserLibrary =  mendeleyLocalDataSet.user_library.Select();

            //MessageBox.Show("Local Rows count "+ rows.Length);
            //MessageBox.Show("Local Row 2 " + rows[0].ItemArray[2]);

            MessageBox.Show("Sync button clicked");

            if (user_library.Length == mendeleyLocalDataSet.user_library.Count)
            {

                MessageBox.Show("Local and Remote data nums are equal");//Количество записей одинаково

                //TEST
                MessageBox.Show(user_library[1].ToString());
                bool findIt = false;

                

                //Count of data are equal
                //Берем mid с обоих баз, сравниваем edit_date
                for (int i = 0; i < user_library.Length; i++)
                {
                    for (int k = 0; k < rowsUserLibrary.Length; k++)
                    {
                        MessageBox.Show("i = "+i+", k = "+k);
                        /*MessageBox.Show("Remote user_library uid "+user_library[i].uid+
                            ", local user_library uid "+rowsUserLibrary[k].ItemArray[0]);*/
                        if (user_library[i].mid == (int)rowsUserLibrary[k].ItemArray[2])
                        {
                            //Если количество статей и mid одинаковы, сравниваем по edit_date
                            findIt = true;
                            //MessageBox.Show("Find it");
                            //idCounter++;
                            CompareArticles((int)rowsUserLibrary[k].ItemArray[2], user_library[i].mid, "JournalArticle");
                            break;
                        }
                    }
                    if (findIt)
                    {
                        //MessageBox.Show("Соответствие найдено");
                        findIt = false;
                        //idCounter++;
                    }
                    else {
                        MessageBox.Show("Соответствие не найдено");
                        //Если количество статей одинаково, но mid отличаются то
                    }
                }
                MessageBox.Show("idCounter " +idCounter);
            }
            else {
                MessageBox.Show("Local and Remote datas are not same length");
            }
        }

        public void CompareArticles(int localMid, int remoteMid, string type) {
            MessageBox.Show("Remote articles count "+journalArticle.Length);

            for (int i =0;i<journalArticle.Length;i++) {
                MessageBox.Show("Remote article id "+journalArticle[i].id);
                if (journalArticle[i].id == remoteMid)
                {
                    //MessageBox.Show("Remote mid title "+journalArticle[i].title);

                    DataRow[] rowsJournalArticle = mendeleyLocalDataSet.JournalArticles.Select();

                    MessageBox.Show("Remote update_date " + journalArticle[i].update_date);

                    /*string testDateTime = "0001-01-01 00:00:00";
                    DateTime testDT = DateTime.Parse(testDateTime);
                    MessageBox.Show(testDT.ToString());*/


                    //MessageBox.Show("Remote update_date" + DateTime.Parse(journalArticle[i].update_date));

                    if (journalArticle[i].update_date == "0000-00-00 00:00:00")
                    {
                        journalArticle[i].update_date = "0001-01-01 00:00:00";
                    }

                    //string remoteUpdateDateStr = DateTime.ParseExact(journalArticle[i].update_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

                    MessageBox.Show("Remote update_date" + DateTime.Parse(journalArticle[i].update_date));

                    if (DateTime.Parse(journalArticle[i].update_date) > DateTime.Parse(rowsJournalArticle[0].ItemArray[14].ToString()))
                    {
                        MessageBox.Show("Remote database is newer");
                        MessageBox.Show("Remote mid update_date " + journalArticle[i].update_date + ", local mid update_date " + rowsJournalArticle[0].ItemArray[14]);
                        //Change local row with data from newer remote (journalArticle)
                    }
                    else
                    {
                        MessageBox.Show("Local database is newer");
                        Sync sync = new Sync();
                        sync.UpdateRemoteJournalArticle(rowsJournalArticle);
                        //Transfer newer local data to website and UPDATE tableRow
                    }

                    return;
                }
                //if we add multiple number of articles in both database
                
            }

            
        }

        private void journalArticlesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //TODO: Uncomment on Release
            //uid = passUID;
            this.Validate();
            this.journalArticlesBindingSource.EndEdit();
            this.user_libraryBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mendeleyLocalDataSet);

        }

        private void Library_Load(object sender, EventArgs e)
        {
            //TODO: move to login
            //How work reset on release debug versions
#if DEBUG
            //MessageBox.Show("DEBUG");
            Properties.Settings.Default.Reset();
#endif
            if (Properties.Settings.Default.FirstRun)
            {
                MessageBox.Show("FirstTimeRunning ");
                FolderBrowserDialog fold = new FolderBrowserDialog();
                fold.Description = "Выберите папку для сохранения статей";
                if (fold.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    //MessageBox.Show(fold.SelectedPath);
                    //TODO: uncomment on release
                    Configuration configManager =ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    KeyValueConfigurationCollection confCollection = configManager.AppSettings.Settings;
                    KeyValueConfigurationElement keyValElem = new KeyValueConfigurationElement("Workspace",fold.SelectedPath);

                    confCollection.Add(keyValElem);

                    configManager.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(configManager.AppSettings.SectionInformation.Name);
                }

                Properties.Settings.Default.FirstRun = false;
                Properties.Settings.Default.Save();
#if DEBUG
                //MessageBox.Show("DEBUG");
                Properties.Settings.Default.Reset();
#endif
            }
            
            this.user_libraryTableAdapter.Fill(this.mendeleyLocalDataSet.user_library);
            
            this.journalArticlesTableAdapter.Fill(this.mendeleyLocalDataSet.JournalArticles);
            FillListViewFromLocalDataSet();

        }

        public void RefreshData() {
            
            this.user_libraryTableAdapter.Fill(this.mendeleyLocalDataSet.user_library);
            
            this.journalArticlesTableAdapter.Fill(this.mendeleyLocalDataSet.JournalArticles);
            FillListViewFromLocalDataSet();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button2, 0, button2.Height);
        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            foreach (DataRow article in mendeleyLocalDataSet.JournalArticles) {
                MessageBox.Show(article.ItemArray[0].ToString());
            }
            /*if (mendeleyLocalDataSet.JournalArticles.Rows.Count != 0)
            {
                int maxSongId = mendeleyLocalDataSet.Tables["JournalArticles"].AsEnumerable().Max(r => r.Field<int>("id"));
                MessageBox.Show(maxSongId.ToString());
            }
            else {
                MessageBox.Show("First record");
            }*/
            MessageBox.Show(mendeleyLocalDataSet.JournalArticles[0].title);
            
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        //Testing REMOVE LATER
        public void sendPostGetJournalArticles()
        {
            string URL = "http://a0021435.xsph.ru/Mendeley/visual/getLibrary.php";
            WebClient webClient = new WebClient();
            webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;

            NameValueCollection formData = new NameValueCollection();
            formData["uid"] = "1";

            byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            //MessageBox.Show("getLibrary response "+responsefromserver);
            webClient.Dispose();

            JavaScriptSerializer js = new JavaScriptSerializer();
            journalArticle = js.Deserialize<JournalArticle[]>(responsefromserver);
            //MessageBox.Show(journalArticle[0].title);

            listView1.Items.Clear();
            //MessageBox.Show(journalArticle.Length.ToString());
            for (int i = 0; i < journalArticle.Length; i++)
            {

                ListViewItem item = new ListViewItem(journalArticle[i].id.ToString());
                item.SubItems.Add(journalArticle[i].filepath);
                item.SubItems.Add(journalArticle[i].authors);
                item.SubItems.Add(journalArticle[i].title);
                item.SubItems.Add(journalArticle[i].year.ToString());
                item.SubItems.Add(journalArticle[i].journal);
                item.SubItems.Add(journalArticle[i].add_date.ToString());

                listView1.Items.Add(item);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendPostGetJournalArticles();
            foreach (DataRow row in mendeleyLocalDataSet.JournalArticles.Rows) {
                row.Delete();
            }
            journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);

            foreach (DataRow row in mendeleyLocalDataSet.user_library.Rows) {
                row.Delete();
            }
            user_libraryTableAdapter.Update(mendeleyLocalDataSet.user_library);

            sendPostGetUserLibrary();
            foreach (JournalArticle article in journalArticle)
            {
                //string remoteUpdateDateStr = DateTime.ParseExact(journalArticle[i].update_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                //MessageBox.Show(article.title);
                MendeleyLocalDataSet.JournalArticlesRow newJournalArticleRow;
                newJournalArticleRow = mendeleyLocalDataSet.JournalArticles.NewJournalArticlesRow();

                newJournalArticleRow.Id = article.id;
                newJournalArticleRow.title = article.title;
                newJournalArticleRow.authors = article.authors;
                newJournalArticleRow._abstract = article.abstractText;
                newJournalArticleRow.year = article.year;
                newJournalArticleRow.journal = article.journal;
                newJournalArticleRow.filepath = article.filepath;
                newJournalArticleRow.volume = article.volume;
                newJournalArticleRow.pages = article.pages;
                newJournalArticleRow.issue = article.issue;
                newJournalArticleRow.ArXivID = article.ArXivID;
                newJournalArticleRow.DOI = article.DOI;
                newJournalArticleRow.PMID = article.PMID;
                newJournalArticleRow.add_date = DateTime.Parse(article.add_date);



                
                DateTime ukDateFormat;
                string ukFormat = "dd.MM.yyyy H:mm:ss";
                //update_date
                DateTime.TryParseExact(article.update_date, ukFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out ukDateFormat);
                DateTime update_date = ukDateFormat;
                //delete_date
                DateTime.TryParseExact(article.delete_date, ukFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out ukDateFormat);
                DateTime delete_date = ukDateFormat;

                newJournalArticleRow.update_date = update_date;
                newJournalArticleRow.delete_date = delete_date;

                mendeleyLocalDataSet.JournalArticles.Rows.Add(newJournalArticleRow);

                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);

            }
            foreach (User_library libItem in user_library)
            {
                //MessageBox.Show(libItem.mid.ToString());

                MendeleyLocalDataSet.user_libraryRow newuser_libraryRow;
                newuser_libraryRow = mendeleyLocalDataSet.user_library.Newuser_libraryRow();

                newuser_libraryRow.id = libItem.id;
                //MessageBox.Show(libItem.id.ToString());
                newuser_libraryRow.mid = libItem.mid;
                newuser_libraryRow.uid = libItem.uid;
                newuser_libraryRow.type = libItem.type;
                newuser_libraryRow.favorite = libItem.favorite;

                mendeleyLocalDataSet.user_library.Rows.Add(newuser_libraryRow);

                user_libraryTableAdapter.Update(mendeleyLocalDataSet.user_library);
            }


        }

        private void txtJournal_TextChanged(object sender, EventArgs e)
        {
            if (txtJournal.Focused) {
                MessageBox.Show("txtJournal in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.journal = txtJournal.Text.ToString();
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
                //MessageBox.Show(journalArticleRow.journal);
                MessageBox.Show("SelectedIndex " + listView1.SelectedIndices[0]);
                listView1.SelectedItems[0].SubItems[5].Text = journalArticleRow.journal;
            }
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            if (txtYear.Focused)
            {
                MessageBox.Show("txtYear in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.year = int.Parse(txtYear.Text.ToString());
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
                listView1.SelectedItems[0].SubItems[4].Text = journalArticleRow.year.ToString();
            }
        }

        private void txtVolume_TextChanged(object sender, EventArgs e)
        {
            if (txtVolume.Focused)
            {
                MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.volume = int.Parse(txtVolume.Text.ToString());
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        private void txtIssue_TextChanged(object sender, EventArgs e)
        {
            if (txtIssue.Focused)
            {
                //MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.issue = int.Parse(txtIssue.Text.ToString());
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        private void txtPages_TextChanged(object sender, EventArgs e)
        {
            if (txtPages.Focused)
            {
                //MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.pages = int.Parse(txtPages.Text.ToString());
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        private void txtAbstract_TextChanged(object sender, EventArgs e)
        {
            if (txtAbstract.Focused)
            {
                //MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow._abstract = txtAbstract.Text.ToString();
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        private void txtArXivID_TextChanged(object sender, EventArgs e)
        {
            if (txtArXivID.Focused)
            {
                //MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.ArXivID = txtArXivID.Text.ToString();
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        private void txtDOI_TextChanged(object sender, EventArgs e)
        {
            if (txtDOI.Focused)
            {
                //MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.DOI = txtDOI.Text.ToString();
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        private void txtPMID_TextChanged(object sender, EventArgs e)
        {
            if (txtPMID.Focused)
            {
                //MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.PMID = txtPMID.Text.ToString();
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        private void txtAuthors_TextChanged(object sender, EventArgs e)
        {
            if (txtAuthors.Focused)
            {
                //MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                journalArticleRow.authors = txtAuthors.Text.ToString();
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
                listView1.SelectedItems[0].SubItems[2].Text = journalArticleRow.authors;
            }
        }


        //TODO: think on it
        private void ArticleChangeWithoutListView(Control txt, object column) {
            if (txt.Focused)
            {
                MessageBox.Show("txtVolume in focus");
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text));
                column = int.Parse(txtVolume.Text.ToString());
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
            }
        }

        public void FillListViewFromLocalDataSet() {
            var array = new object[mendeleyLocalDataSet.user_library.Count];
            array = mendeleyLocalDataSet.JournalArticles.ToArray();
            listView1.Items.Clear();
            for (int i =0;i< mendeleyLocalDataSet.user_library.Count; i++) {
                MendeleyLocalDataSet.JournalArticlesRow journalArticleRow = (MendeleyLocalDataSet.JournalArticlesRow)array[i];
                //MessageBox.Show("Title " + journalArticleRow.title);
                ListViewItem item = new ListViewItem(journalArticleRow.Id.ToString());
                item.SubItems.Add(journalArticleRow.filepath);
                item.SubItems.Add(journalArticleRow.authors);
                item.SubItems.Add(journalArticleRow.title);
                item.SubItems.Add(journalArticleRow.year.ToString());
                item.SubItems.Add(journalArticleRow.journal);
                item.SubItems.Add(journalArticleRow.add_date.ToString());

                listView1.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            //contextMenuStrip1.Show(button3, new Point(0, button3.Height));
        }

        private void contextMenuTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            AddManually add = new AddManually();
            if (add.ShowDialog() == DialogResult.OK) {
                RefreshData();
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
                    if (materialtype.Text.ToString().Equals("JournalArticle"))
                    {
                        //mendeleyLocalDataSet.JournalArticles[8].filepath = fileDestPath;
                        MendeleyLocalDataSet.JournalArticlesRow journalArticleRow =
                            mendeleyLocalDataSet.JournalArticles.FindById(int.Parse(materialid.Text.ToString()));
                        journalArticleRow.filepath = fileDestPath;
                        journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);
                        btnFiles.Text = "Open File";
                        RefreshData();
                        btnFiles.Tag = fileDestPath;
                    }
                }
                
            }
            else
            {
                if (Path.GetExtension(btnFiles.Tag.ToString()).Equals(".pdf"))
                {
                    MessageBox.Show("pdf file exists");
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

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addFileToMainFolder()) {

                MendeleyLocalDataSet.JournalArticlesRow newJournalArticleRow;
                newJournalArticleRow = mendeleyLocalDataSet.JournalArticles.NewJournalArticlesRow();

                newJournalArticleRow.Id = func.newIdForArticles(mendeleyLocalDataSet);
                newJournalArticleRow.title = Path.GetFileNameWithoutExtension(ofd.FileName);
                newJournalArticleRow.authors = "";
                newJournalArticleRow.journal = "";
                newJournalArticleRow.year = DateTime.Now.Year;
                newJournalArticleRow.volume = 0;
                newJournalArticleRow.issue = 0;
                newJournalArticleRow.pages = 0;
                newJournalArticleRow._abstract = "";
                newJournalArticleRow.ArXivID = "";
                newJournalArticleRow.DOI = "";
                newJournalArticleRow.PMID = "";
                newJournalArticleRow.filepath = fileDestPath;
                newJournalArticleRow.add_date = DateTime.Now;

                mendeleyLocalDataSet.JournalArticles.Rows.Add(newJournalArticleRow);
                journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);

                MendeleyLocalDataSet.user_libraryRow newuser_libraryRow;
                newuser_libraryRow = mendeleyLocalDataSet.user_library.Newuser_libraryRow();

                newuser_libraryRow.id = func.newIdForLibrary(mendeleyLocalDataSet);
                newuser_libraryRow.mid = func.newIdForArticles(mendeleyLocalDataSet);
                newuser_libraryRow.uid = int.Parse(passUID);
                newuser_libraryRow.type = "JournalArticle";
                newuser_libraryRow.favorite = 0;

                mendeleyLocalDataSet.user_library.Rows.Add(newuser_libraryRow);
                user_libraryTableAdapter.Update(mendeleyLocalDataSet.user_library);

                RefreshData();
            }
            
        }
    }
}
