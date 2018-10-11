using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfErp.Servicios.Administracion
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfUsuarioRol" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfUsuarioRol
    {
        [OperationContract]
        void DoWork();
    }
}
