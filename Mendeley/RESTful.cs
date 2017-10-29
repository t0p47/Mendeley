using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mendeley
{
    class RESTful
    {
        public static string getGETResponse(string response) {
            return response;
        }

        public static string getPOSTResponse(string response)
        {
            return response;
        }

        public async static Task<string> GetRequest(string url, Action<bool> callback) {
            using (HttpClient client = new HttpClient()) {
                using (HttpResponseMessage response = await client.GetAsync(url)) {
                    using (HttpContent content = response.Content) {
                        string responseContent = await content.ReadAsStringAsync();
                        MessageBox.Show(responseContent);
                        return responseContent;
                    }
                }
            } 
        }

        public async static void PostRequest(string url, IEnumerable<KeyValuePair<string,string>> postData) {
            HttpContent postContent = new FormUrlEncodedContent(postData);
            using (HttpClient client = new HttpClient()) {
                using (HttpResponseMessage response = await client.PostAsync(url,postContent)) {
                    using (HttpContent content = response.Content) {
                        string responseContent = await content.ReadAsStringAsync();
                        //return responseContent;

                        TokenObject tokenObject = JsonConvert.DeserializeObject<TokenObject>(responseContent);

                        MessageBox.Show("POST response "+tokenObject.token);
                        Console.WriteLine(tokenObject.token);
                    }
                }
            }
        }
    }
}
