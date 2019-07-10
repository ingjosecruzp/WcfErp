using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Reportes.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfInventarioFisico" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfInventarioFisico.svc o WcfInventarioFisico.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfInventarioFisico :  ServiceBase<InventarioFisico, EmpresaContext>, IWcfInventarioFisico
    {
        public override InventarioFisico add(InventarioFisico item)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {
                    session.StartTransaction();

                    //Genera la clave del articulo
                    item.Folio = AutoIncrement("InventarioFisico", db.db, session).ToString();
                    item.Estado = "PENDIENTE";

                    db.InventarioFisico.add(item,db,session);

                    session.CommitTransaction();
                }


                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public override InventarioFisico delete(string id)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                InventarioFisico DocumentoInventario = db.InventarioFisico.get(id, db);

                if (DocumentoInventario.Estado == "APLICADO")
                    throw new Exception("No se puede eliminar una toma de inventario ya aplicada");

                db.InventarioFisico.delete(id, db);

                return null;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public string aplicarInventario(string id)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {
                    session.StartTransaction();

                    InventarioFisico DocumentoInventario = db.InventarioFisico.get(id, db);

                    if (DocumentoInventario.Estado == "APLICADO")
                        throw new Exception("Esta toma de inventario ya se encuentra aplicada,no es posible continuar");


                    MovimientosES documentoentrada = new MovimientosES();
                    documentoentrada.Concepto = db.Concepto.get("5d19676ba9c67230a05ebe7b", db); ////Concepto de entrada por toma de inventatario fisico
                    documentoentrada.Almacen  = DocumentoInventario.Almacen;
                    documentoentrada.Fecha = DocumentoInventario.Fecha;
                    documentoentrada.Descripcion = "Toma de inventario con el Folio" + DocumentoInventario.Folio;
                    documentoentrada.Sistema_Origen = "IF";
                    documentoentrada.Cancelado = "NO";

                    MovimientosES documentosalida = new MovimientosES();
                    documentosalida.Concepto = db.Concepto.get("5d196788a9c67230a05ebe7c", db); ////Concepto de entrada por toma de inventatario fisico
                    documentosalida.Almacen = DocumentoInventario.Almacen;
                    documentosalida.Fecha = DocumentoInventario.Fecha;
                    documentosalida.Descripcion = "Toma de inventario con el Folio " + DocumentoInventario.Folio;
                    documentosalida.Sistema_Origen = "IF";
                    documentosalida.Cancelado = "NO";

                    Inventarios inv = new Inventarios();

                    //Separo la fecha del doumento en dia mes y año
                    int dia = DocumentoInventario.Fecha.Day;
                    int mes = DocumentoInventario.Fecha.Month;
                    int ano = DocumentoInventario.Fecha.Year;

                    //Recolectamos en una lista los ids que nos manda el cliente
                    var Ids = (from an in DocumentoInventario.InventarioFisicoDetalle select an.Articulo._id).ToList();

                    //Selecciono los articulos que no estan incluido en el invetario fisico
                    //var filtroArticulos = Builders<Articulo>.Filter.Nin("_id", Ids);
                    //List<Articulo> LstArticulosNoIncluidos = db.Articulo.find(filtroArticulos, "_id,Nombre,Clave,UnidadInventario", db);

                    //Genero el detalle para los articulso que no se encuntran
                    /*foreach(Articulo art in LstArticulosNoIncluidos)
                    {
                        DocumentoInventario.InventarioFisicoDetalle.Add(new InventarioFisicoDetalle
                        {
                            ExistenciaFisica = 0,
                            Articulo = art
                        });
                    }*/

                    //var builderSaldos = Builders<InventariosSaldos>.Filter.In("ArticuloId", Ids) & Builders<InventariosSaldos>.Filter.Eq("AlmacenId", DocumentoInventario.Almacen._id);
                    var builderSaldos = Builders<InventariosSaldos>.Filter.Eq("AlmacenId", DocumentoInventario.Almacen._id);
                    List<InventariosSaldos> InventariosSaldosCompletoServer = db.InventariosSaldos.find(builderSaldos, db);

                    //var List = InventariosSaldosCompletoServer.GroupBy(a => a.ArticuloId).Select(a => a.ToList().First()).ToList();

                    foreach (InventariosSaldos saldo in InventariosSaldosCompletoServer.GroupBy(a => a.ArticuloId).Select(a => a.ToList().First()).ToList())
                    {
                        bool ArticuloIncluido = true; //Controla si articulo venia capturado por el usuario
                        InventarioFisicoDetalle detalle = DocumentoInventario.InventarioFisicoDetalle.Where(a => a.Articulo._id == saldo.ArticuloId).SingleOrDefault();

                        if(detalle == null)
                        {
                            ArticuloIncluido = false;
                            detalle = new InventarioFisicoDetalle();
                            detalle.Articulo = new Articulo();
                            detalle.Articulo._id = saldo.ArticuloId;
                        }
                        
                        ExistenciaValorInventario exitencia = inv.ExistenciaArticulo(detalle.Articulo._id, DocumentoInventario.Almacen.id, DocumentoInventario.Fecha, InventariosSaldosCompletoServer, detalle.Articulo, dia, mes, ano, db);
                        detalle.ExistenciaTeorica = exitencia.Existencia;

                        if (detalle.ExistenciaTeorica != 0.0 && detalle.ExistenciaFisica != 0)
                        {
                            if (ArticuloIncluido == false)
                            {
                                DocumentoInventario.InventarioFisicoDetalle.Add(new InventarioFisicoDetalle
                                {
                                    ExistenciaFisica = 0,
                                    Articulo = db.Articulo.get(detalle.Articulo._id, "_id,Clave,Nombre,UnidadInventario", db)
                                });
                            }

                            detalle.Diferencia = detalle.ExistenciaTeorica - detalle.ExistenciaFisica;

                            //Falta agregar el tipo de seguimiento al articulo
                            InventarioNormal(detalle, documentoentrada, documentosalida, db);
                        }
                    }

                    /*foreach (InventarioFisicoDetalle detalle in DocumentoInventario.InventarioFisicoDetalle)
                    {
                        ExistenciaValorInventario exitencia = inv.ExistenciaArticulo(detalle.Articulo._id, DocumentoInventario.Almacen.id, DocumentoInventario.Fecha, InventariosSaldosCompletoServer, detalle.Articulo, dia, mes, ano, db);
                        detalle.ExistenciaTeorica = exitencia.Existencia;
                        
                        if(detalle.ExistenciaTeorica != 0.0)
                        { 
                            detalle.Diferencia = detalle.ExistenciaTeorica - detalle.ExistenciaFisica;
                            DocumentoInventario.InventarioFisicoDetalle.Add(new InventarioFisicoDetalle
                            {
                                ExistenciaFisica = 0,
                                Articulo = exitencia.Articulo
                            });
                        }

                        //Falta agregar el tipo de seguimiento al articulo
                        InventarioNormal(detalle, documentoentrada, documentosalida, db);
                    }*/

                    if(documentoentrada.Detalles_ES.Count > 0)
                    {
                        inv.add(documentoentrada, db, session);
                    }
                    if (documentosalida.Detalles_ES.Count > 0)
                    {
                        inv.add(documentosalida, db, session);
                    }

                    DocumentoInventario.Estado = "APLICADO";
                    DocumentoInventario.FechaHoraAplicacion = DateTime.Now;

                    db.InventarioFisico.update(DocumentoInventario, DocumentoInventario._id, db, session);

                    session.CommitTransaction();
                }

                    return "";
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public void InventarioNormal(InventarioFisicoDetalle detalle,MovimientosES documentoEntrada,MovimientosES documentoSalida,EmpresaContext db)
        {
            try
            {
                if(detalle.Diferencia < 0.0) //Entrada
                {
                    documentoEntrada.Detalles_ES.Add(new Detalles_ES
                    {
                        Articulo = detalle.Articulo,
                        Cantidad = Math.Abs((double)detalle.Diferencia),
                        Clave    = detalle.Articulo.Clave,
                    });
                }
                else if(detalle.Diferencia > 0.0) //Salida
                {
                    documentoSalida.Detalles_ES.Add(new Detalles_ES
                    {
                        Articulo = detalle.Articulo,
                        Cantidad = Math.Abs((double)detalle.Diferencia),
                        Clave = detalle.Articulo.Clave,
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
