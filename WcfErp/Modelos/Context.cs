using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace WcfErp.Modelos
{
    public class Context
    {
        public ModeloBase<TEntity,TContext> Set<TEntity,TContext>() where TEntity : ModeloBase<TEntity, TContext>
                                                          where TContext :  Context
        {

            return (ModeloBase<TEntity,TContext>)this.GetType().GetProperty(typeof(TEntity).Name).GetValue(this, null);
        }
        public string GetConnectionString()
        {
            try
            {
                return ConfigurationManager.AppSettings["pathMongo"];
            }
            catch (Exception)
            {

                throw;
            }
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