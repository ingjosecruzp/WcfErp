using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Modelos.PVenta
{
    public class Movtos_Cajas : ModeloBase<Movtos_Cajas, EmpresaContext>
    {
        public Cajas Cajas { get; set; }
        public Nullable<DateTime> Fecha { get; set; }
        public string TipoMovto { get; set; }
        public string FormaEmitida { get; set; }
        public Cajeros Cajeros { get; set; }
        public FormadeCobro FormaCobro { get; set; }
        public List<FormaCobroCaja> FormaCobroCierre { get; set; }
        public Nullable<float> Importe { get; set; }

        protected override Movtos_Cajas addValues(Movtos_Cajas item, EmpresaContext db)
        {
            try
            {
                item.Cajas = db.Cajas.get(item.Cajas._id, db);
                if(item.TipoMovto == "Apertura")
                {
                    item.FormaCobro = db.FormaCobroCaja.get(item.FormaCobro._id, db);
                    item.Cajeros = db.Cajeros.get(item.Cajeros._id, db);
                }else if(item.TipoMovto == "CIERRE")
                {
                    foreach (FormaCobroCaja det in item.FormaCobroCierre)
                    {
                        det.Moneda = db.Moneda.get(det.Moneda._id, db);
                        //item.FormaCobroCierre = db.FormaCobroCierre.get(item.FormaCobroCierre._id, db);
                    }
                }

                item.Fecha = DateTime.Now;
                return item;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void ValidarModel(Movtos_Cajas item, string Movimiento)
        {
            try
            {
                if (item.FormaCobroCierre.Count == 0 && item.TipoMovto == "CIERRE")//sin formas de pago
                    throw new Exception("No contiene formas de pago");
                if (String.IsNullOrWhiteSpace(item.Cajas._id))//Sin Caja
                    throw new Exception("Falta capturar la caja");
            }
            catch (Exception)
            {
                throw;

            }
        }

        public List<Movtos_Cajas> find_apertura(FilterDefinition<Movtos_Cajas> filter, int limit, String cadena, EmpresaContext db, string skip = null)
        {
            try
         {
                var builder_sort = Builders<Movtos_Cajas>.Sort;
                var sort = builder_sort.Descending("Fecha");


                IMongoCollection<Movtos_Cajas> Collection = dbMongo.GetCollection<Movtos_Cajas>(typeof(Movtos_Cajas).Name);


                List<Movtos_Cajas> item;

                if (cadena == "")
                {
                    item = Collection.Find<Movtos_Cajas>(filter).Limit(limit).Sort(sort).ToList();
                }
                else
                {
                    JObject rss = cadenaTojObject(cadena);
                    if (skip == null)
                        item = Collection.Find(filter).Project<Movtos_Cajas>(rss.ToString()).Sort(sort).Limit(limit).ToList();
                    else
                    {
                        int skipInt = Int32.Parse(skip);
                        item = Collection.Find(filter).Project<Movtos_Cajas>(rss.ToString()).Limit(limit).Sort(sort).ToList();
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }

}