using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeley
{
    class User_library
    {
        public int id { set; get; }
        public int uid { set; get; }
        public string type { set; get; }
        public int mid { set; get; }
        public int favorite { set; get; }
        public string ToString()
        {
            return string.Format("{0} - {1}", uid, mid);
        }
    }
}
