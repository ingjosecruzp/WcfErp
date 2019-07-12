using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.PuntoVenta
{
    public class Impuestos : ModeloBase<Impuestos, EmpresaContext>
    {
        public TipoImpuesto TipoImpuesto { get; set; }
        public string TipoCalculo { get; set; }
        public double Tasa { get; set; }
        public string TipoIva { get; set; }
        public string Clave { get; set; }
        public int Predeterminado { get; set; }

        protected override Impuestos addValues(Impuestos item, EmpresaContext db)
        {
            try
            {
                item.TipoImpuesto = db.TipoImpuesto.get(item.TipoImpuesto._id, db);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void validateModel(Impuestos item, EmpresaContext db)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(item.Nombre))
                    throw new Exception("Falta capturar el nombre");
                if (String.IsNullOrWhiteSpace(item.TipoCalculo))
                    throw new Exception("Falta seleccionar el tipo de calculo");
                if (String.IsNullOrWhiteSpace(item.TipoImpuesto._id))
                    throw new Exception("Falta seleccionar el tipo de impuesto");
                if (double.IsNaN(item.Tasa))
                    throw new Exception("Indique un valor numerico de 0 o mayor para la Tasa");
                if(item.TipoImpuesto._id== "5d23db9e92a3d90df00280ed" && string.IsNullOrWhiteSpace(item.TipoIva))
                    throw new Exception("Falta seleccionar el tipo de IVA");          
            }
            catch (Exception)
            {
                throw;

            }
        }
    }

    
}