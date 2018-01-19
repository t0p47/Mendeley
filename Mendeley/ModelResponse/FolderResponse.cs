using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeley
{
    class FolderResponse
    {

        public int id { get; set; }
        public int uid { get; set; }
        public string name { get; set; }
        public int parent_id { get; set; }

    }
}
