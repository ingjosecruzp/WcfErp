using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Reportes.Inventarios;
using WcfErp.Reportes;

namespace WcfErp.Servicios.Reportes.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfKardexArticulos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfKardexArticulos.svc or WcfKardexArticulos.svc.cs at the Solution Explorer and start debugging.
    public class WcfKardexArticulos : IWcfKardexArticulos
    {
        public List<KardexArticulos> KardexArticulo(string FechaInicio, string FechaFin, string AlmacenId, string ArticuloId, string GrupoId,string SubGrupoId, string Valoracion)
        {
            try
            {
                /*string FechaInicio="01/11/2018";
                string FechaFin = "03/04/2019";
                string AlmacenId= "5c9676d418cb1b38c0005846";
                string ArticuloId= "5bda1dff68867432000f8e3b";
                string GrupoId=null;
                string SubGrupoId = null;
                string Valoracion = null;*/

                string dateinicio = DateTime.Parse(FechaInicio).Subtract(TimeSpan.FromDays(1)).ToShortDateString();
                string datefin = DateTime.Parse(FechaFin).ToShortDateString();


                DateTime DateInicio = DateTime.Parse(FechaInicio);
                int anoInicio = DateInicio.Year;
                int mesInicio = DateInicio.Month;
                int diaInicio = DateInicio.Day;

                DateTime DateFin = DateTime.Parse(FechaFin);
                int anoFin = DateFin.Year;
                int mesFin = DateFin.Month;
                int diaFin = DateFin.Day;

                EmpresaContext db = new EmpresaContext();


                var min = new DateTime(anoInicio, mesInicio, diaInicio);
                var max = new DateTime(anoFin, mesFin, diaFin, 23, 59, 59);

                //var builderSaldos = Builders<MovimientosES>.Filter.Eq(x => x.Fecha >= min & x.Fecha <= max & x.Almacen.id == AlmacenId);
                //MovimientosEsCompletoServer = CollectionMovimientosEs.Find(x => x.Fecha >= min & x.Fecha <= max & x.Almacen.id == AlmacenId).ToList();

                FilterDefinition<MovimientosES> builderMovs;
                FilterDefinition<InventariosSaldos> builderSaldos;
                List<MovimientosES> MovimientosEsCompletoServer;

                //Selecciono el filtro articulo
                if (ArticuloId != null && ArticuloId != "")
                { 
                    //Filtramos todos los documentos donde se encuntra ese articulo
                    builderMovs = Builders<MovimientosES>.Filter.Gte(x => x.Fecha, min) & Builders<MovimientosES>.Filter.Lt(x => x.Fecha, max)
                               & Builders<MovimientosES>.Filter.Eq(x => x.Almacen._id, AlmacenId) & Builders<MovimientosES>.Filter.ElemMatch(l => l.Detalles_ES, l2 => l2.Articulo._id == ArticuloId);

                    //Filtros para seleccioanr solo los movimientos del articulo seleccionado en invetariosaldos
                    builderSaldos = Builders<InventariosSaldos>.Filter.Eq("AlmacenId", AlmacenId) & Builders<InventariosSaldos>.Filter.Eq("ArticuloId", ArticuloId);

                    MovimientosEsCompletoServer = db.MovimientosES.find(builderMovs, db);

                    //Elimina los articulos que no estan includios en el filtros
                    MovimientosEsCompletoServer.ForEach(a =>
                    {
                        a.Detalles_ES.RemoveAll(d => d.Articulo._id != ArticuloId);
                    });
                }
                //Selecciono el filtro Sugrupo
                else if (SubGrupoId != null && SubGrupoId != "")
                {
                    //Filtramos todos los documentos donde se encuentran articulos que pertenecen al subgrupo seleccionado
                    builderMovs = Builders<MovimientosES>.Filter.Gte(x => x.Fecha, min) & Builders<MovimientosES>.Filter.Lt(x => x.Fecha, max)
                               & Builders<MovimientosES>.Filter.Eq(x => x.Almacen._id, AlmacenId) & Builders<MovimientosES>.Filter.ElemMatch(l => l.Detalles_ES, l2 => l2.Articulo.SubGrupoComponente._id == SubGrupoId);

                    //Filtro todo los movimientos por alamcen
                    builderSaldos = Builders<InventariosSaldos>.Filter.Eq("AlmacenId", AlmacenId);

                    MovimientosEsCompletoServer = db.MovimientosES.find(builderMovs, db);

                    //Elimina los articulos que no pertenecen al subgrupo seleccionado
                    MovimientosEsCompletoServer.ForEach(a =>
                    {
                        a.Detalles_ES.RemoveAll(d => d.Articulo.SubGrupoComponente._id != SubGrupoId);
                    });
                }
                //Selecciono el filtro Grupo
                else if (GrupoId != null && GrupoId != "")
                {
                    //Filtramos todos los documentos donde se encuentran articulos que pertenecen al subgrupo seleccionado
                    builderMovs = Builders<MovimientosES>.Filter.Gte(x => x.Fecha, min) & Builders<MovimientosES>.Filter.Lt(x => x.Fecha, max)
                               & Builders<MovimientosES>.Filter.Eq(x => x.Almacen._id, AlmacenId) & Builders<MovimientosES>.Filter.ElemMatch(l => l.Detalles_ES, l2 => l2.Articulo.SubGrupoComponente.GrupoComponente._id == GrupoId);

                    //Filtro todo los movimientos por alamcen
                    builderSaldos = Builders<InventariosSaldos>.Filter.Eq("AlmacenId", AlmacenId);

                    MovimientosEsCompletoServer = db.MovimientosES.find(builderMovs, db);

                    //Elimina los articulos que no pertenecen al grupo seleccionado
                    MovimientosEsCompletoServer.ForEach(a =>
                    {
                        a.Detalles_ES.RemoveAll(d => d.Articulo.SubGrupoComponente.GrupoComponente._id != GrupoId);
                    });
                }
                else
                {
                    builderMovs = Builders<MovimientosES>.Filter.Gte(x => x.Fecha, min) & Builders<MovimientosES>.Filter.Lt(x => x.Fecha, max) & Builders<MovimientosES>.Filter.Eq(x => x.Almacen._id, AlmacenId);

                    //Filtro todo los movimientos por alamcen
                    builderSaldos = Builders<InventariosSaldos>.Filter.Eq("AlmacenId", AlmacenId);

                    MovimientosEsCompletoServer = db.MovimientosES.find(builderMovs, db);
                }


                //Consultamos la coleccion de invetariosaldos para saber el saldo inicial en una fecha especifica de cada articulo
                List<InventariosSaldos> InventariosSaldosCompletoServer = db.InventariosSaldos.find(builderSaldos, db);

                List<KardexArticulos> KardexInventario = new List<KardexArticulos>();

                foreach (MovimientosES mov in MovimientosEsCompletoServer.OrderBy(a => a.Fecha).ToList())
                {
                    foreach(Detalles_ES detalle in mov.Detalles_ES)
                    {

                        KardexArticulos articulo = new KardexArticulos();

                        /*Verifica si dentro del kardex ya existen al menos uno movimiento del articulo, si no 
                         * es asi agrega el saldo inicial el cual se basa de la existencia.
                         */
                        if (KardexInventario.Where(a => a.Articulo._id == detalle.Articulo._id).Count() == 0)
                        {
                            KardexArticulos articuloSaldoInicial = new KardexArticulos();

                            articuloSaldoInicial = saldoInicial(articuloSaldoInicial, DateInicio, InventariosSaldosCompletoServer,mov, detalle,diaInicio,mesInicio,anoInicio, db);
                            KardexInventario.Add(articuloSaldoInicial);
                        }

                        articulo.Almacen = mov.Almacen;
                        articulo.Folio = mov.Folio;
                        articulo.Fecha = mov.Fecha.ToShortDateString().ToString();
                        articulo.Concepto = mov.Concepto;

                        //Caracteristicas del articulo
                        articulo.Articulo = detalle.Articulo;
                        articulo.SubgrupoComponente = detalle.Articulo.SubGrupoComponente;
                        articulo.GrupoComponente = detalle.Articulo.SubGrupoComponente.GrupoComponente;

                        KardexArticulos articuloAnterior = new KardexArticulos();
                        articuloAnterior = KardexInventario.Where(x => x.Articulo._id == detalle.Articulo._id).Last();

                        if (articulo.Concepto.Naturaleza == "ENTRADA")
                        {
                            articulo.EntradaUnidad = detalle.Cantidad;
                            articulo.EntradaCostoUnitario = detalle.Costo;
                            articulo.EntradaCostoTotal = detalle.CostoTotal;

                            articulo.ExistenciaUnidades = articuloAnterior.ExistenciaUnidades + detalle.Cantidad;
                            articulo.ExistenciaCostoTotal = articuloAnterior.ExistenciaCostoTotal + detalle.CostoTotal;
                        }
                        else if (articulo.Concepto.Naturaleza == "SALIDA")
                        {
                            articulo.SalidaUnidad = detalle.Cantidad;
                            articulo.SalidaCostoUnitario = detalle.Costo;
                            articulo.SalidaCostoTotal = detalle.CostoTotal;

                            articulo.ExistenciaUnidades = articuloAnterior.ExistenciaUnidades - detalle.Cantidad;
                            articulo.ExistenciaCostoTotal = articuloAnterior.ExistenciaCostoTotal - detalle.CostoTotal;
                        }


                        KardexInventario.Add(articulo);
                    }
                }


                return KardexInventario.OrderBy(a => a.Almacen.Nombre).OrderBy(a => a.SubgrupoComponente.Nombre).OrderBy(a => a.Articulo.Nombre).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public KardexArticulos saldoInicial (KardexArticulos articulo,DateTime FechaInicio,List<InventariosSaldos> InventariosSaldosCompletoServer, MovimientosES mov, Detalles_ES detalle,int dia,int mes,int ano,EmpresaContext db)
        {
            try
            {
                //Se declaro asi por un conflico de nombre
                Servicios.Inventarios.Inventarios inventario = new Servicios.Inventarios.Inventarios();

                articulo.Almacen = mov.Almacen;
                articulo.Folio = "";
                articulo.Concepto = new Concepto
                {
                    Nombre="SI - SALDO INICIAL"
                };

                //Caracteristicas del articulo
                articulo.Articulo = detalle.Articulo;
                articulo.SubgrupoComponente = detalle.Articulo.SubGrupoComponente;
                articulo.GrupoComponente = detalle.Articulo.SubGrupoComponente.GrupoComponente;

                ExistenciaValorInventario existencia = inventario.ExistenciaArticulo(detalle.Articulo._id,articulo.Almacen.id, FechaInicio, InventariosSaldosCompletoServer, detalle.Articulo, dia, mes, ano, db);

                articulo.EntradaUnidad = existencia.Existencia;
                articulo.EntradaCostoUnitario = 0.0;
                articulo.EntradaCostoTotal = existencia.CostoUnitario * existencia.Existencia;

                articulo.ExistenciaUnidades = existencia.Existencia;
                articulo.ExistenciaCostoTotal = articulo.EntradaCostoTotal;

                return articulo;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /*public KardexArticulos ExistenciaArticuloPeriodo(ExistenciaValorInventario ExistenciaFechaInicio, ExistenciaValorInventario ExistenciaFechaFin,List<MovimientosES> MovimientosEsCompletoServer)
        {
            try
            {
                KardexArticulos kardex = new KardexArticulos();

                int i = 0;

                kardex.ExistenciaInicial = ExistenciaFechaInicio.Existencia;
                kardex.ExistenciaFinal = ExistenciaFechaFin.Existencia;
                kardex.TotalEntradas = ExistenciaFechaFin.TotalEntradas - ExistenciaFechaInicio.TotalEntradas;
                kardex.TotalSalidas = ExistenciaFechaInicio.TotalSalidas + ExistenciaFechaFin.TotalSalidas;
                var mz = MovimientosEsCompletoServer.SelectMany(l => l.Detalles_ES, (a, b) => new { a, b }).Where(p => p.b.Articulo._id == ExistenciaFechaInicio.Articulo._id).ToList();
                List<MovimientosES> movimientos = mz.Select(a => a.a).ToList();
                if (kardex.detalles == null)
                {
                    //It's null - create it
                    kardex.detalles = new List<DetallesKardexArticulos>();
                }

                foreach (MovimientosES detalle in movimientos)
                {

                    DetallesKardexArticulos detallekardex = new DetallesKardexArticulos();
                    detallekardex.Fecha = detalle.Fecha.ToShortDateString();
                    detallekardex.Concepto = detalle.Concepto.Nombre;
                    detallekardex.Folio = detalle.Folio;
                    detallekardex.Costo = detalle.Detalles_ES.Where(a => a.Articulo._id == ExistenciaFechaInicio.Articulo._id).Sum(b => b.CostoTotal);
                    if (detalle.Concepto.Naturaleza == "ENTRADA")
                        detallekardex.TotalEntrada = detalle.Detalles_ES.Where(a => a.Articulo._id == ExistenciaFechaInicio.Articulo._id).Sum(b => b.Cantidad);
                    else
                        detallekardex.TotalSalida = detalle.Detalles_ES.Where(a => a.Articulo._id == ExistenciaFechaInicio.Articulo._id).Sum(b => b.Cantidad);
                    invfinal += detallekardex.TotalEntrada - detallekardex.TotalSalida;
                    detallekardex.InventarioFinal = invfinal;

                    kardex.detalles.Add(detallekardex);

                    i++;

                }

                kardex.detalles = kardex.detalles.OrderBy(a => Convert.ToDateTime(a.Fecha)).ToList();
                return kardex;
            }
            catch (Exception)
            {

                throw;
            }

        }*/
        public string VerReporte(string parametros)
        {
            try
            {
                var jsonObject = JObject.Parse(parametros);

                List<reportParameter> JasperParametros = new List<reportParameter>();

                foreach (var p in jsonObject)
                {
                    //Console.WriteLine(p.Value.Type); // eg. integer

                    reportParameter param = new reportParameter();
                    param.name = p.Key;
                    param.value.Add(p.Value.ToString());

                    JasperParametros.Add(param);
                }

                //Agrega token
                reportParameter paramToken = new reportParameter();
                paramToken.name = "Token";

                OperationContext currentContext = OperationContext.Current;
                HttpRequestMessageProperty reqMsg = currentContext.IncomingMessageProperties["httpRequest"] as HttpRequestMessageProperty;
                string authToken = reqMsg.Headers["Token"];

                paramToken.value.Add(authToken);
                JasperParametros.Add(paramToken);

                reportParameter param1 = new reportParameter();
                param1.name = "empresa";
                param1.value.Add(getKeyToken("razonsocial", "token"));

                JasperParametros.Add(param1);

                string Archivo = GetTimestamp(DateTime.Now);
                string extension = "pdf";

                ReportesPFD VmReporte = new ReportesPFD("/ERP/Kardex", JasperParametros, extension, Archivo);

                return Archivo + "." + extension;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        //Metodo para dar respuesta las peticiones OPTION CORS
        public void GetOptions()
        {
        }
        public string getKeyToken(string key, string Token)
        {
            try
            {
                OperationContext currentContext = OperationContext.Current;
                HttpRequestMessageProperty reqMsg = currentContext.IncomingMessageProperties["httpRequest"] as HttpRequestMessageProperty;
                string authToken = reqMsg.Headers[Token];
                string value;
                if (authToken != "")
                {
                    var payload = JWT.JsonWebToken.DecodeToObject(authToken, "pwjrnew") as IDictionary<string, object>;
                    value = payload.ContainsKey(key) ? payload[key].ToString() : "";
                }
                else
                {
                    value = "";
                }
                return value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
