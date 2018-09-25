using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Especiales;

namespace WcfErp.Servicios.Especiales
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfLogin" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfLogin.svc or WcfLogin.svc.cs at the Solution Explorer and start debugging.
    public class WcfLogin : IWcfLogin
    {
        public string Login(Login acceso)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                string error = null;
                /*if (ex is DbEntityValidationException)
                    error = ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors.ToList()[0].ValidationErrors.ToList()[0].ErrorMessage;
                else if (ex is DbUpdateException)
                    error = ex.InnerException.InnerException.Message;
                else if (ex is EntityCommandExecutionException)
                    error = ex.InnerException.Message;
                else
                    error = ex.Message;*/
                response.Headers.Add("Error", error);
                return null;
            }
        }
    }
}
