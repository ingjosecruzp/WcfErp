using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfErp.Reportes
{
    public class reportParameter
    {
        public string name { get; set; }
        public List<String> value { get; set; }
        public reportParameter()
        {
            value =new List<String>();
        }
    }
}
