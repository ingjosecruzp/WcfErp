﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.PVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfAperturaCajas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfAperturaCajas.svc o WcfAperturaCajas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfAperturaCajas : ServiceBase<Movtos_Cajas, EmpresaContext>, IWcfAperturaCajas
    {

        public override Movtos_Cajas add(Movtos_Cajas item)
        {
            try
            {

                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {
                    WcfCajas caja = new WcfCajas();
                    caja.CambiarEstadoCaja(item.Cajas._id, "ABIERTA",session);

                    item.TipoMovto = "Apertura";
                    return base.add(item);
                }
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }

        public override List<Movtos_Cajas> lazyloading(string cadena, string skip = null)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                var builder_Mtocajas = Builders<Movtos_Cajas>.Filter;
                var filter_Mtocajas = builder_Mtocajas.Eq("TipoMovto", "Apertura");

                List<Movtos_Cajas> Lista = db.Movtos_Cajas.Filters(filter_Mtocajas, cadena, skip);

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
