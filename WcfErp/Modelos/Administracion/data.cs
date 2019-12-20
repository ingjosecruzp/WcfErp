using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class data
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string value { get; set; }
        public bool EsPadre { get; set; }
        public string _id { get; set; }

        [JsonProperty("data")]
        public List<data> data_submenu { get; set; }
    }
}