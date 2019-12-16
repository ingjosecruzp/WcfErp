using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WcfErp.Modelos;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.Reportes.PuntoVenta;

namespace WcfErp.Servicios.Reportes.PuntoVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfCorteCaja" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfCorteCaja.svc o WcfCorteCaja.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfCorteCaja : IWcfCorteCaja
    {
        public CorteCaja VerReporte(string FechaInicio,string FechaFinal)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();
                List<PuntoVenta_Documento> LstVentas = new List<PuntoVenta_Documento>(); 
               if(FechaInicio!="" && FechaFinal != null)
                {
                    var filter = Builders<PuntoVenta_Documento>.Filter.Gt("Fecha", FechaInicio) & Builders<PuntoVenta_Documento>.Filter.Lt("Fecha", FechaFinal);
                    LstVentas = db.PuntoVenta_Documento.Filters(filter,"");
                }

                CorteCaja PvCorteCaja = new CorteCaja();
                PvCorteCaja.LstVentas = new List<PVentas>();
                decimal TotalMXP = 0;
                decimal TotalUSD = 0;
                decimal TotalEfectivoMXP = 0;
                decimal TotalEfectivoUSD = 0;

                List<PVentas> LstPVentas = new List<PVentas>();

                foreach (PuntoVenta_Documento Venta in LstVentas)
                {
                    PVentas LVenta = new PVentas();
                    LVenta.VtaDetalle = new List<PuntoVtaDet>();
                    LVenta.Folio = Venta.Folio;
                    LVenta.Fecha = Venta.Fecha;
                    LVenta.TipoCambio = 0;
                    LVenta.Fondo = 0;

                    foreach (PuntoVtaDet VentaDet in Venta.PuntoVtaDet)
                    {
                        PuntoVtaDet PvDet = new PuntoVtaDet();
                       
                        Articulo PvArticulo = new Articulo();
                        PvArticulo.Nombre = VentaDet.Articulo.Nombre;
                        PvArticulo.Clave = VentaDet.Articulo.Clave;
                        PvDet.Articulo = PvArticulo;
                       // PvDet.Articulo.Nombre = VentaDet.Articulo.Nombre;
                        //PvDet.Articulo.Clave = VentaDet.Articulo.Clave;
                        PvDet.Cantidad = VentaDet.Cantidad;
                        PvDet.PrecioUnitario = VentaDet.PrecioUnitario;
                        PvDet.ImpuestoPorUnidad = VentaDet.ImpuestoPorUnidad;
                        PvDet.PorcentajeDescto = VentaDet.PorcentajeDescto;
                        PvDet.PrecioTotalNeto = VentaDet.PrecioTotalNeto;
                        PvDet.DescuentoArt = VentaDet.DescuentoArt;
                        PvDet.DescuentoExtra = VentaDet.DescuentoExtra;

                        LVenta.VtaDetalle.Add(PvDet);
                    }
                    LVenta.VtaCobros = new List<PuntoVtaCobros>();
                    decimal tarjetas = 0;
                    decimal efectivo = 0;
                    foreach (PuntoVtaCobros VentaCobro in Venta.PuntoVtaCobros)
                    {
                        PuntoVtaCobros PvCobro = new PuntoVtaCobros();
                        PvCobro.Tipo = VentaCobro.Tipo;
                        if (VentaCobro.Tipo == "TARJETA") { tarjetas = tarjetas + VentaCobro.Importe; }
                        if (VentaCobro.Tipo == "EFECTIVO") { efectivo = efectivo + VentaCobro.Importe; }
                        PvCobro.Importe = VentaCobro.Importe;
                        PvCobro.ImporteMonedaDoc = VentaCobro.ImporteMonedaDoc;
                        
                        LVenta.VtaCobros.Add(PvCobro);
                    }
                    LVenta.Efectivo =efectivo;
                    LVenta.Tajetas = tarjetas;
                    
                    LstPVentas.Add(LVenta);
                    PvCorteCaja.LstVentas.Add(LVenta);
                }
                PvCorteCaja.TotalMXP = TotalMXP;
                PvCorteCaja.TotalUSD = TotalUSD;
                PvCorteCaja.TotalEfectivoMXP = 0;
                PvCorteCaja.TotalEfectivoUSD = 0;
                PvCorteCaja.Tajetas = 0;
                PvCorteCaja.Fondo = 0;
                return PvCorteCaja;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
