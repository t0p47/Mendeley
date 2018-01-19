using Mendeley.ModelResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Mendeley
{
    class Functions
    {

        public static string serverToken;
        public static SqlConnection sqlConnection;

        public String CompareDataToJson(KeyValuePair<String, String> kvp) {
            List<KeyValuePair<String, String>> wordList;
            wordList = new List<KeyValuePair<String, String>>();
            wordList.Add(kvp);

            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(wordList);
            MessageBox.Show(json);
            return json;
        }

        public String getWorkspacePath()
        {
            return Properties.Settings.Default.Workspace;
        }

        public Boolean checkInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean checkTokenIsExpired() {
            string response = RESTful.GetHeadersRequest(EndPoints.URL_REFRESH_TOKEN);
            if (response.Equals("relogin"))
            {
                return true;
            }
            else {
                return false;
            }
        }

        //DeserializeObject
        public Boolean isLogin() {

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT token FROM [Token]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    string tmpToken = Convert.ToString(sqlReader["token"]);
                    if (!string.IsNullOrEmpty(tmpToken) && !string.IsNullOrWhiteSpace(tmpToken))
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally {
                if (sqlReader != null) {
                    sqlReader.Close();
                }
            }
        }

        public Boolean loginFirstTime(string postData) {
            string responseWithToken = RESTful.PostRequest(EndPoints.URL_AUTHENTICATE, postData);

            string token = JsonConvert.DeserializeObject<TokenObject>(responseWithToken).token;

            if (!String.IsNullOrEmpty(token) && !String.IsNullOrWhiteSpace(token))
            {
                Functions.serverToken = token;
                setFirstToken(token);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean login(string postData) {
            string responseWithToken = RESTful.PostRequest(EndPoints.URL_AUTHENTICATE, postData);

            string token = JsonConvert.DeserializeObject<TokenObject>(responseWithToken).token;

            if (!String.IsNullOrEmpty(token) && !String.IsNullOrWhiteSpace(token))
            {
                Functions.serverToken = token;
                setTokenToDB(token);
                return true;
            }
            else {
                return false;
            }
        }

        public static void setTokenToDB(string token) {
            SqlCommand command = new SqlCommand("UPDATE [Token] SET [token]=@Token WHERE [Id]=1", sqlConnection);

            command.Parameters.AddWithValue("Token", token);

            command.ExecuteNonQuery();
            MessageBox.Show("Updating token in local db");

        }

        /*public List<JournalArticle> getAllArticlesFromServer() {
            RESTful.GetHeadersRequest(EndPoints.URL_GET_ALL_ARTICLES);
        }*/


        //TMP
        /*public void getAllFoldersFromServer()
        {
            string foldersResponse = RESTful.GetHeadersRequest(EndPoints.URL_GET_ALL_FOLDERS);

            //string token = JsonConvert.DeserializeObject<TokenObject>(responseWithToken).token;
            var folders = JsonConvert.DeserializeObject<List<FolderResponse>>(foldersResponse);
            MessageBox.Show(folders.ToString());

            foreach (FolderResponse responseFolder in folders)
            {
                Console.WriteLine("Folder response: " + responseFolder.title);
            }


            Console.WriteLine("All folders: " + foldersResponse);
        }*/

        /*private List<Folder> convertFolderResponseToFolder(List<FolderResponse> responseFolders) {
            List<Folder> foldersList = new List<Folder>();

            foreach (FolderResponse responseFolder in responseFolders)
            {

                /*
                 if(folder.getParent_id()!=0){
                    int oldParent_id = folder.getParent_id();
                    String searchNewParent = "SELECT "+KEY_LOCAL_ID+" FROM "+TABLE_FOLDERS+" WHERE "+KEY_GLOBAL_ID+"="+oldParent_id;
                    Cursor cursor = db.rawQuery(searchNewParent,null);

                    int newParentId = 0;

                    if(cursor.moveToFirst()){
                        newParentId = cursor.getInt(0);
                    }

                    values.put(KEY_FOLDER_PARENT_ID, newParentId);
                }else{
                    values.put(KEY_FOLDER_PARENT_ID, 0);
                }
                 

                if (responseFolder.id != 0) {
                    int oldParent_id;
                    int.TryParse(responseFolder.parent_id, out oldParent_id);


                }

                //global, title, parent_id
                Folder folder = new Folder();
            }

            return foldersList;
        }*/

        /*public void getAllArticlesFromServer()
        {
            string articlesResponse = RESTful.GetHeadersRequest(EndPoints.URL_GET_ALL_ARTICLES);

            var articles = JsonConvert.DeserializeObject<List<JournalArticleResponse>>(articlesResponse);
            MessageBox.Show(articles.ToString());

            Console.WriteLine("All articles: " + articlesResponse);
        }*/

        public static string getTokenFromServer() {
            string token = String.Empty;

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT token FROM [Token] WHERE [Id]=1", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    string tmpToken = Convert.ToString(sqlReader["token"]);
                    return tmpToken;
                }
            }
            catch (Exception ex)
            {
                return token;
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                if (sqlReader != null) {
                    sqlReader.Close();
                }

            }
            return token;
        }




        //Work with db

        private static String TABLE_FOLDERS = "[folders]";
        private static String TABLE_ARTICLES = "[journal_articles]";

        //Common keys
        public static String KEY_LOCAL_ID = "local_id";
        public static String KEY_GLOBAL_ID = "global_id";

        //Folders table
        public static String KEY_FOLDER_TITLE = "title";
        public static String KEY_FOLDER_PARENT_ID = "parent_id";

        //Articles(journal_articles) table
        public static String KEY_ARTICLE_TITLE = "title";
        public static String KEY_ARTICLE_AUTHORS = "authors";
        public static String KEY_ARTICLE_ABSTRACT = "abstract";
        public static String KEY_ARTICLE_JOURNAL_ID = "journal_id";
        public static String KEY_ARTICLE_VOLUME = "volume";
        public static String KEY_ARTICLE_ISSUE = "issue";
        public static String KEY_ARTICLE_YEAR = "year";
        public static String KEY_ARTICLE_PAGES = "pages";
        public static String KEY_ARTICLE_ARXIVID = "ArXivID";
        public static String KEY_ARTICLE_DOI = "DOI";
        public static String KEY_ARTICLE_PMID = "PMID";
        public static String KEY_ARTICLE_FOLDER = "folder";
        public static String KEY_ARTICLE_FILEPATH = "filepath";
        public static String KEY_ARTICLE_CREATED_AT = "created_at";
        public static String KEY_ARTICLE_UPDATED_AT = "updated_at";
        public static String KEY_ARTICLE_FAVORITE = "favorite";

        public static String KEY_IS_NEW = "is_new";
        public static String KEY_IS_CHANGE = "is_change";
        public static String KEY_IS_DELETE = "is_delete";


        public void recreateAllTables() {

            string truncateFolderQuery = "TRUNCATE TABLE " + TABLE_FOLDERS;

            SqlCommand commandFolder = new SqlCommand(truncateFolderQuery, sqlConnection);

            commandFolder.ExecuteNonQuery();


            string truncateArticleQuery = "TRUNCATE TABLE "+TABLE_ARTICLES;

            SqlCommand commandArticle = new SqlCommand(truncateArticleQuery, sqlConnection);

            commandArticle.ExecuteNonQuery();
        }




        public List<Folder> getAllFolders() {

            List<Folder> folders = new List<Folder>();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT " + KEY_LOCAL_ID + "," + KEY_GLOBAL_ID + "," + KEY_FOLDER_TITLE + "," + KEY_FOLDER_PARENT_ID + " FROM " + TABLE_FOLDERS+" WHERE ["+KEY_IS_DELETE+"]=0", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {

                    int local_id = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                    int global_id = Convert.ToInt32(sqlReader[KEY_GLOBAL_ID]);
                    String title = Convert.ToString(sqlReader[KEY_FOLDER_TITLE]);
                    int parent_id = Convert.ToInt32(sqlReader[KEY_FOLDER_PARENT_ID]);

                    Folder folder = new Folder(local_id, global_id, title, parent_id);
                    folders.Add(folder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                if (sqlReader != null) {
                    sqlReader.Close();
                }
            }

            return folders;
        }

        public void recreateAllFolders(List<Folder> foldersList) {

            String query = "INSERT INTO " + TABLE_FOLDERS + "(" + KEY_GLOBAL_ID + "," + KEY_FOLDER_TITLE + "," + KEY_FOLDER_PARENT_ID
                + ") VALUES(@" + KEY_GLOBAL_ID + ",@" + KEY_FOLDER_TITLE + ",@" + KEY_FOLDER_PARENT_ID
                + ")";

            foreach (Folder folder in foldersList)
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);

                //Меняем global_id на local_id
                int newParentId = getFolderLocalIdByGlobal(folder.getParent_id());

                command.Parameters.AddWithValue(KEY_GLOBAL_ID, folder.getGlobal_id());
                command.Parameters.AddWithValue(KEY_FOLDER_TITLE, folder.getTitle());
                command.Parameters.AddWithValue(KEY_FOLDER_PARENT_ID, newParentId);

                command.ExecuteNonQuery();
            }
        }

        public void recreateAllArticles(List<JournalArticle> articlesList) {

            String query = "INSERT INTO " + TABLE_ARTICLES + "(" + KEY_GLOBAL_ID + "," + KEY_ARTICLE_TITLE + "," + KEY_ARTICLE_AUTHORS + ","
                + KEY_ARTICLE_ABSTRACT + "," + KEY_ARTICLE_JOURNAL_ID + "," + KEY_ARTICLE_VOLUME + "," + KEY_ARTICLE_ISSUE + ","
                + KEY_ARTICLE_YEAR + "," + KEY_ARTICLE_PAGES + "," + KEY_ARTICLE_ARXIVID + "," + KEY_ARTICLE_DOI + "," + KEY_ARTICLE_PMID + ","
                + KEY_ARTICLE_CREATED_AT + "," + KEY_ARTICLE_UPDATED_AT + "," + KEY_ARTICLE_FAVORITE + "," + KEY_ARTICLE_FOLDER+","
                + KEY_ARTICLE_FILEPATH+") VALUES(@" + KEY_GLOBAL_ID + ",@" + KEY_ARTICLE_TITLE + ",@" + KEY_ARTICLE_AUTHORS + ",@"
                + KEY_ARTICLE_ABSTRACT + ",@" + KEY_ARTICLE_JOURNAL_ID + ",@" + KEY_ARTICLE_VOLUME + ",@" + KEY_ARTICLE_ISSUE + ",@"
                + KEY_ARTICLE_YEAR + ",@" + KEY_ARTICLE_PAGES + ",@" + KEY_ARTICLE_ARXIVID + ",@" + KEY_ARTICLE_DOI + ",@" + KEY_ARTICLE_PMID + ",@"
                + KEY_ARTICLE_CREATED_AT + ",@" + KEY_ARTICLE_UPDATED_AT + ",@" + KEY_ARTICLE_FAVORITE + ",@" + KEY_ARTICLE_FOLDER 
                +",@"+KEY_ARTICLE_FILEPATH+")";

            foreach (JournalArticle article in articlesList)
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);

                //Меняем global_id на local_id
                article.setFolder(getFolderLocalIdByGlobal(article.getFolder()));

                if (article.getAbstractField() == null)
                {
                    article.setAbstractField("");
                }

                if (article.getFilepath() == null) {
                    article.setFilepath("");
                }

                command.Parameters.AddWithValue(KEY_GLOBAL_ID, article.getGlobal_id());
                command.Parameters.AddWithValue(KEY_ARTICLE_TITLE, article.getTitle());
                command.Parameters.AddWithValue(KEY_ARTICLE_AUTHORS, article.getAuthors());
                command.Parameters.AddWithValue(KEY_ARTICLE_ABSTRACT, article.getAbstractField());
                command.Parameters.AddWithValue(KEY_ARTICLE_JOURNAL_ID, article.getJournal());
                command.Parameters.AddWithValue(KEY_ARTICLE_VOLUME, article.getVolume());
                command.Parameters.AddWithValue(KEY_ARTICLE_ISSUE, article.getIssue());
                command.Parameters.AddWithValue(KEY_ARTICLE_YEAR, article.getYear());
                command.Parameters.AddWithValue(KEY_ARTICLE_PAGES, article.getPages());
                command.Parameters.AddWithValue(KEY_ARTICLE_ARXIVID, article.getArXivID());
                command.Parameters.AddWithValue(KEY_ARTICLE_DOI, article.getDOI());
                command.Parameters.AddWithValue(KEY_ARTICLE_PMID, article.getPMID());
                command.Parameters.AddWithValue(KEY_ARTICLE_CREATED_AT, article.getCreated_at());
                command.Parameters.AddWithValue(KEY_ARTICLE_UPDATED_AT, article.getUpdated_at());
                command.Parameters.AddWithValue(KEY_ARTICLE_FAVORITE, article.getFavorite());
                command.Parameters.AddWithValue(KEY_ARTICLE_FOLDER, article.getFolder());
                command.Parameters.AddWithValue(KEY_ARTICLE_FILEPATH, article.getFilepath());

                command.ExecuteNonQuery();
            }

        }

        public int addLocalFolder(String title, int parentFolderId) {

            String query = "INSERT INTO " + TABLE_FOLDERS + "(" + KEY_FOLDER_TITLE + "," + KEY_FOLDER_PARENT_ID + "," + KEY_IS_NEW
                + ") OUTPUT INSERTED."+KEY_LOCAL_ID+" VALUES (@" + KEY_FOLDER_TITLE + ",@" + KEY_FOLDER_PARENT_ID + ",@" + KEY_IS_NEW
                + ")";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue(KEY_FOLDER_TITLE, title);
            command.Parameters.AddWithValue(KEY_FOLDER_PARENT_ID, parentFolderId);
            command.Parameters.AddWithValue(KEY_IS_NEW, 1);

            //TODO: Как вернуть вставленный(сгенерированный) local_id, для новой записи
            //command.Parameters.Add(KEY_LOCAL_ID, SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            //command.ExecuteNonQuery();

            int newFolderId = (int)command.ExecuteScalar();

            return newFolderId;
        }

        public int addLocalArticle(JournalArticle article) {

            String query = "INSERT INTO " + TABLE_ARTICLES + "(" + KEY_ARTICLE_TITLE + "," + KEY_ARTICLE_AUTHORS + ","
                + KEY_ARTICLE_ABSTRACT + "," + KEY_ARTICLE_JOURNAL_ID + "," + KEY_ARTICLE_VOLUME + "," + KEY_ARTICLE_ISSUE + ","
                + KEY_ARTICLE_YEAR + "," + KEY_ARTICLE_PAGES + "," + KEY_ARTICLE_ARXIVID + "," + KEY_ARTICLE_DOI + "," + KEY_ARTICLE_PMID + ","
                + KEY_ARTICLE_CREATED_AT + "," + KEY_ARTICLE_FOLDER + "," + KEY_ARTICLE_FILEPATH + "," + KEY_IS_NEW + ") OUTPUT INSERTED."+KEY_LOCAL_ID+"  VALUES(@"
                + KEY_ARTICLE_TITLE + ",@" + KEY_ARTICLE_AUTHORS + ",@"
                + KEY_ARTICLE_ABSTRACT + ",@" + KEY_ARTICLE_JOURNAL_ID + ",@" + KEY_ARTICLE_VOLUME + ",@" + KEY_ARTICLE_ISSUE + ",@"
                + KEY_ARTICLE_YEAR + ",@" + KEY_ARTICLE_PAGES + ",@" + KEY_ARTICLE_ARXIVID + ",@" + KEY_ARTICLE_DOI + ",@" + KEY_ARTICLE_PMID + ",@"
                + KEY_ARTICLE_CREATED_AT + ",@" + KEY_ARTICLE_FOLDER +",@"+KEY_ARTICLE_FILEPATH+ ",@"+KEY_IS_NEW+")";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            //Меняем global_id на local_id
            article.setFolder(getFolderLocalIdByGlobal(article.getFolder()));

            

            command.Parameters.AddWithValue(KEY_ARTICLE_TITLE, article.getTitle());
            command.Parameters.AddWithValue(KEY_ARTICLE_AUTHORS, article.getAuthors());
            command.Parameters.AddWithValue(KEY_ARTICLE_ABSTRACT, article.getAbstractField());
            command.Parameters.AddWithValue(KEY_ARTICLE_JOURNAL_ID, article.getJournal());

            if (article.getVolume() == -1)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_VOLUME, -1);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_VOLUME, article.getVolume());
            }

            if (article.getIssue() == -1)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_ISSUE, -1);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_ISSUE, article.getIssue());
            }

            if (article.getYear() == -1)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_YEAR, -1);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_YEAR, article.getYear());
            }


            if (article.getPages() == -1)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_PAGES, -1);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_PAGES, article.getPages());
            }

            if (article.getArXivID() == -1)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_ARXIVID, -1);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_ARXIVID, article.getArXivID());
            }

            if (article.getDOI() == -1)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_DOI, -1);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_DOI, article.getDOI());
            }

            if (article.getPMID() == -1)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_PMID, -1);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_PMID, article.getPMID());
            }

            command.Parameters.AddWithValue(KEY_ARTICLE_CREATED_AT, article.getCreated_at());
            command.Parameters.AddWithValue(KEY_ARTICLE_FOLDER, article.getFolder());

            if (article.getFilepath() == null)
            {
                command.Parameters.AddWithValue(KEY_ARTICLE_FILEPATH, DBNull.Value);
            }
            else {
                command.Parameters.AddWithValue(KEY_ARTICLE_FILEPATH,article.getFilepath());
            }

            command.Parameters.AddWithValue(KEY_IS_NEW,"1");

            //TODO: Как вернуть вставленный(сгенерированный) local_id, для новой записи
            int newArticleId = Convert.ToInt32(command.ExecuteScalar());

            return newArticleId;
        }

        public void editArticle(int local_id, string updateField, object value) {


            string updateDate = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            string updateQuery = "UPDATE " + TABLE_ARTICLES + " SET ["+updateField+"]=@"+updateField+",["+KEY_ARTICLE_UPDATED_AT+"]=@"+KEY_ARTICLE_UPDATED_AT+",["+KEY_IS_CHANGE+"]=1"+" WHERE ["+KEY_LOCAL_ID+"]="+local_id;

            SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

            command.Parameters.AddWithValue(updateField,value);
            command.Parameters.AddWithValue(KEY_ARTICLE_UPDATED_AT,updateDate);

            command.ExecuteNonQuery();
        }

        public void changeFolderTitle(int local_id, String folderTitle) {

            String query = "UPDATE " + TABLE_FOLDERS + " SET [" + KEY_FOLDER_TITLE + "]=@" + KEY_FOLDER_TITLE +", "+KEY_IS_CHANGE + "=1 "+" WHERE [" + KEY_LOCAL_ID + "]=" + local_id;

            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue(KEY_FOLDER_TITLE, folderTitle);

            command.ExecuteNonQuery();

        }

        public void deleteParentFolder(int child_local_id) {

            String query = "SELECT " + KEY_FOLDER_PARENT_ID + " FROM " + TABLE_FOLDERS + " WHERE [" + KEY_LOCAL_ID + "]=" + child_local_id; ;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(query, sqlConnection);

            try {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read()) {
                    deleteFolder(Convert.ToInt32(sqlReader[KEY_FOLDER_PARENT_ID]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

        }

        public void deleteFolder(int local_id) {
            //TODO: Не показывать папки с is_delete == 1
            if (checkHaveSubfolders(local_id))
            {
                deleteSubfolders(local_id);
                deleteFolderQuery(local_id);
            }
            else {
                moveArticlesToHomeDir(local_id);
                deleteFolderQuery(local_id);
                //MessageBox.Show("Папка удалена(без подпапок)");
            }
        }

        public void deleteGlobalFolder(int local_id) {
            if (checkHaveSubfolders(local_id))
            {
                deleteGlobalSubfolders(local_id);
            }
            else {
                moveArticlesToHomeDir(local_id);
                deleteGlobalFolderQuery(local_id);
                //MessageBox.Show("Папка удалена");
            }
        }

        private void deleteGlobalSubfolders(int parent_local_id) {

            String query = "SELECT " + KEY_LOCAL_ID + " FROM " + TABLE_FOLDERS + " WHERE [" + KEY_FOLDER_PARENT_ID + "]=" + parent_local_id;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(query, sqlConnection);

            try {
                sqlReader = command.ExecuteReader();

                int rowCounter = 0;

                while (sqlReader.Read()) {
                    int subfolderLocalId = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                    rowCounter++;
                    //Есть подпапки
                    if (checkHaveSubfolders(subfolderLocalId))
                    {
                        deleteGlobalSubfolders(subfolderLocalId);
                        deleteGlobalFolderQuery(subfolderLocalId);
                    }//Нет подпапок
                    else {
                        moveArticlesToHomeDir(subfolderLocalId);
                        deleteGlobalFolderQuery(subfolderLocalId);
                        MessageBox.Show("Папка удалена");
                    }
                }
                if (rowCounter == 0) {
                    moveArticlesToHomeDir(parent_local_id);
                    deleteGlobalFolderQuery(parent_local_id);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private void deleteSubfolders(int parent_local_id){

            string query = "SELECT " + KEY_LOCAL_ID + " FROM " + TABLE_FOLDERS + " WHERE [" + KEY_FOLDER_PARENT_ID + "]=" + parent_local_id;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(query, sqlConnection);

            try {
                sqlReader = command.ExecuteReader();

                if (sqlReader.HasRows)
                {

                    //MessageBox.Show("DeleteSubfolders: folderId: "+parent_local_id+" have subfolders");

                    while (sqlReader.Read())
                    {
                        int subfolderLocalId = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                        if (checkHaveSubfolders(subfolderLocalId))
                        {
                            //MessageBox.Show("Есть подпапки(удаляем их, затем родителя)");
                            deleteSubfolders(subfolderLocalId);
                            //MessageBox.Show("Папка удалена(после удаления подпапок)");
                            deleteFolderQuery(subfolderLocalId);
                            
                        }
                        else
                        {
                            moveArticlesToHomeDir(subfolderLocalId);
                            deleteFolderQuery(subfolderLocalId);
                            //MessageBox.Show("Папка удалена");
                        }
                    }
                }
                else {
                    moveArticlesToHomeDir(parent_local_id);
                    deleteFolderQuery(parent_local_id);
                    //MessageBox.Show("Папка удалена(не оказалось подпапок)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DeleteSubfolders: "+ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void moveArticlesToHomeDir(int local_id) {

            string query = "UPDATE "+TABLE_ARTICLES+" SET ["+KEY_ARTICLE_FOLDER+"]=0 WHERE ["+KEY_LOCAL_ID+"]="+local_id;

            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.ExecuteNonQuery();

        }

        private void deleteFolderQuery(int local_id) {

            if (checkFolderIsNew(local_id))
            {
                string deleteQuery = "DELETE FROM " + TABLE_FOLDERS + " WHERE [" + KEY_LOCAL_ID + "]=" + local_id;

                SqlCommand command = new SqlCommand(deleteQuery, sqlConnection);

                command.ExecuteNonQuery();
            }
            else {

                string updateQuery = "UPDATE "+TABLE_FOLDERS+" SET ["+KEY_IS_DELETE+"]=1 WHERE ["+KEY_LOCAL_ID+"]="+local_id;

                SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

                command.ExecuteNonQuery();

            }

        }

        private void deleteGlobalFolderQuery(int local_id) {

            string deleteQuery = "DELETE FROM "+TABLE_FOLDERS+" WHERE ["+KEY_LOCAL_ID+"]="+local_id;

            SqlCommand command = new SqlCommand(deleteQuery, sqlConnection);

            command.ExecuteNonQuery();

        }

        private Boolean checkHaveSubfolders(int local_id) {

            string selectQuery = "SELECT "+KEY_LOCAL_ID+" FROM " + TABLE_FOLDERS + " WHERE [" + KEY_FOLDER_PARENT_ID + "]=" + local_id;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            try {
                sqlReader = command.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    //MessageBox.Show("Have subfolders");
                    return true;
                }
                else {
                    //MessageBox.Show("Doesn't have subfolders");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
            return false;
        }

        private Boolean checkFolderIsNew(int local_id) {

            string selectQuery = "SELECT " + KEY_IS_NEW + " FROM " + TABLE_FOLDERS + " WHERE " + KEY_LOCAL_ID + "=" + local_id;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            try
            {

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {

                    if (Convert.ToInt32(sqlReader[KEY_IS_NEW]) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                if (sqlReader!=null) {
                    sqlReader.Close();
                }
            }

            return false;

        }

        public void disableIsNewFolder() {

            string updateQuery = "UPDATE "+TABLE_FOLDERS+" SET "+KEY_IS_NEW+"=0 WHERE ["+KEY_IS_NEW+"]=1";

            SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

            command.ExecuteNonQuery();

        }

        public void disableIsRenameFolder() {

            string updateQuery = "UPDATE "+TABLE_FOLDERS+" SET "+KEY_IS_CHANGE+"=0 WHERE "+KEY_IS_CHANGE+"=1";

            SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

            command.ExecuteNonQuery();

        }

        //TODO: JSON checkCreateRenameFolder(JSONObject globalFolderData)
        public int checkCreateRenameFolder(JObject globalFolderData) {

            int global_id = Convert.ToInt32(globalFolderData.GetValue("id"));
            string is_rename = Convert.ToString(globalFolderData.GetValue("is_rename"));
            string folderName = Convert.ToString(globalFolderData.GetValue("name"));
            int globalParentId = Convert.ToInt32(globalFolderData.GetValue("parent_id"));

            string selectQuery = "SELECT " + KEY_LOCAL_ID + "," + KEY_FOLDER_TITLE + " FROM " + TABLE_FOLDERS + " WHERE [" + KEY_GLOBAL_ID + "]=" + global_id;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            try {
                sqlReader = command.ExecuteReader();

                if (sqlReader.HasRows) {
                    while (sqlReader.Read())
                    {

                        if (is_rename.Equals("server") || is_rename.Equals("android"))
                        {
                            int local_id = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                            changeFolderTitle(local_id, folderName);
                            return EndPoints.GLOBAL_FOLDER_RENAMED;
                        }
                        else
                        {
                            return EndPoints.GLOBAL_FOLDER_EXIST;
                        }
                    }
                }
                else {
                    addGlobalFolder(global_id, folderName, globalParentId);
                    return EndPoints.GLOBAL_FOLDER_NEW;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
            return -1;
        }

        private void addGlobalFolder(int global_id, string folderName, int globalParentId) {

            string insertQuery = "INSERT INTO "+TABLE_FOLDERS+"("+KEY_GLOBAL_ID+","+KEY_FOLDER_TITLE
                +","+KEY_FOLDER_PARENT_ID+ ") VALUES (@" + KEY_GLOBAL_ID + ",@" + KEY_FOLDER_TITLE
                + ",@" + KEY_FOLDER_PARENT_ID + ")";

            int newParentId = getFolderLocalIdByGlobal(globalParentId);

            SqlCommand command = new SqlCommand(insertQuery, sqlConnection);

            command.Parameters.AddWithValue(KEY_GLOBAL_ID,global_id);
            command.Parameters.AddWithValue(KEY_FOLDER_TITLE,folderName);
            command.Parameters.AddWithValue(KEY_FOLDER_PARENT_ID,newParentId);

            command.ExecuteNonQuery();
        }

        private int getFolderGlobalIdByLocal(int local_id) {
            if (local_id == 0) {
                return 0;
            }

            string selectQuery = "SELECT " + KEY_GLOBAL_ID + " FROM " + TABLE_FOLDERS + " WHERE " + KEY_LOCAL_ID + "=" + local_id;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    return Convert.ToInt32(sqlReader[KEY_GLOBAL_ID]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //TODO: Проверить доходит ли программа до сюда и закрывает подключение
            finally {
                if (sqlReader != null) {
                    sqlReader.Close();
                }
            }

            return -1;
        }

        private int getFolderLocalIdByGlobal(int global_id)
        {
            if (global_id == 0)
            {
                return 0;
            }

            SqlDataReader sqlReader = null;

            string selectQuery = "SELECT " + KEY_LOCAL_ID + " FROM " + TABLE_FOLDERS + " WHERE [" + KEY_GLOBAL_ID + "]=" + global_id;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    return Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }

            }

            return -1;
        }

        //TODO: addGlobalArticle(JSONObject articleObj)
        public void addGlobalArticle(JObject newArticleObj) {
            String query = "INSERT INTO " + TABLE_ARTICLES + "("+KEY_GLOBAL_ID+"," + KEY_ARTICLE_TITLE + "," + KEY_ARTICLE_AUTHORS + ","
            + KEY_ARTICLE_ABSTRACT + "," + KEY_ARTICLE_JOURNAL_ID + "," + KEY_ARTICLE_VOLUME + "," + KEY_ARTICLE_ISSUE + ","
            + KEY_ARTICLE_YEAR + "," + KEY_ARTICLE_PAGES + "," + KEY_ARTICLE_ARXIVID + "," + KEY_ARTICLE_DOI + "," + KEY_ARTICLE_PMID + ","
            + KEY_ARTICLE_CREATED_AT + "," +KEY_ARTICLE_UPDATED_AT+ "," + KEY_ARTICLE_FOLDER + "," + KEY_ARTICLE_FAVORITE +")  VALUES(@"
            + KEY_GLOBAL_ID+",@"+ KEY_ARTICLE_TITLE + ",@" + KEY_ARTICLE_AUTHORS + ",@"
            + KEY_ARTICLE_ABSTRACT + ",@" + KEY_ARTICLE_JOURNAL_ID + ",@" + KEY_ARTICLE_VOLUME + ",@" + KEY_ARTICLE_ISSUE + ",@"
            + KEY_ARTICLE_YEAR + ",@" + KEY_ARTICLE_PAGES + ",@" + KEY_ARTICLE_ARXIVID + ",@" + KEY_ARTICLE_DOI + ",@" + KEY_ARTICLE_PMID + ",@"
            + KEY_ARTICLE_CREATED_AT + ",@" + KEY_ARTICLE_UPDATED_AT + ",@" + KEY_ARTICLE_FOLDER + ",@" + KEY_ARTICLE_FAVORITE +")";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            int global_id = Convert.ToInt32(newArticleObj.GetValue("id"));
            string title = Convert.ToString(newArticleObj.GetValue("title"));
            string authors = Convert.ToString(newArticleObj.GetValue("authors"));
            string abstractField = Convert.ToString(newArticleObj.GetValue("abstract"));
            string journal_id = Convert.ToString(newArticleObj.GetValue("journal_id"));

            int volume = -1;

            if (Convert.ToString(newArticleObj.GetValue("volume")) != "")
            {
                volume = Convert.ToInt32(newArticleObj.GetValue("volume"));
            }

            int issue = -1;
            if (Convert.ToString(newArticleObj.GetValue("issue")) != "") {
                issue = Convert.ToInt32(newArticleObj.GetValue("issue"));
            }

            int year = -1;
            if (Convert.ToString(newArticleObj.GetValue("year")) != "") {
                year = Convert.ToInt32(newArticleObj.GetValue("year"));
            }

            int pages = -1;
            if (Convert.ToString(newArticleObj.GetValue("pages")) != "") {
                pages = Convert.ToInt32(newArticleObj.GetValue("pages"));
            }


            int ArXivID = -1;
            if (Convert.ToString(newArticleObj.GetValue("ArXivID")) != "") {
                ArXivID = Convert.ToInt32(newArticleObj.GetValue("ArXivID"));
            }

            int DOI = -1;
            if (Convert.ToString(newArticleObj.GetValue("DOI")) != "") {
                DOI = Convert.ToInt32(newArticleObj.GetValue("DOI"));
            }


            int PMID = -1;
            if (Convert.ToString(newArticleObj.GetValue("PMID")) != "") {
                PMID = Convert.ToInt32(newArticleObj.GetValue("PMID"));
            }
            int favorite = Convert.ToInt32(newArticleObj.GetValue("favorite"));
            int folder = Convert.ToInt32(newArticleObj.GetValue("folder"));
            string created_at = Convert.ToString(newArticleObj.GetValue("created_at"));
            string updated_at = Convert.ToString(newArticleObj.GetValue("updated_at"));

            if (abstractField == null)
            {
                abstractField = "";
            }

            folder = getFolderLocalIdByGlobal(folder);

            command.Parameters.AddWithValue(KEY_GLOBAL_ID, global_id);
            command.Parameters.AddWithValue(KEY_ARTICLE_TITLE, title);
            command.Parameters.AddWithValue(KEY_ARTICLE_AUTHORS, authors);
            command.Parameters.AddWithValue(KEY_ARTICLE_ABSTRACT, abstractField);
            command.Parameters.AddWithValue(KEY_ARTICLE_JOURNAL_ID, journal_id);
            command.Parameters.AddWithValue(KEY_ARTICLE_VOLUME, volume);
            command.Parameters.AddWithValue(KEY_ARTICLE_ISSUE, issue);
            command.Parameters.AddWithValue(KEY_ARTICLE_YEAR, year);
            command.Parameters.AddWithValue(KEY_ARTICLE_PAGES, pages);
            command.Parameters.AddWithValue(KEY_ARTICLE_ARXIVID, ArXivID);
            command.Parameters.AddWithValue(KEY_ARTICLE_DOI, DOI);
            command.Parameters.AddWithValue(KEY_ARTICLE_PMID, PMID);
            command.Parameters.AddWithValue(KEY_ARTICLE_CREATED_AT, created_at);
            command.Parameters.AddWithValue(KEY_ARTICLE_UPDATED_AT, updated_at);
            command.Parameters.AddWithValue(KEY_ARTICLE_FAVORITE, favorite);
            command.Parameters.AddWithValue(KEY_ARTICLE_FOLDER, folder);







            command.ExecuteNonQuery();
        }

        public void updateLocalArticleByServer(JObject needToSyncObj) {

            string updateQuery = "UPDATE " + TABLE_ARTICLES + " SET [" + KEY_ARTICLE_TITLE + "] = @" + KEY_ARTICLE_TITLE + ", "
                + "[" + KEY_ARTICLE_AUTHORS + "] = @" + KEY_ARTICLE_AUTHORS + ", " + "[" + KEY_ARTICLE_ABSTRACT + "] = @" + KEY_ARTICLE_ABSTRACT + ", "
                + "[" + KEY_ARTICLE_JOURNAL_ID + "] = @" + KEY_ARTICLE_JOURNAL_ID + ", " + "[" + KEY_ARTICLE_YEAR + "] = @" + KEY_ARTICLE_YEAR + ", "
                + "[" + KEY_ARTICLE_VOLUME + "] = @" + KEY_ARTICLE_VOLUME + ", " + "[" + KEY_ARTICLE_ISSUE + "] = @" + KEY_ARTICLE_ISSUE + ", "
                + "[" + KEY_ARTICLE_PAGES + "] = @" + KEY_ARTICLE_PAGES + ", " + "[" + KEY_ARTICLE_ARXIVID + "] = @" + KEY_ARTICLE_ARXIVID + ", "
                + "[" + KEY_ARTICLE_DOI + "] = @" + KEY_ARTICLE_DOI + ", " + "[" + KEY_ARTICLE_PMID + "] = @" + KEY_ARTICLE_PMID + ", "
                + "[" + KEY_ARTICLE_FOLDER + "] = @" + KEY_ARTICLE_FOLDER + ", [" + KEY_ARTICLE_FAVORITE + "] = @" + KEY_ARTICLE_FAVORITE + ",[" + KEY_ARTICLE_UPDATED_AT + "] = @" + KEY_ARTICLE_UPDATED_AT
                + " WHERE [" + KEY_GLOBAL_ID + "]=@" + KEY_GLOBAL_ID;

            SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

            int global_id = Convert.ToInt32(needToSyncObj.GetValue("id"));
            string title = Convert.ToString(needToSyncObj.GetValue("title"));
            string authors = Convert.ToString(needToSyncObj.GetValue("authors"));
            string abstractField = Convert.ToString(needToSyncObj.GetValue("abstract"));
            string journal_id = Convert.ToString(needToSyncObj.GetValue("journal_id"));

            int volume = -1;

            if (Convert.ToString(needToSyncObj.GetValue("volume")) != "")
            {
                volume = Convert.ToInt32(needToSyncObj.GetValue("volume"));
            }

            int issue = -1;
            if (Convert.ToString(needToSyncObj.GetValue("issue")) != "")
            {
                issue = Convert.ToInt32(needToSyncObj.GetValue("issue"));
            }

            int year = -1;
            if (Convert.ToString(needToSyncObj.GetValue("year")) != "")
            {
                year = Convert.ToInt32(needToSyncObj.GetValue("year"));
            }

            int pages = -1;
            if (Convert.ToString(needToSyncObj.GetValue("pages")) != "")
            {
                pages = Convert.ToInt32(needToSyncObj.GetValue("pages"));
            }


            int ArXivID = -1;
            if (Convert.ToString(needToSyncObj.GetValue("ArXivID")) != "")
            {
                ArXivID = Convert.ToInt32(needToSyncObj.GetValue("ArXivID"));
            }

            int DOI = -1;
            if (Convert.ToString(needToSyncObj.GetValue("DOI")) != "")
            {
                DOI = Convert.ToInt32(needToSyncObj.GetValue("DOI"));
            }


            int PMID = -1;
            if (Convert.ToString(needToSyncObj.GetValue("PMID")) != "")
            {
                PMID = Convert.ToInt32(needToSyncObj.GetValue("PMID"));
            }
            int favorite = Convert.ToInt32(needToSyncObj.GetValue("favorite"));
            int folder = Convert.ToInt32(needToSyncObj.GetValue("folder"));
            string updated_at = Convert.ToString(needToSyncObj.GetValue("updated_at"));

            if (abstractField == null)
            {
                abstractField = "";
            }

            folder = getFolderLocalIdByGlobal(folder);

            command.Parameters.AddWithValue(KEY_GLOBAL_ID, global_id);
            command.Parameters.AddWithValue(KEY_ARTICLE_TITLE, title);
            command.Parameters.AddWithValue(KEY_ARTICLE_AUTHORS, authors);
            command.Parameters.AddWithValue(KEY_ARTICLE_ABSTRACT, abstractField);
            command.Parameters.AddWithValue(KEY_ARTICLE_JOURNAL_ID, journal_id);
            command.Parameters.AddWithValue(KEY_ARTICLE_VOLUME, volume);
            command.Parameters.AddWithValue(KEY_ARTICLE_ISSUE, issue);
            command.Parameters.AddWithValue(KEY_ARTICLE_YEAR, year);
            command.Parameters.AddWithValue(KEY_ARTICLE_PAGES, pages);
            command.Parameters.AddWithValue(KEY_ARTICLE_ARXIVID, ArXivID);
            command.Parameters.AddWithValue(KEY_ARTICLE_DOI, DOI);
            command.Parameters.AddWithValue(KEY_ARTICLE_PMID, PMID);
            command.Parameters.AddWithValue(KEY_ARTICLE_UPDATED_AT, updated_at);
            command.Parameters.AddWithValue(KEY_ARTICLE_FAVORITE, favorite);
            command.Parameters.AddWithValue(KEY_ARTICLE_FOLDER, folder);

            command.ExecuteNonQuery();
        }

        public void deleteLocalArticle(int local_id) {

            if (isArticleNew(local_id))
            {

                string deleteQuery = "DELETE FROM " + TABLE_FOLDERS + " WHERE [" + KEY_LOCAL_ID + "]=" + local_id;

                SqlCommand command = new SqlCommand(deleteQuery, sqlConnection);

                command.ExecuteNonQuery();

            }
            else {

                string updateQuery = "UPDATE "+TABLE_ARTICLES+" SET ["+KEY_IS_DELETE+"]=1 WHERE ["+KEY_LOCAL_ID+"]="+local_id;

                SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

                command.ExecuteNonQuery();

            }

        }

        public void deleteGlobalArticle(int global_id) {

            string deleteQuery = "DELETE FROM "+TABLE_ARTICLES+" WHERE ["+KEY_GLOBAL_ID+"]="+global_id;

            SqlCommand command = new SqlCommand(deleteQuery, sqlConnection);

            command.ExecuteNonQuery();

        }

        public Boolean isArticleNew(int local_id) {

            string selectQuery = "SELECT " + KEY_IS_NEW + " FROM " + TABLE_ARTICLES + " WHERE " + KEY_LOCAL_ID + "=" + local_id;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            try {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read()) {
                    if (Convert.ToInt32(sqlReader[KEY_IS_NEW]) == 1)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

            return false;

        }

        public void setGlobalIdToArticle(int local_id, int global_id) {

            string updateQuery = "UPDATE "+TABLE_ARTICLES+" SET ["+KEY_GLOBAL_ID+"]="+global_id + ", ["+KEY_IS_NEW + "]=0 WHERE "+KEY_LOCAL_ID+"="+local_id;

            SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

            command.ExecuteNonQuery();

        }

        public List<JournalArticle> getRootFolderArticles() {

            List<JournalArticle> articlesList = new List<JournalArticle>();

            string selectQuery = "SELECT " + KEY_LOCAL_ID + "," + KEY_ARTICLE_TITLE + "," + KEY_ARTICLE_AUTHORS + "," + KEY_ARTICLE_JOURNAL_ID
                + "," + KEY_ARTICLE_CREATED_AT + "," + KEY_ARTICLE_FAVORITE + "," + KEY_ARTICLE_FILEPATH + "," + KEY_ARTICLE_VOLUME + ","
                + KEY_ARTICLE_ISSUE + "," + KEY_ARTICLE_PAGES + "," + KEY_ARTICLE_YEAR + "," + KEY_ARTICLE_ARXIVID + ","
                + KEY_ARTICLE_DOI + "," + KEY_ARTICLE_PMID + "," + KEY_ARTICLE_ABSTRACT + " FROM " + TABLE_ARTICLES + " WHERE " + KEY_ARTICLE_FOLDER + "=" + 0;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            SqlDataReader sqlReader = null;

            try {

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read()) {
                    
                    int local_id = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                    string title = Convert.ToString(sqlReader[KEY_ARTICLE_TITLE]);
                    string authors = Convert.ToString(sqlReader[KEY_ARTICLE_AUTHORS]);
                    string journal = Convert.ToString(sqlReader[KEY_ARTICLE_JOURNAL_ID]);
                    string created_at = Convert.ToString(sqlReader[KEY_ARTICLE_CREATED_AT]);
                    int favorite = Convert.ToInt32(sqlReader[KEY_ARTICLE_FAVORITE]);
                    string filepath = Convert.ToString(sqlReader[KEY_ARTICLE_FILEPATH]);

                    int volume = Convert.ToInt32(sqlReader[KEY_ARTICLE_VOLUME]);
                    int issue = Convert.ToInt32(sqlReader[KEY_ARTICLE_ISSUE]);
                    int pages = Convert.ToInt32(sqlReader[KEY_ARTICLE_PAGES]);
                    int year = Convert.ToInt32(sqlReader[KEY_ARTICLE_YEAR]);
                    int ArXivID = Convert.ToInt32(sqlReader[KEY_ARTICLE_ARXIVID]);
                    int DOI = Convert.ToInt32(sqlReader[KEY_ARTICLE_DOI]);
                    int PMID = Convert.ToInt32(sqlReader[KEY_ARTICLE_PMID]);
                    string abstractField = Convert.ToString(sqlReader[KEY_ARTICLE_ABSTRACT]);

                    JournalArticle article = new JournalArticle(local_id, title, authors, abstractField, journal, volume, issue, pages, year, ArXivID, DOI, PMID, created_at, favorite, filepath);
                    articlesList.Add(article);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

            return articlesList;

        }

        public List<JournalArticle> getArticlesInFolder(int folder_local_id) {

            List<JournalArticle> articlesList = new List<JournalArticle>();

            String selectQuery = "SELECT " + KEY_LOCAL_ID + "," + KEY_ARTICLE_TITLE + "," + KEY_ARTICLE_AUTHORS + "," + KEY_ARTICLE_JOURNAL_ID
                + "," + KEY_ARTICLE_CREATED_AT + "," + KEY_ARTICLE_FAVORITE + "," + KEY_ARTICLE_FILEPATH + "," + KEY_ARTICLE_VOLUME + ","
                + KEY_ARTICLE_ISSUE + "," + KEY_ARTICLE_PAGES + "," + KEY_ARTICLE_YEAR + "," + KEY_ARTICLE_ARXIVID + ","
                + KEY_ARTICLE_DOI + "," + KEY_ARTICLE_PMID + "," + KEY_ARTICLE_ABSTRACT + " FROM " + TABLE_ARTICLES + " WHERE " + KEY_ARTICLE_FOLDER + "=" + folder_local_id;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            SqlDataReader sqlReader = null;

            try
            {

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {

                    int local_id = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                    string title = Convert.ToString(sqlReader[KEY_ARTICLE_TITLE]);
                    string authors = Convert.ToString(sqlReader[KEY_ARTICLE_AUTHORS]);
                    string journal = Convert.ToString(sqlReader[KEY_ARTICLE_JOURNAL_ID]);
                    string created_at = Convert.ToString(sqlReader[KEY_ARTICLE_CREATED_AT]);
                    int favorite = Convert.ToInt32(sqlReader[KEY_ARTICLE_FAVORITE]);
                    string filepath = Convert.ToString(sqlReader[KEY_ARTICLE_FILEPATH]);

                    int volume = Convert.ToInt32(sqlReader[KEY_ARTICLE_VOLUME]);
                    int issue = Convert.ToInt32(sqlReader[KEY_ARTICLE_ISSUE]);
                    int pages = Convert.ToInt32(sqlReader[KEY_ARTICLE_PAGES]);
                    int year = Convert.ToInt32(sqlReader[KEY_ARTICLE_YEAR]);
                    int ArXivID = Convert.ToInt32(sqlReader[KEY_ARTICLE_ARXIVID]);
                    int DOI = Convert.ToInt32(sqlReader[KEY_ARTICLE_DOI]);
                    int PMID = Convert.ToInt32(sqlReader[KEY_ARTICLE_PMID]);
                    string abstractField = Convert.ToString(sqlReader[KEY_ARTICLE_ABSTRACT]);

                    JournalArticle article = new JournalArticle(local_id, title, authors, abstractField, journal, volume, issue, pages, year, ArXivID, DOI, PMID, created_at, favorite, filepath);
                    articlesList.Add(article);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

            return articlesList;

        }

        public List<JournalArticle> getAllArticles() {

            List<JournalArticle> articlesList = new List<JournalArticle>();

            string selectQuery = "SELECT " + KEY_LOCAL_ID + "," + KEY_ARTICLE_TITLE + "," + KEY_ARTICLE_AUTHORS + "," + KEY_ARTICLE_JOURNAL_ID
    + "," + KEY_ARTICLE_CREATED_AT + "," + KEY_ARTICLE_FAVORITE + "," + KEY_ARTICLE_FILEPATH + " FROM " + TABLE_ARTICLES;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            SqlDataReader sqlReader = null;

            try
            {

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {

                    int local_id = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                    string title = Convert.ToString(sqlReader[KEY_ARTICLE_TITLE]);
                    string authors = Convert.ToString(sqlReader[KEY_ARTICLE_AUTHORS]);
                    string journal = Convert.ToString(sqlReader[KEY_ARTICLE_JOURNAL_ID]);
                    string created_at = Convert.ToString(sqlReader[KEY_ARTICLE_CREATED_AT]);
                    int favorite = Convert.ToInt32(sqlReader[KEY_ARTICLE_FAVORITE]);
                    string filepath = Convert.ToString(sqlReader[KEY_ARTICLE_FILEPATH]);

                    JournalArticle article = new JournalArticle(local_id, title, authors, journal, created_at, favorite, filepath);
                    articlesList.Add(article);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

            return articlesList;

        }

        public void setFavorite(int local_id, int favorite) {
            string updateQuery = "UPDATE "+TABLE_ARTICLES+" SET ["+KEY_ARTICLE_FAVORITE+"]="+favorite+","+"["+KEY_ARTICLE_UPDATED_AT+"]=@"+KEY_ARTICLE_UPDATED_AT+" WHERE ["+KEY_LOCAL_ID+"]="+local_id;

            SqlCommand command = new SqlCommand(updateQuery, sqlConnection);

            string updateDate = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            command.Parameters.AddWithValue(KEY_ARTICLE_UPDATED_AT, updateDate);


            command.ExecuteNonQuery();

        }

        public List<JournalArticle> getFavoriteArticles()
        {

            List<JournalArticle> articlesList = new List<JournalArticle>();

            string selectQuery = "SELECT " + KEY_LOCAL_ID + "," + KEY_ARTICLE_TITLE + "," + KEY_ARTICLE_AUTHORS + "," + KEY_ARTICLE_JOURNAL_ID
    + "," + KEY_ARTICLE_CREATED_AT + "," + KEY_ARTICLE_FAVORITE + "," + KEY_ARTICLE_FILEPATH + " FROM " + TABLE_ARTICLES+" WHERE "+KEY_ARTICLE_FAVORITE+"=1";

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            SqlDataReader sqlReader = null;

            try
            {

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {

                    int local_id = Convert.ToInt32(sqlReader[KEY_LOCAL_ID]);
                    string title = Convert.ToString(sqlReader[KEY_ARTICLE_TITLE]);
                    string authors = Convert.ToString(sqlReader[KEY_ARTICLE_AUTHORS]);
                    string journal = Convert.ToString(sqlReader[KEY_ARTICLE_JOURNAL_ID]);
                    string created_at = Convert.ToString(sqlReader[KEY_ARTICLE_CREATED_AT]);
                    int favorite = Convert.ToInt32(sqlReader[KEY_ARTICLE_FAVORITE]);
                    string filepath = Convert.ToString(sqlReader[KEY_ARTICLE_FILEPATH]);

                    JournalArticle article = new JournalArticle(local_id, title, authors, journal, created_at, favorite, filepath);
                    articlesList.Add(article);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

            return articlesList;

        }

        public void setFirstToken(string token) {

            string insertQuery = "INSERT INTO Token (token) VALUES(@Token)";

            SqlCommand command = new SqlCommand(insertQuery, sqlConnection);

            command.Parameters.AddWithValue("Token",token);

            command.ExecuteNonQuery();

        }

        public int getTokenCount() {
            int tokenCount = 0;

            string selectQuery = "SELECT COUNT(*) FROM Token";

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            tokenCount = Convert.ToInt32(command.ExecuteScalar());

            return tokenCount;
        }

        //TODO: Check
        public int getArticlesCount() {

            int articlesCount = 0;

            string selectQuery = "SELECT COUNT(*) FROM "+TABLE_ARTICLES;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            articlesCount = Convert.ToInt32(command.ExecuteScalar());

            return articlesCount;

        }

        public string composeJSONFromArticles() {

            List<SortedDictionary<String, String>> articlesList;
            articlesList = new List<SortedDictionary<string, string>>();

            string selectQuery = "SELECT * FROM "+TABLE_ARTICLES;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read()) {
                    SortedDictionary<String, String> dict = new SortedDictionary<string, string>();

                    int is_new = Convert.ToInt32(sqlReader[KEY_IS_NEW]);
                    if (is_new == 1)
                    {
                        string local_id = Convert.ToString(sqlReader[KEY_LOCAL_ID]);
                        string global_folder_parent_id = Convert.ToString(getFolderGlobalIdByLocal(Convert.ToInt32(sqlReader[KEY_ARTICLE_FOLDER])));

                        dict.Add(KEY_LOCAL_ID, local_id);
                        dict.Add(KEY_ARTICLE_TITLE, Convert.ToString(sqlReader[KEY_ARTICLE_TITLE]));
                        dict.Add(KEY_ARTICLE_AUTHORS, Convert.ToString(sqlReader[KEY_ARTICLE_AUTHORS]));
                        dict.Add(KEY_ARTICLE_ABSTRACT, Convert.ToString(sqlReader[KEY_ARTICLE_ABSTRACT]));
                        dict.Add(KEY_ARTICLE_JOURNAL_ID, Convert.ToString(sqlReader[KEY_ARTICLE_JOURNAL_ID]));
                        dict.Add(KEY_ARTICLE_YEAR, Convert.ToString(sqlReader[KEY_ARTICLE_YEAR]));
                        dict.Add(KEY_ARTICLE_VOLUME, Convert.ToString(sqlReader[KEY_ARTICLE_VOLUME]));
                        dict.Add(KEY_ARTICLE_ISSUE, Convert.ToString(sqlReader[KEY_ARTICLE_PAGES]));
                        dict.Add(KEY_ARTICLE_PAGES, Convert.ToString(sqlReader[KEY_ARTICLE_PAGES]));
                        dict.Add(KEY_ARTICLE_ARXIVID, Convert.ToString(sqlReader[KEY_ARTICLE_ARXIVID]));
                        dict.Add(KEY_ARTICLE_DOI, Convert.ToString(sqlReader[KEY_ARTICLE_DOI]));
                        dict.Add(KEY_ARTICLE_PMID, Convert.ToString(sqlReader[KEY_ARTICLE_PMID]));
                        dict.Add(KEY_ARTICLE_FOLDER, global_folder_parent_id);
                        dict.Add(KEY_ARTICLE_UPDATED_AT, Convert.ToString(sqlReader[KEY_ARTICLE_UPDATED_AT]));
                        dict.Add(KEY_ARTICLE_FAVORITE, Convert.ToString(sqlReader[KEY_ARTICLE_FAVORITE]));

                    }
                    else {

                        string global_folder_parent_id = Convert.ToString(getFolderGlobalIdByLocal(Convert.ToInt32(sqlReader[KEY_ARTICLE_FOLDER])));

                        dict.Add(KEY_ARTICLE_TITLE, Convert.ToString(sqlReader[KEY_ARTICLE_TITLE]));
                        dict.Add(KEY_ARTICLE_AUTHORS, Convert.ToString(sqlReader[KEY_ARTICLE_AUTHORS]));
                        dict.Add(KEY_ARTICLE_ABSTRACT, Convert.ToString(sqlReader[KEY_ARTICLE_ABSTRACT]));
                        dict.Add(KEY_ARTICLE_JOURNAL_ID, Convert.ToString(sqlReader[KEY_ARTICLE_JOURNAL_ID]));
                        dict.Add(KEY_ARTICLE_YEAR, Convert.ToString(sqlReader[KEY_ARTICLE_YEAR]));
                        dict.Add(KEY_ARTICLE_VOLUME, Convert.ToString(sqlReader[KEY_ARTICLE_VOLUME]));
                        dict.Add(KEY_ARTICLE_ISSUE, Convert.ToString(sqlReader[KEY_ARTICLE_PAGES]));
                        dict.Add(KEY_ARTICLE_PAGES, Convert.ToString(sqlReader[KEY_ARTICLE_PAGES]));
                        dict.Add(KEY_ARTICLE_ARXIVID, Convert.ToString(sqlReader[KEY_ARTICLE_ARXIVID]));
                        dict.Add(KEY_ARTICLE_DOI, Convert.ToString(sqlReader[KEY_ARTICLE_DOI]));
                        dict.Add(KEY_ARTICLE_PMID, Convert.ToString(sqlReader[KEY_ARTICLE_PMID]));
                        dict.Add(KEY_ARTICLE_FOLDER, global_folder_parent_id);
                        dict.Add(KEY_ARTICLE_UPDATED_AT, Convert.ToString(sqlReader[KEY_ARTICLE_UPDATED_AT]));
                        dict.Add(KEY_ARTICLE_FAVORITE, Convert.ToString(sqlReader[KEY_ARTICLE_FAVORITE]));
                        dict.Add(KEY_GLOBAL_ID, Convert.ToString(sqlReader[KEY_GLOBAL_ID]));
                        dict.Add(KEY_IS_DELETE, Convert.ToString(sqlReader[KEY_IS_DELETE]));

                    }

                    articlesList.Add(dict);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
            
            return JsonConvert.SerializeObject(articlesList);

        }

        public string composeJSONFromFolders() {

            List<SortedDictionary<String, String>> foldersList;
            foldersList = new List<SortedDictionary<string, string>>();
            string selectQuery = "SELECT * FROM " + TABLE_FOLDERS;

            SqlCommand command = new SqlCommand(selectQuery, sqlConnection);

            SqlDataReader sqlReader = null;

            try {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read()) {
                    SortedDictionary<String, String> dict = new SortedDictionary<string, string>();
                    int is_new = Convert.ToInt32(sqlReader[KEY_IS_NEW]);
                    int is_change = Convert.ToInt32(sqlReader[KEY_IS_CHANGE]);
                    int is_delete = Convert.ToInt32(sqlReader[KEY_IS_DELETE]);

                    string folderTitle = Convert.ToString(sqlReader[KEY_FOLDER_TITLE]);
                    string local_id = Convert.ToString(sqlReader[KEY_LOCAL_ID]);
                    string global_id = Convert.ToString(sqlReader[KEY_GLOBAL_ID]);
                    string folderParent = Convert.ToString(sqlReader[KEY_FOLDER_PARENT_ID]);

                    if (is_new == 1 || is_change == 1 || is_delete == 1)
                    {
                        dict.Add(KEY_IS_NEW, Convert.ToString(is_new));
                        dict.Add(KEY_IS_CHANGE, Convert.ToString(is_change));
                        dict.Add(KEY_IS_DELETE, Convert.ToString(is_delete));
                        dict.Add("name", folderTitle);
                        dict.Add(KEY_LOCAL_ID, local_id);
                        dict.Add(KEY_GLOBAL_ID, global_id);
                        dict.Add(KEY_FOLDER_PARENT_ID, folderParent);
                        foldersList.Add(dict);
                    }
                    else {
                        dict.Add(KEY_LOCAL_ID,local_id);
                        dict.Add(KEY_GLOBAL_ID,global_id);
                        dict.Add(KEY_FOLDER_PARENT_ID, folderParent);
                        foldersList.Add(dict);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

            return JsonConvert.SerializeObject(foldersList);
        }

    }
}