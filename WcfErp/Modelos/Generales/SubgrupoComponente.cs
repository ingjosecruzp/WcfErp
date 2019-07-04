using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class SubgrupoComponente : ModeloBase<SubgrupoComponente, EmpresaContext>
    {
        public string Clave { get; set; }
        //[BsonRequired]
        public GrupoComponente GrupoComponente { get; set; }

        protected override SubgrupoComponente addValues(SubgrupoComponente item, EmpresaContext db)
        {
            try
            {
                item.GrupoComponente = db.GrupoComponente.get(item.GrupoComponente._id, db);

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}