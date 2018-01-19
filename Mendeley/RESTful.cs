using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mendeley
{
    class RESTful
    {

        public static string GetHeadersRequest(string url) {
            string strResponse = String.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            WebHeaderCollection headerCollection = new WebHeaderCollection();
            headerCollection.Add(HttpRequestHeader.Authorization, "Bearer "+Functions.getTokenFromServer());

            request.Method = "GET";
            request.Headers = headerCollection;

            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException("Error code: " + response.StatusCode);
                    }

                    //Process the response stream
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponse = reader.ReadToEnd();
                            }//End of using reader
                        }
                    }//End of using responseStream

                }//End using of response
            }
            catch (WebException ex) {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {

                        //MessageBox.Show("Status description: "+response.StatusDescription);

                        Stream receiveStream = response.GetResponseStream();
                        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader readStream = new StreamReader(receiveStream, encode);
                        Char[] read = new Char[256];

                        int count = readStream.Read(read, 0, 256);
                        String jsonResponse = null;

                        while (count > 0)
                        {
                            String str = new String(read, 0, count);
                            jsonResponse += str;
                            count = readStream.Read(read, 0, 256);
                        }

                        JObject jResponse = JObject.Parse(jsonResponse);

                        string errorMessage = (string)jResponse["message"];

                        //Console.Write("ErrorMessage: " + errorMessage);

                        MessageBox.Show("Error message: " + errorMessage + ", from URL: " + url);


                        Console.WriteLine("");

                        readStream.Close();

                        if (errorMessage.Equals("Token has expired"))
                        {
                            string newTokenResponse = GetHeadersRequest(EndPoints.URL_REFRESH_TOKEN);
                            string token = JsonConvert.DeserializeObject<TokenObject>(newTokenResponse).token;
                            Functions.setTokenToDB(token);
                            //MessageBox.Show("New token: " + token);
                            strResponse = GetHeadersRequest(url);
                        }
                        else if (errorMessage.Equals("Token has expired and can no longer be refreshed")) {
                            //TODO: Need to insert email and password
                            return "relogin";
                        } else if (errorMessage.Equals("The token has been blacklisted")) {
                            //TODO: Need to insert email and password
                            return "relogin";
                        }

                    }
                    else
                    {
                        MessageBox.Show("No status code available");
                    }
                }
                else {
                    MessageBox.Show("Something wrong with server.");
                }
                MessageBox.Show(ex.Message.ToString(),ex.Source.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            
            return strResponse;
        }

        public static string PostRequest(string url, string postData) {
            string strResponse = String.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] postBytes = ascii.GetBytes(postData);

            WebHeaderCollection headerCollection = new WebHeaderCollection();
            headerCollection.Add(HttpRequestHeader.Authorization, "Bearer "+Functions.getTokenFromServer());

            request.Method = "POST";
            request.Headers = headerCollection;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;

            Stream postStream = request.GetRequestStream();
            postStream.Write(postBytes,0,postBytes.Length);
            postStream.Flush();
            postStream.Close();

            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException("Error code: " + response.StatusCode.ToString());
                    }

                    //Process the response stream
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponse = reader.ReadToEnd();
                            }//End of using reader
                        }
                    }//End of using responseStream


                }//End of using response
            }
            catch (WebException ex) {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
                var response = ex.Response as HttpWebResponse;
                if (response != null) {
                    Stream receiveStream = response.GetResponseStream();
                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader readStream = new StreamReader(receiveStream,encode);
                    Char[] read = new Char[256];

                    int count = readStream.Read(read,0,256);
                    String jsonResponse = null;

                    while (count>0) {

                        string str = new string(read,0,count);
                        jsonResponse += str;
                        count = readStream.Read(read,0,256);

                    }

                    JObject jResponse = JObject.Parse(jsonResponse);

                    string errorMessage = (string)jResponse["message"];

                    MessageBox.Show("Error message: " + errorMessage + ", from URL: " + url);

                    readStream.Close();


                    if (errorMessage.Equals("Token has expired")) {
                        string newTokenResponse = GetHeadersRequest(EndPoints.URL_REFRESH_TOKEN);
                        string token = JsonConvert.DeserializeObject<TokenObject>(newTokenResponse).token;
                        Functions.setTokenToDB(token);
                        strResponse = PostRequest(url, postData);
                    } else if (errorMessage.Equals("Token has expired and can no longer be refreshed")) {
                        return "relogin";
                    } else if (errorMessage.Equals("The token has been blacklisted")) {

                        return "relogin";
                    }
                }
            }
            
            return strResponse;
        }
    }
}
