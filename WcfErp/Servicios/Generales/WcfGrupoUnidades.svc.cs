using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfGrupoUnidades" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfGrupoUnidades.svc or WcfGrupoUnidades.svc.cs at the Solution Explorer and start debugging.
    public class WcfGrupoUnidades : ServiceBase<GrupoUnidad,EmpresaContext>, IWcfGrupoUnidades
    {
        public GrupoUnidad delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Unidad> searchXUnidad(string busqueda, string _idGrupoUnidad)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<GrupoUnidad> Collection = db.GetCollection<GrupoUnidad>("GrupoUnidad");

                GrupoUnidad GrupoUnidad = Collection.Find<GrupoUnidad>(d => d._id == _idGrupoUnidad).FirstOrDefault();

                //Filtra los objetos del tipo unidades en memoria
                List<Unidad> LstUnidades = GrupoUnidad.GrupoUnidadDetalle.Where(i => i.UnidadEquivalente.Abreviatura.Contains(busqueda)).Select( x => x.UnidadEquivalente).ToList();

                return LstUnidades;
                
                //var builder = Builders<GrupoUnidad>.Filter;
                //var filter = builder.Eq("_id", _idGrupoUnidad);
                //var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("GrupoComponente._id", _idGrupoUnidad);
                //var filter = builder.Regex("GrupoUnidadDetalle.UnidadEquivalente.Abreviatura", new BsonRegularExpression(busqueda, "i"));
                //var filter = builder.AnyEq("GrupoUnidadDetalle.UnidadEquivalente.Abreviatura", busqueda);
                //var filter = builder.ElemMatch(x => x.GrupoUnidadDetalle, x => x.UnidadEquivalente.Abreviatura == busqueda);

                //List <GrupoUnidad> GrupoUnidad = Collection.Find<GrupoUnidad>(filter).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
