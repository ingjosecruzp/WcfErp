using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.PuntoVenta
{
    public class Cajeros : ModeloBase<Cajeros, EmpresaContext>
    {
        public Usuarios Usuarios { get; set; }
        public string ImprimirReportes { get; set; }
        public int PermiteImprimir { get; set; }
        public int OperaCajas { get; set; }
        public List<CajerosCajas> CajerosCajas { get; set; }
        public int AbreCajas { get; set; }
        public List<CajerosCajasAbrir> CajerosCajasAbrir { get; set; }

        protected override Cajeros addValues(Cajeros item, EmpresaContext db)
        {
            try
            {
                UsuarioContext db2 = new UsuarioContext();

                item.Usuarios = db2.Usuarios.get(item.Usuarios._id,"_Id,Nombre,NombreCompleto", db2);
                foreach (CajerosCajas cajasopera in item.CajerosCajas)
                {
                    cajasopera.CajasOp = db.Cajas.get(cajasopera.CajasOp._id, "_id,Nombre", db);
                }

                foreach (CajerosCajasAbrir cajasabre in item.CajerosCajasAbrir)
                {
                    cajasabre.CajasAb = db.Cajas.get(cajasabre.CajasAb._id, "_id,Nombre", db);
                }

                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void validateModel(Cajeros item, EmpresaContext db)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(item.Nombre))
                    throw new Exception("Falta capturar el nombre");
                if (String.IsNullOrWhiteSpace(item.Usuarios._id))
                    throw new Exception("Falta seleccionar el usuario");
                if (String.IsNullOrWhiteSpace(item.ImprimirReportes))
                    throw new Exception("Falta seleccionar imprimir los reportes de");
            }
            catch (Exception)
            {
                throw;

            }
        }
    }

    
}