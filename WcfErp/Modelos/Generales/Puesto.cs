using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Puesto : ModeloBase<Puesto, EmpresaContext>
    {
        [BsonRequired]
        public Departamento Departamento { get; set; }

        protected override Puesto addValues(Puesto item, EmpresaContext db)
        {
            try
            {
                item.Departamento = db.Departamento.get(item.Departamento._id, db);
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}