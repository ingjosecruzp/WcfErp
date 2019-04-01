using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Roles : ModeloBase
    {
        public string Administrador { get; set; }

        public List<BDEmpresas> BDEmpresas { get; set; }
        public Roles()
        {
            this.BDEmpresas = new List<BDEmpresas>();
        }
    }
}