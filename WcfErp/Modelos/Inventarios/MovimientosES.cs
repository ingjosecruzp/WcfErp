using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class MovimientosES : ModeloBase
    {

        /*DOCTO_IN_ID *
        ALMACEN_ID *
        CONCEPTO_IN_ID *
        FOLIO *
        NATURALEZA_CONCEPTO *
        FECHA *
        ALMACEN_DESTINO_ID *
        CENTRO_COSTO_ID ** NO LO MANEJAMOS
        CANCELADO *
        APLICADO ** NO LO MANEJAMOS AUN
        DESCRIPCION *
        CUENTA_CONCEPTO ** NO LO MANEJAMOS
        FORMA_EMITIDA ** NO LO MANEJAMOS AUN
        CONTABILIZADO ** NO LO MANEJAMOS
        SISTEMA_ORIGEN *
        USUARIO_CREADOR *FALTA CREARLO AUTOMATICAMENTE
        FECHA_HORA_CREACION *FALTA CREARLO AUTOMATICAMENTE
        USUARIO_AUT_CREACION *FALTA CREARLO AUTOMATICAMENTE
        USUARIO_ULT_MODIF *FALTA CREARLO AUTOMATICAMENTE
        FECHA_HORA_ULT_MODIF *FALTA CREARLO AUTOMATICAMENTE
        USUARIO_AUT_MODIF *FALTA CREARLO AUTOMATICAMENTE
        USUARIO_CANCELACION *FALTA CREARLO AUTOMATICAMENTE
        FECHA_HORA_CANCELACION*FALTA CREARLO AUTOMATICAMENTE
        USUARIO_AUT_CANCELACION *FALTA CREARLO AUTOMATICAMENTE
        */

        //PENDIENTES AL DAR DE ALTA UN CONCEPTO LA NATURALEZA DE ESTE YA NO SE PUEDE MODIFICAR

        //datos embebidos en los objetos ALMACEN_ID,CONCEPTO_IN_ID,NATURALEZA_CONCEPTO,ALMACEN_DESTINO_ID
        public Almacen Almacen { get; set; }
        public Concepto Concepto { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public Almacen Almacen_Destino { get; set; }
        public string Cancelado { get; set; }
        public string Descripcion { get; set; }
        public string Sistema_Origen { get; set; }
        public List<Detalles_ES> Detalles_ES { get; set; }

        

        public MovimientosES ()
        {
            this.Detalles_ES = new List<Detalles_ES>();
        }
        public void ValidarModel(MovimientosES item)
        {
            try
            {
  
                if (item.Detalles_ES.Count == 0)//sin detalles
                    throw new Exception("No ha realizado ningun movimiento, no es posible guardar");
                if (item.Detalles_ES.Where(c => c.Cantidad == 0).Count() > 0)//Cantidad de entrada de articulos en 0
                    throw new Exception("No se permite guardar  componentes en cantidad cero");
                if (item.Concepto.CostoAutomatico == "NO" && String.IsNullOrWhiteSpace(item.Folio))//Sin folio
                    throw new Exception("Falta capturar el folio");
                if (String.IsNullOrWhiteSpace(item.Concepto._id))//Sin Concepto
                    throw new Exception("Falta capturar el concepto");
                if (String.IsNullOrWhiteSpace(item.Fecha.ToString()))//Sin Fecha
                    throw new Exception("Falta capturar la fecha");
                if (String.IsNullOrWhiteSpace(item.Almacen._id))//Sin Almacen
                    throw new Exception("Falta capturar el almacen");
                if (String.IsNullOrWhiteSpace(item.Almacen._id))//Sin Almacen
                    throw new Exception("Falta capturar el almacen");
                if (item.Concepto._id== "5c59c84f6886742388d9bbcc" && item.Almacen_Destino._id==null) //no se indico el almacen de destino
                    throw new Exception("Falta capturar el almacen de destino");
                if (item.Concepto._id == "5c59c84f6886742388d9bbcc" && item.Almacen_Destino._id == item.Almacen._id) //No es posible hacer un traspaso al mismo almacen
                    throw new Exception("No es posible hacer un traspaso al mismo almacen");

            }
            catch (Exception)
            {
                 throw;

            }
        }
    }
}