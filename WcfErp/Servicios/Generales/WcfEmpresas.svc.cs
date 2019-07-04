using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Generales;

namespace WcfErp.Servicios.Generales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfEmpresas" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfEmpresas.svc or WcfEmpresas.svc.cs at the Solution Explorer and start debugging.
    public class WcfEmpresas : ServiceBase<BDEmpresas,UsuarioContext>, IWcfEmpresas
    {

        //Tipo add(Tipo item);
        public override BDEmpresas add(BDEmpresas bDEmpresas)
        {
            try
            {
                UsuarioContext bd = new UsuarioContext();

                //Genera la empresa
                //BDEmpresas empresa = base.add(bDEmpresas, "Usuarios");
                BDEmpresas empresa = bd.BDEmpresas.add(bDEmpresas, bd);

                //Clona la base de datos principal
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("admin");

                var cadena = String.Format(@"{{ copydb: 1, fromdb: '{0}', todb: '{1}'}}", "AAA010101AAA", empresa.RFC);
                var command = new JsonCommand<BsonDocument>(cadena);
                db.RunCommand(command);

                return empresa;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }


        public List<BDEmpresas> getEmpresasUsuarios()
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<Usuarios> CollectionUsuarios = db.GetCollection<Usuarios>("Usuarios");
                IMongoCollection<Roles> CollectionRoles = db.GetCollection<Roles>("Roles");

                Usuarios user = CollectionUsuarios.Find<Usuarios>(d => d._id == getKeyToken("id","token")).FirstOrDefault();


                List<BDEmpresas> LstEmpresas = new List<BDEmpresas>();


                foreach(Roles rol in user.Roles)
                {
                    Roles roles=CollectionRoles.Find<Roles>(d => d._id == rol._id).FirstOrDefault();
                    foreach (BDEmpresas empresa in roles.BDEmpresas)
                    {
                        BDEmpresas empresaPermiso = new BDEmpresas();
                        empresaPermiso.RFC = empresa.RFC;
                        empresaPermiso.RazonSocial = empresa.RazonSocial;
                        empresaPermiso._id = empresa._id;

                        LstEmpresas.Add(empresaPermiso);
                    }
                }

                return LstEmpresas;
            }
            catch (Exception ex)
            {

                Error(ex, "");
                return null;
            }
        }

        public string selectEmpresa(string EmpresaId)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<Usuarios> CollectionUsuarios = db.GetCollection<Usuarios>("Usuarios");
                IMongoCollection<Roles> CollectionRoles = db.GetCollection<Roles>("Roles");
                IMongoCollection<BDEmpresas> CollectionEmpresas = db.GetCollection<BDEmpresas>("BDEmpresas");

                Usuarios user = CollectionUsuarios.Find<Usuarios>(d => d._id == getKeyToken("id", "token")).FirstOrDefault();


                //Verifica si se tiene permiso para la empresa seleccionada
                foreach (Roles rol in user.Roles)
                {
                    Roles roles = CollectionRoles.Find<Roles>(d => d._id == rol._id && d.BDEmpresas.Any(t => t._id == EmpresaId)).FirstOrDefault();
                    if(roles != null)
                    {
                        BDEmpresas empresa = CollectionEmpresas.Find<BDEmpresas>(e => e._id == EmpresaId).FirstOrDefault();
                        return updateToken(user._id, user.Nombre, empresa,db);
                    }
                }

                throw new Exception("No tiene permiso para la base de datos seleccioanda");
            
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public string updateToken(string id, string user, BDEmpresas empresa,IMongoDatabase db)
        {
            try
            {
                OperationContext currentContext = OperationContext.Current;
                HttpRequestMessageProperty reqMsg = currentContext.IncomingMessageProperties["httpRequest"] as HttpRequestMessageProperty;

                IMongoCollection<tokens> CollectionTokens = db.GetCollection<tokens>("tokens");

                string Nuevotoken = GenerarToken(id, user, empresa);
                string authToken = reqMsg.Headers["token"];

                tokens token = CollectionTokens.Find<tokens>(t => t.Token == authToken).FirstOrDefault();
                token.Token = Nuevotoken;
                token.Usuario = user;
                token.Nombre = user;

                var filter = Builders<tokens>.Filter.Eq(s => s.Token, authToken);

               CollectionTokens.ReplaceOne(filter, token);

                return Nuevotoken;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string GenerarToken(string id, string user, BDEmpresas empresa)
        {
            try
            {
                var payload = new Dictionary<string, object>()
                {
                    { "id", id },
                    { "user", user },
                    { "empresa", empresa.RFC },
                    { "razonsocial", empresa.RazonSocial },
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
    }
}
