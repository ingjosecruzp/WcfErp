using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfTipoImpuesto" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfTipoImpuesto.svc o WcfTipoImpuesto.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfTipoImpuesto : ServiceBase<TipoImpuesto, EmpresaContext>, IWcfTipoImpuesto
    {
        public List<TipoImpuesto> searchXTipoImpuesto(string busqueda)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                var builder = Builders<TipoImpuesto>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) & builder.Eq("Naturaleza", "IMPUESTO");

                List<TipoImpuesto> Documentos = db.TipoImpuesto.Filters(filter);

                return Documentos;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        
    }

    
}
