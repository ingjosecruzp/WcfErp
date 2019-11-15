using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Servicios.PVenta
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WcfAperturaCajas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WcfAperturaCajas.svc o WcfAperturaCajas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WcfAperturaCajas : IWcfAperturaCajas
    {
        public AperturaCajas add(AperturaCajas item)
        {
            throw new NotImplementedException();
        }

        public List<AperturaCajas> all(string campos)
        {
            throw new NotImplementedException();
        }

        public AperturaCajas delete(string id)
        {
            throw new NotImplementedException();
        }

        public void DoWork()
        {
        }

        public List<AperturaCajas> filters(string campos, string skip, string filters)
        {
            throw new NotImplementedException();
        }

        public AperturaCajas get(string id)
        {
            throw new NotImplementedException();
        }

        public void GetOptions()
        {
            throw new NotImplementedException();
        }

        public List<AperturaCajas> lazyloading(string campos, string skip)
        {
            throw new NotImplementedException();
        }

        public string RptDocumento(string id)
        {
            throw new NotImplementedException();
        }

        public AperturaCajas RptDocumentoJasper(string id)
        {
            throw new NotImplementedException();
        }

        public List<AperturaCajas> search(string busqueda)
        {
            throw new NotImplementedException();
        }

        public List<AperturaCajas> searchCampo(string campo, string busqueda)
        {
            throw new NotImplementedException();
        }

        public AperturaCajas update(AperturaCajas item, string id)
        {
            throw new NotImplementedException();
        }
    }
}
