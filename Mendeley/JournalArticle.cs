using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeley
{
    class JournalArticle
    {   
        public int id { get; set; }
        public string abstractText { get;set;}
        public string authors { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string journal { get; set; }
        public string filepath { get; set; }
        public int volume { get; set; }
        public int pages { get; set; }
        public int issue { get; set; }
        public string ArXivID { get; set; }
        public string DOI { get; set; }
        public string PMID { get; set; }
        public string add_date { get; set; }
        public string update_date { get; set; }
        public string delete_date { get; set; }
    }
}
