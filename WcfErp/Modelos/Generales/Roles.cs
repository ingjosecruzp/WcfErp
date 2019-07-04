using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Roles : ModeloBase<Roles,UsuarioContext>
    {
        public string Administrador { get; set; }

        public List<BDEmpresas> BDEmpresas { get; set; }
        public Roles()
        {
            this.BDEmpresas = new List<BDEmpresas>();
        }

        protected override void validateModel(Roles item, UsuarioContext db)
        {
            try
            {
                if (item.BDEmpresas.Count == 0)//sin detalles
                    throw new Exception("Al menos debes seleccionar una empresa");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}