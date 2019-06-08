using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfMovimientosES" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfMovimientosES.svc or WcfMovimientosES.svc.cs at the Solution Explorer and start debugging.
    public class WcfMovimientosES : ServiceBase<MovimientosES>, IWcfMovimientosES
    {
        
        public  override  MovimientosES add(MovimientosES item)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {
                    session.StartTransaction();

                    Inventarios documento = new Inventarios();
                    documento.add(item, db, session);

                IMongoCollection<MovimientosES> Documento = db.GetCollection<MovimientosES>("MovimientosES");// db.GetCollection<MovimientosES>("MovimientosES");
                IMongoCollection<Concepto> Conceptos = db.GetCollection<Concepto>("Concepto");
                IMongoCollection<Almacen> Almacenes = db.GetCollection<Almacen>("Almacen");
                IMongoCollection<Articulo> Articulos = db.GetCollection<Articulo>("Articulo");
                IMongoCollection<InventariosSaldos> CollectionSaldos = db.GetCollection<InventariosSaldos>("InventariosSaldos");
                IMongoCollection<InventariosCostos> CollectionCostos = db.GetCollection<InventariosCostos>("InventariosCostos");

                item.Concepto = Conceptos.Find<Concepto>(d => d._id == item.Concepto.id).Project<Concepto>(Builders<Concepto>.Projection.Include(p => p._id).Include(p => p.Nombre).Include(p => p.Naturaleza).Include(p => p.CostoAutomatico).Include(p => p.FolioAutomatico)).FirstOrDefault();
                item.Almacen = Almacenes.Find<Almacen>(d => d._id == item.Almacen.id).Project<Almacen>(Builders<Almacen>.Projection.Include(p => p._id).Include(p => p.Nombre)).FirstOrDefault();

                if (item.Concepto.FolioAutomatico == "SI"){
                    //item.Folio = AutoIncrement("FolioAutomatico",db).ToString();
                    item.Folio = AutoIncrement(item.Concepto.Clave, db).ToString();
                }

         //       throw new Exception("ataras");

                var builderSaldos = Builders<InventariosSaldos>.Filter;
                var builderCostos = Builders<InventariosCostos>.Filter;
                var Ids = (from an in item.Detalles_ES select an.Articulo).ToList().Select(ab => ab._id); //recolectamos en una lista los ids que nos manda el cliente
                var filter = Builders<Articulo>.Filter.In(myClass => myClass._id, Ids);   //creamos un filtro con la clapsula In
                List<Articulo> ArticuloCompletoServer = Articulos.Find(filter).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(p => p.Nombre).Include(p => p.UnidadInventario.Abreviatura)).ToList(); //Realizamos una sola query a la bd obteniendo solo datos necesarios (en este caso solo el nombre,id y unidad de inventario) para hacerla lo mas liviana 
                List<InventariosSaldos> InventariosSaldosCompletoServer = CollectionSaldos.Find(builderSaldos.In("ArticuloId",Ids) & builderSaldos.Eq("AlmacenId", item.Almacen._id)).ToList();      //   (Builders<InventariosSaldos>.Filter.In(p => p.ArticuloId, Ids)).ToList();    Builders<InventariosSaldos>.Filter.In(p => p.ArticuloId, Ids)).ToList();
                List<InventariosCostos> InventariosCostosCompletoServer = CollectionCostos.Find(builderCostos.In("ArticuloId", Ids) & builderCostos.Eq("AlmacenId", item.Almacen._id)).ToList();
                item.Sistema_Origen = "IN";
                item.Cancelado = "NO";
                item.Fecha = item.Fecha.Date < DateTime.Now.Date ? item.Fecha.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second) : item.Fecha;
                item.Ano = item.Fecha.Year;
                item.Mes = item.Fecha.Month;
                item.Dia = item.Fecha.Day;
                var updatesSaldos = new List<WriteModel<InventariosSaldos>>();
                var updatesCostos = new List<WriteModel<InventariosCostos>>();

                item.ValidarModel(item); //Revisar reglas de validacion para el documento

                foreach (Detalles_ES mov in item.Detalles_ES)
                {
                       
                    mov.Articulo = ArticuloCompletoServer.Find(b=>b._id==mov.Articulo._id);
                    InventariosCostos invcosto = LlenarObjetoInventartiosCostos(item, mov, ArticuloCompletoServer, InventariosCostosCompletoServer);//PRIMERO SE SE LLENA LA COLECCION INVENTARIOS COSTOS ANTES QUE INVENTARIOS SALDOS.
                    InventariosSaldos invsaldo = LlenarObjetoInventartiosSaldos(item, mov, ArticuloCompletoServer, InventariosSaldosCompletoServer);
                    var filtercostos = Builders<InventariosCostos>.Filter.Eq(s => s._id, invcosto._id);
                    var filtersaldos = Builders<InventariosSaldos>.Filter.Eq(s => s._id, invsaldo._id);
                    if (!(invcosto._id == null || invcosto._id == ""))
                        updatesCostos.Add(new ReplaceOneModel<InventariosCostos>(filtercostos, invcosto) { IsUpsert = true });
                    else
                        updatesCostos.Add(new InsertOneModel<InventariosCostos>(invcosto));
                    if (!(invsaldo._id==null || invsaldo._id==""))  //si  no existe un registro en la coleccion inventarios saldos de la combinacion articulo almacen crea uno nuevo de lo contrario hace un update al existente
                        updatesSaldos.Add(new ReplaceOneModel<InventariosSaldos>(filtersaldos, invsaldo) { IsUpsert = true });
                    else
                        updatesSaldos.Add(new InsertOneModel<InventariosSaldos>(invsaldo));
                }

                    return  item;
            }
            catch (Exception ex)
            {
                Error(ex, "");   
                return null;
            }


        }
        public override MovimientosES update(MovimientosES item, string id)
        {
            try
            {
                throw new Exception("Los ajustes de inventario no son modificables");
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public List<MovimientosES> obtenerES(string cadena,string tipoMovimiento)
        {

            try
            {
                var builderMovimientos = Builders<MovimientosES>.Filter;
                JObject rss = new JObject();

                string[] fields = cadena.Split(',');

                string campos = "{";

                foreach (string f in fields)
                {
                    rss.Add(new JProperty(f, "1"));
                }
                Console.WriteLine(rss.ToString());
                campos += "}";

                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<MovimientosES> Collection = db.GetCollection<MovimientosES>(typeof(MovimientosES).Name);

                List<MovimientosES> Lista;

                var filter = Builders<MovimientosES>.Filter.Regex("Nombre", new BsonRegularExpression("", "i"));

                Lista = Collection.Find<MovimientosES>(a => a.Concepto.Naturaleza == tipoMovimiento).Project<MovimientosES>(rss.ToString()).ToList();

                return Lista;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
    }
}
