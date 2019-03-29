using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfErp.Reportes
{
    public class ResponseReportModel
    {
        public string reportURI { get; set; }
        public string requestId { get; set; }
        public string status { get; set; }
        public string totalPages { get; set; }
        public List<exports> exports { get; set; }
        public ResponseReportModel()
        {
            exports = new List<exports>();
        }
    }
}
