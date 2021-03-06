﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos;
using WcfErp.Modelos.Inventarios;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfTipoConceptos" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfTipoConceptos.svc o WcfTipoConceptos.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfTipoConceptos : ServiceBase<TipoConcepto, EmpresaContext>, IWcfTipoConceptos
    {
        public TipoConcepto delete(string id)
        {
            throw new NotImplementedException();
        }
 
    }
}
