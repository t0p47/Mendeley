using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mendeley
{
    class Sync
    {

        string[] journalArticleColumns = { "id", "title", "authors", "abstract", "journal", "year", "volume", "issue", "pages", "ArXivID", "DOI", "PMID", "filename", "add_date", "update_date", "delete_date" };

        public void UpdateRemoteJournalArticle(DataRow[] rowsJournalArticle) {
            MessageBox.Show("Sync Class");

            //MessageBox.Show("We synchronize " + rowsJournalArticle.Length);

            NameValueCollection formData = new NameValueCollection();

            foreach (DataRow row in rowsJournalArticle)
            {

                for (int i = 0;i<row.ItemArray.Length;i++) {
                    //MessageBox.Show("Foreach column " + row.ItemArray[i]);
                    formData[journalArticleColumns[i]] = row.ItemArray[i].ToString();
                }
            }
            sendToWebsite(formData);
        }

        public void sendToWebsite(NameValueCollection formData) {
            string URL = "http://a0021435.xsph.ru/Mendeley/visual/update.php";
            WebClient webClient = new WebClient();
            webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;

            formData["type"] = "JournalArticle";
            formData["update_date"] = DateTime.Parse(formData["update_date"]).ToString("yyyy-MM-dd HH:mm:ss");

            byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            webClient.Dispose();

            MessageBox.Show("Remote response "+responsefromserver);
        }
    }
}
