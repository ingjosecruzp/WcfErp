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
            MongoClient client = new MongoClient("mongodb://adminErp:pwjrnew@18.191.252.222:27017/?authSource=admin");
            //    var session = client.StartSession();//Create a session  transactions




            try
            {

                //   session.StartTransaction();//Begin transaction
                item.Almacen_Destino = new Almacen();
                item.Almacen_Destino._id = "5bd259a71d28282c7ce19c38";


                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<MovimientosES> Documento = db.GetCollection<MovimientosES>("MovimientosES");// db.GetCollection<MovimientosES>("MovimientosES");
                IMongoCollection<Concepto> Conceptos = db.GetCollection<Concepto>("Concepto");
                IMongoCollection<Almacen> Almacenes = db.GetCollection<Almacen>("Almacen");
                IMongoCollection<Articulo> Articulos = db.GetCollection<Articulo>("Articulo");
                IMongoCollection<InventariosSaldos> CollectionSaldos = db.GetCollection<InventariosSaldos>("InventariosSaldos");
                IMongoCollection<InventariosCostos> CollectionCostos = db.GetCollection<InventariosCostos>("InventariosCostos");

                item.Concepto = Conceptos.Find<Concepto>(d => d._id == item.Concepto.id).Project<Concepto>(Builders<Concepto>.Projection.Include(p => p._id).Include(p => p.Nombre).Include(p => p.Naturaleza).Include(p => p.CostoAutomatico).Include(p => p.FolioAutomatico)).FirstOrDefault();
                item.Almacen = Almacenes.Find<Almacen>(d => d._id == item.Almacen.id).Project<Almacen>(Builders<Almacen>.Projection.Include(p => p._id).Include(p => p.Nombre)).FirstOrDefault();

                if (item.Concepto.FolioAutomatico == "SI"){
                    item.Folio = AutoIncrement("FolioAutomatico").ToString();
                }

                var builderSaldos = Builders<InventariosSaldos>.Filter;
                var builderCostos = Builders<InventariosCostos>.Filter;
                var Ids = (from an in item.Detalles_ES select an.Articulo).ToList().Select(ab => ab._id); //recolectamos en una lista los ids que nos manda el cliente
                var filter = Builders<Articulo>.Filter.In(myClass => myClass._id, Ids);   //creamos un filtro con la clapsula In
                List<Articulo> ArticuloCompletoServer = Articulos.Find(filter).Project<Articulo>(Builders<Articulo>.Projection.Include(p => p._id).Include(p => p.Nombre).Include(p => p.UnidadInventario.Abreviatura)).ToList(); //Realizamos una sola query a la bd obteniendo solo datos necesarios (en este caso solo el nombre,id y unidad de inventario) para hacerla lo mas liviana 
                List<InventariosSaldos> InventariosSaldosCompletoServer = CollectionSaldos.Find(builderSaldos.In("ArticuloId",Ids) & builderSaldos.Eq("AlmacenId", item.Almacen._id)).ToList();      //   (Builders<InventariosSaldos>.Filter.In(p => p.ArticuloId, Ids)).ToList();    Builders<InventariosSaldos>.Filter.In(p => p.ArticuloId, Ids)).ToList();
                List<InventariosCostos> InventariosCostosCompletoServer = CollectionCostos.Find(builderCostos.In("ArticuloId", Ids) & builderCostos.Eq("AlmacenId", item.Almacen._id)).ToList();
                item.Sistema_Origen = "IN";
                item.Cancelado = "NO";
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


                CollectionCostos.BulkWrite(updatesCostos);
                CollectionSaldos.BulkWrite(updatesSaldos);
                Documento.InsertOne(item);
                if (item.Concepto._id == "5c59c84f6886742388d9bbcc") // SI ES UN TRASPASO DE SALIDA VOLVEMOS A LLAMAR EL METODO ADD PARA DAR ENTRADA AL ALMACEN DE DESTINO
                   {
                    item._id = null;
                    item.Concepto._id = "5c59d6c16886742450e4527f";
                    item.Almacen._id = item.Almacen_Destino._id;
                    item.Almacen_Destino = item.Almacen;
                    add(item);

                   }
                    
             //   session.CommitTransaction();//Made it here without error? Let's commit the transaction
                

                return  item;
            }
            catch (Exception ex)
            {
             //   session.AbortTransaction();
                Error(ex, "");
                
                return null;
            }


        }

        public int AutoIncrement (string _id)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://adminErp:pwjrnew@18.191.252.222:27017/?authSource=admin");
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                var collection = db.GetCollection<Counters>("Counters");
                var filter = Builders<Counters>.Filter.Eq(x => x._id,_id);
                var update = Builders<Counters>.Update.Inc(x => x.sequence_value, 1);
                var options = new FindOneAndUpdateOptions<Counters>
                {
                    //Sort = Builders<Counters>.Sort.Ascending("Counters"),
                    ReturnDocument= ReturnDocument.After
                };
                Counters id=collection.FindOneAndUpdate(filter, update, options);

                return id.sequence_value;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public InventariosSaldos LlenarObjetoInventartiosSaldos(MovimientosES item,Detalles_ES mov, List<Articulo> ArticuloCompletoServer, List<InventariosSaldos> InventariosSaldosCompletoServer)
        {

            
                                                                       ////////////////////////////////////PARA LA TABLA SALDOS//////////////////////////////////////////////////////
              try
              {

                InventariosSaldos Saldo = new InventariosSaldos();
                InventariosSaldos SaldoActual = new InventariosSaldos();

                mov.Articulo = ArticuloCompletoServer.Find(b => b._id == mov.Articulo._id);
                SaldoActual = InventariosSaldosCompletoServer.Find(b => b.ArticuloId == mov.Articulo._id && b.AlmacenId==item.Almacen._id && b.Ano==item.Fecha.Year && b.Mes == item.Fecha.Month);

                if (SaldoActual == null)
                  {//SI ES UN NUEVO  REGITRO EN LA TABLA INVENTARIOS SALDOS, ES UNO POR CADA MES DEL AÑO EN LA COMBINACION ARTICULO ALMACEN
                      Saldo.ArticuloId = mov.Articulo._id;
                      Saldo.AlmacenId = item.Almacen._id;
                      Saldo.Ano = item.Fecha.Year;
                      Saldo.Mes = item.Fecha.Month;
                      Saldo.UltimoDia = item.Fecha.Day;
                    

                      if (item.Concepto.Naturaleza == "ENTRADA")
                      {
                          Saldo.EntradaUnidades = mov.Cantidad;
                          Saldo.EntradasCosto = mov.Costo * mov.Cantidad;
                      }
                      else if(item.Concepto.Naturaleza == "SALIDA")
                      {
                          Saldo.SalidasUnidades = mov.Cantidad;
                          Saldo.SalidasCosto = mov.Costo * mov.Cantidad;
                      }

                      return Saldo;

                  }
                  else
                  {
                      
                      if (item.Fecha.Day > SaldoActual.UltimoDia)
                          SaldoActual.UltimoDia = item.Fecha.Day;



                      if (item.Concepto.Naturaleza == "ENTRADA")
                      {
                          SaldoActual.EntradaUnidades += mov.Cantidad;
                          SaldoActual.EntradasCosto += mov.Costo * mov.Cantidad;
                      }
                      else if (item.Concepto.Naturaleza == "SALIDA")
                      {
                          SaldoActual.SalidasUnidades += mov.Cantidad;
                          SaldoActual.SalidasCosto += mov.Costo * mov.Cantidad;
                      }
                      return SaldoActual;
                  }


              }
              catch (Exception ex)
              {

                  throw;
          }
          
    
        }

        public InventariosCostos LlenarObjetoInventartiosCostos(MovimientosES item, Detalles_ES mov, List<Articulo> ArticuloCompletoServer, List<InventariosCostos> InventariosCostosCompletoServer)
        {


            
                                                                           ////////////////////////////////////PARA LA TABLA INVENTARIOSCOSTOS/////////////////////////////////////////////
                                                                                      /*------------------- PROMEDIO PONDERADO------------------------*/

                 InventariosCostos Costo = new InventariosCostos();
                 InventariosCostos CostoActual = new InventariosCostos();

                 mov.Articulo = ArticuloCompletoServer.Find(b => b._id == mov.Articulo._id);
                 CostoActual = InventariosCostosCompletoServer.Find(b => b.ArticuloId == mov.Articulo._id && b.AlmacenId == item.Almacen._id);
                 Costo.ArticuloId = mov.Articulo._id;
                 Costo.AlmacenId = item.Almacen._id;
                 Costo.Fecha = item.Fecha;

                  if (CostoActual == null) // SI ES LA PRIMERA ENTRADA X ARTICULO  Y ALMACEN,SI ES UN NUEVO  REGITRO EN LA TABLA INVENTARIOS COSTOS, 
                     {


                          if (item.Concepto.Naturaleza == "ENTRADA") //Revisar si es un documento de entrada o de salida
                          {
                                if (item.Concepto.CostoAutomatico == "NO") // SI NO ES COSTO AUTOMATICO: ya que es la primera entrada de costo se multiplica la cantidad de entrada del articulo * el costo que viene desde el cliente
                                {
                                    mov.CostoTotal = mov.Cantidad * mov.Costo;
                                    Costo.ValorTotal = mov.CostoTotal;

                                }
                                else if (item.Concepto.CostoAutomatico == "SI") // SI ES COSTO AUTOMATICO:  YA QUE ES LA PRIMERA ENTRADA SE LE ASIGNA 0.00 YA QUE NO HAY REGISTRO DE COSTOS
                                {
                                    mov.CostoTotal = 0.00;
                                    Costo.ValorTotal = mov.CostoTotal;

                                }
                                Costo.Existencia = mov.Cantidad;

                                

                          }
                          else if (item.Concepto.Naturaleza == "SALIDA")
                          {

                                if (item.Concepto.CostoAutomatico == "NO") // SI NO ES COSTO AUTOMATICO: ya que es la primera entrada de costo se multiplica la cantidad de entrada del articulo * el costo que viene desde el cliente
                                {
                                    mov.CostoTotal = mov.Cantidad * mov.Costo;
                                    Costo.ValorTotal -= mov.CostoTotal;

                                }
                                else if (item.Concepto.CostoAutomatico == "SI") // SI ES COSTO AUTOMATICO:  YA QUE ES LA PRIMERA ENTRADA SE LE ASIGNA 0.00 YA QUE NO HAY REGISTRO DE COSTOS
                                {
                                    mov.CostoTotal = 0.00;
                                    Costo.ValorTotal -= mov.CostoTotal;

                                }
                                Costo.Existencia -= mov.Cantidad;

                          }
                          Costo.CapaAgotada = Costo.Existencia <= 0 ? "SI" : "NO";

                          return Costo;

                  }
                else     // EN CASO DE QUE NO SEA EL PRIMER MOVIMIENTO EN LA TABLA INVENTARIOSCOSTOS
                  {


                      if (item.Concepto.Naturaleza == "ENTRADA")
                      {

                            if (item.Concepto.CostoAutomatico == "SI")// SI ES COSTO AUTOMATICO 
                               {

                                mov.CostoTotal = CostoActual.ValorTotal >  0 ? (CostoActual.ValorTotal / CostoActual.Existencia) * mov.Cantidad : 0.00;
                                mov.Costo = CostoActual.ValorTotal >  0 ? (CostoActual.ValorTotal / CostoActual.Existencia): 0.00;
                                CostoActual.Existencia = CostoActual.Existencia + mov.Cantidad;
                                CostoActual.ValorTotal = CostoActual.ValorTotal + mov.CostoTotal;

                               }
                            else if (item.Concepto.CostoAutomatico == "NO")
                               {
                                mov.CostoTotal = mov.Cantidad * mov.Costo;
                                CostoActual.Existencia = CostoActual.Existencia + mov.Cantidad;
                                CostoActual.ValorTotal = CostoActual.ValorTotal + mov.CostoTotal;

                                }


                         CostoActual.CapaAgotada = CostoActual.Existencia <= 0 ? "SI" : "NO";

                    }
                    else if (item.Concepto.Naturaleza == "SALIDA")
                      {


                            if (item.Concepto.CostoAutomatico == "SI")// SI ES COSTO AUTOMATICO 
                               {
                                mov.CostoTotal = CostoActual.Existencia >  0 ? (CostoActual.ValorTotal / CostoActual.Existencia) * mov.Cantidad : 0.00; //SI EL VALOR TOTAL DE LA CAPA
                                mov.Costo = CostoActual.Existencia >  0 ? (CostoActual.ValorTotal / CostoActual.Existencia): 0.00;
                                CostoActual.Existencia = CostoActual.Existencia > 0 ? CostoActual.Existencia - mov.Cantidad : 0.00;
                                CostoActual.ValorTotal = CostoActual.Existencia > 0 ? CostoActual.ValorTotal - mov.CostoTotal : 0.00;

                                }
                                else if (item.Concepto.CostoAutomatico == "NO")
                                {
                                 mov.CostoTotal = mov.Cantidad * mov.Costo;
                                 CostoActual.Existencia =  CostoActual.Existencia - mov.Cantidad ;
                                 CostoActual.ValorTotal =  CostoActual.ValorTotal - mov.CostoTotal;


                                 }



                           CostoActual.CapaAgotada = CostoActual.Existencia <= 0 ? "SI" : "NO";

                      }
                      return CostoActual;
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

                MongoClient client = new MongoClient("mongodb://adminErp:pwjrnew@18.191.252.222:27017/?authSource=admin");
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<MovimientosES> Collection = db.GetCollection<MovimientosES>(typeof(MovimientosES).Name);

                List<MovimientosES> Lista;

                /*if(campos != null)
                    Lista = Collection.Find<Modelo>(null).Project<Modelo>(campos).ToList();
                else
                   Lista = Collection.AsQueryable().ToList();*/
                var filter = Builders<MovimientosES>.Filter.Regex("Nombre", new BsonRegularExpression("", "i"));

                //Lista = Collection.Find<Modelo>(filter).Project<Modelo>("{_id:1, Nombre:1,TipoConcepto.Nombre:1}").ToList();
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
