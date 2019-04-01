using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WSLogin" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WSLogin.svc or WSLogin.svc.cs at the Solution Explorer and start debugging.
    public class WSLogin : IWSLogin
    {
        public string login(Usuarios item)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://adminErp:pwjrnew@18.191.252.222:27017/?authSource=admin");
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<Usuarios> Collection = db.GetCollection<Usuarios>("Usuarios");
                IMongoCollection<tokens> CollectionTokens = db.GetCollection<tokens>("tokens");

                Usuarios user = Collection.Find<Usuarios>(d => d.Nombre == item.Nombre && d.Contrasena== item.Contrasena).FirstOrDefault();
                //Usuarios user = Collection.Find<Usuarios>(d => d.Nombre == item.Nombre).FirstOrDefault();

                if (user != null)
                {
                    tokens Token = new tokens();

                    Token.Token = GenerarToken(user.id, user.Nombre);
                    Token.FechaCreacion = DateTime.Now;

                    CollectionTokens.InsertOne(Token);

                    return Token.Token;
                }
                else
                {
                    throw new Exception("Datos incorrectos");
                }

            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                string error = null;

                error = ex.Message;

                response.StatusCode = HttpStatusCode.InternalServerError;
                response.StatusDescription = error;
                return null;
            }
        }
        public string GenerarToken(string id, string user)
        {
            try
            {
                var payload = new Dictionary<string, object>()
                {
                    { "id", id },
                    { "user", user },
                    { "Fecha",DateTime.Now}
                };
                var secretKey = "pwjrnew";
                return JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GetOptions()
        {
        }
    }
}
