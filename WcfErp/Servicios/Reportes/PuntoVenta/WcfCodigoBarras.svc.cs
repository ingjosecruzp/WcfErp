using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Reportes.PuntoVenta;
using WcfErp.Reportes;

namespace WcfErp.Servicios.Reportes.PuntoVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfCodigoBarras" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfCodigoBarras.svc o WcfCodigoBarras.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfCodigoBarras : IWcfCodigoBarras
    {
        public List<CodigoBarra> getCodigosBarra(string GrupoId, string SubGrupoId,string ArticuloId)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                List<Articulo> LstArticulos = new List<Articulo>();

                //Selecciono el filtro subgrupo
                if (SubGrupoId != null && SubGrupoId != "")
                {
                    var filter = Builders<Articulo>.Filter.Eq("SubGrupoComponente._id", SubGrupoId);
                    LstArticulos = db.Articulo.Filters(filter, "Clave,Peso,Pureza,Paises.Abreviatura");
                }
                //Selecciono el filtro grupo
                else if (GrupoId != null && GrupoId != "")
                {
                    var filter = Builders<Articulo>.Filter.Eq("GrupoComponente._id", GrupoId);
                    LstArticulos = db.Articulo.Filters(filter, "Clave,Peso,Pureza,Paises.Abreviatura");
                }
                //Selecciono el filtro articulo
                else if (ArticuloId != null && ArticuloId != "")
                {
                    var filter = Builders<Articulo>.Filter.Eq("_id", ArticuloId);
                    LstArticulos = db.Articulo.Filters(filter, "Clave,Peso,Pureza,Paises.Abreviatura");
                }

                List<CodigoBarra> LstCodigos = new List<CodigoBarra>();

                foreach(Articulo articulo in LstArticulos)
                {
                    CodigoBarra codigo = new CodigoBarra();
                    codigo.codigo = articulo.Clave;
                    codigo.peso = articulo.Peso + " GRS";
                    codigo.procedencia =articulo.Paises != null ? "HECHO " + articulo.Paises.Abreviatura : "HECHO N/A";
                    //codigo.pureza = articulo.Pureza.ToString();
                    codigo.pureza = ".925";
                    codigo.precio = "78 USD";

                    LstCodigos.Add(codigo);
                }

                return LstCodigos;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public string VerReporte(string parametros)
        {
            try
            {
                var jsonObject = JObject.Parse(parametros);

                List<reportParameter> JasperParametros = new List<reportParameter>();

                foreach (var p in jsonObject)
                {
                    //Console.WriteLine(p.Value.Type); // eg. integer

                    reportParameter param = new reportParameter();
                    param.name = p.Key;
                    param.value.Add(p.Value.ToString());

                    JasperParametros.Add(param);
                }

                //Agrega token
                reportParameter paramToken = new reportParameter();
                paramToken.name = "Token";

                OperationContext currentContext = OperationContext.Current;
                HttpRequestMessageProperty reqMsg = currentContext.IncomingMessageProperties["httpRequest"] as HttpRequestMessageProperty;
                string authToken = reqMsg.Headers["Token"];

                paramToken.value.Add(authToken);
                JasperParametros.Add(paramToken);

                reportParameter param1 = new reportParameter();
                param1.name = "empresa";
                param1.value.Add(getKeyToken("razonsocial", "token"));

                JasperParametros.Add(param1);

                string Archivo = GetTimestamp(DateTime.Now);
                string extension = "pdf";

                ReportesPFD VmReporte = new ReportesPFD("/ERP/CodigosBarra", JasperParametros, extension, Archivo);

                return Archivo + "." + extension;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        //Metodo para dar respuesta las peticiones OPTION CORS
        public void GetOptions()
        {
        }
        public string getKeyToken(string key, string Token)
        {
            try
            {
                OperationContext currentContext = OperationContext.Current;
                HttpRequestMessageProperty reqMsg = currentContext.IncomingMessageProperties["httpRequest"] as HttpRequestMessageProperty;
                string authToken = reqMsg.Headers[Token];
                string value;
                if (authToken != "")
                {
                    var payload = JWT.JsonWebToken.DecodeToObject(authToken, "pwjrnew") as IDictionary<string, object>;
                    value = payload.ContainsKey(key) ? payload[key].ToString() : "";
                }
                else
                {
                    value = "";
                }
                return value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

