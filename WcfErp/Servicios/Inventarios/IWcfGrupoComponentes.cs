using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfErp.Servicios.Inventarios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWcfGrupoComponentes" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWcfGrupoComponentes
    {
        [OperationContract]
        void DoWork();
    }
}
