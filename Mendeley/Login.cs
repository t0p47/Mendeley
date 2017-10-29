using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net;
using System.Collections.Specialized;

namespace Mendeley
{
    public partial class Login : Form
    {
        private string uid;

        public Login()
        {
            InitializeComponent();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {

            IEnumerable<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("email",EmailText.Text.ToString()),
                new KeyValuePair<string, string>("password",PasswordText.Text.ToString())
            };
            RESTful.PostRequest("http://45.76.186.7/api/authenticate", postData);
            //MessageBox.Show(RESTful.PostRequest("http://localhost/mendeley/v1/user/login", postData).Result);


            /*if (EmailText.Text.ToString().Equals("offline")) {
                Library main = new Library();
                main.passUID = "1";
                main.ShowDialog();
                this.Hide();
            }
            authen();*/
            /*if (CheckUser(EmailText.Text, PasswordText.Text))
            {
                Library main = new Library();
                main.passUID = uid;
                main.ShowDialog();
                this.Hide();
            }
            else {
                MessageBox.Show("Вы ввели неверный логи или пароль");
            }*/

        }

        bool CheckUser(string email, string password) {

            MySqlConnectionStringBuilder mysqlSB = new MySqlConnectionStringBuilder();

            mysqlSB.Server = "141.8.194.25";

            mysqlSB.Database = "a0021435_mendeley";

            mysqlSB.UserID = "a0021435_user";

            mysqlSB.Password = "351942025500ghj";

            email = "\'" + email + "\'";

            string queryString = "SELECT password, uid FROM users WHERE email="+email;

            using (MySqlConnection con = new MySqlConnection()) {
                con.ConnectionString = mysqlSB.ConnectionString;

                MySqlCommand com = new MySqlCommand(queryString, con);

                try {
                    con.Open();

                    using (MySqlDataReader dr = com.ExecuteReader()) {
                        if (dr.HasRows) {
                            if (dr.Read()) {
                                if (dr.GetString("password") == password) {
                                    uid = dr.GetString("uid");
                                    MessageBox.Show(uid);
                                    return true;
                                }
                            }
                            
                            
                        }
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Exception: "+ex.Message);
                }
            }
            PasswordText.Clear();
            return false;
        }

        private void authen() {
            //MessageBox.Show(EmailText.Text);
            string URL = "http://a0021435.xsph.ru/Mendeley/visual/login.php";
            WebClient webClient = new WebClient();
            webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;

            NameValueCollection formData = new NameValueCollection();
            formData["email"] = EmailText.Text.ToString();
            formData["pass"] = PasswordText.Text.ToString();

            byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            if (!responsefromserver.Equals("no"))
            {
                //MessageBox.Show("UID " + responsefromserver);
                Library main = new Library();
                main.passUID = responsefromserver;
                main.ShowDialog();
                this.Hide();
            }
            else {
                MessageBox.Show("Вы ввели неверный логин или пароль.");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Online
            //System.Diagnostics.Process.Start("http://www.testingweb.ru/Mendeley/");
           /* System.Diagnostics.Process.Start("http://localhost/Mendeley");
            IEnumerable<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(),
            };
            RESTful.PostRequest*/
        }
    }
}
