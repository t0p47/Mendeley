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
        public JournalArticle article { get; set; }

        Library main = new Library();
        Functions func = new Functions();
        String filePath = null;
        String fileName = null;

        OpenFileDialog ofd = new OpenFileDialog();

        public AddManually()
        {
            InitializeComponent();
        }

        private void AddManually_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTitle.Text = "No title";
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
            String title = txtTitle.Text;
            String authors = txtAuthors.Text;
            String abstractField = txtAbstract.Text;
            String journal = txtJournal.Text;

            if (String.IsNullOrEmpty(authors) || String.IsNullOrEmpty(journal)) {

                MessageBox.Show("Проверьте поля авторы и журнал");
                return;
            }

            int volume = -1;
            if (!String.IsNullOrEmpty(txtVolume.Text))
            {
                volume = Convert.ToInt32(txtVolume.Text);
            }

            int issue = -1;
            if (!String.IsNullOrEmpty(txtIssue.Text))
            {
                issue = Convert.ToInt32(txtIssue.Text);
            }

            int year = -1;
            if (!String.IsNullOrEmpty(txtYear.Text))
            {
                year = Convert.ToInt32(txtYear.Text);
            }

            int pages = -1;
            if (!String.IsNullOrEmpty(txtPages.Text))
            {
                pages = Convert.ToInt32(txtPages.Text);
            }

            int ArXivID = -1;
            if (!String.IsNullOrEmpty(txtArXivID.Text))
            {
                ArXivID = Convert.ToInt32(txtArXivID.Text);
            }

            int DOI = -1;
            if (!String.IsNullOrEmpty(txtDOI.Text))
            {
                DOI = Convert.ToInt32(txtDOI.Text);
            }

            int PMID = -1;
            if (!String.IsNullOrEmpty(txtPMID.Text))
            {
                PMID = Convert.ToInt32(txtPMID.Text);
            }

            string filePath = null;
            if (!String.IsNullOrEmpty(fileName)) {
                //MessageBox.Show("Saving file");
                string fileDestPath = Path.Combine(func.getWorkspacePath(), fileName);
                //MessageBox.Show("Save file: "+fileDestPath);
                File.Copy(ofd.FileName, fileDestPath, true);
                filePath = fileDestPath;
            }

            //MessageBox.Show("File is "+filePath);

            article = new JournalArticle(title, authors, abstractField, journal, volume, issue, year, pages, ArXivID, DOI, PMID, filePath);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnFiles_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                float fileSize = new FileInfo(ofd.FileName).Length;
                fileSize = fileSize / 1024 / 1024;
                if (fileSize <= 10)
                {
                    fileName = ofd.SafeFileName;
                }
                else
                {
                    MessageBox.Show("Размер файла превышает допустимый");
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }


            /*MessageBox.Show("Get workspace path: "+getWorkspacePath());

            OpenFileDialog ofd = new OpenFileDialog();
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                float fileSize = new FileInfo(ofd.FileName).Length;
                fileSize = fileSize / 1024 / 1024;
                if (fileSize <= 10)
                {
                    fileName = ofd.SafeFileName;
                    filePath = ofd.FileName;

                    MessageBox.Show("fileName: "+fileName+", filePath: "+filePath);
                }
                else
                {
                    fileName = null;
                    filePath = null;
                    MessageBox.Show("Размер файла превышает допустимый");
                }
            }*/
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}











