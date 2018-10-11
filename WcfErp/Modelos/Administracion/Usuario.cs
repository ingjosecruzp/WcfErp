using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Administracion
{
    public class Usuario : ModeloBase
    {
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public UsuarioRol Rol { get; set; }
        public string EstatusUsuario { get; set; }
    }
}