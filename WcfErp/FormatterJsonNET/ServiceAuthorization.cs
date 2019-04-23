using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;
using WcfErp.Modelos;

namespace WcfErp.FormatterJsonNET
{
    public class ServiceAuthorization : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        { // Do custom stuff here...
          //Extract the Authorization header, and parse out the credentials converting the Base64 string:
            return true;
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS") return true;
            if (operationContext.RequestContext.RequestMessage.Headers.To.AbsoluteUri == "http://localhost:60493/Servicios/WSLogin.svc/") return true;
            

            string authHeader = WebOperationContext.Current.IncomingRequest.Headers["token"];
            if ((authHeader != null) && (authHeader != string.Empty))
            {

                MongoClient client = new MongoClient(ConfigurationManager.AppSettings["pathMongo"]);
                IMongoDatabase db = client.GetDatabase("Usuarios");

                IMongoCollection<tokens> CollectionTokens = db.GetCollection<tokens>("tokens");

                tokens token = CollectionTokens.Find<tokens>(t => t.Token == authHeader).FirstOrDefault();

                if (token != null)
                {
                    //User is authrized and originating call will proceed  
                    return true;
                }
                else
                {
                    //not authorized  
                    return false;
                }
            }
            else
            {
                //No authorization header was provided, so challenge the client to provide before proceeding:  
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"MyWCFService\"");
                //Throw an exception with the associated HTTP status code equivalent to HTTP status 401  
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
        }
    }
}