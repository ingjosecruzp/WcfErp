using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfErp.Modelos.PuntoVenta
{
    public class TipoImpuesto : ModeloBase<TipoImpuesto, EmpresaContext>
    {
        protected override TipoImpuesto addValues(TipoImpuesto item, EmpresaContext db)
        {
            try
            {
                foreach(TiposGravan tipo in item.TiposGravan)
                {
                    tipo.TipoImpuesto = db.TipoImpuesto.get(tipo.TipoImpuesto._id, "_Id,Nombre", db);
                }
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Naturaleza { get; set; }
        public Nullable<int> Grava { get; set; }
        public List<TiposGravan> TiposGravan { get; set; }
        public Nullable<int> Predeterminado { get; set; }

        public override void validateModel(TipoImpuesto item, EmpresaContext db)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(item.Nombre))
                    throw new Exception("Falta capturar el nombre");
                if (String.IsNullOrWhiteSpace(item.Naturaleza))
                    throw new Exception("Falta seleccionar la naturaleza");
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}