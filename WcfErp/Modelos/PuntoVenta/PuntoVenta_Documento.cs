using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Ventas;
using WcfErp.Modelos.PVenta;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.PuntoVenta
{
    public class PuntoVenta_Documento : ModeloBase<PuntoVenta_Documento, EmpresaContext>
    {
        public Cajas Caja { get; set; }
        public string TipoDocto { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
       // public int Ano { get; set; }
        //public int Mes { get; set; }
        //public int Dia { get; set; }
        //public DateTime Hora { get; set; }
        public Cajeros Cajero { get; set; }
        public Clientes Cliente { get; set; }
        public Almacen Almacen { get; set; }
        //public int LugarExpedicion { get; set; } //esta nulo
        //public int Moneda { get; set; }
        public string ImpuestoIncluido { get; set; }
        //public TipodeCambio TipodeCambio { get; set; }
        public string TipoDescuento { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
        public decimal DescuentoImporte { get; set; }
        public string Estatus { get; set; }
        public string Aplicado { get; set; }
        public decimal ImporteNeto { get; set; }
        public decimal TotalImpuestos { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal ImporteDonativo { get; set; }
        //public decimal TotalFPGC { get; set; }
        public string TicketEmitido { get; set; }  //default N
        //public char FormaGlobalEmitida { get; set; }
        //public char FormaEmitida { get; set; }
        //public char Contabilizado { get; set; }
        public string SistemaOrigen { get; set; }
        //public Vendedor Vendedor { get; set; }
        /*public char CargarSun { get; set; }
        public string Persona { get; set; }
        public char TipoGenFac { get; set; }
        public char EsFacGlobal { get; set; }
        public DateTime FechaIniFacGlobal { get; set; }
        public DateTime FechaFinFacGlobal { get; set; }
        public char InclFacturadosGlobal { get; set; }
        public int AlmacenIdFacGlobal { get; set; }
        public string ReferReting { get; set; }
        public char UnidComprom { get; set; }
        public string Descripcion { get; set; }
        public int ImpuestoSustituidoId { get; set; }
        public int ImpuestoSustitutoId { get; set; }
        public char EsCFD { get; set; }
        public string ModalidadFacturacion { get; set; }
        public char Enviado { get; set; }
        public string MailEnviado { get; set; }*/
        //public Usuarios UsuarioCreador { get; set; }
        /*public char CFDICertificado { get; set; }
        public char UsoCFDI { get; set; }
        public char MetodoPagoSAT { get; set; }
        public int PartidaAjusteId { get; set; }
        public decimal PrecioOrigPartidaAjuste { get; set; }*/

        public List<PuntoVtaDet> PuntoVtaDet { get; set; }
        public List<PuntoVtaCobros> PuntoVtaCobros { get; set; }
        public List<PuntoVtaImpuestos> PuntoVtaImpuestos { get; set; }        

        protected override PuntoVenta_Documento addValues(PuntoVenta_Documento item, EmpresaContext db)
        {
            try
            {
                item.Almacen = db.Almacen.get(item.Almacen._id, "_id,Nombre", db);
                item.Caja = db.Cajas.get(item.Caja._id, "_id,Nombre", db);
                item.Cajero = db.Cajeros.get(item.Cajero._id, "_id,Nombre", db);
                //item.Cliente = db.Clientes.get(item.Cliente._id,db); //db.Clientes.get(item.Cliente._id, "_id,Nombre", db);
                foreach (PuntoVtaDet det in item.PuntoVtaDet)
                {
                    det.Articulo = db.Articulo.get(det.Articulo._id, "_id,Clave,Nombre", db);
                }

                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}