using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace WcfErp.Modelos.Generales
{
    public class Pureza : ModeloBase<Pureza>
    {
        [BsonRequired]
        public GrupoComponente GrupoComponente { get; set; }

        protected override Pureza addValues(Pureza item,EmpresaContext db)
        {
            try
            {
                item.GrupoComponente=db.GrupoComponente.get(item.GrupoComponente._id,db);
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}