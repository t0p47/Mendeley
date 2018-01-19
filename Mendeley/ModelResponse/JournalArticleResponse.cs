using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeley.ModelResponse
{
    class JournalArticleResponse
    {
        public int id {get; set;}
        public string title {get; set;}
        public string authors {get; set;}
        public string @abstract {get; set;}
        public string journal_id {get; set;}
        public int volume {get; set;}
        public int issue {get; set;}
        public int year {get; set;}
        public int pages {get; set;}
        public int ArXivID {get; set;}
        public int DOI {get; set;}
        public int PMID {get; set;}
        public int folder {get; set;}
        public string filepath {get; set;}
        public int takescount {get; set;}
        public int uid {get; set;}
        public string created_at {get; set;}
        public string updated_at {get; set;}
        public string delete_date {get; set;}
        public int favorite {get; set;}

    }
}
