using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfConceptos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfConceptos.svc or WcfConceptos.svc.cs at the Solution Explorer and start debugging.
    public class WcfConceptos : ServiceBase<Concepto>,IWcfConceptos
    {
        public override Concepto add(Concepto item)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<TipoConcepto> Collection = db.GetCollection<TipoConcepto>("TipoConcepto");

                item.TipoConcepto = Collection.Find<TipoConcepto>(d => d._id == item.TipoConcepto.id).FirstOrDefault();

                return base.add(item);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }
        public override Concepto update(Concepto item, string id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://Alba:pwjrnew@18.191.252.222:27017/PAMC861025DB7");
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<TipoConcepto> Collection = db.GetCollection<TipoConcepto>("TipoConcepto");

                item.TipoConcepto = Collection.Find<TipoConcepto>(d => d._id == item.TipoConcepto.id).FirstOrDefault();

                return base.update(item,id);
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }
        public Concepto delete(string id)
        {
            throw new NotImplementedException();
        }


    }
}
