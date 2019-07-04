using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Ventas;

namespace WcfErp.Modelos.PuntoVenta
{
    public class PuntoVenta_Documento : ModeloBase<PuntoVenta_Documento, EmpresaContext>
    {
        /*
            CREATE TABLE DOCTOS_PV (
            DOCTO_PV_ID                 ENTERO_ID NOT NULL  ENTERO_ID = INTEGER ,
            CAJA_ID                     ENTERO_ID NOT NULL  ENTERO_ID = INTEGER ,
            TIPO_DOCTO                  CHAR(1) NOT NULL,
            FOLIO                       FOLIO_TYPE NOT NULL  FOLIO_TYPE = CHAR(9) ,
            FECHA                       FECHA DEFAULT 'TODAY' NOT NULL  FECHA = DATE ,
            HORA                        HORA DEFAULT 'NOW' NOT NULL  HORA = TIME ,
            CAJERO_ID                   ENTERO_ID  ENTERO_ID = INTEGER ,
            CLAVE_CLIENTE               CLAVE_MAESTRO_TYPE  CLAVE_MAESTRO_TYPE = VARCHAR(20) ,
            CLIENTE_ID                  ENTERO_ID  ENTERO_ID = INTEGER ,
            CLAVE_CLIENTE_FAC           CLAVE_MAESTRO_TYPE  CLAVE_MAESTRO_TYPE = VARCHAR(20) ,
            CLIENTE_FAC_ID              ENTERO_ID  ENTERO_ID = INTEGER ,
            DIR_CLI_ID                  ENTERO_ID  ENTERO_ID = INTEGER ,
            ALMACEN_ID                  ENTERO_ID  ENTERO_ID = INTEGER ,
            LUGAR_EXPEDICION_ID         ENTERO_ID  ENTERO_ID = INTEGER ,
            MONEDA_ID                   ENTERO_ID  ENTERO_ID = INTEGER ,
            IMPUESTO_INCLUIDO           SI_NO_S  SI_NO_S = CHAR(1) DEFAULT 'S' CHECK (VALUE IN ('S', 'N')) ,
            TIPO_CAMBIO                 IMPORTE_UNITARIO  IMPORTE_UNITARIO = NUMERIC(18,6) DEFAULT 0 NOT NULL ,
            TIPO_DSCTO                  CHAR(1),
            DSCTO_PCTJE                 PORCENTAJE_0  PORCENTAJE_0 = NUMERIC(9,6) DEFAULT 0 NOT NULL CHECK (VALUE >= 0) ,
            DSCTO_IMPORTE               IMPORTE_MONETARIO_0  IMPORTE_MONETARIO_0 = NUMERIC(15,2) DEFAULT 0 NOT NULL CHECK (VALUE >= 0) ,
            ESTATUS                     CHAR(1),
            APLICADO                    SI_NO_S  SI_NO_S = CHAR(1) DEFAULT 'S' CHECK (VALUE IN ('S', 'N')) ,
            IMPORTE_NETO                IMPORTE_MONETARIO_0  IMPORTE_MONETARIO_0 = NUMERIC(15,2) DEFAULT 0 NOT NULL CHECK (VALUE >= 0) ,
            TOTAL_IMPUESTOS             IMPORTE_MONETARIO_0  IMPORTE_MONETARIO_0 = NUMERIC(15,2) DEFAULT 0 NOT NULL CHECK (VALUE >= 0) ,
            IMPORTE_DONATIVO            IMPORTE_MONETARIO_0  IMPORTE_MONETARIO_0 = NUMERIC(15,2) DEFAULT 0 NOT NULL CHECK (VALUE >= 0) ,
            TOTAL_FPGC                  IMPORTE_MONETARIO_0  IMPORTE_MONETARIO_0 = NUMERIC(15,2) DEFAULT 0 NOT NULL CHECK (VALUE >= 0) ,
            TICKET_EMITIDO              SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            FORMA_GLOBAL_EMITIDA        SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            FORMA_EMITIDA               SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            CONTABILIZADO               SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            SISTEMA_ORIGEN              CLAVE_SISTEMA_TYPE NOT NULL  CLAVE_SISTEMA_TYPE = CHAR(2) ,
            VENDEDOR_ID                 ENTERO_ID  ENTERO_ID = INTEGER ,
            CARGAR_SUN                  SI_NO_S  SI_NO_S = CHAR(1) DEFAULT 'S' CHECK (VALUE IN ('S', 'N')) ,
            PERSONA                     NOMBRE_MEDIO  NOMBRE_MEDIO = VARCHAR(50) ,
            TIPO_GEN_FAC                CHAR(1),
            ES_FAC_GLOBAL               SI_NO_N NOT NULL  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            FECHA_INI_FAC_GLOBAL        FECHA  FECHA = DATE ,
            FECHA_FIN_FAC_GLOBAL        FECHA  FECHA = DATE ,
            INCL_FACTURADOS_FAC_GLOBAL  SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            ALMACEN_ID_FAC_GLOBAL       ENTERO_ID  ENTERO_ID = INTEGER ,
            REFER_RETING                NOMBRE_MEDIO  NOMBRE_MEDIO = VARCHAR(50) ,
            UNID_COMPROM                SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            DESCRIPCION                 DESCRIPCION_LARGA  DESCRIPCION_LARGA = VARCHAR(200) ,
            IMPUESTO_SUSTITUIDO_ID      ENTERO_ID  ENTERO_ID = INTEGER ,
            IMPUESTO_SUSTITUTO_ID       ENTERO_ID  ENTERO_ID = INTEGER ,
            ES_CFD                      SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            MODALIDAD_FACTURACION       VARCHAR(10),
            ENVIADO                     SI_NO_N  SI_NO_N = CHAR(1) DEFAULT 'N' CHECK (VALUE IN ('S', 'N')) ,
            EMAIL_ENVIO                 EMAIL_TYPE  EMAIL_TYPE = VARCHAR(200) ,
            USUARIO_CREADOR             USUARIO_TYPE  USUARIO_TYPE = VARCHAR(31) DEFAULT USER ,
            CFDI_CERTIFICADO            CHAR(1) DEFAULT 'N' NOT NULL,
            USO_CFDI                    CHAR(3),
            METODO_PAGO_SAT             CHAR(3),
            PARTIDA_AJUSTE_ID           ENTERO_ID  ENTERO_ID = INTEGER ,
            PRECIO_ORIG_PARTIDA_AJUSTE  IMPORTE_UNITARIO  IMPORTE_UNITARIO = NUMERIC(18,6) DEFAULT 0 NOT NULL ,
            FECHA_HORA_CREACION         FECHA_Y_HORA  FECHA_Y_HORA = TIMESTAMP DEFAULT 'NOW' ,
            USUARIO_ULT_MODIF           USUARIO_TYPE  USUARIO_TYPE = VARCHAR(31) DEFAULT USER ,
            USUARIO_AUT_CREACION        VARCHAR(31),
            FECHA_HORA_ULT_MODIF        FECHA_Y_HORA  FECHA_Y_HORA = TIMESTAMP DEFAULT 'NOW' ,
            USUARIO_CANCELACION         VARCHAR(31),
            USUARIO_AUT_MODIF           VARCHAR(31),
            FECHA_HORA_CANCELACION      TIMESTAMP,
            FECHA_HORA_ENVIO            FECHA_Y_HORA  FECHA_Y_HORA = TIMESTAMP DEFAULT 'NOW' ,
            USUARIO_AUT_CANCELACION     VARCHAR(31)
);
         * */


   
        public Cajas Caja { get; set; }
        public char Tipo_Docto { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public DateTime Hora { get; set; }
        public Cajeros Cajero { get; set; }
        public string Clave_Cliente { get; set; }
        public Clientes Cliente { get; set; }
   /*     CLIENTE_ID                  ENTERO_ID  ENTERO_ID = INTEGER ,
    CLAVE_CLIENTE_FAC           CLAVE_MAESTRO_TYPE  CLAVE_MAESTRO_TYPE = VARCHAR(20) ,*/

    }
}