using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeley
{
    class EndPoints
    {

        #region REST_API
        public static string URL_AUTHENTICATE = "http://45.76.186.7/api/authenticate";
        public static string URL_REFRESH_TOKEN = "http://45.76.186.7/api/refresh-token";
        public static string URL_SYNC_FOLDERS = "http://45.76.186.7/api/android/sync/folders";
        public static string URL_SYNC_ARTICLES = "http://45.76.186.7/api/android/sync/articles";

        public static string URL_SEND_FOLDERS = "http://45.76.186.7/api/android/send/folders";
        public static string URL_SEND_ARTICLES = "http://45.76.186.7/api/android/send/articles";

        public static string URL_SEND_FILE = "http://45.76.186.7/api/windows/send/file";
        public static string URL_FOLDER_REQUEST_BACK = "http://45.76.186.7/api/android/folder/request";
        #endregion

        #region SIMPLE_LINKS
        public static string URL_REGISTER = "http://45.76.186.7/register";
        #endregion

        #region GLOBAL_VARIABLES
        public static int GLOBAL_FOLDER_EXIST = 0;
        public static int GLOBAL_FOLDER_NEW = 1;
        public static int GLOBAL_FOLDER_RENAMED = 2;
        #endregion

    }
}
