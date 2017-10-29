using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mendeley
{
    public partial class AddManually : Form
    {
        Library main = new Library();
        Functions func = new Functions();
        String filePath = null;
        String fileName = null;

        public AddManually()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString()+", e "+ comboBox1.SelectedIndex);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    this.Controls.Clear();
                    CreateComboBox(0);
                    TextBox txtBox = new TextBox();
        Button btnAdd = new Button();
        ListBox lstBox = new ListBox();
        CheckBox chkBox = new CheckBox();
        Label lblCount = new Label();
                    this.Controls.Add(btnAdd);
                   
                break;
            }

}

        void CreateComboBox(int index) {

            ComboBox comboBox1 = new ComboBox();
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] {
            "JournalArticle",
            "Book",
            "Film"});
            comboBox1.SelectedIndex = index;
            comboBox1.Name = "comboBox1";
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            comboBox1.Location = new Point(12, 12);
            Controls.Add(comboBox1);
            
        }

        private void AddManually_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mendeleyLocalDataSet.user_library' table. You can move, or remove it, as needed.
            this.user_libraryTableAdapter.Fill(this.mendeleyLocalDataSet.user_library);
            // TODO: This line of code loads data into the 'mendeleyLocalDataSet.JournalArticles' table. You can move, or remove it, as needed.
            this.journalArticlesTableAdapter.Fill(this.mendeleyLocalDataSet.JournalArticles);
            btnSave.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTitle.Text = "No Title";
            txtAbstract.Text = "";
            txtAuthors.Text = "";
            txtJournal.Text = "";
            txtPages.Text = "";
            txtIssue.Text = "";
            txtVolume.Text = "";
            txtArXivID.Text = "";
            txtDOI.Text = "";
            txtPMID.Text = "";
            txtYear.Text = "";
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtTitle.Text.ToString().Equals("No title") || txtTitle.Text.ToString().Equals(""))
            {
                btnSave.Enabled = false;
            }
            else {
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //mendeleyLocalDataSet.JournalArticles.add
            addArticleLocal();
            main.FillListViewFromLocalDataSet();
        }

        private String getWorkspacePath()
        {
            Configuration configMan = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection keyValColl = configMan.AppSettings.Settings;
            //MessageBox.Show(keyValColl["Workspace"].Value);
            return keyValColl["Workspace"].Value;
        }



        private void addArticleLocal() {

            int tmp = 0;
            MendeleyLocalDataSet.JournalArticlesRow newJournalArticleRow;
            newJournalArticleRow = mendeleyLocalDataSet.JournalArticles.NewJournalArticlesRow();

            newJournalArticleRow.Id = func.newIdForArticles(mendeleyLocalDataSet);
            newJournalArticleRow.title = txtTitle.Text;
            newJournalArticleRow.authors = txtAuthors.Text;
            newJournalArticleRow.journal = txtJournal.Text;
            if (int.TryParse(txtYear.Text.ToString(), out tmp))
            {
                newJournalArticleRow.year = tmp;
            }
            else {
                newJournalArticleRow.year = DateTime.Now.Year;
            }

            if (int.TryParse(txtVolume.Text.ToString(), out tmp))
            {
                newJournalArticleRow.volume = tmp;
            }
            else
            {
                newJournalArticleRow.volume = 0;
            }

            if (int.TryParse(txtIssue.Text.ToString(), out tmp))
            {
                newJournalArticleRow.issue = tmp;
            }
            else
            {
                newJournalArticleRow.issue = 0;
            }

            if (int.TryParse(txtPages.Text.ToString(), out tmp))
            {
                newJournalArticleRow.pages = tmp;
            }
            else
            {
                newJournalArticleRow.pages = 0;
            }
            newJournalArticleRow._abstract = txtAbstract.Text;
            newJournalArticleRow.ArXivID = txtArXivID.Text;
            newJournalArticleRow.DOI = txtDOI.Text;
            newJournalArticleRow.PMID = txtPMID.Text;
            newJournalArticleRow.add_date = DateTime.Now;


            if (fileName != null && filePath != null)
            {
                String destPath = Path.Combine(func.getWorkspacePath(), fileName);
                File.Copy(filePath, destPath, true);
                newJournalArticleRow.filepath = destPath;
            }
            else {

                newJournalArticleRow.filepath = "";
            }


            mendeleyLocalDataSet.JournalArticles.Rows.Add(newJournalArticleRow);
            journalArticlesTableAdapter.Update(mendeleyLocalDataSet.JournalArticles);

            MendeleyLocalDataSet.user_libraryRow newuser_libraryRow;
            newuser_libraryRow = mendeleyLocalDataSet.user_library.Newuser_libraryRow();            

            newuser_libraryRow.id = func.newIdForLibrary(mendeleyLocalDataSet);
            newuser_libraryRow.mid = func.newIdForArticles(mendeleyLocalDataSet);
            newuser_libraryRow.uid = int.Parse(main.passUID);
            newuser_libraryRow.type = "JournalArticle";
            newuser_libraryRow.favorite = 0;

            mendeleyLocalDataSet.user_library.Rows.Add(newuser_libraryRow);
            user_libraryTableAdapter.Update(mendeleyLocalDataSet.user_library);

            this.DialogResult = DialogResult.OK;
            //MessageBox.Show(mendeleyLocalDataSet.JournalArticles.Count().ToString());
            this.Close();

        }

        private void journalArticlesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.journalArticlesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.mendeleyLocalDataSet);

        }

        private void btnFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                float fileSize = new FileInfo(ofd.FileName).Length;
                fileSize = fileSize / 1024 / 1024;
                if (fileSize <= 10)
                {
                    fileName = ofd.SafeFileName;
                    filePath = ofd.FileName;
                }
                else
                {
                    fileName = null;
                    filePath = null;
                    MessageBox.Show("Размер файла превышает допустимый");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}











