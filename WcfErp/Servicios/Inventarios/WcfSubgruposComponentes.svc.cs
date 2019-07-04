using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfSubgruposComponentes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfSubgruposComponentes.svc or WcfSubgruposComponentes.svc.cs at the Solution Explorer and start debugging.
    public class WcfSubgruposComponentes : ServiceBase<SubgrupoComponente, EmpresaContext>, IWcfSubgruposComponentes
    {
        public SubgrupoComponente delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<SubgrupoComponente> searchXGrupo(string busqueda, string _id)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                var builder = Builders<SubgrupoComponente>.Filter;
                var filter = builder.Regex("Nombre", new BsonRegularExpression(busqueda, "i")) &  builder.Eq("GrupoComponente._id", _id);

                List<SubgrupoComponente> Documentos = db.SubgrupoComponente.Filters(filter);

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
