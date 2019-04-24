using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class Usuario : ModeloBase
    {

        public string NombreCompleto { get; set; }
        public string Contrasena { get; set; }
        public string Status { get; set; }
        public UsuarioRol UsuarioRol { get; set; }
    }
}