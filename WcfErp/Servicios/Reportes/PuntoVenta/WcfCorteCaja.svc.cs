using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.PVenta;
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
                decimal TotalMXN = 0;
                decimal TotalUSD = 0;
                decimal TotalEfectivoMXN = 0;
                decimal TotalEfectivoUSD = 0;
                decimal TotalTarjetasMXN = 0;
                decimal TotalTarjetasUSD = 0;

                List<PVentas> LstPVentas = new List<PVentas>();
                
                foreach (PuntoVenta_Documento Venta in LstVentas)
                {
                    PVentas LVenta = new PVentas();
                    LVenta.VtaDetalle = new List<PuntoVtaDet>();
                    
                    LVenta.Folio = Venta.Folio;
                    LVenta.Fecha = Venta.Fecha;
                    //VENTAS DETALLE                  
                    foreach (PuntoVtaDet VentaDet in Venta.PuntoVtaDet)
                    {
                        PuntoVtaDet PvDet = new PuntoVtaDet();
                       
                        Articulo PvArticulo = new Articulo();
                        PvArticulo.Nombre = VentaDet.Articulo.Nombre;
                        PvArticulo.Clave = VentaDet.Articulo.Clave;
                        PvDet.Articulo = PvArticulo;
                        PvDet.Cantidad = VentaDet.Cantidad;
                        PvDet.PrecioUnitario = VentaDet.PrecioUnitario;
                        PvDet.ImpuestoPorUnidad = VentaDet.ImpuestoPorUnidad;
                        PvDet.PorcentajeDescto = VentaDet.PorcentajeDescto;
                        PvDet.PrecioTotalNeto = VentaDet.PrecioTotalNeto;
                        PvDet.DescuentoArt = VentaDet.DescuentoArt;
                        PvDet.DescuentoExtra = VentaDet.DescuentoExtra;
                        
                        LVenta.VtaDetalle.Add(PvDet);
                    }
                    
                    //VENTAS COBROS
                    LVenta.VtaCobros = new List<PuntoVtaCobros>();
                    decimal tarjetasusd = 0;
                    decimal tarjetasmxn = 0;
                    decimal efectivousd = 0;
                    decimal efectivomxn = 0;
                    foreach (PuntoVtaCobros VentaCobro in Venta.PuntoVtaCobros)
                    {
                        PuntoVtaCobros PvCobro = new PuntoVtaCobros();
                        if (VentaCobro.TipodeCambio.Moneda.Simbolo == "MXN")
                        {
                            if (VentaCobro.Tipo == "TARJETA") { tarjetasmxn = tarjetasmxn + VentaCobro.Importe; }
                            if (VentaCobro.Tipo == "EFECTIVO") { efectivomxn = efectivomxn + VentaCobro.Importe; }
                        }
                        else
                        {
                            if (VentaCobro.Tipo == "TARJETA") { tarjetasusd = tarjetasusd + VentaCobro.Importe; }
                            if (VentaCobro.Tipo == "EFECTIVO") { efectivousd = efectivousd + VentaCobro.Importe; }
                        }
                        
                        PvCobro.TipodeCambio = new TipodeCambio();
                        PvCobro.TipodeCambio.Moneda = new Moneda();
                        PvCobro.Tipo = VentaCobro.Tipo;
                        PvCobro.TipodeCambio.Moneda.Simbolo = VentaCobro.TipodeCambio.Moneda.Simbolo;
                        PvCobro.TipodeCambio.TipoCambio = VentaCobro.TipodeCambio.TipoCambio;
                        
                        PvCobro.Importe = VentaCobro.Importe;
                        PvCobro.ImporteMonedaDoc = VentaCobro.ImporteMonedaDoc;
                        
                        LVenta.VtaCobros.Add(PvCobro);
                    }

                    LVenta.EfectivoMXN = efectivomxn;
                    LVenta.TajetasMXN = tarjetasmxn;
                    LVenta.EfectivoUSD = efectivousd;
                    LVenta.TajetasUSD = tarjetasusd;
                    LVenta.TotalMXN = efectivomxn + tarjetasmxn;
                    LVenta.TotalUSD = efectivousd + tarjetasusd;
                    TotalEfectivoMXN = TotalEfectivoMXN + efectivomxn;
                    TotalEfectivoUSD = TotalEfectivoUSD + efectivousd;
                    TotalTarjetasMXN = TotalTarjetasMXN + tarjetasmxn;
                    TotalTarjetasUSD = TotalTarjetasUSD + tarjetasusd;
                    TotalMXN = TotalMXN+LVenta.TotalMXN;
                    TotalUSD = TotalUSD+LVenta.TotalUSD;

                    LstPVentas.Add(LVenta);
                    PvCorteCaja.LstVentas.Add(LVenta);
                }

                //VENTAS POR VENDEDOR
                List<VtasVendedor> VentasPorVendedor = LstVentas.GroupBy(t => t.Vendedor._id)
                           .Select(t => new VtasVendedor
                           {
                               //Id = t.Key,0
                               Vendedor = t.FirstOrDefault().Vendedor.Nombre,
                               NumVentas = t.Count(),
                               NumPiezas = t.Sum(ta => ta.PuntoVtaDet.ToList().Sum( a => a.Cantidad)),
                               //TotalMXP = t.Sum(ta => ta.PuntoVtaCobros.ToList().Where(a => a.TipodeCambio.Moneda.Simbolo=="MXN").Sum(a => a.Importe)),
                               //TotalUSD = t.Sum(ta => ta.PuntoVtaCobros.ToList().Where(a => a.TipodeCambio.Moneda.Simbolo == "USD").Sum(a => a.Importe)),
                               TotalVentas = t.Sum(ta => ta.TotalVenta)
                           }).ToList();
                PvCorteCaja.LstVtasVendedor = new List<VtasVendedor>();
                foreach (VtasVendedor PvVentasVendedor in VentasPorVendedor)
                {
                    VtasVendedor VentasVendedor = new VtasVendedor();
                    VentasVendedor.Vendedor = PvVentasVendedor.Vendedor;
                    VentasVendedor.NumVentas = PvVentasVendedor.NumVentas;
                    VentasVendedor.NumPiezas = PvVentasVendedor.NumPiezas;
                    VentasVendedor.TotalVentas = PvVentasVendedor.TotalVentas;

                    PvCorteCaja.LstVtasVendedor.Add(VentasVendedor);
                }

                PvCorteCaja.TotalMXN = TotalMXN;
                PvCorteCaja.TotalUSD = TotalUSD;
                PvCorteCaja.EfectivoMXN = TotalEfectivoMXN;
                PvCorteCaja.EfectivoUSD = TotalEfectivoUSD;
                PvCorteCaja.TajetasUSD = TotalTarjetasUSD;
                PvCorteCaja.TajetasMXN = TotalTarjetasMXN;
                PvCorteCaja.Fondo = 0;
                
                return PvCorteCaja;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
