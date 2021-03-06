﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfMovimientosES" in both code and config file together.
    [ServiceContract]
    public interface IWcfMovimientosES : ServiciosBase<MovimientosES>
    {
         [OperationContract]
         [WebInvoke(UriTemplate = "campos={cadena}/tipoMovimiento={tipoMovimiento}",
         BodyStyle = WebMessageBodyStyle.Bare,
         ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
         Method = "GET")]
        List<MovimientosES> obtenerES(string cadena ,string tipoMovimiento);
      
    }
}
