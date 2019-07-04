using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using WcfErp.Modelos.Inventarios;

namespace WcfErp.Modelos.Generales
{
    public class Municipio : ModeloBase<Municipio, EmpresaContext>
    {
        public Estado Estado { get; set; }
        public Paises Paises { get; set; }

        protected override Municipio addValues(Municipio item, EmpresaContext db)
        {
            try
            {
                item.Estado = db.Estado.get(item.Estado._id, db);
                item.Paises = db.Paises.get(item.Paises._id, db);

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}