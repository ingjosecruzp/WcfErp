using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Modelos.Compras
{
    public class DoctoCompras : ModeloBase<DoctoCompras, EmpresaContext>
    {
        //datos embebidos en los objetos ALMACEN_ID,CONCEPTO_IN_ID,NATURALEZA_CONCEPTO,ALMACEN_DESTINO_ID
        public Almacen Almacen { get; set; }
        public Proveedor proveedor { get; set; }
        public Concepto Concepto { get; set; }
        public string Folio { get; set; }
        public string Factura { get; set; }
        public DateTime Fecha { get; set; }
        public string Cancelado { get; set; }
        public string Descripcion { get; set; }
        public string Sistema_Origen { get; set; }
        public List<DetallesCompras> DetallesCompras { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public double TotaldetalleEntrada { get; set; }

        public DoctoCompras()
        {
            this.DetallesCompras = new List<DetallesCompras>();
        }

        public void ValidarModel(DoctoCompras item)
        {
            try
            {
                if (item.DetallesCompras.Count == 0)//sin detalles
                    throw new Exception("No ha realizado ningun movimiento, no es posible guardar");
                if (item.DetallesCompras.Where(c => c.Cantidad == 0).Count() > 0)//Cantidad de entrada de articulos en 0
                    throw new Exception("No se permite guardar  componentes en cantidad cero");
                if (item.Concepto.CostoAutomatico == "NO" && String.IsNullOrWhiteSpace(item.Folio))//Sin folio
                    throw new Exception("Falta capturar el folio");
                if (String.IsNullOrWhiteSpace(item.Concepto._id))//Sin Concepto
                    throw new Exception("Falta capturar el concepto");
                if (String.IsNullOrWhiteSpace(item.Fecha.ToString()))//Sin Fecha
                    throw new Exception("Falta capturar la fecha");
                if (String.IsNullOrWhiteSpace(item.Almacen._id))//Sin Almacen
                    throw new Exception("Falta capturar el almacen");
                if (String.IsNullOrWhiteSpace(item.Almacen._id))//Sin Almacen
                    throw new Exception("Falta capturar el almacen");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
