using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Mendeley
{
    class Functions
    {
        public String CompareDataToJson(KeyValuePair<String, String> kvp) {
            List<KeyValuePair<String, String>> wordList;
            wordList = new List<KeyValuePair<String,String>>();
            wordList.Add(kvp);

            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(wordList);
            MessageBox.Show(json);
            return json;
        }

        public String getWorkspacePath()
        {
            Configuration configMan = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection keyValColl = configMan.AppSettings.Settings;
            return keyValColl["Workspace"].Value;
        }

        public int newIdForLibrary(MendeleyLocalDataSet mendeleyLocalDataSet)
        {

            if (mendeleyLocalDataSet.JournalArticles.Rows.Count != 0)
            {
                return mendeleyLocalDataSet.Tables["user_library"].AsEnumerable().Max(r => r.Field<int>("id"))+1;
            }
            else
            {
                return 1;
            }
        }

        public int newIdForArticles(MendeleyLocalDataSet mendeleyLocalDataSet)
        {

            if (mendeleyLocalDataSet.JournalArticles.Rows.Count != 0)
            {
                return mendeleyLocalDataSet.Tables["JournalArticles"].AsEnumerable().Max(r => r.Field<int>("id"))+1;
            }
            else
            {
                return 1;
            }
        }
    }
}
