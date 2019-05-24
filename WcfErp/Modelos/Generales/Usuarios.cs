using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Usuarios : ModeloBase<Usuarios>
    {
        public string NombreCompleto { get; set; }
        public string Contrasena { get; set; }
        public string Status { get; set; }
        public List<Roles> Roles { get; set; }

        public Usuarios()
        {
            this.Roles = new List<Roles>();
        }
    }
}