using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Reportes.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    public class Inventarios
    {
        public MovimientosES add(MovimientosES item,EmpresaContext db, IClientSessionHandle session = null)
        {
            try
            {

                item.Concepto = db.Concepto.getbyFields(item.Concepto.id, "_id,Clave,Nombre,Naturaleza,CostoAutomatico,FolioAutomatico", db);
                item.Almacen = db.Almacen.getbyFields(item.Almacen.id, "_id,Nombre", db);

                if (item.Concepto.FolioAutomatico == "SI")
                {
                    if (item.Concepto.Clave==null)
                        throw new Exception("El concepto seleccionado no tiene una clave asignada");

                    item.Folio = AutoIncrement(item.Concepto.Clave, db.db, session).ToString();
                }


                //Genera filtros para busqueda
                var builderSaldos = Builders<InventariosSaldos>.Filter;
                var builderCostos = Builders<InventariosCostos>.Filter;

                var Ids = (from an in item.Detalles_ES select an.Articulo).ToList().Select(ab => ab._id); //recolectamos en una lista los ids que nos manda el cliente
                var filter = Builders<Articulo>.Filter.In(myClass => myClass._id, Ids);   //creamos un filtro con la clapsula In

                //Realizamos una sola query a la bd obteniendo solo datos necesarios (en este caso solo el nombre,id y unidad de inventario) para hacerla lo mas liviana 
                List<Articulo> ArticuloCompletoServer = db.Articulo.Filters(filter, "Nombre,Clave,UnidadInventario.Abreviatura,SubGrupoComponente");

                List<InventariosSaldos> InventariosSaldosCompletoServer = db.InventariosSaldos.Filters(builderSaldos.In("ArticuloId", Ids) & builderSaldos.Eq("AlmacenId", item.Almacen._id));
                List<InventariosCostos> InventariosCostosCompletoServer = db.InventariosCostos.Filters(builderCostos.In("ArticuloId", Ids) & builderCostos.Eq("AlmacenId", item.Almacen._id));

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

                    mov.Articulo = ArticuloCompletoServer.Find(b => b._id == mov.Articulo._id);

                    //PRIMERO SE SE LLENA LA COLECCION INVENTARIOS COSTOS ANTES QUE INVENTARIOS SALDOS.
                    InventariosCostos invcosto = LlenarObjetoInventartiosCostos(item, mov, ArticuloCompletoServer, InventariosCostosCompletoServer);
                    InventariosSaldos invsaldo = LlenarObjetoInventartiosSaldos(item, mov, ArticuloCompletoServer, InventariosSaldosCompletoServer);

                    var filtercostos = Builders<InventariosCostos>.Filter.Eq(s => s._id, invcosto._id);
                    var filtersaldos = Builders<InventariosSaldos>.Filter.Eq(s => s._id, invsaldo._id);

                    if (!(invcosto._id == null || invcosto._id == ""))
                        updatesCostos.Add(new ReplaceOneModel<InventariosCostos>(filtercostos, invcosto) { IsUpsert = true });
                    else
                        updatesCostos.Add(new InsertOneModel<InventariosCostos>(invcosto));

                    //si  no existe un registro en la coleccion inventarios saldos de la combinacion articulo almacen crea uno nuevo de lo contrario hace un update al existente
                    if (!(invsaldo._id == null || invsaldo._id == ""))
                        updatesSaldos.Add(new ReplaceOneModel<InventariosSaldos>(filtersaldos, invsaldo) { IsUpsert = true });
                    else
                        updatesSaldos.Add(new InsertOneModel<InventariosSaldos>(invsaldo));
                }

                db.InventariosCostos.updateMany(updatesCostos, db, session);
                db.InventariosSaldos.updateMany(updatesSaldos, db, session);

                db.MovimientosES.add(item, db, session);

                // SI ES UN TRASPASO DE SALIDA VOLVEMOS A LLAMAR EL METODO ADD PARA DAR ENTRADA AL ALMACEN DE DESTINO
                if (item.Concepto._id == "5c59c84f6886742388d9bbcc")
                {
                    item.Almacen_Destino = db.Almacen.getbyFields(item.Almacen_Destino._id, "_id,Nombre", db);

                    item._id = null;
                    item.Concepto._id = "5c59d6c16886742450e4527f";
                    item.Almacen._id = item.Almacen_Destino._id;
                    item.Almacen_Destino = item.Almacen;
                    add(item,db,session);
                }


                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public InventariosSaldos LlenarObjetoInventartiosSaldos(MovimientosES item, Detalles_ES mov, List<Articulo> ArticuloCompletoServer, List<InventariosSaldos> InventariosSaldosCompletoServer)
        {
            ////////////////////////////////////PARA LA TABLA SALDOS//////////////////////////////////////////////////////
            try
            {

                InventariosSaldos Saldo = new InventariosSaldos();
                InventariosSaldos SaldoActual = new InventariosSaldos();

                mov.Articulo = ArticuloCompletoServer.Find(b => b._id == mov.Articulo._id);
                SaldoActual = InventariosSaldosCompletoServer.Find(b => b.ArticuloId == mov.Articulo._id && b.AlmacenId == item.Almacen._id && b.Ano == item.Fecha.Year && b.Mes == item.Fecha.Month);

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
                    else if (item.Concepto.Naturaleza == "SALIDA")
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

                        mov.CostoTotal = CostoActual.ValorTotal > 0 ? (CostoActual.ValorTotal / CostoActual.Existencia) * mov.Cantidad : 0.00;
                        mov.Costo = CostoActual.ValorTotal > 0 ? (CostoActual.ValorTotal / CostoActual.Existencia) : 0.00;
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
                        mov.CostoTotal = CostoActual.Existencia > 0 ? (CostoActual.ValorTotal / CostoActual.Existencia) * mov.Cantidad : 0.00; //SI EL VALOR TOTAL DE LA CAPA
                        mov.Costo = CostoActual.Existencia > 0 ? (CostoActual.ValorTotal / CostoActual.Existencia) : 0.00;
                        CostoActual.Existencia = CostoActual.Existencia > 0 ? CostoActual.Existencia - mov.Cantidad : 0.00;
                        CostoActual.ValorTotal = CostoActual.Existencia > 0 ? CostoActual.ValorTotal - mov.CostoTotal : 0.00;

                    }
                    else if (item.Concepto.CostoAutomatico == "NO")
                    {
                        mov.CostoTotal = mov.Cantidad * mov.Costo;
                        CostoActual.Existencia = CostoActual.Existencia - mov.Cantidad;
                        CostoActual.ValorTotal = CostoActual.ValorTotal - mov.CostoTotal;


                    }



                    CostoActual.CapaAgotada = CostoActual.Existencia <= 0 ? "SI" : "NO";

                }
                return CostoActual;
            }

        }
        //Obtiene la existencia de un articulo en una fecha especificia
        public ExistenciaValorInventario ExistenciaArticulo(string articuloId, string almacenId, DateTime date, List<InventariosSaldos> Inv, Articulo Art, int dia, int mes, int ano,EmpresaContext db)
        {
            try
            {
                DateTime ultimodiamesanterior = new DateTime();
                double Entradas = 0;
                double Salidas = 0;
                double EntradasCostos = 0;
                double SalidasCostos = 0;
                double SaldoMesesAnteriores = 0;
                double ValorTotalMesesAnteriores = 0;
                List<MovimientosES> MovimientosEsCompletoServer = new List<MovimientosES>();


                //revisamos el ultimo dia del mes dado en el reporte
                var ultimodia = Inv.Find(b => b.ArticuloId == Art._id && b.AlmacenId == almacenId && b.Ano <= date.Year && b.Mes == date.Month);
                // si la fecha del reporte es igual o mayor que el ultimo dia procedemos a sacar la existencia de la tabla inventarios saldos
                if ((ultimodia != null) && date.Day >= ultimodia.UltimoDia)
                {
                    var saldomesesanteriores = Inv.FindAll(b => b.ArticuloId == articuloId && b.AlmacenId == almacenId && (b.Ano == date.Year && b.Mes <= date.Month || b.Ano < date.Year)).ToList();
                    SaldoMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradaUnidades - a.SalidasUnidades);
                    ValorTotalMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradasCosto - a.SalidasCosto);

                }
                else // de lo contrario barremos los moviemiento de los detalles de las entradas y salidas
                {
                    //ahora revisamos el ultimo dia del mes anterior
                    ultimodia = Inv.Find(b => b.ArticuloId == Art._id && b.AlmacenId == almacenId && b.Ano <= date.Year && b.Mes < date.Month || b.Ano < date.Year);
                    // date = new DateTime(date.Year, date.Month, date.Day, 11, 59, 59);

                    if (ultimodia == null) // si no hay ningun movimiento en los meses anteriores
                    {
                        //builderMovimientos.Eq("Almacen._id", almacenId) & builderMovimientos.Where(a => a.Ano <= ano && a.Mes <= mes && a.Dia <= dia)
                        var builderMovimientos = Builders<MovimientosES>.Filter.Eq("Almacen._id", almacenId) & Builders<MovimientosES>.Filter.Where(a => a.Ano <= ano && a.Mes <= mes && a.Dia <= dia);
                        //MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos).ToList();
                        MovimientosEsCompletoServer = db.MovimientosES.find(builderMovimientos,db);
                    }
                    else //si, si hay movimiento en meses anteriores
                    {
                        //(builderMovimientos & builderMovimientos.Where(a => a.Fecha > ultimodiamesanterior && (a.Ano <= ano && a.Mes <= mes && a.Dia <= dia))
                        var builderMovimientos = Builders<MovimientosES>.Filter.Eq("Almacen._id", almacenId) & Builders<MovimientosES>.Filter.Where(a => a.Fecha > ultimodiamesanterior && (a.Ano <= ano && a.Mes <= mes && a.Dia <= dia));
                        ultimodiamesanterior = new DateTime(ultimodia.Ano, ultimodia.Mes, ultimodia.UltimoDia, 23, 59, 59);
                        //MovimientosEsCompletoServer = CollectionMovimientosEs.Find(builderMovimientos).ToList();
                        MovimientosEsCompletoServer = db.MovimientosES.find(builderMovimientos, db);
                    }

                    var me = MovimientosEsCompletoServer.Where(i => i.Concepto.Naturaleza == "ENTRADA").SelectMany(l => l.Detalles_ES).Where(p => p.Articulo._id == Art._id);
                    var ms = MovimientosEsCompletoServer.Where(i => i.Concepto.Naturaleza == "SALIDA").SelectMany(l => l.Detalles_ES).Where(p => p.Articulo._id == Art._id);
                    Entradas = me.Sum(o => o.Cantidad);
                    EntradasCostos = me.Sum(o => o.CostoTotal);
                    Salidas = ms.Sum(o => o.Cantidad);
                    SalidasCostos = ms.Sum(o => o.CostoTotal);


                    var saldomesesanteriores = Inv.FindAll(b => b.ArticuloId == articuloId && b.AlmacenId == almacenId && (b.Ano == date.Year && b.Mes < date.Month || b.Ano < date.Year)).ToList();
                    SaldoMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradaUnidades - a.SalidasUnidades);
                    ValorTotalMesesAnteriores = saldomesesanteriores.Sum(a => a.EntradasCosto - a.SalidasCosto);
                }

                var existencia = SaldoMesesAnteriores + Entradas - Salidas;
                var ValorTotal = ValorTotalMesesAnteriores + EntradasCostos - SalidasCostos;

                ExistenciaValorInventario existenciaInventario = new ExistenciaValorInventario();
                existenciaInventario.Fecha = date.ToString();
                existenciaInventario.Existencia = existencia;
                existenciaInventario.ValorTotal = ValorTotal;
                existenciaInventario.CostoUnitario = ValorTotal > 0 ? ValorTotal / existencia : 0.00;
                existenciaInventario.Articulo = Art;
                existenciaInventario.SubgrupoComponente = Art.SubGrupoComponente == null ? null : Art.SubGrupoComponente;
                existenciaInventario.GrupoComponente = Art.GrupoComponente == null ? null : Art.GrupoComponente;
                //existenciaInventario.UnidadInventario = Art.UnidadInventario.Abreviatura;
                existenciaInventario.UnidadInventario = Art.UnidadInventario == null ? null : Art.UnidadInventario.Abreviatura;
                existenciaInventario.TotalEntradas = SaldoMesesAnteriores + Entradas;
                existenciaInventario.TotalSalidas = Salidas;
                existenciaInventario.movimientos = MovimientosEsCompletoServer;

                return existenciaInventario;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public int AutoIncrement(string _id, IMongoDatabase db, IClientSessionHandle session)
        {
            try
            {
                var collection = db.GetCollection<Counters>("Counters");
                var filter = Builders<Counters>.Filter.Eq(x => x._id, _id);
                var update = Builders<Counters>.Update.Inc(x => x.sequence_value, 1);
                var options = new FindOneAndUpdateOptions<Counters>
                {
                    //Sort = Builders<Counters>.Sort.Ascending("Counters"),
                    ReturnDocument = ReturnDocument.After
                };
                Counters id = collection.FindOneAndUpdate(session, filter, update, options);

                return id.sequence_value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}