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
using Mono.Web;
using System.Data.SqlClient;

namespace Mendeley
{
    public partial class Login : Form
    {
        private string uid;
        Functions func;
        SqlConnection sqlConnection;

        public Login()
        {
            InitializeComponent();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {

            NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            outgoingQueryString.Add("email", EmailText.Text.ToString());
            outgoingQueryString.Add("password", PasswordText.Text.ToString());
            string postData = outgoingQueryString.ToString();


            if (func.getTokenCount()==0) {
                //MessageBox.Show("Login first time");
                if (func.loginFirstTime(postData)) {
                    Library main = new Library();
                    main.haveInternet = true;
                    main.ShowDialog();
                    main.TopMost = true;
                    this.Close();
                }

            }
            if (func.login(postData))
            {
                //MessageBox.Show("Login second and other time");
                Library main = new Library();
                main.haveInternet = true;
                main.ShowDialog();
                main.TopMost = true;
                this.Close();
            }

            else
            {
                MessageBox.Show("Something went wrong!");
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Online
            System.Diagnostics.Process.Start(EndPoints.URL_REGISTER);
           /* System.Diagnostics.Process.Start("http://localhost/Mendeley");
            IEnumerable<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(),
            };
            RESTful.PostRequest*/
        }

        private void Login_Load(object sender, EventArgs e)
        {

            string connectionStringDynamic = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ScienceLib\ScienceLib.mdf;Integrated Security=True;MultipleActiveResultSets=True";

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Z\Programming\VisualC#\Mendeley\Mendeley\Mendeley.mdf;Integrated Security=True;MultipleActiveResultSets=True";

            string tmpConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\01Laptop\Desktop\ScienceLib.mdf;Integrated Security=True;MultipleActiveResultSets=True";

            //sqlConnection = new SqlConnection(tmpConnectionString);

            sqlConnection = new SqlConnection(connectionStringDynamic);

            sqlConnection.Open();

            Functions.sqlConnection = sqlConnection;

            func = new Functions();

            if (func.checkInternetConnection())
            {

                //MessageBox.Show(Convert.ToString(func.getTokenCount()));
                //MessageBox.Show("First run "+Properties.Settings.Default.FirstRun);
                //MessageBox.Show("We have internet connection");
                if (func.getTokenCount()!=0)
                {
                    if (func.isLogin() && !func.checkTokenIsExpired())
                    {
                        //MessageBox.Show("We already loggedIn");
                        Library main = new Library();
                        main.haveInternet = true;
                        main.ShowDialog();
                        main.TopMost = true;
                        this.Close();
                    }
                    else
                    {
                        //MessageBox.Show("Need to login.");
                    }
                }
                else {
                    Properties.Settings.Default.Workspace = null;
                    //MessageBox.Show("Need to login first run");
                }
                
            }
            else {
                MessageBox.Show("We don't internet connection");
                /*Library main = new Library();
                main.haveInternet = false;
                main.ShowDialog();
                main.TopMost = true;
                this.Close();*/
            }




        }
    }
}
