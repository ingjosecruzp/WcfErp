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

    }
}
