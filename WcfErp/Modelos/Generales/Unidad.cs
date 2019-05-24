using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.Generales
{
    public class Unidad : ModeloBase<Unidad>
    {
        
        public string Abreviatura { get; set; }

        protected override void addIndex(IMongoCollection<Unidad> Collection,EmpresaContext db)
        {
            try
            {
                //Collection.Indexes.

                var keys = Builders<Unidad>.IndexKeys.Ascending("Nombre");
                var indexOptions = new CreateIndexOptions { Unique = true };
                var model = new CreateIndexModel<Unidad>(keys, indexOptions);

                Collection.Indexes.CreateOne(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}