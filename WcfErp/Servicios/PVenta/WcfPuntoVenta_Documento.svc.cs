using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfPuntoVenta_Documento" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfPuntoVenta_Documento.svc o WcfPuntoVenta_Documento.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfPuntoVenta_Documento : ServiceBase<PuntoVenta_Documento, EmpresaContext>,  IWcfPuntoVenta_Documento
    {
        public override PuntoVenta_Documento add(PuntoVenta_Documento item)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {
                    session.StartTransaction();


                    var builder = Builders<TipodeCambio>.Sort;
                    var filter = builder.Descending("Fecha");

                    List<TipodeCambio> Lst_Tipos = db.TipodeCambio.Filters(filter);
                    String monedaAnterior = "";
                    foreach (PuntoVtaCobros cobro in item.PuntoVtaCobros)
                    {
                        Boolean entro = false;
                        String moneda = "MXN";
                        if (cobro.Tipo.Contains("DLS"))
                        {
                            moneda = "DLS";
                        }
                        foreach (TipodeCambio cambio in Lst_Tipos)
                        {
                            if(monedaAnterior != moneda)
                            {
                                entro = false;
                            }

                            if (cambio.Moneda.Simbolo == moneda && entro == false)
                            {
                                cobro.TipodeCambio = cambio;
                                entro = true;
                                monedaAnterior = moneda;
                            }
                        }
                    }

                    db.PuntoVenta_Documento.add(item, db, session);

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

        public Movtos_Cajas validarApertura()
        {
            try
            {
                string usuario = getKeyToken("user", "token");

                EmpresaContext db = new EmpresaContext();

                var builder = Builders<Cajeros>.Filter;
                var filter = builder.Eq("Usuarios.Nombre", usuario);

                List<Cajeros> LstCajeros = db.Cajeros.find(filter, db).ToList();

                if (LstCajeros.Count == 0)
                    throw new Exception("El usuario no es cajero");

                //una vez que es cajero, se tiene que ver si tiene una apertura con una caja
                //si tiene una apertura, que busque la caja a la que hace referencia para ver si está abierta
                //si no está abierta, que haga una apertura
                //si está abierta, que retorne la apertura

                foreach (Cajeros caj in LstCajeros)
                {
                    var builder_Mtocajas = Builders<Movtos_Cajas>.Filter;
                    var filter_Mtocajas = builder_Mtocajas.Eq("TipoMovto", "Apertura") & builder_Mtocajas.Eq("Cajeros._id", caj._id);

                    List<Movtos_Cajas> LstCajasAbiertas = db.Movtos_Cajas.find_apertura(filter_Mtocajas, 1, "_id,Cajas,Cajeros", db).ToList();

                    if (LstCajasAbiertas.Count == 0)
                        throw new Exception("El cajero no tiene apertura de caja");

                    foreach (Movtos_Cajas abierta in LstCajasAbiertas)
                    {
                        var builder_cajas = Builders<Cajas>.Filter;
                        var filter_cajas = builder_cajas.Eq("_id", abierta.Cajas._id) & builder_cajas.Eq("Estado", "ABIERTA");

                        List<Cajas> LstCajas = db.Cajas.find(filter_cajas, db).ToList();

                        if (LstCajas.Count != 0)
                           return abierta;
                    }
                }
                throw new Exception("El cajero no tiene apertura de caja");

                return null;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
    }
}
