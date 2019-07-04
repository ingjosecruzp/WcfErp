using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Usuarios : ModeloBase<Usuarios, UsuarioContext>
    {
        public string NombreCompleto { get; set; }
        public string Contrasena { get; set; }
        public string Status { get; set; }
        public List<Roles> Roles { get; set; }

        public Usuarios()
        {
            //this.Roles = new List<Roles>();
        }

        protected override Usuarios addValues(Usuarios item, UsuarioContext db)
        {
            try
            {
                for (int i = 0; i < item.Roles.Count; i++)
                {
                    //item.Roles[i] = Collection_Roles.Find<Roles>(d => d._id == item.Roles[i]._id).FirstOrDefault();
                    item.Roles[i] = db.Roles.get(item.Roles[i]._id,db);
                }

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}