using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.PuntoVenta;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfPuntoVenta_Documento" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfPuntoVenta_Documento.svc o WcfPuntoVenta_Documento.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfPuntoVenta_Documento : ServiceBase<PuntoVenta_Documento, EmpresaContext>,  IWcfPuntoVenta_Documento
    {
       
    }
}
